using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Server.C_TFTP
{
    class WRQ : ErrorType
    {
        // Définition des variables *****

        // Chaîne de caractères renfermant le code d'erreur
        string errorMsg;

        // Socket WRQ
        Socket sWRQ = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local et distant
        EndPoint PointDistantWRQ = new IPEndPoint(0, 0);
        EndPoint PointLocalWRQ = new IPEndPoint(0, 69);

        // Instantiation du formulaire pour envoyer le statut du serveur
        frmServer f;

        // On appelle l'objet frmServer pour envoyer un statut
        public WRQ(frmServer myForm)
        {
            f = myForm;
        }

        // Variables pour le fichier WRQ
        string fileWRQ;
        FileStream fs;

        public void SetPointDistant(EndPoint PointDistant)
        {
            PointDistantWRQ = PointDistant;
        }

        public void SetFichier(string fileName)
        {
            fileWRQ = "C:\\TFTP\\" + fileName;
        }

        public void WRQThread()
        {
            // Définition des variables pour le thread
            byte[] bTrame = new byte[4] { 0, 4, 0, 0 }; // Pour envoyer un premier ack et commencer le transfert
            bool bRead;
            byte[] bTamponReception = new byte[516];
            int indice = 0, nRead = 0, nTimeOut = 0, nBlockError = 0, nBlock = 1;
            if (!(File.Exists(fileWRQ)))
            {
                // Ouverture du fichier avec filestream
                fs = File.Open(fileWRQ, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                // Envoyer un ack pour commencer le transfert
                sWRQ.SendTo(bTrame, 4, SocketFlags.None, PointDistantWRQ);
                f.Invoke(f.ServerStatus, new object[] { "On envoit un premier ACK au client pour faire la demande" });
                do
                {
                    SendAck(bTrame);
                    if (!(bRead = sWRQ.Poll(5000000, SelectMode.SelectRead)))
                    {
                        nTimeOut++;
                        f.Invoke(f.ServerStatus, new object[] { "Attente du client, le pare feu est-il désactivé des deux côtés?" });
                    }
                    else
                    {
                        nTimeOut = 0;
                        // Recevoir les informations des blocs
                        //sWRQ.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
                        nRead = sWRQ.ReceiveFrom(bTamponReception, ref PointDistantWRQ);
                        if (!(bTamponReception[0] == 0 && bTamponReception[1] == 3 && bTamponReception[2] == (byte)(nBlock >> 8) && bTamponReception[3] == (byte)(nBlock % 256)))
                        {
                            nBlockError++;
                            f.Invoke(f.ServerStatus, new object[] { "Une erreur de block a été rencontrée."});
                            // Verification de nblock requise
                        }
                        else
                        {
                            nBlockError = 0;
                            // Écrire les octets dans le fichier à retranscrire
                            for (indice = 4; indice < nRead; indice++)
                            {
                                fs.WriteByte(bTamponReception[indice]);
                            }
                            // Envoi d'un ack
                            bTrame[2] = (byte)(nBlock >> 8);
                            bTrame[3] = (byte)(nBlock % 256);
                            SendAck(bTrame);
                            nBlock++;
                        }
                    }
                }
                while (nRead == 516 && nTimeOut < 10 && nBlockError < 3);
                f.Invoke(f.ServerStatus, new object[] { string.Format("Total de block transférés : {0}, dans le fichier {1}", nBlock, fileWRQ) });
                // Réagir à une erreur dans un transfert de bloc
                if (nBlockError == 3)
                {
                    // Envoi de l'erreur pour lier le bon type avec le code d'erreur
                    errorMsg = DetectionTypeErreur(sWRQ, PointDistantWRQ, 5);
                    f.Invoke(f.ServerStatus, new object[] { errorMsg });
                }
                fs.Close();
                f.Invoke(f.ServerStatus, new object[] { "Le transfert s'est effectué avec succès !\r\n" });
            }
            else
            {
                errorMsg = DetectionTypeErreur(sWRQ, PointDistantWRQ, 6);
                f.Invoke(f.ServerStatus, new object[] { errorMsg });
            }
        }

        // Méthode d'envoi de ACK au client
        private void SendAck(byte[] bTrame)
        {
            sWRQ.SendTo(bTrame, 4, SocketFlags.None, PointDistantWRQ);
        }
    }
}
