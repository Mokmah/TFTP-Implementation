using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client.C_TFTPClient
{
    class Upload : ErrorType
    {
        // Définition des variables *****

        // Chaîne de caractères renfermant le code d'erreur
        string errorMsg;

        // Socket pour l'Upload
        Socket sUpload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local et distant
        EndPoint PointDistantUpload = new IPEndPoint(0, 0);
        EndPoint PointLocalUpload = new IPEndPoint(0, 69);

        // Instantiation du formulaire pour y envoyer le statut du client
        frmClient f;

        // On appelle l'objet frmClient pour envoyer le statut
        public Upload(frmClient myForm)
        {
            f = myForm;
        }

        // Variables pour le fichier à upload
        string fileUpload;
        FileStream fs;

        private void ConnexionToServer()
        {
            try
            {
                sUpload.Bind(PointLocalUpload);
                f.Invoke(f.ServerStatus, new object[] { "La connexion au serveur est établie." });
            }
            catch(SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { "La connexion au serveur n'a pas marchée... Veuillez réessayer" });
            }
            
        }

        public void SetPointDistant(EndPoint pointDistant)
        {
            PointDistantUpload = pointDistant;
        }

        public void SetFichier(string fileName)
        {
            fileUpload = string.Format("@{0}",fileName);
        }

        public void UploadThread()
        {
            // Définition des variables pour le thread
            byte[] bTrame = new byte[4];
        }

    }
}
