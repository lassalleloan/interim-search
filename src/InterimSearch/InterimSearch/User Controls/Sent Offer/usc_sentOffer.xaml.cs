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
using InterimSearch.User_Controls.Home;
using InterimSearch.User_Controls.Sent_Offer.Settings;
using System.Windows.Controls;
#endregion

namespace InterimSearch.User_Controls.Sent_Offer
{
    /// <summary>
    /// Logique d'interaction pour usc_sentOffer.xaml
    /// </summary>
    public partial class usc_sentOffer : UserControl
    {

        #region Constructors

        #region usc_sentOffer
        /// <summary>
        /// Initialise une nouvelle instance de la classe "usc_sentOffer".
        /// </summary>
        public usc_sentOffer()
        {
            InitializeComponent();

            // Assigne le nom de l'interface des propositions envoyées à la fenêtre.
            Switcher.ChangeWindowTitle(SentOffer_Val.Default.SentOfferTitle);

            // Ajoute des contrôles pour visualiser des données.
            SentOffer.AddSentOfferControls(stp_dynamicsBase);

            // Vérifie si il existe des résultats.
            if (stp_dynamicsBase.Children.Count == 0)
            {
                // Ajoute un contrôle lors du premier lancement de l'application.
                Label lbl_anyDataControl = SentOffer.AddAnyAvaibilityDateControl();
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
            usc_home usc_homeSentOffer = new usc_home();
            Switcher.Switch(usc_homeSentOffer);
        }
        #endregion

        #endregion

    }
}
