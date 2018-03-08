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

        }
    }
}
