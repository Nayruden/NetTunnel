using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NetTunnel
{
    public partial class ServicesWindow : Form
    {
        private Service modifying_service;

        public ServicesWindow()
        {
            InitializeComponent();

            var bs = new BindingSource();
            bs.DataSource = KnownServices.services;
            servicesComboBox.DataSource = bs;
            servicesComboBox.SelectedIndex = -1;

            bs = new BindingSource();
            bs.DataSource = SharedServices.services;
            servicesListBox.DataSource = bs;
            servicesListBox.SelectedIndex = -1;

            clearPortsAndProto(); // Make sure it's clean
            refreshServices(); // Set check states
        }

        private void populateWith(Service service)
        {
            serviceNameBox.Text = service.service_name;
            portsAndProtoListView.Items.Clear(); // Reset the list view
            foreach (var port_range in service.port_ranges)
            {
                string port_range_string;
                if (port_range.start == port_range.end)
                    port_range_string = port_range.start.ToString();
                else
                    port_range_string = port_range.start + "-" + port_range.end;

                var protocol = port_range.protocols.ToString();
                if (protocol == "BOTH") protocol = "Both";
                portsAndProtoListView.Items.Add(new ListViewItem(new[] { port_range_string, protocol }));
            }
        }

        private void servicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (servicesComboBox.SelectedIndex < 0) return; // Not interested

            populateWith((Service)servicesComboBox.SelectedItem); // Populate from template                
            modifyServiceButton.Enabled = false;
        }

        private void servicesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (servicesListBox.SelectedIndex < 0) return; // Not interested

            var service = (Service)servicesListBox.SelectedItem;
            populateWith(service); // Populate from selected item
            modifying_service = service;
            modifyServiceButton.Enabled = true;
        }

        private void addPortsButton_Click(object sender, EventArgs e)
        {
            if (!validatePortsBox()) return;

            string proto;
            if (UDPRadioButton.Checked) proto = UDPRadioButton.Text;
            else if (bothRadioButton.Checked) proto = bothRadioButton.Text;
            else proto = TCPRadioButton.Text; // Default case
            portsAndProtoListView.Items.Add(new ListViewItem(new[] { portsBox.Text, proto }));
        }

        private bool validatePortsBox()
        {
            var trimmed_ports = portsBox.Text.Trim();
            ushort begin, end;
            if (trimmed_ports.Length == 0 || !PortRange.parseRange(trimmed_ports, out begin, out end))
            {
                portsBox.BackColor = Color.MistyRose;
                return false;
            }

            portsBox.BackColor = SystemColors.Window;
            return true;
        }

        private void portsBox_Validating(object sender, CancelEventArgs e)
        {
            validatePortsBox();
        }

        private void clearPortsAndProto()
        {
            serviceNameBox.ResetText();
            portsAndProtoListView.Items.Clear();
            portsBox.ResetText();
            modifyServiceButton.Enabled = false;
        }

        private void removePortsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in portsAndProtoListView.SelectedItems)
                portsAndProtoListView.Items.Remove(item);
        }

        private void commitService( bool modify )
        {
            if (portsAndProtoListView.Items.Count == 0) return; // Not interested

            Service service;
            if (!modify)
                service = new Service(serviceNameBox.Text);
            else
            {
                service = (Service)servicesListBox.SelectedItem;
                service.service_name = serviceNameBox.Text;
            }

            service.port_ranges = new PortRange[portsAndProtoListView.Items.Count];
            foreach (ListViewItem item in portsAndProtoListView.Items)
            {
                PortRange port_range = new PortRange();
                var ret = port_range.parseRange(item.Text);
                Debug.Assert(ret); // We should have already validated this

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

            var bs = (BindingSource)servicesListBox.DataSource;
            if (!modify)
            {
                bs.Add(service);
            }                            

            SharedServices.services.Sort();
            refreshServices(); // Reordered, need to refresh
            servicesListBox.SelectedItem = service;
        }

        private void refreshServices()
        {
            var bs = (BindingSource)servicesListBox.DataSource;
            bs.ResetBindings(false);

            int i = 0;
            foreach (Service service in bs.List)
            {
                servicesListBox.SetItemChecked(i, service.enabled);          
                i++;
            }
        }

        private void addServiceButton_Click(object sender, EventArgs e)
        {
            commitService( false );            
        }

        private void modifyServiceButton_Click(object sender, EventArgs e)
        {
            commitService( true );
        }

        private void deleteServiceButton_Click(object sender, EventArgs e)
        {
            ((BindingSource)servicesListBox.DataSource).RemoveCurrent();
            refreshServices();
        }

        private void servicesListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Note the change
            ((Service)servicesListBox.Items[e.Index]).enabled = e.NewValue == CheckState.Checked;
        }
    }
}
