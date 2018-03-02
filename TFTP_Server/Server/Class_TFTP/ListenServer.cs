using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class ListenServer
    {
        // Variable de fin de la thread
        public bool m_fin;
        private Socket socket;

        public void ListenThread()
        {
            // Création de la chainequi va renfermer le nom du fichier a remplir
            string fileName;
            // Création du point local et du point distant
            EndPoint LocalPoint = new IPEndPoint(0, 69);
            EndPoint DistantPoint = new IPEndPoint(0, 0);
            // Création du tableau de byte qui va renfermer la réception
            byte[] bReception = new byte[516];

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
                                for (int i = 2; bReception[i] != (byte)0; i++)
                                {
                                    fileName += (char)bReception[i];
                                }
                                break; // La methode n'est pas complete
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
            return 0;
        }
    }
}
