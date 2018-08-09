#region Application Description
/**********************************************************************\
*                            InterimSearch                            *
*                                                                     *
* Auteur : L. Lassalle                                                *
* Date de création : 12/05/2015                                       *
* Date de révision : 12/05/2015                                       *
* Description :                                                       *
* Application permettant de rechercher les disponibilités             *
* d'intérimaires, de leurs faire une proposition et de les affecter à *
* une mission.                                                        *
\**********************************************************************/
#endregion

#region References
using InterimSearch.User_Controls.Authentication;
using System.Windows;
using System.Windows.Controls;
#endregion

namespace InterimSearch.Page_Switcher
{
    /// <summary>
    /// Logique d'interaction pour wdw_pageSwitcher.xaml
    /// </summary>
    public partial class wdw_pageSwitcher : Window
    {
        #region Constructors
        /// <summary>
        /// Initialise une nouvelle instance de la classe wdw_pageSwitcher.
        /// </summary>
        public wdw_pageSwitcher()
        {
            InitializeComponent();

            Switcher.pageSwitcher = this;

            // Affiche l'interface d'authentification.
            usc_authen authenPageSwitcher = new usc_authen();
            Switcher.Switch(authenPageSwitcher);
        }
        #endregion

        #region Events

        #region wdw_pageSwitcher_Closing
        /// <summary>
        /// Action lors de la fermeture de la fenêtre principale "wdw_nfc_switcher".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wdw_pageSwitcher_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Arrêt de l'application complète.
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        #region Methods

        #region Navigate
        /// <summary>
        /// Navigue entre les différentes interfaces.
        /// </summary>
        /// <param name="_nextPage">Page suivante.</param>
        internal void Navigate(UserControl _nextPage)
        {
            UserControl nextPage = _nextPage;

            // Assigne le contenu de la fenêtre de l'application.
            this.Content = nextPage;
        }
        #endregion

        #endregion

    }
}
