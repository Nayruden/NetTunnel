using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;




namespace NetTunnel
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void servicesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var services_window = new ServicesWindow();
            var result = services_window.ShowDialog(this);
            Console.WriteLine(result.ToString());
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings_window = new SettingsWindow();
            var result = settings_window.ShowDialog(this);
            Console.WriteLine(result.ToString());
        }

        private void chatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine((int)e.KeyChar);
            if (e.KeyChar == '\r')
            {
                sendButton_Click(sender, null); // Reroute
                e.Handled = true;
            }
        }

        private void chatBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If alt-enter, make a new line and increase size to show it
            if (e.KeyValue == '\r' && (e.Modifiers & Keys.Alt) != 0)
            {
                chatContainer.SplitterDistance -= 13; // Value found by trial and error
                chatBox.Text += "\r\n";
                chatBox.SelectionStart = chatBox.GetFirstCharIndexFromLine(chatBox.Lines.Length - 1); // Send caret to new line
                e.SuppressKeyPress = true;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (chatBox.TextLength == 0) return; // Not interested in empty text

            chatHistoryBox.Text += chatBox.Text + Environment.NewLine;
            chatBox.ResetText();
            chatContainer.SplitterDistance = chatContainer.Height - chatContainer.Panel2MinSize - chatContainer.SplitterWidth; // Set back to minimum distance
        }
    }
}
