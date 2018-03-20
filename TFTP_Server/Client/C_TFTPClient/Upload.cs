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

        // Chaîne de caractères renfermant le code d'erreur
        string errorMsg;

        // Socket pour l'Upload
        Socket sUpload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local
        EndPoint PointDistantUpload;
        EndPoint PointLocalUpload = new IPEndPoint(0, 0);

        // Instantiation du formulaire pour y envoyer le statut du client
        frmClient f;

        // On appelle l'objet frmClient pour envoyer le statut
        public Upload(frmClient myForm)
        {
            f = myForm;
        }

        // Variables pour le fichier à upload
        string fileToUpload, distantFileName;
        FileStream fs;

        public void ConnexionToServer(string ipServer)
        {
            // Instantiation du point distant avec le bon ip
            PointDistantUpload = new IPEndPoint(IPAddress.Parse(ipServer), 69);
            try
            {
                sUpload.Bind(PointLocalUpload);
                sUpload.ReceiveTimeout = 15000;
                f.Invoke(f.ServerStatus, new object[] { "La connexion au serveur est établie." });
            }
            catch(SocketException se)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("La connexion au serveur n'a pas marchée : {0}", se.Message) });
            }
        }

        //public void SetPointDistant(EndPoint pointDistant)
        //{
        //    PointDistantUpload = pointDistant;
        //}

        public void SetFichier(string localFileName, string RemoteFileName)
        {
            fileToUpload = string.Format("{0}", localFileName);
            distantFileName = RemoteFileName;

        }

        public void UploadThread()
        {
            // Définition des variables pour le thread
            byte[] bTrame = new byte[516];
            bool bRead;
            byte[] bTamponReception = new byte[516];
            int nRead = 0, nTimeOut = 0, nAckError = 0, nBlock = 0;
            FileStream fs;

            TrameUploadEncoding(out bTrame);

            // Ajouter une validation éventuellement
            fs = new FileStream(fileToUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            // Envoi de la requête
            sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointDistantUpload);
            // Réception ensuite
            nRead = sUpload.ReceiveFrom(bTamponReception,SocketFlags.None, ref PointLocalUpload);
            bTrame = new byte[516];
            do
            {
                if (bTamponReception[1] == 4 && (((bTamponReception[2] << 8) % 256) | bTamponReception[3]) == nBlock)
                {
                    bTrame[0] = 0;
                    bTrame[1] = 3;
                    bTrame[2] = (byte)(nBlock >> 8);
                    bTrame[3] = (byte)(nBlock % 256);
                    for (int i = 4; i < fs.Length + 4; i++)
                    {
                        bTrame[i] = (byte)fs.ReadByte();
                    }
                    nRead = sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointLocalUpload);
                    nBlock++;
                }
                sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
            } while (bTamponReception[1] != 5 && nRead == 516);

            sUpload.Close();
            fs.Close();


        }

        public bool TrameUploadEncoding(out byte[] Trame)
        {
            // On crée notre propre trame à envoyer au serveur 
            byte[] fileToBytes = Encoding.ASCII.GetBytes(distantFileName);
            byte[] modeToBytes = Encoding.ASCII.GetBytes("octet");

            // Création de la trame
            Trame = new byte[4 + fileToBytes.Length + modeToBytes.Length];
            Trame[0] = 0;
            Trame[1] = 2; // WRQ
            Array.Copy(fileToBytes, 0, Trame, 2, fileToBytes.Length); // File Conversion
            Trame[fileToBytes.Length + 2] = 0;
            Array.Copy(modeToBytes, 0, Trame, fileToBytes.Length + 3, modeToBytes.Length); // Mode Conversion
            Trame[Trame.Length - 1] = 0;
            return true;
        }
    }
}
