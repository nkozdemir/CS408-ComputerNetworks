using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace project_client
{
    public partial class Form1 : Form
    {
        bool terminating = false;       // Flag to signal termination of the client
        bool connected = false;         // Flag to indicate whether the client is connected
        Socket clientSocket;            // Socket for communication with the server
        string clientUser;              // Username of the client

        public Form1()
        {
            // Setup the form and register the FormClosing event
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        // Event handler for the "Connect" button
        private void button_connect_Click(object sender, EventArgs e)
        {
            // Create a new socket for the client
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Get the server IP address and port number from the user input
            string IP = textBox_ip.Text;
            int portNum;

            // Try to parse the port number
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    // Attempt to connect to the server
                    clientSocket.Connect(IP, portNum);

                    // Send the username to the server
                    string username = textBox_username.Text;
                    SendUsername(username);

                    // Receive the server's response
                    Byte[] responseBuffer = new Byte[64];
                    clientSocket.Receive(responseBuffer);
                    string responseMessage = Encoding.Default.GetString(responseBuffer).Trim();

                    // Check if the connection was successful
                    if (responseMessage.StartsWith("Connected"))
                    {
                        // Update UI elements and start a new thread to receive messages
                        button_connect.Enabled = false;
                        button_disconnect.Enabled = true;
                        button_if_sub.Enabled = true;
                        button_sps_sub.Enabled = true;
                        connected = true;
                        clientUser = username;
                        logs.AppendText($"Connected to the server as '{username}'!\n");

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                    else
                    {
                        // Display an error message if the connection failed
                        logs.AppendText($"Connection failed: {responseMessage}\n");
                        clientSocket.Close();
                    }
                }
                catch
                {
                    // Display an error message if the connection attempt fails
                    logs.AppendText("Could not connect to the server.\n");
                }
            }
            else
            {
                // Display an error message if the port number is invalid
                logs.AppendText("Check the port number!\n");
            }
        }

        // Method to send the username to the server
        private void SendUsername(string username)
        {
            if (username != "")
            {
                Byte[] buffer = Encoding.Default.GetBytes(username);
                clientSocket.Send(buffer);
            }
        }

        // Event handler for the form closing event
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        // Method to continuously receive messages from the server
        private void Receive()
        {
            while (connected)
            {
                try
                {
                    // Receive data from the server
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    // Convert the received data to a string
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // Extract channel, username, and message from the incoming message
                    string[] messageParts = incomingMessage.Split(new char[] { ' ' }, 3);
                    string channel = messageParts[0].Substring(1); // Remove leading '/'
                    string username = messageParts[1];
                    string message = messageParts[2];

                    // Check if the message is for a specific channel
                    if (channel == "IF100")
                    {
                        // Display the message in the IF channel chat
                        DisplayMessageOnChannelTextBox(username, message, if_chat);
                    }
                    else if (channel == "SPS101")
                    {
                        // Display the message in the SPS channel chat
                        DisplayMessageOnChannelTextBox(username, message, sps_chat);
                    }
                    else
                    {
                        // Display the message in the general logs
                        logs.AppendText(incomingMessage);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        // Display a message if the server disconnects
                        logs.AppendText("The server has disconnected.\n");
                        DisableUIElements("IF100");
                        button_if_sub.Enabled = false;
                        DisableUIElements("SPS101");
                        button_sps_sub.Enabled = false;
                        button_connect.Enabled = true;
                    }

                    // Close the client socket and set the connected flag to false
                    clientSocket.Close();
                    connected = false;
                }
            }
        }

        // Method to display a message in the specified channel's chat box
        private void DisplayMessageOnChannelTextBox(string username, string message, RichTextBox channelTextBox)
        {
            string displayUsername = (username == clientUser) ? "You" : username;
            channelTextBox.AppendText($"{displayUsername}: {message}\n");
        }

        // Method to subscribe to a channel
        private void SubscribeToChannel(string channel)
        {
            try
            {
                // Send the subscribe command to the server with the channel information
                string command = $"/subscribe {channel}";
                Byte[] buffer = Encoding.Default.GetBytes(command);
                clientSocket.Send(buffer);

                // Display a success message in the logs
                logs.AppendText($"Successfully subscribed to the channel {channel}.\n");

                // Enable UI elements for the subscribed channel
                EnableUIElements(channel);

                // Display a welcome message in the channel's chat box
                GetChannelTextBox(channel).AppendText($"Welcome to the {channel} channel.\n");
            }
            catch
            {
                // Handle exceptions (e.g., connection issues) and display an error message
                logs.AppendText($"Failed to subscribe to the channel {channel}.\n");
            }
        }

        // Method to get the RichTextBox associated with a specific channel
        private RichTextBox GetChannelTextBox(string channel)
        {
            return channel == "IF100" ? if_chat : sps_chat;
        }

        // Method to unsubscribe from a channel
        private void UnsubscribeFromChannel(string channel)
        {
            try
            {
                // Send the unsubscribe command to the server with the channel information
                string command = $"/unsubscribe {channel}";
                Byte[] buffer = Encoding.Default.GetBytes(command);
                clientSocket.Send(buffer);

                // Display a success message in the logs
                logs.AppendText($"Successfully unsubscribed from the channel {channel}.\n");

                // Disable UI elements for the unsubscribed channel
                DisableUIElements(channel);
            }
            catch
            {
                // Handle exceptions (e.g., connection issues) and display an error message
                logs.AppendText($"Failed to unsubscribe from the channel {channel}.\n");
            }
        }

        // Event handler for the "Subscribe to IF100" button
        private void button_if_sub_Click(object sender, EventArgs e)
        {
            SubscribeToChannel("IF100");
        }

        // Event handler for the "Unsubscribe from IF100" button
        private void button_if_unsub_Click(object sender, EventArgs e)
        {
            UnsubscribeFromChannel("IF100");
        }

        // Event handler for the "Subscribe to SPS101" button
        private void button_sps_sub_Click(object sender, EventArgs e)
        {
            SubscribeToChannel("SPS101");
        }

        // Event handler for the "Unsubscribe from SPS101" button
        private void button_sps_unsub_Click(object sender, EventArgs e)
        {
            UnsubscribeFromChannel("SPS101");
        }

        // Method to enable UI elements after subscription
        private void EnableUIElements(string channel)
        {
            switch (channel)
            {
                case "IF100":
                    button_if_sub.Enabled = false;
                    button_if_unsub.Enabled = true;
                    button_if_send.Enabled = true;
                    textBox_if_msg.Enabled = true;
                    break;
                case "SPS101":
                    button_sps_sub.Enabled = false;
                    button_sps_unsub.Enabled = true;
                    button_sps_send.Enabled = true;
                    textBox_sps_msg.Enabled = true;
                    break;
            }
        }

        // Method to disable UI elements after unsubscription
        private void DisableUIElements(string channel)
        {
            switch (channel)
            {
                case "IF100":
                    button_if_sub.Enabled = true;
                    button_if_unsub.Enabled = false;
                    button_if_send.Enabled = false;
                    textBox_if_msg.Enabled = false;
                    if_chat.Clear();
                    textBox_if_msg.Clear();
                    break;
                case "SPS101":
                    button_sps_sub.Enabled = true;
                    button_sps_unsub.Enabled = false;
                    button_sps_send.Enabled = false;
                    textBox_sps_msg.Enabled = false;
                    sps_chat.Clear();
                    textBox_sps_msg.Clear();
                    break;
            }
        }

        // Method to send a message to a channel
        private void SendMessage(string channel, string message)
        {
            try
            {
                // Send the message to the server with the channel information
                string formattedMessage = $"/{channel} {clientUser} {message}";
                Byte[] buffer = Encoding.Default.GetBytes(formattedMessage);
                clientSocket.Send(buffer);

                // Display a success message in the logs
                logs.AppendText($"Message successfully sent to the {channel} channel.\n");
            }
            catch
            {
                // Handle exceptions (e.g., connection issues) and display an error message
                logs.AppendText($"Failed to send the message to the {channel} channel.\n");
            }
        }

        // Event handler for the "Send to IF100" button
        private void button_if_send_Click(object sender, EventArgs e)
        {
            string message = textBox_if_msg.Text;
            if (message != "") SendMessage("IF100", message);
            textBox_if_msg.Clear();
        }

        // Event handler for the "Send to SPS101" button
        private void button_sps_send_Click(object sender, EventArgs e)
        {
            string message = textBox_sps_msg.Text;
            if (message != "") SendMessage("SPS101", message);
            textBox_sps_msg.Clear();
        }

        // Event handler for the "Disconnect" button
        private void button_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (connected)
                {
                    // Inform the server about the disconnection
                    string disconnectMessage = "/disconnect";
                    Byte[] buffer = Encoding.Default.GetBytes(disconnectMessage);
                    clientSocket.Send(buffer);

                    // Close the client socket
                    clientSocket.Close();
                    connected = false;
                    terminating = true;

                    // Enable the "Connect" button and disable other UI elements
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    DisableUIElements("IF100");
                    button_if_sub.Enabled = false;
                    DisableUIElements("SPS101");
                    button_sps_sub.Enabled = false;

                    // Display a disconnection message in the logs
                    logs.AppendText("Disconnected from the server.\n");
                    terminating = false;
                }
            }
            catch
            {
                // Display an error message if an error occurs during disconnection
                logs.AppendText("Error during disconnection.\n");
            }
        }
    }
}
