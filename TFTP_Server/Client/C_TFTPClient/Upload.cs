using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
            int nBlock = 0, nLength = 516;
            BinaryReader br;
            byte[] bTrame, bTamponReception = new byte[516];

            TrameUploadEncoding(out bTrame);

            // Entrer dans le fichier pour lire son contenu et l'envoyer
            try
            {
                br = new BinaryReader(new FileStream(fileToUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }
            catch (Exception e)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu un problème dans l'ouverture du fichier local {0} ", fileToUpload) + e.Message });
                return;
            }

            sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointDistantUpload);

            // Réception de la part du serveur
            try
            {
                sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
            }
            catch (Exception e)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu un problème dans la réception de la demande au serveur {0}", PointDistantUpload.ToString()) });
                return;
            }

            while (bTamponReception[1] != 5 && nLength == 516)
            {
                if (bTamponReception[1] != 4 && (((bTamponReception[2] << 8) % 256 | bTamponReception[3]) == nBlock))
                {
                    br.ReadBytes(512);
                    TrameUploadEncoding(out bTrame);

                    try
                    {
                        nLength = sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointDistantUpload);
                    }
                    catch(Exception e)
                    {
                        f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu un problème dans l'envoi d'un ack au serveur {0} ", PointDistantUpload.ToString()) + e.Message });
                        return;
                    }
                }
                try
                {
                    sUpload.ReceiveFrom(bTamponReception, ref PointLocalUpload);
                }
                catch (Exception e)
                {
                    f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu un problème dans la réception des données du serveur {0}", PointDistantUpload.ToString()) });
                    return;
                }
            }

            sUpload.Close();
            br.Close();

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
