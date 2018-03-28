using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;

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
        C_TFTPClient.Upload upload;

        // Variable pour le thread
        Thread t;

        // Méthode déléguée pour afficher le statut du client
        public delegate void dSetText(string str);
        public dSetText ServerStatus;

        public frmClient()
        {
            InitializeComponent();
            UpdateStatus("Bienvenue dans le client de transfert de fichiers TFTP !\r\n");
            txtServerIPAdress.Text = "127.0.0.1";
            txtDownloadIP.Text = "127.0.0.1";

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
            bool flag = ValidateUploadBoxes();
            if (flag)
            {
                upload = new C_TFTPClient.Upload(this);
                upload.SetFichier(txtFilePath.Text, txtRemoteFileName.Text);
                upload.SetPointDistant(IPAddress.Parse(txtServerIPAdress.Text));
                t = new Thread(new ThreadStart(upload.UploadFile)); // Partir le thread et le linker avec la méthode de Upload
                t.IsBackground = true; // Fermer le thread quand la fenêtre ferme
                t.Start();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bool flag = ValidateDownloadBoxes();
            if (flag)
            {
                C_TFTPClient.Download download = new C_TFTPClient.Download(this);
                download.SetFichier(txtNewFileDownload.Text, txtDownloadRemoteFIle.Text);
                download.SetPointDistant(IPAddress.Parse(txtDownloadIP.Text));
                t = new Thread(new ThreadStart(download.DownloadFile)); // Partir le thread et le linker avec la méthode de download
                t.IsBackground = true; // Fermer le thread quand la fenêtre ferme
                t.Start();
            }
        }

        public void UpdateStatus(string Status)
        {
            // Envoi du statut dans le textbox du formulaire
            txtStatus.Text += Status + "\r\n";
        }

        private bool ValidateUploadBoxes()
        {
            if (txtServerIPAdress.Text == "")
            {
                MessageBox.Show("Vous devez entrer une addresse IP valide.", "Avertissement IP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (txtFilePath.Text == "")
            {
                MessageBox.Show("Vous devez entrer un fichier local valide.", "Avertissement fichier local", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (txtRemoteFileName.Text == "")
            {
                MessageBox.Show("Vous devez entrer un nom de fichier distant valide.", "Avertissement fichier distant", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

        private bool ValidateDownloadBoxes()
        {
            if (txtDownloadIP.Text == "")
            {
                MessageBox.Show("Vous devez entrer une addresse IP valide.", "Avertissement IP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (txtNewFileDownload.Text == "")
            {
                MessageBox.Show("Vous devez entrer un nouveau nom pour le fichier à télécharger", "Avertissement fichier local", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (txtDownloadRemoteFIle.Text == "")
            {
                MessageBox.Show("Vous devez entrer un nom de fichier distant valide.", "Avertissement fichier distant", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }
    }
}
