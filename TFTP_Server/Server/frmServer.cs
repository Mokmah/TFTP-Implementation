using System;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        // Définition des variables membres
        Thread t;

        // Objets des autres classes pour intéragir avec le formulaire de façon à les faire intéragir ensemble
        C_TFTP.ListenServer server;
        C_TFTP.RRQ rrQ;
        C_TFTP.WRQ wrQ;

        // Méthode déléguée pour envoyer le statut des autres classes au formulaire
        public delegate void dSetText(string str);
        public dSetText ServerStatus;

        public frmServer()
        {
            InitializeComponent();

            //Instantiation des autres classes en leur donnant le handle du formulaire en paramètre
            server = new C_TFTP.ListenServer(this); // Instanciation du serveur
            rrQ = new C_TFTP.RRQ(this);
            wrQ = new C_TFTP.WRQ(this);

            // Instantiation de la méthode déléguée en y associant sa méthode cible.
            ServerStatus = new dSetText(UpdateStatus);
        }

        private void btnDemarrer_Click(object sender, EventArgs e)
        {
            //Désactiver le bouton démarrer
            btnDemarrer.Enabled = false;

            //Instancier l'objet t pour démarrer le thread «ListenThread» de la classe ListenServer
            t = new Thread(new ThreadStart(server.ListenThread));

            //Démarrer la thread
            t.Start();

            // Background worker

            // Afficher le statut
            UpdateStatus("Démarrage du serveur TFTP\r\n");
        }

        private void btnArreter_Click(object sender, EventArgs e)
        {
            // Terminer le thread
            server.m_fin = true;

            //Fermer l'application
            UpdateStatus("Le serveur TFTP est arrêté");

            //Fermeture de l'application
            Application.Exit();
        }

        public void UpdateStatus(string Status)
        {
            // Envoi du statut dans le textbox du formulaire
            txtStatus.Text += Status + "\r\n";
        }
    }
}
