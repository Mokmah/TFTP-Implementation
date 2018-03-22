using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.C_TFTPClient
{
    /// <summary>
    /// Classe du client TFTP qui servira télécharger des fichiers du poste du serveur vers le nôtre
    /// - Téléchargement de fichiers -
    /// </summary>
    class Download
    {
        // Définition des variables ***

        // Socket pour le download
        Socket sDownload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local et distant
        EndPoint pointDistantDownload;
        EndPoint pointLocalDownload = new IPEndPoint(0,0);

        // Tableaux de bytes qui vont renfermer les trames pour l'intéraction avec le serveur
        byte[] bTrame;
        byte[] bTamponReception = new byte[516];

        // Instantiation du formulaire pour y envoyer le statut du client
        frmClient f;

        // On appelle l'objet frmClient pour envoyer le statut du client
        public Download(frmClient myForm)
        {
            f = myForm;
        }

        // Fichier local et distant
        string lFileDownload, rFileDownload;
        FileStream fs;

        public void SetPointDistant(IPAddress IP)
        {
            pointDistantDownload = new IPEndPoint(IP, 69);
            sDownload.Bind(pointLocalDownload);
            sDownload.ReceiveTimeout = 15000;
        }

        public void SetFichier(string local, string remote)
        {
            lFileDownload = local;
            rFileDownload = remote;
        }

        public void DownloadFile()
        {
            // Définition des variables pour la méthode Download
            int bLen;
            byte[] file = new byte[516];

            // Encoder la première trame
            initDownloadEncoding();

            // Ouverture du fichier avec Filestream
            fs = new FileStream(lFileDownload, FileMode.Create, FileAccess.Write, FileShare.Read);

            // Le point local prend la définition du pointDistant
            pointLocalDownload = pointDistantDownload;

            // Envoyer de la demande de download
            sendToServer();

            // Réception de l'accord pour download
            try
            {
                sDownload.ReceiveFrom(bTamponReception, ref pointLocalDownload);
            }
            catch(Exception se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception : ", se.Message) });
            }

            // Même port pour le serveur que pour le client
            changePort(pointLocalDownload.ToString(), pointDistantDownload.ToString());

            // Transfert de blocks

        }

        #region Méthodes pour communiquer avec le serveur
        private void initDownloadEncoding()
        {
            // Transformation du mode et du fichier en bytes
            byte[] bFile = Encoding.ASCII.GetBytes(rFileDownload);
            byte[] bMode = Encoding.ASCII.GetBytes("octet");

            bTrame = new byte[4 + bFile.Length + bMode.Length];

            bTrame[0] = 0;
            bTrame[1] = 1; // Write request
            Array.Copy(bFile, 0, bTrame, 2, bFile.Length); // Ajout du fichier
            bTrame[bFile.Length + 2] = 0;
            Array.Copy(bMode, 0, bTrame, bFile.Length + 3, bMode.Length); // Ajout du mode
            bTrame[bTrame.Length - 1] = 0;
            f.Invoke(f.ServerStatus, new object[] { "La trame initiale de téléchargement est prête à être envoyée." });
        }


        // Méthode pour envoyer une trame au serveur au début du transfert
        private void sendToServer()
        {
            // Envoi de la demande de download
            try
            {
                sDownload.SendTo(bTrame, bTrame.Length, SocketFlags.None, pointDistantDownload);
            }
            catch (SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la transmission : ", se.Message) });
                return;
            }
        }

        private void sendAck()
        {

        }

        private void changePort(string localEndPoint, string remoteEndPoint)
        {
            // Prendre l'IP et le port du local
            string[] local;
            local = localEndPoint.Split('.');
            int Port = Int32.Parse(local[1]);
            //Prendre l'IP et le port du distant
            string[] distant;
            distant = remoteEndPoint.Split('.');
            IPAddress IP = IPAddress.Parse(distant[0]);
            // Changement du point distant avec le port du point local
            pointDistantDownload = new IPEndPoint(IP, Port);
        }
        #endregion
    }
}
