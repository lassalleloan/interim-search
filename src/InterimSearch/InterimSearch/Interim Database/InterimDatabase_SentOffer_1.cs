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
#endregion

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetAbortAvibilityID
        /// <summary>
        /// Obtient les ID des disponibilités annulées.
        /// </summary>
        /// <remarks>
        /// Obtient les ID des disponibilités annulées.
        /// </remarks>
        /// <returns>List : Réussite -> Liste des ID des disponibilités annulées. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetAbortAvibilityID(string _avaibilityIDRef)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAbortAvaibilityID, connectionSQL))
                    {
                        string avaibilityIDRef = _avaibilityIDRef;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, avaibilityIDRef);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> abortAvaibilityID = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient les ID des disponibilités annulées.
                                    abortAvaibilityID.Add(dataReader[Interim_Fields.Default.IDAvaibility].ToString());
                                }

                                return abortAvaibilityID;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetAbortAvibilityID, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetAvaibilityDate
        /// <summary>
        /// Obtient les dates des disponibilités.
        /// </summary>
        /// <remarks>
        /// Obtient les dates des disponibilités.
        /// </remarks>
        /// <returns>List : Réussite -> Liste des dates des disponibilités. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetAvaibilityDate()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAvaibilityDate, connectionSQL))
                    {
                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> avaibilityDateList = new List<string>();
                                string date = string.Empty;

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient les dates des disponibilités.
                                    avaibilityDateList.Add(dataReader[Interim_Fields.Default.Avaibility_Date].ToString().Substring(0, 10));
                                }

                                return avaibilityDateList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetAvaibilityDate, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetAvaibilityIDAll
        /// <summary>
        /// Obtient les ID des disponibilités en fonction d'une date et d'horaires de travail.
        /// </summary>
        /// <remarks>
        /// Obtient les ID des disponibilités en fonction d'une date et d'horaires de travail.
        /// </remarks>
        /// <param name="_avaibilityDate">Date</param>
        /// <param name="_workTimetableStart">horaire de début de travail</param>
        /// <param name="_workTimetableEnd">Horaire de fin de travail</param>
        /// <returns>List : Réussite -> Liste des ID des disponibilités. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetAvaibilityIDAll(string _avaibilityDate, string _workTimetableStart, string _workTimetableEnd)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAvaibilityIDAll, connectionSQL))
                    {
                        DateTime avaibilityDate = Convert.ToDateTime(_avaibilityDate);
                        string workTimetableStart = _workTimetableStart;
                        string workTimetableEnd = _workTimetableEnd;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.Avaibility_Date, avaibilityDate);
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.WorkTimetable_Start, workTimetableStart);
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.WorkTimetable_End, workTimetableEnd);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> avaibilityIDAllList = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient les ID des disponibilités en fonction d'une date et d'horaires de travail.
                                    avaibilityIDAllList.Add(dataReader[Interim_Fields.Default.IDAvaibility].ToString());
                                }

                                return avaibilityIDAllList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetAvaibilityIDAll, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetAvaibilityTemporaryID
        /// <summary>
        /// Obtient l'ID d'un intérimaire en fonction de l'ID de la disponibilité.
        /// </summary>
        /// <remarks>
        /// Obtient l'ID d'un intérimaire en fonction de l'ID de la disponibilité.
        /// </remarks>
        /// <param name="_IDAvaibility">ID disponibilité</param>
        /// <returns>String : Réussite -> ID d'un intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetAvaibilityTemporaryID(string _IDAvaibility)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAvaibilityTemporaryID, connectionSQL))
                    {
                        string IDAvaibility = _IDAvaibility;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDAvaibility, IDAvaibility);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient l'ID d'un intérimaire
                                return dataReader[Interim_Fields.Default.IDTemporary].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetAvaibilityTemporaryID, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetAvaibilityWorkTimetable
        /// <summary>
        /// Obtient une liste des horaires de travail en fonction de la date.
        /// </summary>
        /// <remarks>
        /// Obtient une liste des horaires de travail en fonction de la date.
        /// </remarks>
        /// <param name="_avaibilityDate">Date</param>
        /// <returns>List : Réussite -> Liste des horaires de travail. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<TimeSpan> GetAvaibilityWorkTimetable(string _avaibilityDate)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAvaibilityWorkTimetable, connectionSQL))
                    {
                        DateTime avaibilityDate = Convert.ToDateTime(_avaibilityDate);

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.Avaibility_Date, avaibilityDate);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<TimeSpan> workTimetableList = new List<TimeSpan>();
                                TimeSpan startTime = new TimeSpan();
                                TimeSpan endTime = new TimeSpan();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    /// Obtient une liste des horaires de travail en fonction de la date.
                                    startTime = (TimeSpan)dataReader[Interim_Fields.Default.WorkTimetable_Start];
                                    workTimetableList.Add(startTime);

                                    endTime = (TimeSpan)dataReader[Interim_Fields.Default.WorkTimetable_End];
                                    workTimetableList.Add(endTime);
                                }

                                return workTimetableList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetAvaibilityWorkTimetable, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetTemporaryAcceptedMission
        /// <summary>
        /// Obtient la valeur de l'acceptation d'une proposition de mission d'une disponibilité.
        /// </summary>
        /// <remarks>
        /// Obtient la valeur de l'acceptation d'une proposition de mission d'une disponibilité.
        /// </remarks>
        /// <param name="_avaibilityID">Date de la disponibilité</param>
        /// <returns>List : Réussite -> Valeur de l'acceptation. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryAcceptedMission(string _avaibilityID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryAcceptedMission, connectionSQL))
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
                                // Obtient la valeur de l'acceptation.
                                return dataReader[Interim_Fields.Default.Avaibility_AcceptedMission].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryAcceptedMission, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #endregion

    }
}
