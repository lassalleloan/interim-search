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
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion;

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetTemporaryPostCode
        /// <summary>
        /// Obtient le code postale de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le code postale de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Code postale de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryPostCode(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryPostCode, connectionSQL))
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
                                // Obtient le code postale de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_PostCode].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryPostCode, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryProfession
        /// <summary>
        /// Obtient la profession de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient la profession de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Profession de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryProfession(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryProfession, connectionSQL))
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
                                // Obtient la profession de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Profession_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryProfession, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryRegion
        /// <summary>
        /// Obtient la région de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient la région de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Région de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryRegion(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryRegion, connectionSQL))
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
                                // Obtient la région de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Region_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryRegion, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryStreet
        /// <summary>
        /// Obtient le nom de rue de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le nom de rue de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nom de rue de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryStreet(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryStreet, connectionSQL))
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
                                // Obtient le nom de rue de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_Street].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryStreet, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryWorkReport
        /// <summary>
        /// Obtient les dates, les horaires de travail et les services par rapport à un intérimaire.
        /// </summary>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>List[] : Réussite -> Tableau de listes. Echec -> Valeur null</returns>
        public static List<string>[] GetTemporaryWorkReport(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryWorkReport, connectionSQL))
                    {
                        string temporaryID = _temporaryID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDTemporary, temporaryID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] workReportInformations = new List<string>[6];
                                List<string> avaibilityDate = new List<string>();
                                List<string> workTimetableStart = new List<string>();
                                List<string> workTimetableEnd = new List<string>();
                                List<string> avaibilityService = new List<string>();

                                // Lit tant qu'il existe un enregistrement.
                                while (dataReader.Read())
                                {
                                    // Obtient les dates, les horaires de travail et les services par rapport à un intérimaire.
                                    avaibilityDate.Add(dataReader[Interim_Fields.Default.Avaibility_Date].ToString().Substring(0, 10));
                                    workTimetableStart.Add(dataReader[Interim_Fields.Default.WorkTimetable_Start].ToString().Substring(0, 5));
                                    workTimetableEnd.Add(dataReader[Interim_Fields.Default.WorkTimetable_End].ToString().Substring(0, 5));
                                    avaibilityService.Add(dataReader[Interim_Fields.Default.Service_Name].ToString());
                                }

                                workReportInformations[0] = avaibilityDate;
                                workReportInformations[1] = workTimetableStart;
                                workReportInformations[2] = workTimetableEnd;
                                workReportInformations[3] = avaibilityService;

                                bool isCorrectWorkTimetableList = ((workTimetableStart != null) && (workTimetableEnd != null));

                                // Vérifie le contenu des listes.
                                if (isCorrectWorkTimetableList)
                                {
                                    workReportInformations[4] = WorkTimetableSum(workTimetableStart, workTimetableEnd);
                                }

                                return workReportInformations;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryWorkReport, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region WorkTimetableSum
        /// <summary>
        /// Calcul le nombre d'heures total entre deux intervals avec une pause déjeuné de 30 minutes.
        /// </summary>
        /// <param name="_workTimetableStart">Heure de début</param>
        /// <param name="_workTimetableEnd">Heure de fin</param>
        /// <returns>List : Listes du nombres d'heures total avec la pause déjeuné.</returns>
        private static List<string> WorkTimetableSum(List<string> _workTimetableStart, List<string> _workTimetableEnd)
        {
            List<string> workTimetableStart = _workTimetableStart;
            List<string> workTimetableEnd = _workTimetableEnd;
            List<string> workTimetableSum = new List<string>();
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            string timeSpend = string.Empty;

            for (int index = 0; index < workTimetableStart.Count; index++)
            {
                startTime = Convert.ToDateTime(workTimetableStart[index]);
                endTime = Convert.ToDateTime(workTimetableEnd[index]);

                // Vérifie l'horaire la plus grande.
                if (startTime.TimeOfDay > endTime.TimeOfDay)
                {
                    timeSpend = startTime.Add(-endTime.TimeOfDay.Add(new TimeSpan(0, 30, 0))).ToShortTimeString();
                }
                else
                {
                    timeSpend = endTime.Add(-startTime.TimeOfDay.Add(new TimeSpan(0, 30, 0))).ToShortTimeString();
                }

                workTimetableSum.Add(timeSpend);
            }

            return workTimetableSum;
        }
        #endregion

        #endregion

    }
}
