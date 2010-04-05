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
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace NetTunnel
{
    public partial class MainWindow : Form
    {
        private readonly CancellationTokenSource tunnel_thread_canceller;
        private readonly CancellationTokenSource server_thread_canceller;
        private readonly UserManager user_manager;
        private readonly Tunnel.TunnelManager tunnel_manager;
        private readonly ServerCommunicationHandler server_communication_handler;

        public MainWindow()
        {
            InitializeComponent();

            refreshServices();

            ////////////////////////////
            // Now setup new threads! //
            ////////////////////////////
            var server_endpoint = new IPEndPoint(Dns.GetHostAddresses("server.ulyssesmod.net")[0], 4141);

            // Handle trace information
            Trace.Listeners.Clear(); // Clear default listeners

            // File listener
            File.Delete("output.txt");
            Trace.Listeners.Add(new TextWriterTraceListener("output.txt"));

            // Console listener
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            // Always flush on every message
            Trace.AutoFlush = true;

            Trace.TraceInformation("Starting up...");

            user_manager = new UserManager();

            // Start the tunnel processing thread
            tunnel_thread_canceller = new CancellationTokenSource();
            Tunnel.TunnelManager tunnel_manager = new Tunnel.TunnelManager(tunnel_thread_canceller.Token, server_endpoint);
            Task.Factory.StartNew(tunnel_manager.Run);

            // Start the server socket processing thread
            server_thread_canceller = new CancellationTokenSource();
            server_communication_handler = new ServerCommunicationHandler(this, server_thread_canceller.Token, tunnel_manager, user_manager, server_endpoint);
            Task.Factory.StartNew(server_communication_handler.Run);
        }

        public void Error( string str )
        {
            MessageBox.Show(this, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }

        public void nameChanged( string old_name, string new_name )
        {
            chatHistoryBox.AppendText("***{0} has changed their name to {1}***{2}".F(old_name, new_name, Environment.NewLine));
        }

        private void servicesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var services_window = new ServicesWindow( user_manager.local_user.services );
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

            foreach (var service in user_manager.local_user.services)
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

            var s = "[{0}] {1}: {2}{3}".F(DateTime.Now.ToShortTimeString(), Settings.Default.Nick, trimmed_text, Environment.NewLine);
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

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

        }   
    }
}
