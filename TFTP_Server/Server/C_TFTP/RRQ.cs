using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
   
