using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.C_TFTPClient
{
    /// <summary>
    /// Classe du client TFTP qui servira à écrire des fichiers sur le poste sur lequel est situé le serveur
    /// - Téléversement de fichiers -
    /// </summary>
    class Upload
    {
        // Définition des variables *****

        // Socket pour l'Upload
        Socket sUpload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local et distant
        EndPoint PointDistantUpload;
        EndPoint PointLocalUpload = new IPEndPoint(0, 0);

        // Tableau de bytes qui va renfermer les trames pour l'intéraction avec le serveur
        byte[] bTrame;
        byte[] bTamponReception = new byte[516];
        int nBlock = 0, nRead = 0;

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
            sUpload.ReceiveTimeout = 15000;
        }

        public void SetFichier(string local, string remote)
        {
            lFileUpload = local;
            rFileUpload = remote;
        }

        public void UploadFile()
        {
            // Définition des variables du thread
            int bLen = 516;
            byte[] file = new byte[516];

            if (File.Exists(lFileUpload)) // Vérification de l'existence du fichier local avant de le lire.
            {
                // Encoder la trame initiale pour l'Upload
                initUploadEncoding();
                // Envoi de la demande d'Upload
                sendToServer();

                // Réception de l'accord pour Upload
                try
                {
                    sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
                }
                catch (Exception se)
                {
                    f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception : ", se.Message) });
                }

                // Ouverture du fichier avec un filestream
                fs = File.Open(lFileUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                
                f.Invoke(f.ServerStatus, new object[] { string.Format("Ouverture du fichier à lire : {0}", lFileUpload) });
                f.Invoke(f.ServerStatus, new object[] { "Début du transfert..." });
                // Boucle de thread de transfert de blocks vers le serveur
                while (bTamponReception[1] != 5 && bLen == 516)
                {
                    if (bTamponReception[1] == 4 && (((bTamponReception[2] << 8) & 0xff00) | bTamponReception[3]) == nBlock)
                    {
                        file = new byte[512];
                        nRead = 0;
                        // Encoder la trame contenant le fichier à transmettre
                        nRead = fs.Read(file, 0, 512);
                        nBlock++;
                        dataEncoding(nBlock, file);

                        //Envoi de la trame avec le contenu du fichier
                        try
                        {
                            bLen = sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointLocalUpload);
                        }
                        catch (SocketException se)
                        {
                            f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la transmission : ", se.Message) });
                            return;
                        }
                    }
                    // Réception de la trame avec le bon # de bloc correspondant
                    try
                    {
                        sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
                    }
                    catch (SocketException se)
                    {
                        f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception : ", se.Message) });
                        return;
                    }
                }
                f.Invoke(f.ServerStatus, new object[] { string.Format("Total de blocs transférés : {0} envoyés à {1}", nBlock, PointDistantUpload.ToString()) });
                f.Invoke(f.ServerStatus, new object[] { "Le transfert s'est terminé avec succès ! \r\n" });
            }
            else // Quand le fichier local n'existe pas 
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Le fichier local n'existe pas : ", lFileUpload) });
                return;
            }
            fs.Close();
            sUpload.Close();
        }
        #region Méthodes pour intéragir avec le serveur
        // Méthode pour envoyer une trame au serveur au début du transfert
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
                return;
            }
        }
        // Méthode pour recevoir une trame de la part du serveur au début du transfert
        private void receiveFomServer()
        {
            try
            {
                sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
            }
            catch (SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu une erreur lors de la réception : ", se.Message) });
                return;
            }
        }
        // Encoder la première trame à envoyer au serveur pour initialiser le téléversement
        private void initUploadEncoding()
        {
            // Transformation du mode et du fichier en bytes
            byte[] bFile = Encoding.ASCII.GetBytes(rFileUpload);
            byte[] bMode = Encoding.ASCII.GetBytes("octet");

            bTrame = new byte[4 + bFile.Length + bMode.Length];

            bTrame[0] = 0;
            bTrame[1] = 2; // Write request
            Array.Copy(bFile, 0, bTrame, 2, bFile.Length); // Ajout du fichier
            bTrame[bFile.Length + 2] = 0;
            Array.Copy(bMode, 0, bTrame, bFile.Length + 3, bMode.Length); // Ajout du mode
            bTrame[bTrame.Length - 1] = 0;
            f.Invoke(f.ServerStatus, new object[] { "La trame initiale de téléversement est prête à être envoyée." });
        }
        // Encoder les trames de données qui seront envoyées au serveur
        private void dataEncoding(int nBlock, byte[] fileContent)
        {
            // Le tableau de byte sera aussi gros que le fichier + les éléments TFTP
            bTrame = new byte[4 + nRead];
            // AJouter le bon type de paquet avec le bon # de bloc  
            bTrame[0] = 0;
            bTrame[1] = 3;
            bTrame[2] = bTrame[2] = (byte)(nBlock >> 8);
            bTrame[3] = (byte)(nBlock % 256);
            Array.Copy(fileContent, 0, bTrame, 4, nRead);
        }
        #endregion
    }
}
