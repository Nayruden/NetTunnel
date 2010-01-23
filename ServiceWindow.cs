using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace NetTunnel
{
    public partial class ServiceWindow : Form
    {
        private static string portsText = "Examples: '5800-5900' or '21' (w/o quotes)";
        private MainWindow main_window;
        private Service? edit_on; // Object from the main window we're editing, if any

        public ServiceWindow( MainWindow main_window, Service? edit_on )
        {
            InitializeComponent();
            portsBox.Text = portsText;
            this.main_window = main_window;
            this.edit_on = edit_on;

            // Add some templates
            foreach (var service in KnownServices.services)
                templateCB.Items.Add(service);

            // Prepopulate with existing service
            if (edit_on != null)
                populateWith((Service)edit_on);
        }

        private void populateWith(Service service)
        {
            serviceNameBox.Text = service.service_name;
            portsAndProtoLV.Items.Clear(); // Reset the list view
            foreach (var port_range in service.port_ranges)
            {
                string port_range_string;
                if (port_range.start == port_range.end)
                    port_range_string = port_range.start.ToString();
                else
                    port_range_string = port_range.start + "-" + port_range.end;

                var protocol = port_range.protocols.ToString();
                if (protocol == "BOTH") protocol = "Both";
                portsAndProtoLV.Items.Add(new ListViewItem(new[] { port_range_string, protocol }));
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string proto;
            if (UDPButton.Checked) proto = UDPButton.Text;         
            else if (bothButton.Checked) proto = bothButton.Text;
            else proto = TCPButton.Text; // Default case
            portsAndProtoLV.Items.Add( new ListViewItem( new[] { portsBox.Text, proto } ) );
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in portsAndProtoLV.SelectedItems)
                portsAndProtoLV.Items.Remove(item);
        }

        private void portsBox_Click(object sender, EventArgs e)
        {
            if ( portsBox.Text == portsText )
                portsBox.Text = "";
        }

        private void portsBox_Leave(object sender, EventArgs e)
        {
            if (portsBox.TextLength == 0)
                portsBox.Text = portsText;
        }

        private void templateCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            populateWith((Service)templateCB.SelectedItem);            
        }

        private void portsBox_Validating(object sender, CancelEventArgs e)
        {
            if (!validatePortsBox())
                e.Cancel = true;
        }

        private bool validatePortsBox()
        {
            // Can we correctly parse this?
            PortRange port_range = new PortRange();
            if (!port_range.parseRange(portsBox.Text))
            {
                MessageBox.Show("Please enter a port range like '67' or '67-75' without quotes");
                return false;
            }
            else if (port_range.start > port_range.end)
            {
                MessageBox.Show("Please make sure that the first number is smaller or the same as the second number");
                return false;
            }
            return true;
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            if (serviceNameBox.TextLength == 0)
            {
                MessageBox.Show("Please specify a name for the service");
                return;
            }

            Service service = new Service(serviceNameBox.Text);
            service.port_ranges = new PortRange[portsAndProtoLV.Items.Count];
            foreach (ListViewItem item in portsAndProtoLV.Items)
            {
                PortRange port_range = new PortRange();
                var ret = port_range.parseRange(item.Text);
                Debug.Assert(ret);

                Protocols protocols;
                switch (item.SubItems[1].Text)
                {
                    case "TCP":
                        protocols = Protocols.TCP;
                        break;

                    case "UDP":
                        protocols = Protocols.UDP;
                        break;

                    case "Both":
                        protocols = Protocols.BOTH;
                        break;

                    default:
                        protocols = Protocols.TCP; // Just to make the compiler happy
                        Debug.Assert(false, "Bad case!");
                        break;
                }

                port_range.protocols = protocols;

                service.port_ranges[item.Index] = port_range;
            }

            main_window.addService(service);
            if (edit_on != null) // Clear old one if we were editing
                main_window.removeService((Service)edit_on);

            Close();
        }        
    }

    
}
