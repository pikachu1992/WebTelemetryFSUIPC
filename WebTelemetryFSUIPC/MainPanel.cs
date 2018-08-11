using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebTelemetryFSUIPC
{
    public partial class MainPanel : Form
    {
        Server ServerObject = null;

        public MainPanel()
        {
            InitializeComponent();
        }

        public void AddLog(String msg)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtServerLog.Text += msg;
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServerObject.Start();
        }

        private void MainPanel_Load(object sender, EventArgs e)
        {
            ServerObject = new Server(this);
        }
    }
}
