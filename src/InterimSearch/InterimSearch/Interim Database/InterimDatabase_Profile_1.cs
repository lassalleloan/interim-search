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
#endregion;

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Methods

        #region GetEmploymentAgencyID
        /// <summary>
        /// Obtient l'ID de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient l'ID de l'agence d'intérim.
        /// </remarks>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <returns>String : Réussite -> ID de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyID(string _temporaryID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyID, connectionSQL))
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
                                // Obtient l'ID de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.IDEmploymentAgency].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyID, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyCity
        /// <summary>
        /// Obtient la ville de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient la ville de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Ville de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyCity(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyCity, connectionSQL))
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
                                // Obtient la ville de l'agence d'intérim.
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
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyCity, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyCountry
        /// <summary>
        /// Obtient le pays de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le pays de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Pays de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyCountry(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyCountry, connectionSQL))
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
                                // Obtient le pays de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.Country_Name].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyCountry, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyEmailAdress
        /// <summary>
        /// Obtient l'adresse email de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient l'adresse email de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Adresse email de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyEmailAdress(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyEmailAdress, connectionSQL))
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
                                // Obtient l'adresse email de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.EmploymentAgency_EmailAdress].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyEmailAdress, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyNumber
        /// <summary>
        /// Obtient le numéro de rue de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le numéro de rue de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Numéro de rue de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyNumber(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyNumber, connectionSQL))
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
                                // Obtient le numéro de rue de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.EmploymentAgency_Number].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyNumber, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyPhoneNumber
        /// <summary>
        /// Obtient le numéro de téléphone de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le numéro de téléphone de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Numéro de téléphone de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyPhoneNumber(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyPhoneNumber, connectionSQL))
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
                                // Obtient le numéro de téléphone de l'agence d'intérim.
                                string phoneNumber = dataReader[Interim_Fields.Default.EmploymentAgency_PhoneNumber].ToString();

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
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyPhoneNumber, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region GetEmploymentAgencyPostCode
        /// <summary>
        /// Obtient le code postale de l'agence d'intérim.
        /// </summary>
        /// <remarks>
        /// Obtient le code postale de l'agence d'intérim.
        /// </remarks>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>String : Réussite -> Code postale de l'agence d'intérim. Echec -> Valeur vide.</returns>
        /// <exception cref="SqlConnection, SqlCommand, SqlDataReader">
        /// Exception levée par l'objet SqlConnection, SqlCommand ou SqlDataReader.
        /// </exception>
        public static string GetEmploymentAgencyPostCode(string _employmentAgencyID)
        {
            try
            {
                using (SqlConnection connectionSQL = new SqlConnection(Interim_Code.Default.InterimDatabaseConnectionString))
                {
                    connectionSQL.Open();

                    using (SqlCommand commandSQL = new SqlCommand(Interim_Proc.Default.Proc_GetEmploymentAgencyPostCode, connectionSQL))
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
                                // Obtient le code postale de l'agence d'intérim.
                                return dataReader[Interim_Fields.Default.EmploymentAgency_PostCode].ToString();
                            }

                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Interim_Err.Default.GetEmploymentAgencyPostCode, Interim_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #endregion
    }
}