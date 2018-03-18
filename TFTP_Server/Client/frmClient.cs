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

        public frmClient()
        {
            InitializeComponent();
            txtServerIPAdress.Text = "127.0.0.1";
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

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }
    }
}
