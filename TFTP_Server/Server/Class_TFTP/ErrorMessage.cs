using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.TFTP
{
    // Messages d'erreurs TFTP, voir page 68
    enum ErrorMessage
    {
        NON_DEFINI,
        FICHIER_NON_TROUVE,
        VIOLATION_ACCES,
        DISQUE_PLEIN,
        OPERATION_TFTP_ILLEGALE,
        TRANSFERT_ID_INCONNU,
        FICHIER_EXISTANT,
        UTILISATEUR_INCONNU
    };
}
