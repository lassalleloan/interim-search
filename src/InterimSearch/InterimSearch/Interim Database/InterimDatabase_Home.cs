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
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetProfession
        /// <summary>
        /// Obtient une liste des noms des professions.
        /// </summary>
        /// <remarks>
        /// Obtient une liste des noms des professions triés par ordre alphabétique.
        /// </remarks>
        /// <returns>List : Réussite -> Liste des noms des professions. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetProfession()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetProfession, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> professionNameList = new List<string>();

                                professionNameList.Add(Interim_Val.Default.AllProfession);

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nom des professions.
                                    professionNameList.Add(dataReader[Interim_Fields.Default.Profession_Name].ToString());
                                }

                                return professionNameList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetProfession, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetWorkTimetable
        /// <summary>
        /// Obtient une liste des horaires de travail.
        /// </summary>
        /// <remarks>
        /// Obtient une liste des horaires de travail.
        /// </remarks>
        /// <returns>List : Réussite -> Liste des horaires de travail. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<TimeSpan> GetWorkTimetable()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetWorkTimetable, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

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
                                    // Obtient les des horaires de travail.
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
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetWorkTimetable, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

    }
}
