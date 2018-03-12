using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Server.C_TFTP
{
    class WRQ : ErrorType
    {
        // Définition des variables
        Socket sWRQ = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        EndPoint PointDistantWRQ = new IPEndPoint(0, 0);
        EndPoint PointLocalWRQ = new IPEndPoint(0, 69);
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
            byte[] bTrame = new byte[4] { 0, 4, 0, 0 };
            bool bRead;
            byte[] bTamponReception = new byte[516];
            int indice = 0, nRead = 0, nTimeOut = 0, nBlockError = 0, nBlock = 1;
            if (!(File.Exists(fileWRQ)))
            {
                // Ouverture du fichier avec filestream
                fs = File.Open(fileWRQ, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                // Envoyer un ack pour commencer le transfert
                sWRQ.SendTo(bTrame, 4, SocketFlags.None, PointDistantWRQ);
                do
                {
                    SendAck(bTrame);
                    if (!(bRead = sWRQ.Poll(5000000, SelectMode.SelectRead)))
                    {
                        nTimeOut++;
                        // Envoyer un ack si il n'a pas eu de reponse du client
                    }
                    else
                    {
                        nTimeOut = 0;
                        // Recevoir les informations des blocs
                        nRead = sWRQ.ReceiveFrom(bTamponReception, ref PointDistantWRQ);
                        if (!(bTamponReception[0] == 0 && bTamponReception[1] == 3 && bTamponReception[2] == (byte)(nBlock >> 8) && bTamponReception[3] == (byte)(nBlock % 256)))
                        {
                            nBlockError++;
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
                            bTrame[2] = (byte)(nBlock >> 8);
                            bTrame[3] = (byte)(nBlock % 256);
                            SendAck(bTrame);
                            nBlock++;
                        }
                    }
                }
                while (bRead && nTimeOut < 10 && nBlockError < 3);

                // Réagir à une erreur dans un transfert de bloc
                if (nBlockError == 3)
                {
                    // Envoi de l'erreur pour lier le bon type avec le code d'erreur
                    DetectionTypeErreur(sWRQ, PointDistantWRQ, 5);
                }
                fs.Close();
            }
            else
            {
                DetectionTypeErreur(sWRQ, PointDistantWRQ, 6);
            }
        }

        // Envoyer un ack au client
        private void SendAck(byte[] bTrame)
        {
            sWRQ.SendTo(bTrame, 4, SocketFlags.None, PointDistantWRQ);
        }
    }
}
