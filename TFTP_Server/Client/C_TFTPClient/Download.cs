using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.C_TFTPClient
{
    /// <summary>
    /// Classe du client TFTP qui servira télécharger des fichiers du poste du serveur vers le nôtre
    /// - Téléchargement de fichiers -
    /// </summary>
    class Download
    {
        // Définition des variables ***

        // Socket pour le download
        Socket sDownload = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Point local et distant
        EndPoint pointDistantDownload;
        EndPoint pointLocalDownload = new IPEndPoint(0, 0);


    }
}
