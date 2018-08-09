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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion;

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetTemporaryIDList
        /// <summary>
        /// Obtient une liste des ID des intérimaires.
        /// </summary>
        /// <remarks>
        /// Obtient une liste des ID des intérimaires
        /// triés dans l'ordre alphabétiques des noms d'intérimaires.
        /// </remarks>
        /// <returns>List : Réussite -> Liste des ID des intérimaires. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetTemporaryID()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    string querySelect = Interim_Query.Default.SelectIDTemporary + Interim_Query.Default.InnerJoinBasics + Interim_Query.Default.ClauseBasics;

                    // Vérifie la sélection d'une profession particulière.
                    if (IsParticularProfession)
                    {
                        querySelect += Interim_Query.Default.ClauseProfession;
                    }

                    // Vérifie la sélection d'une catégorie d'âge particulière.
                    if (IsParticularAge)
                    {
                        querySelect += Interim_Query.Default.ClauseBirthDate;
                    }

                    // Vérifie la sélection d'une catégorie de nombres d'années d'expérience.
                    if (IsParticularNbrYearExp)
                    {
                        querySelect += Interim_Query.Default.ClauseSartDateExp;
                    }

                    querySelect += Interim_Query.Default.OrderBy;

                    using (SqlCommand commandSQL = new SqlCommand(querySelect, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.Date, Date);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.WorkTimetableStart, WorkTimetableStart);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.WorkTimetableEnd, WorkTimetableEnd);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.IsCertified, IsCertified);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.Profession, Profession);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.AgeYearOld, AgeYearOld);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.AgeYearRecent, AgeYearRecent);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.NbrYearExpOld, NbrYearExpOld);
                        commandSQL.Parameters.AddWithValue(Interim_Query.Default.NbrYearExpRecent, NbrYearExpRecent);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> temporaryIDList = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient l'ID des intérimaires.
                                    temporaryIDList.Add(dataReader[Interim_Fields.Default.IDTemporary].ToString());
                                }

                                return temporaryIDList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryID, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetTemporaryName
        /// <summary>
        /// Obtient le nom de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le nom de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nom d'un intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryName(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryName, connectionSQL))
                    {
                        string temporaryID = _temporaryID;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDTemporary, temporaryID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient le nom  de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryName, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryNbrYearExp
        /// <summary>
        /// Obtient le nombres d'années d'expériences de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le nombres d'années d'expériences de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nombres d'années d'expériences d'un intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryNbrYearExp(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryNbrYearExp, connectionSQL))
                    {
                        string temporaryID = _temporaryID;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDTemporary, temporaryID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient le nombres d'années d'expériences de l'intérimaire.
                                return NbrSpendYear(dataReader[Interim_Fields.Default.Temporary_YearStartDateExperience].ToString());
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryNbrYearExp, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryTitle
        /// <summary>
        /// Obtient le titre de civilité de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le titre de civilité de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Titre de civilité de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryTitle(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryTitle, connectionSQL))
                    {
                        string temporaryID = _temporaryID;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDTemporary, temporaryID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient le titre de civilité de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_Title].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryTitle, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region MonthName
        /// <summary>
        /// Retourne le nom complet du mois de la date sélectionnée.
        /// </summary>
        /// <returns>String : Nom du mois.</returns>
        public static string MonthName()
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(Date).Month);

            return char.ToUpper(monthName[0]) + monthName.Substring(1);
        }
        #endregion

        #region SetSendDateAndService
        /// <summary>
        /// Enregistre la date de l'envoi et le service de la proposition de mission.
        /// </summary>
        /// <remarks>
        /// Enregistre la date de l'envoi et le service de la proposition de mission.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <param name="_serviceName">Nom du service</param>
        /// <returns>Boolean : True -> Réussite des enregistrements. False -> Echec des enregistrements.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static bool SetSendDateAndService(string _temporaryID, string _serviceName)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_SetSendDateAndService, connectionSQL))
                    {
                        const int NUMBER_LINE_ASSIGNS = 1;
                        string temporaryID = _temporaryID;
                        string serviceName = _serviceName;
                        string avaibilityID = GetAvaibilityID(temporaryID);
                        bool isCorrectAvaibilityID = (!string.IsNullOrEmpty(avaibilityID));

                        if (isCorrectAvaibilityID)
                        {
                            // Assigne le type procédure stockée à la commande.
                            commandSQL.CommandType = CommandType.StoredProcedure;

                            // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                            commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityID);
                            commandSQL.Parameters.AddWithValue(Interim_Fields.Default.Service_Name, serviceName);

                            // Exécute la commande SQL.
                            return (commandSQL.ExecuteNonQuery() == NUMBER_LINE_ASSIGNS);
                        }

                        return false;
                    }
                }
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Interim_Err.Default.SetSendDateAndService, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region WorkTimetableEnd
        /// <summary>
        /// Obtient la valeur de la propriété WorkTimetableEnd.
        /// </summary>
        /// <returns>String : Valeur de la porpriété WorkTimetableEnd</returns>
        public static string ReturnWorkTimetableEnd()
        {
            return WorkTimetableEnd;
        }
        #endregion

        #endregion

    }
}
