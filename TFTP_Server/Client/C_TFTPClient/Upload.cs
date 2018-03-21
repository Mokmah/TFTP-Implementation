using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.C_TFTPClient
{
    class Upload : ErrorType
    {
        // Définition des variables *****

        // Socket pour l'Upload
        Socket sUpload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local
        EndPoint PointDistantUpload;
        EndPoint PointLocalUpload = new IPEndPoint(0, 69);

        // Tableau de bytes qui va renfermer les trames pour l'intéraction avec le serveur
        byte[] bTrame = new byte[516];
        byte[] bTamponReception = new byte[516];

        // Instantiation du formulaire pour y envoyer le statut du client
        frmClient f;

        // On appelle l'objet frmClient pour envoyer le statut
        public Upload(frmClient myForm)
        {
            f = myForm;
        }

        // Fichier local et distant
        string lFileUpload, rFileUpload;
        FileStream fs;

        public void SetPointDistant(IPAddress IP)
        {
            PointDistantUpload = new IPEndPoint(IP, 69);
            sUpload.Bind(PointLocalUpload);
        }

        public void SetFichier(string local, string remote)
        {
            lFileUpload = local;
            rFileUpload = remote;
        }

        public void uploadThread()
        {
            // Définition des variables du thread
            bool bRead;
            int nRead = 0, nTimeOut = 0, nAckError = 0, nBlock = 0;

            if (File.Exists(lFileUpload)) // Vérification de l'existence du fichier local avant de le lire.
            {
                initUploadEncoding();
                // Envoi de la demande d'Upload
                sendToServer();
                // Réception de l'accord pour Upload
                receiveFomServer();
                // S'assurer de la réception d'un ACK
                if (bTamponReception[1] == 4)
                {
                    f.Invoke(f.ServerStatus, new object[] { string.Format("Nous avons l'accord du serveur pour faire un upload")});
                }

                // Ouverture du fichier avec un filestream
                fs = File.Open(lFileUpload, FileMode.Open, FileAccess.Read, FileShare.Read);
                f.Invoke(f.ServerStatus, new object[] { string.Format("Ouverture du fichier local à lire.") });

                do
                {
                    // Encoder la trame contenant le fichier à transmettre
                    nRead = fs.Read(bTrame, 4, 512);
                    nBlock++;
                    bTrame[0] = 0;
                    bTrame[1] = 3;
                    bTrame[2] = (byte)(nBlock >> 8);
                    bTrame[3] = (byte)(nBlock % 256);
                    do
                    {
                        sUpload.SendTo(bTrame, 4 + nRead, SocketFlags.None, PointDistantUpload);
                        // Attendre la réception de la trame de la part du serveur et mettre des timeout si c'est trop long 
                        if (!(bRead = sUpload.Poll(5000000, SelectMode.SelectRead)))
                        {
                            nTimeOut++;
                            f.Invoke(f.ServerStatus, new object[] { "Attente du client, le pare feu est-il désactivé des deux côtés?" });
                        }
                        else
                        {
                            nTimeOut = 0;
                            // Réception du Ack de la part du serveur pour ce qui est du contenu du fichier
                            sUpload.ReceiveFrom(bTamponReception, ref PointDistantUpload);
                            // Vérification d'une erreur de transfert de bloc
                            if (!(bTamponReception[0] == 0 && bTamponReception[1] == 4))
                            {
                                nAckError++;
                            }
                            // S'il n'y en a pas, ajouter le nombre de bloc à la trame dans le cas d'un réenvoi
                            else
                            {
                                nAckError = 0;
                                nBlock = bTamponReception[2] << 8;
                                nBlock += bTamponReception[3];
                            }
                        }
                    } while (!bRead && nTimeOut < 10 && nAckError < 3);
                } while (nRead == 512 && nTimeOut < 10 && nAckError < 3);
            }
            else // Quand le fichier local n'existe pas 
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Le fichier local n'existe pas : ", lFileUpload) });
            }
            f.Invoke(f.ServerStatus, new object[] { "Le transfert s'est effectué avec succès ! \r\n" });
            fs.Close();
            sUpload.Close();
        }
        #region Méthodes pour intéragir avec le serveur
        // Méthode pour envoyer une trame au serveur
        private void sendToServer()
        {
            // Envoi de la demande d'Upload
            try
            {
                sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointDistantUpload);
            }
            catch (SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la transmission : ", se.Message) });
            }
        }
        // Méthode pour recevoir une trame de la part du serveur
        private void receiveFomServer()
        {
            try
            {
                sUpload.ReceiveFrom(bTamponReception, ref PointDistantUpload);
            }
            catch (SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception : ", se.Message) });
            }
        }
        // Encoder la première trame à envoyer au serveur pour initialiser le téléversement
        private void initUploadEncoding()
        {
            // Transformation du mode et du fichier en bytes
            byte[] bFile = Encoding.ASCII.GetBytes(rFileUpload);
            byte[] bMode = Encoding.ASCII.GetBytes("octet");

            bTrame[0] = 0;
            bTrame[1] = 2; // Write request
            Array.Copy(bFile, 0, bTrame, 2, bFile.Length); // Ajout du fichier
            bTrame[bFile.Length + 2] = 0;
            Array.Copy(bMode, 0, bTrame, bFile.Length + 3, bMode.Length); // Ajout du mode
            bTrame[bTrame.Length - 1] = 0;
            f.Invoke(f.ServerStatus, new object[] { "La trame initiale est prête à être envoyée." });
        }
        #endregion
    }
}
