using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server.C_TFTP
{
    class RRQ : ErrorType
    {
        Socket sRRQ = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        EndPoint PointDistantRRQ = new IPEndPoint(0, 0);
        EndPoint PointLocalRRQ = new IPEndPoint(0, 69);
        string fileRRQ;
        FileStream fs;

        public void SetPointDistant(EndPoint PointDistant)
        {
            PointDistantRRQ = PointDistant;
        }

        public void SetFichier(string fileName)
        {
            fileRRQ = fileName;
        }

        public void RRQThread()
        {
            // Description des variables du thread
            byte[] tTrame = new byte[516];
            bool bRead;
            byte[] Reception = new byte[516];
            int nRead = 0, nTimeOut = 0, nAckError = 0, nBlock = 0;
            if (File.Exists(fileRRQ))
            {
                // Ouverture du fichier avec un filestream
                fs = File.Open(fileRRQ, FileMode.Open, FileAccess.Read, FileShare.Read);
                do
                {
                    nRead = fs.Read(tTrame, 4, 512);
                    nBlock++;
                    tTrame[0] = 0;
                    tTrame[1] = 3;
                    tTrame[2] = (byte)(nBlock >> 8);
                    tTrame[3] = (byte)(nBlock % 256);
                    do
                    {
                        sRRQ.SendTo(tTrame, 4 + nRead, SocketFlags.None, PointDistantRRQ);
                        if (!(bRead = sRRQ.Poll(5000000, SelectMode.SelectRead)))
                        {
                            nTimeOut++;
                            sRRQ.SendTo(tTrame, 4 + nRead, SocketFlags.None, PointDistantRRQ);
                        }
                        else
                        {
                            nTimeOut = 0;
                            sRRQ.ReceiveFrom(Reception, ref PointDistantRRQ);
                            // Verification dans une erreur de transfert de bloc
                            if (!(Reception[0] == 0 && Reception[1] == 4))
                            {
                                nAckError++;
                            }
                            else
                            {
                                nAckError = 0;
                                nBlock = (((int)Reception[2]) << 8);
                                nBlock += ((int)Reception[3]);
                            }
                        }
                    }
                    while (!bRead && nTimeOut < 10 && nAckError < 3);
                }
                while (nRead == 512 && nTimeOut < 10 && nAckError < 3);
                // Detection du type d'erreur
                if (nAckError == 3)
                {
                    DetectionTypeErreur(sRRQ, PointDistantRRQ, 5);
                }
                fs.Close();
            }
            else // Si le fichier n'existe pas
            {
                DetectionTypeErreur(sRRQ, PointDistantRRQ, 1);
            }
        }
    }
}
   
