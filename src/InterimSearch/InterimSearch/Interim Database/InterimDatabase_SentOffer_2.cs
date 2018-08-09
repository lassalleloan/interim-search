#region Application Description
/**********************************************************************\
*                            InterimSearch                            *
*                                                                     *
* Auteur : L. Lassalle                                                *
* Date de création : 12/05/2015                                       *
* Date de révision : 18/05/2015                                       *
* Description :                                                       *
* Application permettant de rechercher les disponibilités             *
* d'intérimaires, de leurs faire une proposition et de les affecter à *
* une mission.                                                        *
\**********************************************************************/
#endregion

#region References
using InterimSearch.Interim_Database.Settings;
using System.Data;
using System.Data.SqlClient;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetTemporaryDate
        /// <summary>
        /// Obtient la date de la mission en fonction de l'ID de la disponibilité.
        /// </summary>
        /// <remarks>
        /// Obtient la date de la mission en fonction de l'ID de la disponibilité.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> Date de mission. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryDate(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryDate, connectionSQL))
                    {
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient la date de la mission.
                                return dataReader[Interim_Fields.Default.Avaibility_Date].ToString().Substring(0, 10);
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryDate, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporarySendDate
        /// <summary>
        /// Obtient la date d'envoi de proposition de mission de la disponibilité.
        /// </summary>
        /// <remarks>
        /// Obtient la date d'envoi de proposition de mission de la disponibilité.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> Date d'envoie de la proposition de mission. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporarySendDate(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporarySendDate, connectionSQL))
                    {
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient la date d'envoi de proposition de mission de la disponibilité.
                                return dataReader[Interim_Fields.Default.Avaibility_SendDate].ToString().Substring(0, 10);
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporarySendDate, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryService
        /// <summary>
        /// Obtient le nom du service en fonction de l'ID de la disponibilité.
        /// </summary>
        /// <remarks>
        /// Obtient le nom du service en fonction de l'ID de la disponibilité.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> Nom du service. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryService(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryService, connectionSQL))
                    {
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient le nom du service de la disponibilité.
                                return dataReader[Interim_Fields.Default.Service_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryService, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryWorkTimetableEnd
        /// <summary>
        /// Obtient l'heure de fin de la mission.
        /// </summary>
        /// <remarks>
        /// Obtient l'heure de fin de la mission.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> Heure de fin de mission. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryWorkTimetableEnd(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryWorkTimetableEnd, connectionSQL))
                    {
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient l'heure de fin de la mission.
                                return dataReader[Interim_Fields.Default.WorkTimetable_End].ToString().Substring(0, 5);
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryWorkTimetableEnd, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryWorkTimetableStart
        /// <summary>
        /// Obtient l'heure de début de la mission.
        /// </summary>
        /// <remarks>
        /// Obtient l'heure de début de la mission.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> Heure de début de mission. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryWorkTimetableStart(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryWorkTimetableStart, connectionSQL))
                    {
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient l'heure de début de la mission.
                                return dataReader[Interim_Fields.Default.WorkTimetable_Start].ToString().Substring(0, 5);
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryWorkTimetableStart, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region SetAbortMission
        /// <summary>
        /// Enregistre l'annulation de la proposition de mission.
        /// </summary>
        /// <remarks>
        /// Enregistre l'annulation de la proposition de mission.
        /// </remarks>
        /// <param name="_avaibilityID">Id de la disponibilité</param>
        /// <returns>Boolean : True -> Réussite de l'enregistrement. False -> Echec de l'enregistrement.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static bool SetAbortMission(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_SetAbortMission, connectionSQL))
                    {
                        const int NUMBER_LINE_ASSIGNS = 1;
                        string avaibilityID = _avaibilityID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);

                        // Exécute la commande SQL.
                        return (commandSQL.ExecuteNonQuery() == NUMBER_LINE_ASSIGNS);
                    }
                }
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Interim_Err.Default.SetAbortMission, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region SetStateMission
        /// <summary>
        /// Enregistre le statut de la proposition de mission.
        /// </summary>
        /// <remarks>
        /// Enregistre le statut de la proposition de mission.
        /// </remarks>
        /// <param name="_avaibilityID">Id de la disponibilité</param>
        /// <param name="_state">Etat de la proposition de mission</param>
        /// <returns>Boolean : True -> Réussite de l'enregistrement. False -> Echec de l'enregistrement.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static bool SetStateMission(string _avaibilityID, int _state)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_SetStateMission, connectionSQL))
                    {
                        const int NUMBER_LINE_ASSIGNS = 1;
                        string avaibilityID = _avaibilityID;
                        int state = _state;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.Avaibility_AcceptedMission, state);

                        // Exécute la commande SQL.
                        return (commandSQL.ExecuteNonQuery() == NUMBER_LINE_ASSIGNS);
                    }
                }
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Interim_Err.Default.SetStateMission, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #endregion

    }
}
