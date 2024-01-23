using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace project_server
{
    public partial class Form1 : Form
    {
        // Socket for server communication
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Lists and dictionaries to manage client information
        List<Socket> clientSockets = new List<Socket>();
        Dictionary<Socket, List<string>> clientSubscriptions = new Dictionary<Socket, List<string>>();
        Dictionary<Socket, string> clientUsernames = new Dictionary<Socket, string>();
        Dictionary<string, List<string>> channelMessages = new Dictionary<string, List<string>>();

        // Lists to keep track of connected and subscribed clients
        List<string> connectedClients = new List<string>();
        List<string> ifSubscribedClients = new List<string>();
        List<string> spsSubscribedClients = new List<string>();

        // Flags for server status
        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            // Set up the form and register the FormClosing event
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        // Event handler for the form closing event
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up resources and exit the application
            listening = false;
            terminating = true;

            foreach (var clientSocket in clientSockets)
            {
                clientSocket.Dispose();
            }

            Environment.Exit(0);
        }

        // Event handler for the "Listen" button click
        private void button_listen_Click(object sender, EventArgs e)
        {
            // Start listening on the specified port
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;

                // Start a new thread to accept incoming client connections
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");
            }
            else
            {
                logs.AppendText("Please check port number.\n");
            }
        }

        // Method to handle client connection acceptance
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    // Accept a new client connection
                    Socket newClient = serverSocket.Accept();
                    string username = ReceiveUsername(newClient);

                    // Check if the username is available
                    if (!IsUsernameTaken(username))
                    {
                        // Add the client to the lists and dictionaries
                        clientSockets.Add(newClient);
                        clientSubscriptions[newClient] = new List<string>();
                        clientUsernames[newClient] = username;

                        // Update connected clients list and UI
                        connectedClients.Add(username);
                        UpdateConnectedClientsUI();

                        // Display connection message
                        logs.AppendText($"Client '{username}' is connected.\n");
                        SendUsernameSuccess(newClient);

                        // Start a new thread to receive messages from the client
                        Thread receiveThread = new Thread(() => Receive(newClient));
                        receiveThread.Start();
                    }
                    else
                    {
                        // Username is taken, inform the client and close the connection
                        SendUsernameError(newClient);
                        newClient.Shutdown(SocketShutdown.Both);
                        newClient.Close();
                    }
                }
                catch
                {
                    // Handle exceptions when accepting client connections
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }
                }
            }
        }

        // Method to receive the username from the client
        private string ReceiveUsername(Socket clientSocket)
        {
            Byte[] buffer = new Byte[64];
            clientSocket.Receive(buffer);

            string username = Encoding.Default.GetString(buffer);
            username = username.Substring(0, username.IndexOf("\0"));

            return username;
        }

        // Method to check if a username is already taken
        private bool IsUsernameTaken(string username)
        {
            return clientUsernames.Values.Any(existingUsername => existingUsername.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        // Method to send an error message to the client when the username is taken
        private void SendUsernameError(Socket clientSocket)
        {
            string errorMessage = "Username is already taken. Please choose another username.\n";
            Byte[] buffer = Encoding.Default.GetBytes(errorMessage);
            clientSocket.Send(buffer);
        }

        // Method to send a success message to the client after a successful connection
        private void SendUsernameSuccess(Socket clientSocket)
        {
            string successMessage = "Connected";
            Byte[] buffer = Encoding.Default.GetBytes(successMessage);
            clientSocket.Send(buffer);
        }

        // Method to receive messages from a client
        private void Receive(Socket thisClient)
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    // Receive data from the client
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    // Convert the received data to a string
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // Check for disconnect command
                    if (incomingMessage == "/disconnect")
                    {
                        logs.AppendText($"Client '{clientUsernames[thisClient]}' has disconnected.\n");

                        // Update subscribed clients lists
                        ifSubscribedClients.Remove(clientUsernames[thisClient]);
                        UpdateIfSubscribedClientsUI();
                        spsSubscribedClients.Remove(clientUsernames[thisClient]);
                        UpdateSpsSubscribedClientsUI();
                        connectedClients.Remove(clientUsernames[thisClient]);
                        UpdateConnectedClientsUI();

                        // Close client socket and remove from dictionaries
                        thisClient.Close();
                        clientSockets.Remove(thisClient);
                        clientSubscriptions.Remove(thisClient);
                        clientUsernames.Remove(thisClient);

                        connected = false;
                    }
                    // Check for channel subscription command
                    else if (incomingMessage.StartsWith("/subscribe "))
                    {
                        string channel = incomingMessage.Substring("/subscribe ".Length);
                        SubscribeToChannel(thisClient, channel);
                    }
                    // Check for channel unsubscribe command
                    else if (incomingMessage.StartsWith("/unsubscribe "))
                    {
                        string channel = incomingMessage.Substring("/unsubscribe ".Length);
                        UnsubscribeFromChannel(thisClient, channel);
                    }
                    // Regular message
                    else
                    {
                        // Extract channel information from the message
                        string[] messageParts = incomingMessage.Split(new char[] { ' ' }, 3);

                        // Check if the message starts with a slash ("/") followed by the channel name
                        if (messageParts[0].StartsWith("/"))
                        {
                            string channel = messageParts[0].Substring(1);
                            // Check if the channel is allowed
                            if (IsAllowedChannel(channel))
                            {
                                // Save the message to the channel-specific list
                                if (!channelMessages.ContainsKey(channel))
                                {
                                    channelMessages[channel] = new List<string>();
                                }

                                // Display the user name, channel, and message
                                logs.AppendText($"User '{clientUsernames[thisClient]}' on channel '{channel}': {messageParts[2]}\n");

                                // Broadcast the message to clients subscribed to the channel
                                BroadcastChannelMessage(channel, clientUsernames[thisClient], messageParts[2]);
                            }
                            else
                            {
                                logs.AppendText($"Invalid channel: {channel}\n");
                            }
                        }
                        else
                        {
                            // Message for the logs
                            logs.AppendText(incomingMessage);
                        }
                    }
                }
                catch
                {
                    // Client disconnected, clean up and inform
                    logs.AppendText($"Client '{clientUsernames[thisClient]}' has disconnected.\n");

                    ifSubscribedClients.Remove(clientUsernames[thisClient]);
                    UpdateIfSubscribedClientsUI();
                    spsSubscribedClients.Remove(clientUsernames[thisClient]);
                    UpdateSpsSubscribedClientsUI();
                    connectedClients.Remove(clientUsernames[thisClient]);
                    UpdateConnectedClientsUI();

                    clientSockets.Remove(thisClient);
                    clientSubscriptions.Remove(thisClient);
                    clientUsernames.Remove(thisClient);

                    connected = false;
                }
            }
        }

        // Method to subscribe a client to a channel
        private void SubscribeToChannel(Socket client, string channel)
        {
            // Check if the channel is allowed
            if (IsAllowedChannel(channel))
            {
                // Check if the client is not already subscribed
                if (!clientSubscriptions[client].Contains(channel))
                {
                    clientSubscriptions[client].Add(channel);
                    logs.AppendText($"Client '{clientUsernames[client]}' subscribed to channel: {channel}\n");
                    // Update subscribed clients lists
                    if (channel == "IF100")
                    {
                        ifSubscribedClients.Add(clientUsernames[client]);
                        UpdateIfSubscribedClientsUI();
                    }
                    else if (channel == "SPS101")
                    {
                        spsSubscribedClients.Add(clientUsernames[client]);
                        UpdateSpsSubscribedClientsUI();
                    }
                }
                else
                {
                    logs.AppendText($"Client '{clientUsernames[client]}' is already subscribed to channel: {channel}\n");
                }
            }
            else
            {
                logs.AppendText($"Invalid channel: {channel}\n");
            }
        }

        // Method to unsubscribe a client from a channel
        private void UnsubscribeFromChannel(Socket client, string channel)
        {
            // Check if the channel is allowed
            if (IsAllowedChannel(channel))
            {
                // Check if the client is subscribed
                if (clientSubscriptions[client].Contains(channel))
                {
                    clientSubscriptions[client].Remove(channel);
                    logs.AppendText($"Client '{clientUsernames[client]}' unsubscribed from channel: {channel}\n");
                    // Update subscribed clients lists
                    if (channel == "IF100")
                    {
                        ifSubscribedClients.Remove(clientUsernames[client]);
                        UpdateIfSubscribedClientsUI();
                    }
                    else if (channel == "SPS101")
                    {
                        spsSubscribedClients.Remove(clientUsernames[client]);
                        UpdateSpsSubscribedClientsUI();
                    }
                }
                else
                {
                    logs.AppendText($"Client '{clientUsernames[client]}' is not subscribed to channel: {channel}\n");
                }
            }
            else
            {
                logs.AppendText($"Invalid channel: {channel}\n");
            }
        }

        // Helper method to check if the channel is allowed
        private bool IsAllowedChannel(string channel)
        {
            if (channel == "IF100" || channel == "SPS101")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to broadcast a message to clients subscribed to a channel
        private void BroadcastChannelMessage(string channel, string username, string message)
        {
            foreach (Socket client in clientSockets)
            {
                // Check if the client is subscribed to the channel
                if (GetSubscribedChannels(client).Contains(channel))
                {
                    try
                    {
                        // Format the message with channel, username, and original message
                        string formattedMessage = $"/{channel} {username} {message}";
                        Byte[] buffer = Encoding.Default.GetBytes(formattedMessage);
                        client.Send(buffer);
                    }
                    catch
                    {
                        // Handle exceptions when broadcasting messages
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }
                }
            }
        }

        // Method to get the list of channels a client is subscribed to
        private List<string> GetSubscribedChannels(Socket client)
        {
            return clientSubscriptions[client];
        }

        // Helper method to update the connected clients UI
        private void UpdateConnectedClientsUI()
        {
            conn_clients.Text = $"{string.Join("\n", connectedClients)}\n";
        }

        // Helper method to update the IF100 subscribed clients UI
        private void UpdateIfSubscribedClientsUI()
        {
            if_sub_list.Text = $"{string.Join("\n", ifSubscribedClients)}\n";
        }

        // Helper method to update the SPS101 subscribed clients UI
        private void UpdateSpsSubscribedClientsUI()
        {
            sps_sub_list.Text = $"{string.Join("\n", spsSubscribedClients)}\n";
        }
    }
}