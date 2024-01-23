
namespace project_client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.if_chat = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_if_sub = new System.Windows.Forms.Button();
            this.button_if_unsub = new System.Windows.Forms.Button();
            this.button_sps_unsub = new System.Windows.Forms.Button();
            this.button_sps_sub = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.sps_chat = new System.Windows.Forms.RichTextBox();
            this.textBox_if_msg = new System.Windows.Forms.TextBox();
            this.textBox_sps_msg = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_if_send = new System.Windows.Forms.Button();
            this.button_sps_send = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(90, 24);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 20);
            this.textBox_ip.TabIndex = 3;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(279, 24);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 20);
            this.textBox_port.TabIndex = 4;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(467, 24);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(100, 20);
            this.textBox_username.TabIndex = 5;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(618, 24);
            this.button_connect.Margin = new System.Windows.Forms.Padding(2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(57, 22);
            this.button_connect.TabIndex = 6;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // if_chat
            // 
            this.if_chat.Location = new System.Drawing.Point(119, 144);
            this.if_chat.Margin = new System.Windows.Forms.Padding(2);
            this.if_chat.Name = "if_chat";
            this.if_chat.Size = new System.Drawing.Size(300, 286);
            this.if_chat.TabIndex = 7;
            this.if_chat.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "IF 100";
            // 
            // button_if_sub
            // 
            this.button_if_sub.Enabled = false;
            this.button_if_sub.Location = new System.Drawing.Point(20, 177);
            this.button_if_sub.Margin = new System.Windows.Forms.Padding(2);
            this.button_if_sub.Name = "button_if_sub";
            this.button_if_sub.Size = new System.Drawing.Size(76, 23);
            this.button_if_sub.TabIndex = 9;
            this.button_if_sub.Text = "Subscribe";
            this.button_if_sub.UseVisualStyleBackColor = true;
            this.button_if_sub.Click += new System.EventHandler(this.button_if_sub_Click);
            // 
            // button_if_unsub
            // 
            this.button_if_unsub.Enabled = false;
            this.button_if_unsub.Location = new System.Drawing.Point(20, 225);
            this.button_if_unsub.Margin = new System.Windows.Forms.Padding(2);
            this.button_if_unsub.Name = "button_if_unsub";
            this.button_if_unsub.Size = new System.Drawing.Size(76, 23);
            this.button_if_unsub.TabIndex = 10;
            this.button_if_unsub.Text = "Unsubscribe";
            this.button_if_unsub.UseVisualStyleBackColor = true;
            this.button_if_unsub.Click += new System.EventHandler(this.button_if_unsub_Click);
            // 
            // button_sps_unsub
            // 
            this.button_sps_unsub.Enabled = false;
            this.button_sps_unsub.Location = new System.Drawing.Point(433, 225);
            this.button_sps_unsub.Margin = new System.Windows.Forms.Padding(2);
            this.button_sps_unsub.Name = "button_sps_unsub";
            this.button_sps_unsub.Size = new System.Drawing.Size(82, 23);
            this.button_sps_unsub.TabIndex = 13;
            this.button_sps_unsub.Text = "Unsubscribe";
            this.button_sps_unsub.UseVisualStyleBackColor = true;
            this.button_sps_unsub.Click += new System.EventHandler(this.button_sps_unsub_Click);
            // 
            // button_sps_sub
            // 
            this.button_sps_sub.Enabled = false;
            this.button_sps_sub.Location = new System.Drawing.Point(433, 177);
            this.button_sps_sub.Margin = new System.Windows.Forms.Padding(2);
            this.button_sps_sub.Name = "button_sps_sub";
            this.button_sps_sub.Size = new System.Drawing.Size(82, 23);
            this.button_sps_sub.TabIndex = 12;
            this.button_sps_sub.Text = "Subscribe";
            this.button_sps_sub.UseVisualStyleBackColor = true;
            this.button_sps_sub.Click += new System.EventHandler(this.button_sps_sub_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 144);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "SPS 101";
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(246, 70);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(448, 49);
            this.logs.TabIndex = 15;
            this.logs.Text = "";
            // 
            // sps_chat
            // 
            this.sps_chat.Location = new System.Drawing.Point(541, 143);
            this.sps_chat.Margin = new System.Windows.Forms.Padding(2);
            this.sps_chat.Name = "sps_chat";
            this.sps_chat.Size = new System.Drawing.Size(300, 287);
            this.sps_chat.TabIndex = 16;
            this.sps_chat.Text = "";
            // 
            // textBox_if_msg
            // 
            this.textBox_if_msg.Enabled = false;
            this.textBox_if_msg.Location = new System.Drawing.Point(119, 452);
            this.textBox_if_msg.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_if_msg.Name = "textBox_if_msg";
            this.textBox_if_msg.Size = new System.Drawing.Size(300, 20);
            this.textBox_if_msg.TabIndex = 17;
            // 
            // textBox_sps_msg
            // 
            this.textBox_sps_msg.Enabled = false;
            this.textBox_sps_msg.Location = new System.Drawing.Point(541, 452);
            this.textBox_sps_msg.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_sps_msg.Name = "textBox_sps_msg";
            this.textBox_sps_msg.Size = new System.Drawing.Size(300, 20);
            this.textBox_sps_msg.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 455);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Message:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(461, 455);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Message:";
            // 
            // button_if_send
            // 
            this.button_if_send.Enabled = false;
            this.button_if_send.Location = new System.Drawing.Point(119, 487);
            this.button_if_send.Margin = new System.Windows.Forms.Padding(2);
            this.button_if_send.Name = "button_if_send";
            this.button_if_send.Size = new System.Drawing.Size(68, 23);
            this.button_if_send.TabIndex = 21;
            this.button_if_send.Text = "Send";
            this.button_if_send.UseVisualStyleBackColor = true;
            this.button_if_send.Click += new System.EventHandler(this.button_if_send_Click);
            // 
            // button_sps_send
            // 
            this.button_sps_send.Enabled = false;
            this.button_sps_send.Location = new System.Drawing.Point(541, 487);
            this.button_sps_send.Margin = new System.Windows.Forms.Padding(2);
            this.button_sps_send.Name = "button_sps_send";
            this.button_sps_send.Size = new System.Drawing.Size(68, 23);
            this.button_sps_send.TabIndex = 22;
            this.button_sps_send.Text = "Send";
            this.button_sps_send.UseVisualStyleBackColor = true;
            this.button_sps_send.Click += new System.EventHandler(this.button_sps_send_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(720, 24);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(75, 23);
            this.button_disconnect.TabIndex = 23;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 533);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_sps_send);
            this.Controls.Add(this.button_if_send);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_sps_msg);
            this.Controls.Add(this.textBox_if_msg);
            this.Controls.Add(this.sps_chat);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_sps_unsub);
            this.Controls.Add(this.button_sps_sub);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_if_unsub);
            this.Controls.Add(this.button_if_sub);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.if_chat);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox if_chat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_if_sub;
        private System.Windows.Forms.Button button_if_unsub;
        private System.Windows.Forms.Button button_sps_unsub;
        private System.Windows.Forms.Button button_sps_sub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.RichTextBox sps_chat;
        private System.Windows.Forms.TextBox textBox_if_msg;
        private System.Windows.Forms.TextBox textBox_sps_msg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_if_send;
        private System.Windows.Forms.Button button_sps_send;
        private System.Windows.Forms.Button button_disconnect;
    }
}

