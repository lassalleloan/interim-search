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

        #region GetCountAcceptedMissionEmploymentAgency
        /// <summary>
        /// Obtient le nombre de missions acceptées en fonction de leur agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre de missions acceptées en fonction de leur agence d'intérim.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre de missions acceptées. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountAcceptedMissionEmploymentAgency()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountAcceptedMissionEmploymentAgency, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countAcceptedMissionEmploymentAgency = new List<string>[2];
                                List<string> employmentAgency = new List<string>();
                                List<string> countAcceptedMission = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre de missions acceptées par agence d'intérim.
                                    employmentAgency.Add(dataReader[Interim_Fields.Default.EmploymentAgency_Name].ToString());
                                    countAcceptedMission.Add(dataReader[Interim_Fields.Default.Count_AcceptedMission].ToString());
                                }

                                countAcceptedMissionEmploymentAgency[0] = employmentAgency;
                                countAcceptedMissionEmploymentAgency[1] = countAcceptedMission;

                                return countAcceptedMissionEmploymentAgency;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountAcceptedMissionEmploymentAgency, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetCountAcceptedMissionService
        /// <summary>
        /// Obtient le nombre de missions en fonction de leurs services.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre de missions en fonction de leurs services.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre de missions par service. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountAcceptedMissionService()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountAcceptedMissionService, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countAcceptedMissionService = new List<string>[2];
                                List<string> service = new List<string>();
                                List<string> countAcceptedMission = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre de missions en fonction de leurs services.
                                    service.Add(dataReader[Interim_Fields.Default.Service_Name].ToString());
                                    countAcceptedMission.Add(dataReader[Interim_Fields.Default.Count_AcceptedMission].ToString());
                                }

                                countAcceptedMissionService[0] = service;
                                countAcceptedMissionService[1] = countAcceptedMission;

                                return countAcceptedMissionService;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountAcceptedMissionService, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetCountRejectedMissionEmploymentAgency
        /// <summary>
        /// Obtient le nombre de missions refusées en fonction de leur agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre de missions refusées en fonction de leur agence d'intérim.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre de missions refusées. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountRejectedMissionEmploymentAgency()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountRejectedMissionEmploymentAgency, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countRejectedMissionEmploymentAgency = new List<string>[2];
                                List<string> employmentAgency = new List<string>();
                                List<string> countRejectedMission = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre de missions refusées par agence d'intérim.
                                    employmentAgency.Add(dataReader[Interim_Fields.Default.EmploymentAgency_Name].ToString());
                                    countRejectedMission.Add(dataReader[Interim_Fields.Default.Count_RejectedMission].ToString());
                                }

                                countRejectedMissionEmploymentAgency[0] = employmentAgency;
                                countRejectedMissionEmploymentAgency[1] = countRejectedMission;

                                return countRejectedMissionEmploymentAgency;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountRejectedMissionEmploymentAgency, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetCountTemporaryEmploymentAgency
        /// Obtient le nombre d'intérimaires en fonction de leurs agences d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre d'intérimaires en fonction de leurs agences d'intérim.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre d'intérimaire par agence d'intérim. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountTemporaryEmploymentAgency()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountTemporaryEmploymentAgency, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countTemporaryEmploymentAgency = new List<string>[2];
                                List<string> employmentAgencyName = new List<string>();
                                List<string> countTemporary = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre d'intérimaires en fonction de leurs agences d'intérim.
                                    employmentAgencyName.Add(dataReader[Interim_Fields.Default.EmploymentAgency_Name].ToString());
                                    countTemporary.Add(dataReader[Interim_Fields.Default.Count_Temporary].ToString());
                                }

                                countTemporaryEmploymentAgency[0] = employmentAgencyName;
                                countTemporaryEmploymentAgency[1] = countTemporary;

                                return countTemporaryEmploymentAgency;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountTemporaryEmploymentAgency, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetCountTemporaryProfession
        /// <summary>
        /// Obtient le nombre d'intérimaires en fonction de leur profession.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre d'intérimaires en fonction de leur profession.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre d'intérimaire par profession. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountTemporaryProfession()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountTemporaryProfession, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countTemporaryProfession = new List<string>[2];
                                List<string> profession = new List<string>();
                                List<string> countTemporary = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre d'intérimaires en fonction de leur profession.
                                    profession.Add(dataReader[Interim_Fields.Default.Profession_Name].ToString());
                                    countTemporary.Add(dataReader[Interim_Fields.Default.Count_Temporary].ToString());
                                }

                                countTemporaryProfession[0] = profession;
                                countTemporaryProfession[1] = countTemporary;

                                return countTemporaryProfession;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountTemporaryProfession, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetCountWaitedMissionEmploymentAgency
        /// <summary>
        /// Obtient le nombre de missions en attente en fonction de leur agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le nombre de missions en attente en fonction de leur agence d'intérim.
        /// </remarks>
        /// <returns>List[] : Réussite -> Tableau du nombre de missions en attente. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string>[] GetCountWaitedMissionEmploymentAgency()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetCountWaitedMissionEmploymentAgency, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string>[] countRejectedMissionEmploymentAgency = new List<string>[2];
                                List<string> employmentAgency = new List<string>();
                                List<string> countRejectedMission = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nombre de missions en attente par agence d'intérim.
                                    employmentAgency.Add(dataReader[Interim_Fields.Default.EmploymentAgency_Name].ToString());
                                    countRejectedMission.Add(dataReader[Interim_Fields.Default.Count_WaitedMission].ToString());
                                }

                                countRejectedMissionEmploymentAgency[0] = employmentAgency;
                                countRejectedMissionEmploymentAgency[1] = countRejectedMission;

                                return countRejectedMissionEmploymentAgency;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetCountWaitedMissionEmploymentAgency, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

    }
}
