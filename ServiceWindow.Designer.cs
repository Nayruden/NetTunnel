namespace NetTunnel
{
    partial class ServiceWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.templateCB = new System.Windows.Forms.ComboBox();
            this.portsAndProtoLV = new System.Windows.Forms.ListView();
            this.ports = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.portsBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UDPButton = new System.Windows.Forms.RadioButton();
            this.TCPButton = new System.Windows.Forms.RadioButton();
            this.bothButton = new System.Windows.Forms.RadioButton();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.serviceNameBox = new System.Windows.Forms.TextBox();
            this.okayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Templates:";
            // 
            // templateCB
            // 
            this.templateCB.FormattingEnabled = true;
            this.templateCB.Location = new System.Drawing.Point(78, 10);
            this.templateCB.Name = "templateCB";
            this.templateCB.Size = new System.Drawing.Size(194, 21);
            this.templateCB.TabIndex = 1;
            this.templateCB.Text = "Select a template...";
            this.templateCB.SelectionChangeCommitted += new System.EventHandler(this.templateCB_SelectionChangeCommitted);
            // 
            // portsAndProtoLV
            // 
            this.portsAndProtoLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ports,
            this.protocol});
            this.portsAndProtoLV.FullRowSelect = true;
            this.portsAndProtoLV.GridLines = true;
            this.portsAndProtoLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.portsAndProtoLV.Location = new System.Drawing.Point(12, 68);
            this.portsAndProtoLV.Name = "portsAndProtoLV";
            this.portsAndProtoLV.Size = new System.Drawing.Size(260, 129);
            this.portsAndProtoLV.TabIndex = 2;
            this.portsAndProtoLV.UseCompatibleStateImageBehavior = false;
            this.portsAndProtoLV.View = System.Windows.Forms.View.Details;
            // 
            // ports
            // 
            this.ports.Text = "Ports";
            this.ports.Width = 199;
            // 
            // protocol
            // 
            this.protocol.Text = "Protocol";
            this.protocol.Width = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port(s):";
            // 
            // portsBox
            // 
            this.portsBox.Location = new System.Drawing.Point(58, 235);
            this.portsBox.Name = "portsBox";
            this.portsBox.Size = new System.Drawing.Size(214, 20);
            this.portsBox.TabIndex = 4;
            this.portsBox.Click += new System.EventHandler(this.portsBox_Click);
            this.portsBox.Leave += new System.EventHandler(this.portsBox_Leave);
            this.portsBox.Validating += new System.ComponentModel.CancelEventHandler(this.portsBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Protocol:";
            // 
            // UDPButton
            // 
            this.UDPButton.AutoSize = true;
            this.UDPButton.Location = new System.Drawing.Point(67, 269);
            this.UDPButton.Name = "UDPButton";
            this.UDPButton.Size = new System.Drawing.Size(48, 17);
            this.UDPButton.TabIndex = 6;
            this.UDPButton.TabStop = true;
            this.UDPButton.Text = "UDP";
            this.UDPButton.UseVisualStyleBackColor = true;
            // 
            // TCPButton
            // 
            this.TCPButton.AutoSize = true;
            this.TCPButton.Location = new System.Drawing.Point(121, 269);
            this.TCPButton.Name = "TCPButton";
            this.TCPButton.Size = new System.Drawing.Size(46, 17);
            this.TCPButton.TabIndex = 7;
            this.TCPButton.TabStop = true;
            this.TCPButton.Text = "TCP";
            this.TCPButton.UseVisualStyleBackColor = true;
            // 
            // bothButton
            // 
            this.bothButton.AutoSize = true;
            this.bothButton.Location = new System.Drawing.Point(173, 269);
            this.bothButton.Name = "bothButton";
            this.bothButton.Size = new System.Drawing.Size(47, 17);
            this.bothButton.TabIndex = 8;
            this.bothButton.TabStop = true;
            this.bothButton.Text = "Both";
            this.bothButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(32, 203);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(21, 23);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "-";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(12, 203);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(21, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Service Name:";
            // 
            // serviceNameBox
            // 
            this.serviceNameBox.Location = new System.Drawing.Point(98, 42);
            this.serviceNameBox.Name = "serviceNameBox";
            this.serviceNameBox.Size = new System.Drawing.Size(174, 20);
            this.serviceNameBox.TabIndex = 13;
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(58, 296);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(162, 23);
            this.okayButton.TabIndex = 14;
            this.okayButton.Text = "Okay";
            this.okayButton.UseVisualStyleBackColor = true;
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // ServiceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 331);
            this.Controls.Add(this.okayButton);
            this.Controls.Add(this.serviceNameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.bothButton);
            this.Controls.Add(this.TCPButton);
            this.Controls.Add(this.UDPButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.portsAndProtoLV);
            this.Controls.Add(this.templateCB);
            this.Controls.Add(this.label1);
            this.Name = "ServiceWindow";
            this.Text = "ServiceWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox templateCB;
        private System.Windows.Forms.ListView portsAndProtoLV;
        private System.Windows.Forms.ColumnHeader ports;
        private System.Windows.Forms.ColumnHeader protocol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton UDPButton;
        private System.Windows.Forms.RadioButton TCPButton;
        private System.Windows.Forms.RadioButton bothButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serviceNameBox;
        private System.Windows.Forms.Button okayButton;
    }
}