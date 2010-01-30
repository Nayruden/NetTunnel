using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NetTunnel.Properties;




namespace NetTunnel
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            refreshServices();
        }

        public void nameChanged( string old_name, string new_name )
        {
            chatHistoryBox.AppendText("***{0} has changed their name to {1}***{2}".F(old_name, new_name, Environment.NewLine));
        }

        private void servicesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var services_window = new ServicesWindow();
            var result = services_window.ShowDialog(this);

            refreshServices();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about_window = new AboutWindow();
            var result = about_window.ShowDialog(this);
        }

        private void refreshServices()
        {
            servicesToolStripMenuItem.DropDownItems.Clear();
            servicesToolStripMenuItem.DropDownItems.Add(servicesMenuToolStripMenuItem);
            servicesToolStripMenuItem.DropDownItems.Add(toolStripLine);

            foreach (var service in SharedServices.services)
            {
                var s = service; // Need to copy in for lamdba
                var toolstrip_item = new ToolStripMenuItem();
                toolstrip_item.Checked = service.enabled;
                toolstrip_item.CheckOnClick = true;
                toolstrip_item.Size = new Size(159, 22);
                toolstrip_item.Text = service.service_name;                
                toolstrip_item.CheckedChanged += new EventHandler((object sender, EventArgs e) => s.enabled = toolstrip_item.Checked);

                servicesToolStripMenuItem.DropDownItems.Add(toolstrip_item);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings_window = new SettingsWindow();
            var result = settings_window.ShowDialog(this);
        }

        private void chatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                sendButton_Click(sender, null); // Reroute
                e.Handled = true;
            }
        }

        private void sizeSplitter()
        {
            const int text_height = 13; // Value found by trial and error
            chatContainer.SplitterDistance = chatContainer.Height - chatContainer.Panel2MinSize - 
                                             chatContainer.SplitterWidth - (chatBox.Lines.Length-1) * text_height;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var trimmed_text = chatBox.Text.Trim();
            if (trimmed_text.Length == 0) return; // Not interested in empty text

            var s = "[{0}] {1}: {2}{3}".F(DateTime.Now.ToShortTimeString(), Settings.Default.Nick, chatBox.Text, Environment.NewLine);
            chatHistoryBox.AppendText(s);
            chatBox.ResetText();
            chatBox.Focus(); // Make sure focus remains
        }

        private void chatBox_TextChanged(object sender, EventArgs e)
        {
            sizeSplitter();
        }

        private void chatHistoryBox_TextChanged(object sender, EventArgs e)
        {
            // Autoscroll to bottom
            chatHistoryBox.Select(chatHistoryBox.Text.Length, 0);
            chatHistoryBox.ScrollToCaret();
        }

        private void chatHistoryBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (chatHistoryBox.SelectionLength > 0)
                Clipboard.SetText(chatHistoryBox.SelectedText); // Copy to clipboard
            chatBox.Focus(); // Focus on box again
        }      
    }
}
