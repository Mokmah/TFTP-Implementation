using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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

        // Variable du numéro de packet pour intéraction avec le serveur
        int nBlock = 1;

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
            sDownload.Bind(new IPEndPoint(0,0));
            sDownload.ReceiveTimeout = 15000;
        }

        public void SetFichier(string local, string remote)
        {
            lFileDownload = "C:\\TFTP\\" + local;
            rFileDownload = remote;
        }

        public void DownloadFile()
        {
            // Définition des variables pour la méthode Download
            int bLen  = 516;
            byte[] file = new byte[516];

            // Encoder la première trame
            InitDownloadEncoding();

            // Ouverture du fichier avec Filestream
            try
            {
                fs = new FileStream(lFileDownload, FileMode.Create, FileAccess.Write, FileShare.Read);
            }
            catch(Exception e)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la création du fichier {0}", lFileDownload) });
                return;
            }
            f.Invoke(f.ServerStatus, new object[] { string.Format("Ouverture du fichier à lire {0}", lFileDownload) });

            // Envoyer de la demande de download
            SendToServer();

            // Réception de l'accord pour download
            try
            {
                bLen = sDownload.ReceiveFrom(bTamponReception, ref pointDistantDownload);
                
            }
            catch(Exception se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception", se.Message.ToString()) });
                return;
            }

            f.Invoke(f.ServerStatus, new object[] { "Début du transfert..." });

            // Est-ce qu'il faut changer de port et interchanger le point local du point distant?

            // Transfert de blocks
            while (bTamponReception[1] != 5 && bLen == 516) 
            {
                // On reçoit le block suivant
                if ((((bTamponReception[2] << 8) & 0xff00) | bTamponReception[3]) == nBlock)
                {
                    // Écrire les données reçues dans le fichier correspondant
                    fs.Write(bTamponReception, 4, bLen - 4);

                    // Envoi d'un ack au serveur pour confirmer la réception
                    SendDataAck();
                    nBlock++;
                }

                // On attend de voir si c'est le dernier block
                if (bLen == 516) 
                {
                    // Réception du prochain packet de données
                    try
                    {
                        bLen = sDownload.ReceiveFrom(bTamponReception, ref pointDistantDownload);
                    }
                    catch (Exception e)
                    {
                        f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception du block de données ", e.Message) });
                        return;
                    }
                    // On envoie un autre Ack si c'est le dernier block
                }
            }

                // S'il y a une erreur, récupérer le type et l'afficher
                if (bTamponReception[1] == 5)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors du transfert {0}", Encoding.GetEncoding(437).GetString(bTamponReception, 4, bTamponReception.Length - 5).Trim('\0')) });
                return;
            }
            // Envoi du dernier ACK pour terminer le transfert
            SendDataAck();
            // Fermer le socket et le fichier pour terminer le transfert
            f.Invoke(f.ServerStatus, new object[] { string.Format("Total de blocs transférés : {0} reçus de {1}", nBlock, pointDistantDownload.ToString()) });
            f.Invoke(f.ServerStatus, new object[] { "Le transfert s'est terminé avec succès ! \r\n" });
            sDownload.Close();
            fs.Close();

        }

        #region Méthodes pour communiquer avec le serveur
        private void InitDownloadEncoding()
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
        private void SendToServer()
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

        private void SendDataAck()
        {
            // Encoder la trame pour le ACK
            bTrame = new byte[4];
            bTrame[0] = 0;
            bTrame[1] = 4;
            bTrame[2] = (byte)(nBlock >> 8); // Numéro  de block correspondant au bloc actuel
            bTrame[3] = (byte)(nBlock % 256);
            // Envoi de la trame au serveur
            try
            {
                sDownload.SendTo(bTrame, bTrame.Length, SocketFlags.None, pointDistantDownload);
            }
            catch(Exception e)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la transmission du ACK ", e.Message) });
                return;
            }
        }
        #endregion
    }
}
