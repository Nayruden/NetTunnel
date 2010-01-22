using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetTunnel
{
    public partial class ServiceWindow : Form
    {
        public ServiceWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string proto;
            if (UDPButton.Checked) proto = UDPButton.Text;         
            else if (bothButton.Checked) proto = bothButton.Text;
            else proto = TCPButton.Text; // Default case
            portsAndProtoLB.Items.Add( new ListViewItem( new[] { portsBox.Text, proto } ) );
        }
    }
}
