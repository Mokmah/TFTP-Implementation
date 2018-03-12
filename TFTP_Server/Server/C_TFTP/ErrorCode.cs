namespace Server.C_TFTP
{
    // Messages d'erreurs TFTP, voir page 68
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
