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
using InterimSearch.Page_Switcher;
using InterimSearch.User_Controls.Graphs_And_Stats;
using InterimSearch.User_Controls.Home.Settings;
using InterimSearch.User_Controls.Results;
using InterimSearch.User_Controls.Sent_Offer;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using Visibility = System.Windows.Visibility;
#endregion

namespace InterimSearch.User_Controls.Home
{
    /// <summary>
    /// Logique d'interaction pour usc_home.xaml
    /// </summary>
    public partial class usc_home : UserControl
    {

        #region Constructor

        #region usc_home
        /// <summary>
        /// Initialise une nouvelle instance de la classe usc_home.
        /// </summary>
        public usc_home()
        {
            InitializeComponent();

            // Assigne le nom de l'interface d'accueil à la fenêtre.
            Switcher.ChangeWindowTitle(Home_Val.Default.HomeTitle);

            // Assigne les horaires de travail contenues dans la base de données aux contrôles.
            Home.MergeWorkTimetable(cbo_workTimetable);

            bool isCorrectCBOWorkTimetable = (cbo_workTimetable.ItemsSource != null);

            // Vérifie le contenu de la comboBox.
            if (isCorrectCBOWorkTimetable)
            {
                // Assigne les professions contenues dans la base de données aux contrôles.
                Home.ProfessionList(cbo_profession);

                bool isCorrectCBOProfession = (cbo_profession.ItemsSource != null);

                // Vérifie le contenu de la comboBox.
                if (isCorrectCBOProfession)
                {
                    // Assigne la date du jours en cours au champs corrspondant.
                    txt_date.Text = DateTime.Today.ToShortDateString();

                    // Masque les contrôles représentant les critères supplémentaires.
                    cal_date.Visibility = Visibility.Hidden;
                    VisibilityCriteriaControls(Visibility.Hidden);

                    // Assigne le focus au champs de saisie du mot de passe.
                    txt_date.Focus();
                }
                else
                {
                    // Affichage d'un message d'erreur.
                    MessageBox.Show(Home_Val.Default.LaunchAgain, Home_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Home_Val.Default.LaunchAgain, Home_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region Events

        #region cal_date_SelectedDatesChanged
        /// <summary>
        /// Action lors du changement de la date sélectionnée dans le calendrier graphique.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cal_date_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Assigne la date sélectionnée au champs corrspondant.
            txt_date.Text = cal_date.SelectedDate.Value.ToShortDateString();

            // Masque le calendrier graphique.
            cal_date.Visibility = Visibility.Hidden;
        }
        #endregion

        #region cmd_calendar_Click
        /// <summary>
        /// Action lors du clic sur le bouton "cmd_calendar".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_calendar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool calVisible = (cal_date.Visibility == Visibility.Visible);

            // Vérifie l'affichage du calendrier graphique.
            if (calVisible)
            {
                // Masque le calendrier graphique.
                cal_date.Visibility = Visibility.Hidden;
            }
            else
            {
                // Affiche le calendrier graphique.
                cal_date.Visibility = Visibility.Visible;

                // Masque les contrôles représentant les critères supplémentaires.
                VisibilityCriteriaControls(Visibility.Hidden);
            }
        }
        #endregion

        #region cmd_search_Click
        /// <summary>
        /// Action lors du clic sur le bouton "cmd_search".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_search_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isNullWorkTimetable = (cbo_workTimetable.SelectedItem == null);
            bool isNullProfession = (cbo_profession.SelectedItem == null);
            bool isNullDate = Home.IsNullDate(txt_date.Text);

            // Vérifie la saisie des critères.
            if (isNullWorkTimetable)
            {
                // Affiche un message d'erreur.
                lbl_message.Content = Home_Val.Default.NullWorkTimetable;
            }
            else if (isNullProfession)
            {
                // Affiche un message d'erreur.
                lbl_message.Content = Home_Val.Default.NullProfession;
            }
            else if (isNullDate)
            {
                // Affiche un message d'erreur.
                lbl_message.Content = Home_Val.Default.NullDate;
            }
            else
            {
                // Assigne les critères de recherche à des propriétés.
                CriteriaList();

                // Affiche l'interface des résultats.
                usc_results usc_resultsHome = new usc_results();
                Switcher.Switch(usc_resultsHome);
            }
        }
        #endregion

        #region lbl_addCriteria_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_addCriteria" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_addCriteria_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bool certifiedVisible = (chk_certified.Visibility == Visibility.Visible);

            // Vérifie l'affichage de la case à cochée correspondante.
            if (certifiedVisible)
            {
                // Change le contenu de l'étiquette correspondante.
                lbl_addCriteria.Content = Home_Val.Default.ShowCriteria;

                // Masque les contrôles représentant les critères supplémentaires.
                VisibilityCriteriaControls(Visibility.Hidden);
            }
            else
            {
                // Change le contenu de l'étiquette correspondante.
                lbl_addCriteria.Content = Home_Val.Default.HideCriteria;

                // MAsque le calendier graphique.
                cal_date.Visibility = Visibility.Hidden;

                // Affiche les contrôles représentant les critères supplémentaires.
                VisibilityCriteriaControls(Visibility.Visible);
            }
        }
        #endregion

        #region lbl_graphStat_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_sentOffer" et de sa remontée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_graphStat_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Affiche l'interface des graphiques et statistiques.
            usc_graphStat usc_graphStatHome = new usc_graphStat();
            Switcher.Switch(usc_graphStatHome);
        }
        #endregion

        #region lbl_sentOffer_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_sentOffer" et de sa remontée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_sentOffer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Affiche l'interface des propositions envoyées.
            usc_sentOffer usc_sentOfferHome = new usc_sentOffer();
            Switcher.Switch(usc_sentOfferHome);
        }
        #endregion

        #region txt_date_GotFocus
        /// <summary>
        /// Action lors de la réception du focus sur le champs "txt_date".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_date_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            // Masque le calendrier graphique.
            cal_date.Visibility = Visibility.Hidden;
        }
        #endregion

        #region usc_home_KeyUp
        /// <summary>
        /// Action lors de la remontée d'une touche du clavier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usc_home_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool isKeyEnterPess = (e.Key == Key.Enter);

            // Vérifie la remontée de la touche Enter.
            if (isKeyEnterPess)
            {
                // Actionne l'action du bouton "cmd_search".
                cmd_search_Click(sender, e);
            }
        }
        #endregion

        #endregion

        #region Methods

        #region CriteriaList
        /// <summary>
        /// Assigne les critères à une liste.
        /// </summary>
        private void CriteriaList()
        {
            // Assigne à des chaînes de caratères le contenu des criètres.
            string dateString = txt_date.Text.ToString();
            string workTimetableString = cbo_workTimetable.SelectionBoxItem.ToString();
            string professionString = cbo_profession.SelectionBoxItem.ToString();
            string certifiedString = chk_certified.IsChecked.ToString();
            string categAgeString = cbo_age.SelectionBoxItem.ToString();
            string categNbrYearExpString = cbo_nbrYearExp.SelectionBoxItem.ToString();

            // Regroupe les critères dans une liste de chaînes de caractères.
            Home.CriteriaList(dateString, workTimetableString, professionString, certifiedString, categAgeString, categNbrYearExpString);
        }
        #endregion

        #region VisibilityControls
        /// <summary>
        /// Assigne un état d'affichage de plusieurs contrôles. 
        /// </summary>
        /// <param name="_visibility">Etat d'affihage</param>
        private void VisibilityCriteriaControls(Visibility _visibility)
        {
            Visibility visibility = _visibility;

            chk_certified.Visibility = visibility;
            lbl_age.Visibility = visibility;
            cbo_age.Visibility = visibility;
            lbl_nbrYearExp.Visibility = visibility;
            cbo_nbrYearExp.Visibility = visibility;
        }
        #endregion

        #endregion

    }
}
