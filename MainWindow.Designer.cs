namespace NetTunnel
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.joinButton = new System.Windows.Forms.Button();
            this.joinBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nickBox = new System.Windows.Forms.TextBox();
            this.channelsLB = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editServiceButton = new System.Windows.Forms.Button();
            this.deleteServiceButton = new System.Windows.Forms.Button();
            this.newServiceButton = new System.Windows.Forms.Button();
            this.servicesLB = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chatTabControl = new System.Windows.Forms.TabControl();
            this.LobbyPage = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.chatHistory = new System.Windows.Forms.TextBox();
            this.leaveChannelButton = new System.Windows.Forms.Button();
            this.userServicesLB = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usersLB = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.chatTabControl.SuspendLayout();
            this.LobbyPage.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.joinButton);
            this.splitContainer1.Panel1.Controls.Add(this.joinBox);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.nickBox);
            this.splitContainer1.Panel1.Controls.Add(this.channelsLB);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.editServiceButton);
            this.splitContainer1.Panel1.Controls.Add(this.deleteServiceButton);
            this.splitContainer1.Panel1.Controls.Add(this.newServiceButton);
            this.splitContainer1.Panel1.Controls.Add(this.servicesLB);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chatTabControl);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 730);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 0;
            // 
            // joinButton
            // 
            this.joinButton.Location = new System.Drawing.Point(99, 632);
            this.joinButton.Name = "joinButton";
            this.joinButton.Size = new System.Drawing.Size(41, 20);
            this.joinButton.TabIndex = 11;
            this.joinButton.Text = "Join";
            this.joinButton.UseVisualStyleBackColor = true;
            this.joinButton.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // joinBox
            // 
            this.joinBox.Location = new System.Drawing.Point(3, 632);
            this.joinBox.Name = "joinBox";
            this.joinBox.Size = new System.Drawing.Size(99, 20);
            this.joinBox.TabIndex = 10;
            this.joinBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.joinBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 616);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Join channel:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 691);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nick:";
            // 
            // nickBox
            // 
            this.nickBox.Location = new System.Drawing.Point(3, 707);
            this.nickBox.Name = "nickBox";
            this.nickBox.Size = new System.Drawing.Size(134, 20);
            this.nickBox.TabIndex = 7;
            this.nickBox.Text = "Zoot";
            // 
            // channelsLB
            // 
            this.channelsLB.FormattingEnabled = true;
            this.channelsLB.Location = new System.Drawing.Point(11, 242);
            this.channelsLB.Name = "channelsLB";
            this.channelsLB.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.channelsLB.Size = new System.Drawing.Size(120, 95);
            this.channelsLB.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selected service is shared with:";
            // 
            // editServiceButton
            // 
            this.editServiceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editServiceButton.Location = new System.Drawing.Point(89, 170);
            this.editServiceButton.Name = "editServiceButton";
            this.editServiceButton.Size = new System.Drawing.Size(37, 23);
            this.editServiceButton.TabIndex = 3;
            this.editServiceButton.Text = "Edit";
            this.editServiceButton.UseVisualStyleBackColor = true;
            this.editServiceButton.Click += new System.EventHandler(this.editServiceButton_Click);
            // 
            // deleteServiceButton
            // 
            this.deleteServiceButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteServiceButton.Location = new System.Drawing.Point(32, 170);
            this.deleteServiceButton.Name = "deleteServiceButton";
            this.deleteServiceButton.Size = new System.Drawing.Size(21, 23);
            this.deleteServiceButton.TabIndex = 2;
            this.deleteServiceButton.Text = "-";
            this.deleteServiceButton.UseVisualStyleBackColor = true;
            this.deleteServiceButton.Click += new System.EventHandler(this.deleteServiceButton_Click);
            // 
            // newServiceButton
            // 
            this.newServiceButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newServiceButton.Location = new System.Drawing.Point(12, 170);
            this.newServiceButton.Name = "newServiceButton";
            this.newServiceButton.Size = new System.Drawing.Size(21, 23);
            this.newServiceButton.TabIndex = 1;
            this.newServiceButton.Text = "+";
            this.newServiceButton.UseVisualStyleBackColor = true;
            this.newServiceButton.Click += new System.EventHandler(this.newServiceButton_Click);
            // 
            // servicesLB
            // 
            this.servicesLB.Location = new System.Drawing.Point(12, 25);
            this.servicesLB.Name = "servicesLB";
            this.servicesLB.Size = new System.Drawing.Size(123, 139);
            this.servicesLB.TabIndex = 0;
            this.servicesLB.SelectedIndexChanged += new System.EventHandler(this.servicesLB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Services:";
            // 
            // chatTabControl
            // 
            this.chatTabControl.Controls.Add(this.LobbyPage);
            this.chatTabControl.Location = new System.Drawing.Point(7, 25);
            this.chatTabControl.Name = "chatTabControl";
            this.chatTabControl.SelectedIndex = 0;
            this.chatTabControl.Size = new System.Drawing.Size(854, 702);
            this.chatTabControl.TabIndex = 1;
            this.chatTabControl.SelectedIndexChanged += new System.EventHandler(this.chatTabControl_SelectedIndexChanged);
            // 
            // LobbyPage
            // 
            this.LobbyPage.Controls.Add(this.splitContainer2);
            this.LobbyPage.Location = new System.Drawing.Point(4, 22);
            this.LobbyPage.Name = "LobbyPage";
            this.LobbyPage.Padding = new System.Windows.Forms.Padding(3);
            this.LobbyPage.Size = new System.Drawing.Size(846, 676);
            this.LobbyPage.TabIndex = 0;
            this.LobbyPage.Text = "Lobby";
            this.LobbyPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.sendButton);
            this.splitContainer2.Panel1.Controls.Add(this.chatBox);
            this.splitContainer2.Panel1.Controls.Add(this.chatHistory);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.leaveChannelButton);
            this.splitContainer2.Panel2.Controls.Add(this.userServicesLB);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.usersLB);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Size = new System.Drawing.Size(840, 670);
            this.splitContainer2.SplitterDistance = 695;
            this.splitContainer2.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.sendButton.Location = new System.Drawing.Point(643, 647);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(52, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // chatBox
            // 
            this.chatBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.chatBox.Location = new System.Drawing.Point(0, 647);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(645, 20);
            this.chatBox.TabIndex = 1;
            this.chatBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatBox_KeyPress);
            // 
            // chatHistory
            // 
            this.chatHistory.BackColor = System.Drawing.SystemColors.Control;
            this.chatHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.chatHistory.Location = new System.Drawing.Point(0, 0);
            this.chatHistory.Multiline = true;
            this.chatHistory.Name = "chatHistory";
            this.chatHistory.ReadOnly = true;
            this.chatHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatHistory.Size = new System.Drawing.Size(695, 647);
            this.chatHistory.TabIndex = 0;
            // 
            // leaveChannelButton
            // 
            this.leaveChannelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.leaveChannelButton.Enabled = false;
            this.leaveChannelButton.Location = new System.Drawing.Point(0, 647);
            this.leaveChannelButton.Name = "leaveChannelButton";
            this.leaveChannelButton.Size = new System.Drawing.Size(141, 23);
            this.leaveChannelButton.TabIndex = 5;
            this.leaveChannelButton.Text = "Leave Channel";
            this.leaveChannelButton.UseVisualStyleBackColor = true;
            this.leaveChannelButton.Click += new System.EventHandler(this.leaveChannelButton_Click);
            // 
            // userServicesLB
            // 
            this.userServicesLB.FormattingEnabled = true;
            this.userServicesLB.Items.AddRange(new object[] {
            "Ventrilo",
            "NetCat",
            "Apache",
            "Srcds"});
            this.userServicesLB.Location = new System.Drawing.Point(4, 306);
            this.userServicesLB.Name = "userServicesLB";
            this.userServicesLB.Size = new System.Drawing.Size(134, 134);
            this.userServicesLB.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 29);
            this.label6.TabIndex = 3;
            this.label6.Text = "Selected user sharing services:";
            // 
            // usersLB
            // 
            this.usersLB.FormattingEnabled = true;
            this.usersLB.Items.AddRange(new object[] {
            "Zoot",
            "Alice",
            "Bob",
            "Trudy"});
            this.usersLB.Location = new System.Drawing.Point(6, 19);
            this.usersLB.Name = "usersLB";
            this.usersLB.Size = new System.Drawing.Size(132, 225);
            this.usersLB.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Users:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Chat:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.chatTabControl.ResumeLayout(false);
            this.LobbyPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox servicesLB;
        private System.Windows.Forms.Button newServiceButton;
        private System.Windows.Forms.Button editServiceButton;
        private System.Windows.Forms.Button deleteServiceButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox channelsLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nickBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl chatTabControl;
        private System.Windows.Forms.TabPage LobbyPage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox chatHistory;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button joinButton;
        private System.Windows.Forms.TextBox joinBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox userServicesLB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox usersLB;
        private System.Windows.Forms.Button leaveChannelButton;
    }
}

