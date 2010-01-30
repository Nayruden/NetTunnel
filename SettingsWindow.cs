using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetTunnel.Properties;

namespace NetTunnel
{
    public partial class SettingsWindow : Form
    {
        private readonly string old_nick;

        public SettingsWindow()
        {
            InitializeComponent();

            old_nick = Settings.Default.Nick;
        }

        private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (old_nick != nickBox.Text)
                ((MainWindow)Owner).nameChanged(old_nick, nickBox.Text);

            Settings.Default.Save();
        }
    }
}
