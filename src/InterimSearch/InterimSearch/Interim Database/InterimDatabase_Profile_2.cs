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

        #region GetEmploymentAgencyRegion
        /// <summary>
        /// Obtient la région de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient la région de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Région de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyRegion(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyRegion, connectionSQL))
                    {
                        string employmentAgencyID = _employmentAgencyID;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDEmploymentAgency, employmentAgencyID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient la région de l'agence d'intérim.
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
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyRegion, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyStreet
        /// <summary>
        /// Obtient la rue de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient la rue de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Rue de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyStreet(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyStreet, connectionSQL))
                    {
                        string employmentAgencyID = _employmentAgencyID;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.CommandType = CommandType.StoredProcedure;

                        // Ajoute une valeur à l'endroit spécifié dans la commande SQL.
                        commandSQL.Parameters.AddWithValue(Interim_Fields.Default.IDEmploymentAgency, employmentAgencyID);

                        using (SqlDataReader dataReader = commandSQL.ExecuteReader())
                        {
                            // Vérifie si la commande SQL retourne au moins une ligne et lit tant qu'il existe des lignes.
                            if (dataReader.HasRows && dataReader.Read())
                            {
                                // Obtient la rue de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.EmploymentAgency_Street].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyStreet, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryCertified
        /// <summary>
        /// Obtient l'état de certification de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient l'état de certification de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>Boolean : Réussite -> Etat de certification de l'intérimaire. Echec -> False.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static bool GetTemporaryCertified(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryCertified, connectionSQL))
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
                                // Obtient l'état de certification de l'intérimaire.
                                return Convert.ToBoolean(dataReader[Interim_Fields.Default.Temporary_Certified]);
                            }

                            return false;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryCertified, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region GetTemporaryBirthDate
        /// <summary>
        /// Obtient la date d'anniversaire de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient la date d'anniversaire de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Date d'anniversaire de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryBirthDate(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryBirthDate, connectionSQL))
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
                                // Obtient la date d'anniversaire de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_BirthDate].ToString().Substring(0, 10);
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryBirthDate, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryEmploymentAgency
        /// <summary>
        /// Obtient le nom de l'agence d'intérim de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le nom de l'agence d'intérim de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nom de l'agence d'intérim de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryEmploymentAgency(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryEmploymentAgency, connectionSQL))
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
                                // Obtient le nom de l'agence d'intérim de l'intérimaire.
                                return dataReader[Interim_Fields.Default.EmploymentAgency_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryEmploymentAgency, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryNumber
        /// <summary>
        /// Obtient le numéro de rue de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le numéro de rue de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Nom de rue de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryNumber(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryNumber, connectionSQL))
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
                                // Obtient le numéro de rue de l'intérimaire.
                                return dataReader[Interim_Fields.Default.Temporary_Number].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryNumber, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetTemporaryPhoneNumber
        /// <summary>
        /// Obtient le numéro de téléphone de l'intérimaire.
        /// </summary>
        /// <remarks>
        /// Obtient le numéro de téléphone de l'intérimaire.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> Numéro de téléphone de l'intérimaire. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetTemporaryPhoneNumber(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetTemporaryPhoneNumber, connectionSQL))
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
                                // Obtient le numéro de téléphone de l'intérimaire.
                                string phoneNumber = dataReader[Interim_Fields.Default.Temporary_PhoneNumber].ToString();

                                for (int index = 2; index <= phoneNumber.Length; index += 7)
                                {
                                    phoneNumber = phoneNumber.Insert(index, " ");
                                    phoneNumber = phoneNumber.Insert(index + 3, " ");
                                }

                                return phoneNumber;
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetTemporaryPhoneNumber, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #endregion

    }
}
