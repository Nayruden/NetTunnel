namespace NetTunnel
{
    partial class ServicesWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesWindow));
            this.servicesGroupBox = new System.Windows.Forms.GroupBox();
            this.editServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serviceNameBox = new System.Windows.Forms.TextBox();
            this.portsAndProtoListView = new System.Windows.Forms.ListView();
            this.portsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.protocolHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.portsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UDPRadioButton = new System.Windows.Forms.RadioButton();
            this.TCPRadioButton = new System.Windows.Forms.RadioButton();
            this.bothRadioButton = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.addServiceButton = new System.Windows.Forms.Button();
            this.modifyServiceButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.servicesListBox = new System.Windows.Forms.CheckedListBox();
            this.servicesGroupBox.SuspendLayout();
            this.editServiceGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // servicesGroupBox
            // 
            this.servicesGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.servicesGroupBox.Controls.Add(this.servicesListBox);
            this.servicesGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.servicesGroupBox.Location = new System.Drawing.Point(13, 13);
            this.servicesGroupBox.Name = "servicesGroupBox";
            this.servicesGroupBox.Size = new System.Drawing.Size(113, 284);
            this.servicesGroupBox.TabIndex = 2;
            this.servicesGroupBox.TabStop = false;
            this.servicesGroupBox.Text = "Services";
            // 
            // editServiceGroupBox
            // 
            this.editServiceGroupBox.Controls.Add(this.modifyServiceButton);
            this.editServiceGroupBox.Controls.Add(this.addServiceButton);
            this.editServiceGroupBox.Controls.Add(this.label5);
            this.editServiceGroupBox.Controls.Add(this.bothRadioButton);
            this.editServiceGroupBox.Controls.Add(this.TCPRadioButton);
            this.editServiceGroupBox.Controls.Add(this.UDPRadioButton);
            this.editServiceGroupBox.Controls.Add(this.label4);
            this.editServiceGroupBox.Controls.Add(this.portsBox);
            this.editServiceGroupBox.Controls.Add(this.label3);
            this.editServiceGroupBox.Controls.Add(this.button2);
            this.editServiceGroupBox.Controls.Add(this.button1);
            this.editServiceGroupBox.Controls.Add(this.portsAndProtoListView);
            this.editServiceGroupBox.Controls.Add(this.serviceNameBox);
            this.editServiceGroupBox.Controls.Add(this.label2);
            this.editServiceGroupBox.Location = new System.Drawing.Point(133, 33);
            this.editServiceGroupBox.Name = "editServiceGroupBox";
            this.editServiceGroupBox.Size = new System.Drawing.Size(242, 302);
            this.editServiceGroupBox.TabIndex = 3;
            this.editServiceGroupBox.TabStop = false;
            this.editServiceGroupBox.Text = "Modify/Add Service";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Load Template:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(219, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(155, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Service Name:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // serviceNameBox
            // 
            this.serviceNameBox.Location = new System.Drawing.Point(86, 13);
            this.serviceNameBox.Name = "serviceNameBox";
            this.serviceNameBox.Size = new System.Drawing.Size(144, 20);
            this.serviceNameBox.TabIndex = 1;
            // 
            // portsAndProtoListView
            // 
            this.portsAndProtoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.portsHeader,
            this.protocolHeader});
            this.portsAndProtoListView.FullRowSelect = true;
            this.portsAndProtoListView.GridLines = true;
            this.portsAndProtoListView.Location = new System.Drawing.Point(6, 39);
            this.portsAndProtoListView.Name = "portsAndProtoListView";
            this.portsAndProtoListView.Size = new System.Drawing.Size(224, 133);
            this.portsAndProtoListView.TabIndex = 2;
            this.portsAndProtoListView.UseCompatibleStateImageBehavior = false;
            this.portsAndProtoListView.View = System.Windows.Forms.View.Details;
            // 
            // portsHeader
            // 
            this.portsHeader.Text = "Ports";
            this.portsHeader.Width = 160;
            // 
            // protocolHeader
            // 
            this.protocolHeader.Text = "Protocol";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(26, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port(s):";
            // 
            // portsBox
            // 
            this.portsBox.Location = new System.Drawing.Point(55, 208);
            this.portsBox.Name = "portsBox";
            this.portsBox.Size = new System.Drawing.Size(178, 20);
            this.portsBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Protocol:";
            // 
            // UDPRadioButton
            // 
            this.UDPRadioButton.AutoSize = true;
            this.UDPRadioButton.Location = new System.Drawing.Point(64, 238);
            this.UDPRadioButton.Name = "UDPRadioButton";
            this.UDPRadioButton.Size = new System.Drawing.Size(48, 17);
            this.UDPRadioButton.TabIndex = 8;
            this.UDPRadioButton.TabStop = true;
            this.UDPRadioButton.Text = "UDP";
            this.UDPRadioButton.UseVisualStyleBackColor = true;
            // 
            // TCPRadioButton
            // 
            this.TCPRadioButton.AutoSize = true;
            this.TCPRadioButton.Location = new System.Drawing.Point(118, 238);
            this.TCPRadioButton.Name = "TCPRadioButton";
            this.TCPRadioButton.Size = new System.Drawing.Size(46, 17);
            this.TCPRadioButton.TabIndex = 9;
            this.TCPRadioButton.TabStop = true;
            this.TCPRadioButton.Text = "TCP";
            this.TCPRadioButton.UseVisualStyleBackColor = true;
            // 
            // bothRadioButton
            // 
            this.bothRadioButton.AutoSize = true;
            this.bothRadioButton.Location = new System.Drawing.Point(170, 238);
            this.bothRadioButton.Name = "bothRadioButton";
            this.bothRadioButton.Size = new System.Drawing.Size(47, 17);
            this.bothRadioButton.TabIndex = 10;
            this.bothRadioButton.TabStop = true;
            this.bothRadioButton.Text = "Both";
            this.bothRadioButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(62, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 26);
            this.label5.TabIndex = 11;
            this.label5.Text = "Specify a port or a range, examples: \'5800-5900\' or \'21\'";
            // 
            // addServiceButton
            // 
            this.addServiceButton.Location = new System.Drawing.Point(26, 270);
            this.addServiceButton.Name = "addServiceButton";
            this.addServiceButton.Size = new System.Drawing.Size(80, 23);
            this.addServiceButton.TabIndex = 12;
            this.addServiceButton.Text = "Add As New";
            this.addServiceButton.UseVisualStyleBackColor = true;
            // 
            // modifyServiceButton
            // 
            this.modifyServiceButton.Enabled = false;
            this.modifyServiceButton.Location = new System.Drawing.Point(112, 270);
            this.modifyServiceButton.Name = "modifyServiceButton";
            this.modifyServiceButton.Size = new System.Drawing.Size(80, 23);
            this.modifyServiceButton.TabIndex = 13;
            this.modifyServiceButton.Text = "Modify";
            this.modifyServiceButton.UseVisualStyleBackColor = true;
            this.modifyServiceButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(30, 303);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(80, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "Delete";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // servicesListBox
            // 
            this.servicesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesListBox.FormattingEnabled = true;
            this.servicesListBox.Items.AddRange(new object[] {
            "Srcds",
            "Apache",
            "Ventrilo",
            "Cities Online"});
            this.servicesListBox.Location = new System.Drawing.Point(3, 16);
            this.servicesListBox.Name = "servicesListBox";
            this.servicesListBox.Size = new System.Drawing.Size(107, 265);
            this.servicesListBox.TabIndex = 0;
            // 
            // ServicesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 340);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editServiceGroupBox);
            this.Controls.Add(this.servicesGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServicesWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Services";
            this.TopMost = true;
            this.servicesGroupBox.ResumeLayout(false);
            this.editServiceGroupBox.ResumeLayout(false);
            this.editServiceGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox servicesGroupBox;
        private System.Windows.Forms.GroupBox editServiceGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton bothRadioButton;
        private System.Windows.Forms.RadioButton TCPRadioButton;
        private System.Windows.Forms.RadioButton UDPRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox portsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView portsAndProtoListView;
        private System.Windows.Forms.ColumnHeader portsHeader;
        private System.Windows.Forms.ColumnHeader protocolHeader;
        private System.Windows.Forms.TextBox serviceNameBox;
        private System.Windows.Forms.Button modifyServiceButton;
        private System.Windows.Forms.Button addServiceButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckedListBox servicesListBox;
    }
}