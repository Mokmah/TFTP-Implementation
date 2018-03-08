using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server.C_TFTP
{
    class ListenServer : ErrorType
    {
        // Variable de fin de la thread
        public bool m_fin;
        private Socket socket;

        public void ListenThread()
        {
            // Création de la chainequi va renfermer le nom du fichier a remplir
            string fileName, mode = null;
            // Création du point local et du point distant
            EndPoint LocalPoint = new IPEndPoint(0, 69);
            EndPoint DistantPoint = new IPEndPoint(0, 0);
            // Création du tableau de byte qui va renfermer la réception
            byte[] bReception = new byte[516];
            int indice = 0;

            // Écoute du serveur
            try
            {
                // Instanciation du socket
                socket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(LocalPoint);

                //Boucle du thread
                while (!m_fin)
                {
                    if (socket.Available > 0)
                    {
                        socket.ReceiveFrom(bReception, ref DistantPoint);

                        switch (TrameValidation(bReception))
                        {
                            case 1: // Read request
                                fileName = null;
                                // Savoir le fichier a transporter
                                for (int i = 2; bReception[i] != 0; i++)
                                {
                                    fileName += (char)bReception[i];
                                }
                                indice++;

                                // Savoir le mode de transmission du fichier
                                while(bReception[indice] != 0)
                                {
                                    mode += (char)bReception[indice];
                                    indice++;
                                }

                                // Instanciation de la classe RRQ
                                RRQ rrQ = new RRQ();
                                rrQ.SetFichier(fileName);
                                rrQ.SetPointDistant(DistantPoint);
                                Thread threadRRQ = new Thread(new ThreadStart(rrQ.RRQThread));
                                threadRRQ.Start();

                                break;

                            case 2: // Write Request
                                fileName = null;

                                break;
                            case -1: // Ni RRQ, ni WRQ donc erreur
                                break;
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Problème avec le Socket",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        // Méthode pour valider la trame reçue
        private int TrameValidation(byte[] bTrame)
        {
            if (bTrame[0] == 0 && bTrame[1] == 1)
            {
                return 1;
            }
            else
            {
                if (bTrame[0] == 0 && bTrame[1] == 2)
                {
                    return 2;
                }
            }

            return 0;
        }
    }
}
