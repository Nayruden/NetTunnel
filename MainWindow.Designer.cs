﻿namespace NetTunnel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.servicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ventriloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.srcdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatTabControl = new System.Windows.Forms.TabControl();
            this.lobbyPage = new System.Windows.Forms.TabPage();
            this.chatTabContainer = new System.Windows.Forms.SplitContainer();
            this.discoveryContainer = new System.Windows.Forms.SplitContainer();
            this.usersGroupBox = new System.Windows.Forms.GroupBox();
            this.usersListBox = new System.Windows.Forms.ListBox();
            this.discoveryViewByToolStrip = new System.Windows.Forms.ToolStrip();
            this.discoveryViewByButton = new System.Windows.Forms.ToolStripSplitButton();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesGroupBox = new System.Windows.Forms.GroupBox();
            this.servicesListBox = new System.Windows.Forms.ListBox();
            this.chatContainer = new System.Windows.Forms.SplitContainer();
            this.chatBoxContainer = new System.Windows.Forms.SplitContainer();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.chatHistoryBox = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            this.chatTabControl.SuspendLayout();
            this.lobbyPage.SuspendLayout();
            this.chatTabContainer.Panel1.SuspendLayout();
            this.chatTabContainer.Panel2.SuspendLayout();
            this.chatTabContainer.SuspendLayout();
            this.discoveryContainer.Panel1.SuspendLayout();
            this.discoveryContainer.Panel2.SuspendLayout();
            this.discoveryContainer.SuspendLayout();
            this.usersGroupBox.SuspendLayout();
            this.discoveryViewByToolStrip.SuspendLayout();
            this.servicesGroupBox.SuspendLayout();
            this.chatContainer.Panel1.SuspendLayout();
            this.chatContainer.Panel2.SuspendLayout();
            this.chatContainer.SuspendLayout();
            this.chatBoxContainer.Panel1.SuspendLayout();
            this.chatBoxContainer.Panel2.SuspendLayout();
            this.chatBoxContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servicesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(584, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "topMenuStrip";
            // 
            // servicesToolStripMenuItem
            // 
            this.servicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servicesMenuToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ventriloToolStripMenuItem,
            this.apacheToolStripMenuItem,
            this.srcdsToolStripMenuItem});
            this.servicesToolStripMenuItem.Name = "servicesToolStripMenuItem";
            this.servicesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.servicesToolStripMenuItem.Text = "Services";
            // 
            // servicesMenuToolStripMenuItem
            // 
            this.servicesMenuToolStripMenuItem.Name = "servicesMenuToolStripMenuItem";
            this.servicesMenuToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.servicesMenuToolStripMenuItem.Text = "Services Menu...";
            this.servicesMenuToolStripMenuItem.Click += new System.EventHandler(this.servicesMenuToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // ventriloToolStripMenuItem
            // 
            this.ventriloToolStripMenuItem.Checked = true;
            this.ventriloToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ventriloToolStripMenuItem.Name = "ventriloToolStripMenuItem";
            this.ventriloToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ventriloToolStripMenuItem.Text = "Ventrilo";
            // 
            // apacheToolStripMenuItem
            // 
            this.apacheToolStripMenuItem.Name = "apacheToolStripMenuItem";
            this.apacheToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.apacheToolStripMenuItem.Text = "Apache";
            // 
            // srcdsToolStripMenuItem
            // 
            this.srcdsToolStripMenuItem.Checked = true;
            this.srcdsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.srcdsToolStripMenuItem.Name = "srcdsToolStripMenuItem";
            this.srcdsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.srcdsToolStripMenuItem.Text = "Srcds";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.settingsToolStripMenuItem.Text = "Settings...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // chatTabControl
            // 
            this.chatTabControl.Controls.Add(this.lobbyPage);
            this.chatTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatTabControl.Location = new System.Drawing.Point(0, 24);
            this.chatTabControl.Name = "chatTabControl";
            this.chatTabControl.SelectedIndex = 0;
            this.chatTabControl.Size = new System.Drawing.Size(584, 538);
            this.chatTabControl.TabIndex = 1;
            // 
            // lobbyPage
            // 
            this.lobbyPage.Controls.Add(this.chatTabContainer);
            this.lobbyPage.Location = new System.Drawing.Point(4, 22);
            this.lobbyPage.Name = "lobbyPage";
            this.lobbyPage.Padding = new System.Windows.Forms.Padding(3);
            this.lobbyPage.Size = new System.Drawing.Size(576, 512);
            this.lobbyPage.TabIndex = 0;
            this.lobbyPage.Text = "Lobby";
            this.lobbyPage.UseVisualStyleBackColor = true;
            // 
            // chatTabContainer
            // 
            this.chatTabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatTabContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.chatTabContainer.Location = new System.Drawing.Point(3, 3);
            this.chatTabContainer.Name = "chatTabContainer";
            // 
            // chatTabContainer.Panel1
            // 
            this.chatTabContainer.Panel1.Controls.Add(this.chatContainer);
            this.chatTabContainer.Panel1MinSize = 200;
            // 
            // chatTabContainer.Panel2
            // 
            this.chatTabContainer.Panel2.Controls.Add(this.discoveryContainer);
            this.chatTabContainer.Panel2MinSize = 80;
            this.chatTabContainer.Size = new System.Drawing.Size(570, 506);
            this.chatTabContainer.SplitterDistance = 486;
            this.chatTabContainer.TabIndex = 0;
            // 
            // discoveryContainer
            // 
            this.discoveryContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discoveryContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.discoveryContainer.Location = new System.Drawing.Point(0, 0);
            this.discoveryContainer.Name = "discoveryContainer";
            this.discoveryContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // discoveryContainer.Panel1
            // 
            this.discoveryContainer.Panel1.Controls.Add(this.usersGroupBox);
            this.discoveryContainer.Panel1.Controls.Add(this.discoveryViewByToolStrip);
            this.discoveryContainer.Panel1MinSize = 125;
            // 
            // discoveryContainer.Panel2
            // 
            this.discoveryContainer.Panel2.Controls.Add(this.servicesGroupBox);
            this.discoveryContainer.Panel2MinSize = 100;
            this.discoveryContainer.Size = new System.Drawing.Size(80, 506);
            this.discoveryContainer.SplitterDistance = 288;
            this.discoveryContainer.TabIndex = 0;
            // 
            // usersGroupBox
            // 
            this.usersGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.usersGroupBox.Controls.Add(this.usersListBox);
            this.usersGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersGroupBox.Location = new System.Drawing.Point(0, 25);
            this.usersGroupBox.Name = "usersGroupBox";
            this.usersGroupBox.Size = new System.Drawing.Size(80, 263);
            this.usersGroupBox.TabIndex = 0;
            this.usersGroupBox.TabStop = false;
            this.usersGroupBox.Text = "Users";
            // 
            // usersListBox
            // 
            this.usersListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersListBox.FormattingEnabled = true;
            this.usersListBox.Items.AddRange(new object[] {
            "Zoot",
            "Bob",
            "Alice",
            "Trudy"});
            this.usersListBox.Location = new System.Drawing.Point(3, 16);
            this.usersListBox.Name = "usersListBox";
            this.usersListBox.Size = new System.Drawing.Size(74, 244);
            this.usersListBox.TabIndex = 0;
            // 
            // discoveryViewByToolStrip
            // 
            this.discoveryViewByToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.discoveryViewByToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discoveryViewByButton});
            this.discoveryViewByToolStrip.Location = new System.Drawing.Point(0, 0);
            this.discoveryViewByToolStrip.Name = "discoveryViewByToolStrip";
            this.discoveryViewByToolStrip.Size = new System.Drawing.Size(80, 25);
            this.discoveryViewByToolStrip.TabIndex = 1;
            this.discoveryViewByToolStrip.Text = "viewByToolStrip";
            // 
            // discoveryViewByButton
            // 
            this.discoveryViewByButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.discoveryViewByButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.serviceToolStripMenuItem});
            this.discoveryViewByButton.Image = ((System.Drawing.Image)(resources.GetObject("discoveryViewByButton.Image")));
            this.discoveryViewByButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.discoveryViewByButton.Name = "discoveryViewByButton";
            this.discoveryViewByButton.Size = new System.Drawing.Size(64, 22);
            this.discoveryViewByButton.Text = "View by";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Checked = true;
            this.userToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.userToolStripMenuItem.Text = "User";
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.serviceToolStripMenuItem.Text = "Service";
            // 
            // servicesGroupBox
            // 
            this.servicesGroupBox.Controls.Add(this.servicesListBox);
            this.servicesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.servicesGroupBox.Name = "servicesGroupBox";
            this.servicesGroupBox.Size = new System.Drawing.Size(80, 214);
            this.servicesGroupBox.TabIndex = 0;
            this.servicesGroupBox.TabStop = false;
            this.servicesGroupBox.Text = "Services";
            // 
            // servicesListBox
            // 
            this.servicesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.servicesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesListBox.FormattingEnabled = true;
            this.servicesListBox.Items.AddRange(new object[] {
            "Apache",
            "Srcds",
            "Ventrilo"});
            this.servicesListBox.Location = new System.Drawing.Point(3, 16);
            this.servicesListBox.Name = "servicesListBox";
            this.servicesListBox.Size = new System.Drawing.Size(74, 195);
            this.servicesListBox.TabIndex = 0;
            // 
            // chatContainer
            // 
            this.chatContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.chatContainer.Location = new System.Drawing.Point(0, 0);
            this.chatContainer.Name = "chatContainer";
            this.chatContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // chatContainer.Panel1
            // 
            this.chatContainer.Panel1.Controls.Add(this.chatHistoryBox);
            this.chatContainer.Panel1MinSize = 200;
            // 
            // chatContainer.Panel2
            // 
            this.chatContainer.Panel2.Controls.Add(this.chatBoxContainer);
            this.chatContainer.Panel2MinSize = 20;
            this.chatContainer.Size = new System.Drawing.Size(486, 506);
            this.chatContainer.SplitterDistance = 482;
            this.chatContainer.TabIndex = 0;
            // 
            // chatBoxContainer
            // 
            this.chatBoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBoxContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.chatBoxContainer.IsSplitterFixed = true;
            this.chatBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.chatBoxContainer.Name = "chatBoxContainer";
            // 
            // chatBoxContainer.Panel1
            // 
            this.chatBoxContainer.Panel1.Controls.Add(this.chatBox);
            // 
            // chatBoxContainer.Panel2
            // 
            this.chatBoxContainer.Panel2.Controls.Add(this.sendButton);
            this.chatBoxContainer.Size = new System.Drawing.Size(486, 20);
            this.chatBoxContainer.SplitterDistance = 440;
            this.chatBoxContainer.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendButton.Location = new System.Drawing.Point(0, 0);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(42, 20);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // chatBox
            // 
            this.chatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBox.Location = new System.Drawing.Point(0, 0);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(440, 20);
            this.chatBox.TabIndex = 0;
            // 
            // chatHistoryBox
            // 
            this.chatHistoryBox.BackColor = System.Drawing.SystemColors.Window;
            this.chatHistoryBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatHistoryBox.Location = new System.Drawing.Point(0, 0);
            this.chatHistoryBox.Multiline = true;
            this.chatHistoryBox.Name = "chatHistoryBox";
            this.chatHistoryBox.ReadOnly = true;
            this.chatHistoryBox.Size = new System.Drawing.Size(486, 482);
            this.chatHistoryBox.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.chatTabControl);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "MainWindow";
            this.Text = "NetTunnel";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.chatTabControl.ResumeLayout(false);
            this.lobbyPage.ResumeLayout(false);
            this.chatTabContainer.Panel1.ResumeLayout(false);
            this.chatTabContainer.Panel2.ResumeLayout(false);
            this.chatTabContainer.ResumeLayout(false);
            this.discoveryContainer.Panel1.ResumeLayout(false);
            this.discoveryContainer.Panel1.PerformLayout();
            this.discoveryContainer.Panel2.ResumeLayout(false);
            this.discoveryContainer.ResumeLayout(false);
            this.usersGroupBox.ResumeLayout(false);
            this.discoveryViewByToolStrip.ResumeLayout(false);
            this.discoveryViewByToolStrip.PerformLayout();
            this.servicesGroupBox.ResumeLayout(false);
            this.chatContainer.Panel1.ResumeLayout(false);
            this.chatContainer.Panel1.PerformLayout();
            this.chatContainer.Panel2.ResumeLayout(false);
            this.chatContainer.ResumeLayout(false);
            this.chatBoxContainer.Panel1.ResumeLayout(false);
            this.chatBoxContainer.Panel1.PerformLayout();
            this.chatBoxContainer.Panel2.ResumeLayout(false);
            this.chatBoxContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem servicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ventriloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem srcdsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl chatTabControl;
        private System.Windows.Forms.TabPage lobbyPage;
        private System.Windows.Forms.SplitContainer chatTabContainer;
        private System.Windows.Forms.SplitContainer discoveryContainer;
        private System.Windows.Forms.GroupBox usersGroupBox;
        private System.Windows.Forms.ListBox usersListBox;
        private System.Windows.Forms.GroupBox servicesGroupBox;
        private System.Windows.Forms.ToolStrip discoveryViewByToolStrip;
        private System.Windows.Forms.ToolStripSplitButton discoveryViewByButton;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ListBox servicesListBox;
        private System.Windows.Forms.SplitContainer chatContainer;
        private System.Windows.Forms.SplitContainer chatBoxContainer;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.TextBox chatHistoryBox;
    }
}

