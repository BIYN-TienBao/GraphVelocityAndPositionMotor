namespace TestUDPStreaming
{
    partial class TestUDPStreaming
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
            this.ReceiveGroupBox = new System.Windows.Forms.GroupBox();
            this.ReceiveTextBox = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.GroupBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SendTextBox = new System.Windows.Forms.TextBox();
            this.UDPGroupBox = new System.Windows.Forms.GroupBox();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.LabelAnnouncement = new System.Windows.Forms.Label();
            this.StartCameraButton = new System.Windows.Forms.Button();
            this.CamPictureBox = new System.Windows.Forms.PictureBox();
            this.ReceiveGroupBox.SuspendLayout();
            this.Send.SuspendLayout();
            this.UDPGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CamPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ReceiveGroupBox
            // 
            this.ReceiveGroupBox.Controls.Add(this.ReceiveTextBox);
            this.ReceiveGroupBox.Location = new System.Drawing.Point(262, 103);
            this.ReceiveGroupBox.Name = "ReceiveGroupBox";
            this.ReceiveGroupBox.Size = new System.Drawing.Size(227, 258);
            this.ReceiveGroupBox.TabIndex = 83;
            this.ReceiveGroupBox.TabStop = false;
            this.ReceiveGroupBox.Text = "Receive";
            // 
            // ReceiveTextBox
            // 
            this.ReceiveTextBox.Location = new System.Drawing.Point(7, 22);
            this.ReceiveTextBox.Multiline = true;
            this.ReceiveTextBox.Name = "ReceiveTextBox";
            this.ReceiveTextBox.ReadOnly = true;
            this.ReceiveTextBox.Size = new System.Drawing.Size(214, 230);
            this.ReceiveTextBox.TabIndex = 0;
            // 
            // Send
            // 
            this.Send.Controls.Add(this.SendButton);
            this.Send.Controls.Add(this.SendTextBox);
            this.Send.Location = new System.Drawing.Point(12, 103);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(227, 297);
            this.Send.TabIndex = 82;
            this.Send.TabStop = false;
            this.Send.Text = "Send";
            // 
            // SendButton
            // 
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.Location = new System.Drawing.Point(153, 258);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(68, 33);
            this.SendButton.TabIndex = 13;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click_1);
            // 
            // SendTextBox
            // 
            this.SendTextBox.Location = new System.Drawing.Point(7, 22);
            this.SendTextBox.Multiline = true;
            this.SendTextBox.Name = "SendTextBox";
            this.SendTextBox.Size = new System.Drawing.Size(214, 230);
            this.SendTextBox.TabIndex = 0;
            // 
            // UDPGroupBox
            // 
            this.UDPGroupBox.Controls.Add(this.IPTextBox);
            this.UDPGroupBox.Controls.Add(this.label4);
            this.UDPGroupBox.Controls.Add(this.label5);
            this.UDPGroupBox.Controls.Add(this.ConnectButton);
            this.UDPGroupBox.Controls.Add(this.PortTextBox);
            this.UDPGroupBox.Location = new System.Drawing.Point(12, 12);
            this.UDPGroupBox.Name = "UDPGroupBox";
            this.UDPGroupBox.Size = new System.Drawing.Size(601, 60);
            this.UDPGroupBox.TabIndex = 81;
            this.UDPGroupBox.TabStop = false;
            this.UDPGroupBox.Text = "TCP";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPTextBox.Location = new System.Drawing.Point(31, 25);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(118, 26);
            this.IPTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(261, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "IP";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.Location = new System.Drawing.Point(496, 15);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(91, 38);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click_1);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortTextBox.Location = new System.Drawing.Point(298, 25);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(118, 26);
            this.PortTextBox.TabIndex = 9;
            // 
            // LabelAnnouncement
            // 
            this.LabelAnnouncement.AutoSize = true;
            this.LabelAnnouncement.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAnnouncement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelAnnouncement.Location = new System.Drawing.Point(18, 84);
            this.LabelAnnouncement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAnnouncement.Name = "LabelAnnouncement";
            this.LabelAnnouncement.Size = new System.Drawing.Size(31, 16);
            this.LabelAnnouncement.TabIndex = 84;
            this.LabelAnnouncement.Text = "Abc";
            // 
            // StartCameraButton
            // 
            this.StartCameraButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartCameraButton.Location = new System.Drawing.Point(508, 84);
            this.StartCameraButton.Name = "StartCameraButton";
            this.StartCameraButton.Size = new System.Drawing.Size(121, 44);
            this.StartCameraButton.TabIndex = 14;
            this.StartCameraButton.Text = "Start Camera";
            this.StartCameraButton.UseVisualStyleBackColor = true;
            this.StartCameraButton.Click += new System.EventHandler(this.StartCameraButton_Click);
            // 
            // CamPictureBox
            // 
            this.CamPictureBox.Location = new System.Drawing.Point(513, 151);
            this.CamPictureBox.Name = "CamPictureBox";
            this.CamPictureBox.Size = new System.Drawing.Size(724, 417);
            this.CamPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CamPictureBox.TabIndex = 85;
            this.CamPictureBox.TabStop = false;
            // 
            // TestUDPStreaming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 591);
            this.Controls.Add(this.CamPictureBox);
            this.Controls.Add(this.StartCameraButton);
            this.Controls.Add(this.LabelAnnouncement);
            this.Controls.Add(this.ReceiveGroupBox);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.UDPGroupBox);
            this.Name = "TestUDPStreaming";
            this.Text = "Test UDP streaming";
            this.ReceiveGroupBox.ResumeLayout(false);
            this.ReceiveGroupBox.PerformLayout();
            this.Send.ResumeLayout(false);
            this.Send.PerformLayout();
            this.UDPGroupBox.ResumeLayout(false);
            this.UDPGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CamPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ReceiveGroupBox;
        private System.Windows.Forms.TextBox ReceiveTextBox;
        private System.Windows.Forms.GroupBox Send;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox SendTextBox;
        private System.Windows.Forms.GroupBox UDPGroupBox;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label LabelAnnouncement;
        private System.Windows.Forms.Button StartCameraButton;
        private System.Windows.Forms.PictureBox CamPictureBox;
    }
}

