/// <summary>
/// Description des erreurs possibles dans un transfert de fichiers TFTP
/// </summary>

namespace Client.C_TFTPClient
{
       // Erreurs possibles dans un transfert TFTP
       enum ErrorCode
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
