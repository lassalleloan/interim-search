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
#endregion

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region DayName
        /// <summary>
        /// Retourne le nom complet du jour de la date sélectionnée.
        /// </summary>
        /// <returns>String : Nom du jour.</returns>
        public static string DayName()
        {
            string dayName = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(Date).DayOfWeek);

            return char.ToUpper(dayName[0]) + dayName.Substring(1);
        }
        #endregion

        #region DayNumber
        /// <summary>
        /// Retourne le numéro du jour de la date sélectionnée.
        /// </summary>
        /// <returns>String : Numéro du jour.</returns>
        public static string DayNumber()
        {
            return Convert.ToDateTime(InterimDatabase.Date).Day.ToString();
        }
        #endregion

        #region GetAvaibilityID
        /// <summary>
        /// Obtient l'ID de la disponibilité par rapport à l'ID de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient l'ID de la disponibilité par rapport à l'ID de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> ID de la disponibilité. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        private static string GetAvaibilityID(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetAvaibilityID, connectionSQL))
                    {
                        string temporaryID = _temporaryID;

                        // Assigne le type procédure stockée à la commande.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.Avaibility_Date, Date);
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDTemporary, temporaryID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et si il est possible de lire.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient l'ID de la disponibilité par rapport à l'ID de l'intérimaire.
                                return dataReader[Interim_Fields.Default.IDAvaibility].ToString();
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetAvaibilityID, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetService
        /// <summary>
        /// Obtient une liste des noms des services.
        /// </summary>
        /// <remarks>
        /// Obtient une liste des noms des services.
        /// </remarks>
        /// <returns>String : Réussite -> Liste des noms des services. Echec -> Valeur null.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static List<string> GetService()
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetService, connectionSQL))
                    {
                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne.
                            if (dataReader.HasRows)
                            {
                                List<string> serviceNameList = new List<string>();

                                // Lit tant qu'il existe des lignes.
                                while (dataReader.Read())
                                {
                                    // Obtient le nom des services.
                                    serviceNameList.Add(dataReader[Interim_Fields.Default.Service_Name].ToString());
                                }

                                return serviceNameList;
                            }

                            return null;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                System.Windows.Forms.MessageBox.Show(Interim_Err.Default.GetService, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region GetTemporaryAge
        /// <summary>
        /// Obtient l'âge de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient l'âge de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Age de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryAge(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryAge, connectionSQL))
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
                                // Obtient l'âge de l'intérimaire.
                                return NbrSpendYear(dataReader[Interim_Fields.Default.Temporary_YearBirthDate].ToString());
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryAge, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryCity
        /// <summary>
        /// Obtient le nom de ville de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le nom de ville de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nom de ville de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryCity(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryCity, connectionSQL))
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
                                // Obtient le nom de la ville de l'intérimaire.
                                return dataReader[Interim_Fields.Default.City_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryCity, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryEmailAdress
        /// <summary>
        /// Obtient l'adresse email de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient l'adresse email de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Adresse email de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryEmailAdress(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryEmailAdress, connectionSQL))
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
                                // Obtient l'adresse email de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_EmailAdress].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryEmailAdress, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryFirstName
        /// <summary>
        /// Obtient le prénom de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le prénom d'un intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Prénom d'un intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryFirstName(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryFirstName, connectionSQL))
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
                                // Obtient le prénom d'un intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_FirstName].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryFirstName, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #endregion

    }
}
