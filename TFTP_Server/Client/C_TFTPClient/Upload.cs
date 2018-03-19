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
        EndPoint PointLocalUpload = new IPEndPoint(0, 69);

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
            PointDistantUpload = new IPEndPoint(IPAddress.Parse(ipServer), 0);
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
            fileToUpload = string.Format("@{0}", localFileName);
            distantFileName = RemoteFileName;

        }

        public void UploadThread()
        {
            // Définition des variables pour le thread
            int nBlock = 0, nLength = 516;
            BinaryReader br;
            IPEndPoint server;
            byte[] bTrame, bTamponReception = new byte[516];

            // Entrer dans le fichier pour lire son contenu et l'envoyer
            try
            {
                br = new BinaryReader(new FileStream(fileToUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            }
            catch (Exception e)
            {
                f.Invoke(f.ServerStatus, new object[] { string.Format("Il y a eu un problème dans l'ouverture du fichier local {0}", fileToUpload) });
                return;
            }

            sUpload.SendTo(bTrame, bTrame.Length, SocketFlags.None, PointDistantUpload);

        }

        public bool TrameUploadEncoding(out byte[] Data)

        {

            byte[] FileBytes = Encoding.ASCII.GetBytes(distantFileName);
            byte[] ModeBytes = Encoding.ASCII.GetBytes("octet");

            Data = new byte[4 + FileBytes.Length + ModeBytes.Length];
            Data[0] = 0;
            Data[1] = (byte)Type;
            Array.Copy(FileBytes, 0, Data, 2, FileBytes.Length);
            Data[FileBytes.Length + 2] = 0;
            Array.Copy(ModeBytes, 0, Data, FileBytes.Length + 3, ModeBytes.Length);
            Data[Data.Length - 1] = 0;

            return true;



        }

    }
}
