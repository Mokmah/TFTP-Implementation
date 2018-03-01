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
            // Création du point local et du point distant
            EndPoint LocalPoint = new IPEndPoint(0, 69);
            EndPoint DistantPoint = new IPEndPoint(0, 0);

            // Écoute du serveur
            try
            {
                // Instanciation du socket
                socket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(LocalPoint);

                while (!m_fin)
                {
                    if (socket.Available > 0)
                    {

                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Problème avec le Socket",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
