using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server.C_TFTP
{
    class ErrorType
    {
        protected void DetectionTypeErreur(Socket s, EndPoint pointDistant, int code)
        {
            // Tableaux de bytes renfermant les trames
            byte[] Trame = new byte[512];
            byte[] Ack = new byte[4] { 0, 4, 0, 0 };

            // Instanciation du formulaire pour envoyer le status de l'erreur
            frmServer f = new frmServer();

            // Message d'erreur correspondant au type d'erreur
            string MessageErreur = ((ErrorCode)code).ToString();

            // Variables d'incrémentation
            int indice, iMessage = 0;
            Trame[0] = 0;
            Trame[1] = 5;
            Trame[2] = 0;
            Trame[3] = (byte)code;

            f.UpdateStatus(string.Format("Une erreur est survenue : {0}", MessageErreur));
            // Choix du message selon le type de problème (voir p 69 dans cahier)
            for (indice = 4; indice < MessageErreur.Length; indice++)
            {
                Trame[indice] = (byte)MessageErreur[iMessage];
                iMessage++;
            }
            // Envoi du type d'erreur au serveur / client
            Trame[indice] = 0;
            s.SendTo(Trame, pointDistant);
            f.UpdateStatus("Une autre demande a été envoyé au client");
        }
    }
}
