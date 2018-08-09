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
using InterimSearch.User_Controls.Results.Settings;
using System;
using System.Windows.Controls;
#endregion

namespace InterimSearch.User_Controls.Results
{
    /// <summary>
    /// Logique d'interaction pour usc_results.xaml
    /// </summary>
    public partial class usc_results : UserControl
    {

        #region Constructors

        #region usc_results
        /// <summary>
        /// Initialise une nouvelle instance de la classe "usc_results".
        /// </summary>
        public usc_results()
        {
            InitializeComponent();

            // Assigne le nom de l'interface des résultats à la fenêtre.
            Switcher.ChangeWindowTitle(Results_Val.Default.ResultsTitle);

            // Assigne aux contrôles les critères de recherche.
            SetCriteria();

            // Ajoute des contrôles pour visualiser des données.
            InterimDatabase.IsCertified = true;
            Results.AddProfilResumeControls(stp_dynamicsBase);

            // Vérifie si le critères de certification est sélectionné.
            if (!Convert.ToBoolean(Results.Criteria[3]))
            {
                // Ajoute des contrôles pour visualiser des données.
                InterimDatabase.IsCertified = false;
                Results.AddProfilResumeControls(stp_dynamicsBase);
            }

            // Vérifie si il existe des résultats.
            if (stp_dynamicsBase.Children.Count == 0)
            {
                // Ajoute un contrôle lors du premier lancement de l'application.
                Label lbl_anyDataControl = Results.AddAnyProfilResumeControl();
                stp_dynamicsBase.Children.Add(lbl_anyDataControl);
            }
        }
        #endregion

        #endregion

        #region Events

        #region lbl_home_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_home" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_home_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Affiche l'interface d'accueil.
            usc_home usc_homeResults = new usc_home();
            Switcher.Switch(usc_homeResults);
        }
        #endregion

        #endregion

        #region Methods

        #region SetCriteria
        /// <summary>
        /// Assigne aux contrôles les critères de recherche.
        /// </summary>
        private void SetCriteria()
        {
            lbl_date.Content = Results.Criteria[0];
            lbl_workTimetable.Content = Results.Criteria[1];
            lbl_professionValue.Content = Results.Criteria[2];
            lbl_certified.Content = Results.ConvertCertified(Results.Criteria[3]);
            lbl_ageValue.Content = Results.Criteria[4];
            lbl_nbrYearExpValue.Content = Results.Criteria[5];
        }
        #endregion

        private void usc_resultsResults_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        #endregion

    }
}
