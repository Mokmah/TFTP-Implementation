using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

/************************
 *  Par William Garneau 
 ***********************/

namespace Client
{
    public partial class frmClient : Form
    {
        // Définition des variables membres ***
        
        // Variables pour aller chercher le fichier local
        OpenFileDialog fileDialog = new OpenFileDialog();
        string m_FilePath;

        // Accès aux méthodes des autres classes


        // Méthode déléguée pour afficher le statut du client
        public delegate void dSetText(string str);
        public dSetText ServerStatus;

        public frmClient()
        {
            InitializeComponent();
            txtServerIPAdress.Text = "192.168.1.118";

            // Instantiation de la méthode déléguée en y associant sa méthode cible.
            ServerStatus = new dSetText(UpdateStatus);
        }

        // Évènement pour chercher un fichier dans un dialogue et retourner un string correspondant
        private void btnFileSearch_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            m_FilePath = fileDialog.FileName;
            txtFilePath.Text = m_FilePath;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            C_TFTPClient.Upload upload = new C_TFTPClient.Upload(this);
            upload.ConnexionToServer(txtServerIPAdress.Text);
            
            upload.SetFichier(m_FilePath, txtRemoteFileName.Text);
            Thread threadUpload = new Thread(new ThreadStart(upload.UploadThread));
            threadUpload.Start();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }

        public void UpdateStatus(string Status)
        {
            // Envoi du statut dans le textbox du formulaire
            txtStatus.Text += Status + "\r\n";
        }
    }
}
