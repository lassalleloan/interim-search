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
using InterimSearch.Interim_Database;
using InterimSearch.Page_Switcher;
using InterimSearch.User_Controls.Home;
using InterimSearch.User_Controls.Profile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using Process = System.Diagnostics.Process;
#endregion

namespace InterimSearch.User_Controls.Profile
{
    /// <summary>
    /// Interaction logic for wdw_profile.xaml
    /// </summary>
    public partial class wdw_profile : Window
    {

        #region Global Variables

        private static wdw_profile _instance = null;

        #endregion

        #region Constructors

        #region wdw_profile
        /// <summary>
        /// Initialise une nouvelle instance de la classe wdw_profile.
        /// </summary>
        public wdw_profile(string _temporaryID)
        {
            string temporaryID = _temporaryID;
            bool isNullInstance = (_instance == null);

            // Assigne à une propriété, l'ID de l'intériamire.
            Profile.TemporaryID = temporaryID;

            // Vérifie l'existance d'une unique instance.
            if (isNullInstance)
            {
                InitializeComponent();

                // Assigne le nom de l'interface de profil d'intérimaire à la fenêtre.
                wdw_profileProfile.Title = Profile_Val.Default.ProfileTitle;

                // Assigne aux contrôles les informations de l'intérimaire.
                TemporaryInformations();

                // Affiche la fenêtre et assigne l'instance à cette fenêtre.
                wdw_profileProfile.Show();
                _instance = wdw_profileProfile;
            }
            else
            {
                // Assigne l'instance au premier plan.
                _instance.Focus();
            }
        }
        #endregion

        #endregion

        #region Events

        #region lbl_emailAdress_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_emailAdress" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_emailAdress_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Vérifie le contenu de l'étiquette de l'adresse email.
            if (Profile.IsCorrectEmailAdress(lbl_emailAdress.Content.ToString()))
            {
                try
                {
                    // Ouverture du client de messagerie par défaut avec un email vierge et l'adresse de son destinataire.
                    Process.Start(Profile_Code.Default.Mailto + lbl_emailAdress.Content.ToString());
                }
                catch
                {
                    // Affiche un message d'erreur.
                    MessageBox.Show(Profile_Val.Default.NullEmailApplication, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Profile_Val.Default.NullEmailAdress, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region lbl_home_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_home" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_home_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Fermeture de la fenêtre de profil d'intérimaire.
            wdw_profileProfile.Close();

            // Affiche l'interface d'accueil.
            usc_home usc_homeHome = new usc_home();
            Switcher.Switch(usc_homeHome);

            // Restaure la fenêtre d'accueilau premier plan.
            Application.Current.Windows[0].WindowState = WindowState.Normal;
        }
        #endregion

        #region lbl_results_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_results" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_results_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Fermeture de la fenêtre de profil d'intérimaire.
            wdw_profileProfile.Close();

            // Restaure la fenêtre d'accueilau premier plan.
            Application.Current.Windows[0].WindowState = WindowState.Normal;
        }
        #endregion

        #region lbl_viewWorkReport_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "viewWorkReport" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_viewWorkReport_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<string>[] workTimetableInformations = InterimDatabase.GetTemporaryWorkReport(Profile.TemporaryID);
            bool isCorrectWorkReportInformations = (workTimetableInformations != null);

            // Vérifie le contenu du tableau.
            if (isCorrectWorkReportInformations)
            {
                string employmentagencyID = InterimDatabase.GetEmploymentAgencyID(Profile.TemporaryID);
                bool isCorrectEmploymentAgencyID = (!string.IsNullOrEmpty(employmentagencyID));

                // Vérifie le contenu de la chaîne de caractères.
                if (isCorrectEmploymentAgencyID)
                {
                    workTimetableInformations[5] = WorkReportInformations(employmentagencyID);

                    // Vérifie l'existence du chemin d'accès au dossier.
                    if (!Directory.Exists(workTimetableInformations[5][15]))
                    {
                        System.IO.Directory.CreateDirectory(workTimetableInformations[5][15]);
                    }

                    // Vérifie l'existence du chemin d'accès au fichier.
                    if (!File.Exists(workTimetableInformations[5][16]))
                    {
                        workTimetableInformations[5].Add(Profile.CreateWorkReport(workTimetableInformations));

                        // Vérifie le contenu de la liste et l'enregistrement du rapport de travail.
                        if (Profile.IsNullList(workTimetableInformations[5]) || (!Profile.SaveWorkReport(workTimetableInformations[5])))
                        {
                            // Affiche un message d'erreur.
                            MessageBox.Show(Profile_Val.Default.NullWorkReport, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Essaye d'ouvrir le rapport de travail.
                    Profile.TryOpenFile(workTimetableInformations[5][16]);
                }
                else
                {
                    // Affiche un message d'erreur.
                    MessageBox.Show(Profile_Val.Default.NullEmploymentAgencyID, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Profile_Val.Default.NullWorkReportInformations, Profile_Val.Default.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region wdw_profile_Closing
        /// <summary>
        /// Action lors de la fermeture de la window "wdw_profile".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wdw_profile_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Met à null la valeur de l'instance.
            _instance = null;
        }
        #endregion

        #endregion

        #region Methods

        #region TemporaryInformations
        /// <summary>
        /// Assigne aux contrôles les informations de l'intérimaire.
        /// </summary>
        private void TemporaryInformations()
        {
            List<string> temporaryInformations = Profile.TemporaryInformations();
            bool temporaryCertified = InterimDatabase.GetTemporaryCertified(Profile.TemporaryID);

            // Vérifie le contenu des informations de l'intérimaire.
            if (Profile.IsCorrectList(temporaryInformations))
            {
                // Assigne aux contrôles les informations de l'intérimaire.
                lbl_title.Content = temporaryInformations[0];
                lbl_name.Content = temporaryInformations[1];
                lbl_firstName.Content = temporaryInformations[2];
                lbl_ageValue.Content = temporaryInformations[3] + Profile_Val.Default.Year;

                lbl_number.Content = temporaryInformations[4];
                lbl_street.Content = temporaryInformations[5];
                lbl_postCode.Content = temporaryInformations[6];
                lbl_city.Content = temporaryInformations[7];
                lbl_region.Content = temporaryInformations[8];

                lbl_phoneNumber.Content = Profile_Val.Default.InternationalSymbol + temporaryInformations[9];
                lbl_emailAdress.Content = temporaryInformations[10];

                lbl_professionValue.Content = temporaryInformations[11];
                lbl_nbrYearExpValue.Content = temporaryInformations[12] + Profile_Val.Default.Year;
                chk_certified.IsChecked = temporaryCertified;
                lbl_emplAgencyName.Content = temporaryInformations[13];
            }
            else
            {
                // Affichage un message d'erreur.
                MessageBox.Show(Profile_Val.Default.TemporaryInformations, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region WorkReportInformations
        /// <summary>
        /// Obtient les informations du rapport de travail.
        /// </summary>
        /// <param name="_employmentAgencyID">ID de l'agence d'intérim</param>
        /// <returns>List : Informations du rapport de travail.</returns>
        private List<string> WorkReportInformations(string _employmentAgencyID)
        {
            string employmentAgencyID = _employmentAgencyID;
            List<string> workReportInformations = new List<string>();

            // Assigne les informations du rapport de travail.
            workReportInformations.Add(lbl_emplAgencyName.Content.ToString());
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyNumber(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyStreet(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyPostCode(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyCity(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyRegion(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyCountry(employmentAgencyID));
            workReportInformations.Add(Profile_Val.Default.InternationalSymbol + InterimDatabase.GetEmploymentAgencyPhoneNumber(employmentAgencyID));
            workReportInformations.Add(InterimDatabase.GetEmploymentAgencyEmailAdress(employmentAgencyID));
            workReportInformations.Add(lbl_title.Content.ToString());
            workReportInformations.Add(lbl_name.Content.ToString());
            workReportInformations.Add(lbl_firstName.Content.ToString());
            workReportInformations.Add(InterimDatabase.GetTemporaryBirthDate(Profile.TemporaryID));
            workReportInformations.Add(lbl_professionValue.Content.ToString());
            workReportInformations.Add(DateTime.Now.ToShortDateString());

            // Assigne les chemins d'accès au rapport de travail.
            Profile.AddPaths(workReportInformations);

            return workReportInformations;
        }
        #endregion

        #endregion

    }
}
