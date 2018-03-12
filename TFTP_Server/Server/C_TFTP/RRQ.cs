using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server.C_TFTP
{
    class RRQ : ErrorType
    {
        // Définition des variables***

        // Chaîne de caractères renfermant le message d'erreur
        string errorMsg;
        // Instantiation du formulaire pour envoyer le statut du serveur
        frmServer f;

        // On appelle l'objet frmServer pour envoyer un statut
        public RRQ(frmServer myForm)
        {
            f = myForm;
        }

        // Instantiation du socket RRQ
        Socket sRRQ = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //Coordonnées pour le point distant et le point local
        EndPoint PointDistantRRQ = new IPEndPoint(0, 0);
        EndPoint PointLocalRRQ = new IPEndPoint(0, 69);

        //Accès au fichier RRQ
        string fileRRQ;
        FileStream fs;


        public void SetPointDistant(EndPoint PointDistant)
        {
            PointDistantRRQ = PointDistant;
        }

        public void SetFichier(string fileName)
        {
            fileRRQ = "C:\\TFTP\\" + fileName;
        }

        public void RRQThread()
        {
            // Description des variables du thread
            byte[] bTrame = new byte[516];
            bool bRead;
            byte[] bTamponReception = new byte[516];
            int nRead = 0, nTimeOut = 0, nAckError = 0, nBlock = 0;
            if (File.Exists(fileRRQ))
            {
                // Ouverture du fichier avec un filestream
                fs = File.Open(fileRRQ, FileMode.Open, FileAccess.Read, FileShare.Read);
                do
                {
                    nRead = fs.Read(bTrame, 4, 512);
                    nBlock++;
                    bTrame[0] = 0;
                    bTrame[1] = 3;
                    bTrame[2] = (byte)(nBlock >> 8);
                    bTrame[3] = (byte)(nBlock % 256);
                    do
                    {
                        sRRQ.SendTo(bTrame, 4 + nRead, SocketFlags.None, PointDistantRRQ);
                        if (!(bRead = sRRQ.Poll(5000000, SelectMode.SelectRead)))
                        {
                            nTimeOut++;
                            f.Invoke(f.ServerStatus, new object[] { "Attente du client" });
                        }
                        else
                        {
                            nTimeOut = 0;
                            sRRQ.ReceiveFrom(bTamponReception, ref PointDistantRRQ);
                            // Verification dans une erreur de transfert de bloc
                            if (!(bTamponReception[0] == 0 && bTamponReception[1] == 4))
                            {
                                nAckError++;
                            }
                            else
                            {
                                nAckError = 0;
                                nBlock = bTamponReception[2] << 8;
                                nBlock += bTamponReception[3];
                            }
                        }
                    }
                    while (!bRead && nTimeOut < 10 && nAckError < 3);
                }
                while (nRead == 512 && nTimeOut < 10 && nAckError < 3);
                // Detection du type d'erreur
                if (nAckError == 3)
                {
                    errorMsg = DetectionTypeErreur(sRRQ, PointDistantRRQ, 5);
                    f.Invoke(f.ServerStatus, new object[] { errorMsg });
                }
                fs.Close();
                f.Invoke(f.ServerStatus, new object[] { string.Format("Total de blocs transférés : {0} envoyés à {1}", nBlock, PointDistantRRQ.ToString()) });
                f.Invoke(f.ServerStatus, new object[] { "Le transfert s'est effectué avec succès.\r\n" });
            }
            else // Si le fichier n'existe pas
            {
                errorMsg = DetectionTypeErreur(sRRQ, PointDistantRRQ, 1);
                f.Invoke(f.ServerStatus, new object[] { errorMsg });
            }
        }
    }
}
   
