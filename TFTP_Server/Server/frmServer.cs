using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        // Définition des variables membres
        Thread t;
        

        public frmServer()
        {
            InitializeComponent();
        }

        private void btnDemarrer_Click(object sender, EventArgs e)
        {

        }

        private void btnArreter_Click(object sender, EventArgs e)
        {

        }
    }
}
