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
        C_TFTP.ListenServer server;

        public frmServer()
        {
            InitializeComponent();
            server = new C_TFTP.ListenServer(); // Instanciation du serveur
        }

        private void btnDemarrer_Click(object sender, EventArgs e)
        {
            //Désactiver le bouton démarrer
            btnDemarrer.Enabled = false;

            //Instancier l'objet t pour démarrer le thread «ListenThread» de la classe ListenServer
            t = new Thread(new ThreadStart(server.ListenThread));

            //Démarrer la thread
            t.Start();
        }

        private void btnArreter_Click(object sender, EventArgs e)
        {
            // Terminer le thread
            server.m_fin = true;
            //Fermer l'application
            Application.Exit();
        }
    }
}
