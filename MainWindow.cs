﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
