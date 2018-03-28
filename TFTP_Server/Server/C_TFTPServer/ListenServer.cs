using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace Server.C_TFTP
{
    /// <summary>
    /// Classe qui va écouter en permanence si des trames TFTP lui sont envoyées.
    /// - Serveur d'écoute TFTP -
    /// </summary>
    class ListenServer : ErrorType
    {
        // Variable de fin de la thread
        public bool m_fin;
        private Socket socket;
        frmServer f;

        // On appelle l'objet frmServer pour envoyer un statut au formulaire
        public ListenServer(frmServer myForm)
        {
            f = myForm;
        }

        public void ListenThread()
        {
            // Création de la chainequi va renfermer le nom du fichier a remplir
            string fileName, mode = null;

            // Création du point local et du point distant
            EndPoint LocalPoint = new IPEndPoint(0, 69);
            EndPoint DistantPoint = new IPEndPoint(0, 0);

            // Création du tableau de byte qui va renfermer la réception
            byte[] bTamponReception = new byte[516];
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
                        socket.ReceiveFrom(bTamponReception, ref DistantPoint);

                        switch (TrameValidation(bTamponReception))
                        {
                            case 1: // Read request
                                f.Invoke(f.ServerStatus, new object[] {"Une demande de lecture a été créée"});
                                fileName = null;
                                // Savoir le fichier a transporter
                                for (int i = 2; bTamponReception[i] != 0; i++)
                                {
                                    fileName += (char)bTamponReception[i];
                                }
                                indice++;

                                // Savoir le mode de transmission du fichier
                                while(bTamponReception[indice] != 0)
                                {
                                    mode += (char)bTamponReception[indice];
                                    indice++;
                                }

                                // Instanciation de la classe RRQ
                                RRQ rrQ = new RRQ(f);
                                rrQ.SetFichier(fileName);
                                rrQ.SetPointDistant(DistantPoint);
                                f.Invoke(f.ServerStatus, new object[] { string.Format("Transfert du fichier {0} vers {1}", fileName, DistantPoint.ToString()) });
                                Thread threadRRQ = new Thread(new ThreadStart(rrQ.RRQThread));
                                threadRRQ.IsBackground = true;
                                threadRRQ.Start();
                                break;

                            case 2: // Write Request
                                f.Invoke(f.ServerStatus, new object[] { "Une demande d'écriture a été créée" });
                                fileName = null;
                                // Savoir le fichier à transporter
                                for (int i = 2; bTamponReception[i] != 0; i++)
                                {
                                    fileName += (char)bTamponReception[i];
                                }
                                indice++;

                                // Savoir le mode de transmission du fichier
                                while(bTamponReception[indice] !=0)
                                {
                                    mode += (char)bTamponReception[indice];
                                    indice++;
                                }
                                WRQ wrQ = new WRQ(f);
                                wrQ.SetFichier(fileName);
                                wrQ.SetPointDistant(DistantPoint);
                                f.Invoke(f.ServerStatus, new object[] { string.Format("Transfert du fichier {0} venant de {1}", fileName, DistantPoint.ToString()) });
                                Thread threadWRQ = new Thread(new ThreadStart(wrQ.WRQThread));
                                threadWRQ.Start();
                                threadWRQ.IsBackground = true;
                                break;
                            case 0: // Ni RRQ, ni WRQ donc erreur
                                // Operation TFTP illégale
                                f.Invoke(f.ServerStatus, new object[] { "Une demande illégale a été rencontrée." });
                                DetectionTypeErreur(socket, DistantPoint, 4);
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
