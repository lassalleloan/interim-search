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
using InterimSearch.User_Controls.Authentication.Settings;
using InterimSearch.User_Controls.Home;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#endregion

namespace InterimSearch.User_Controls.Authentication
{
    /// <summary>
    /// Logique d'interaction pour usc_authen.xaml
    /// </summary>
    public partial class usc_authen : UserControl
    {

        #region Constructors

        #region usc_authen
        /// <summary>
        /// Initialise une nouvelle instance de la classe usc_authen.
        /// </summary>
        public usc_authen()
        {
            InitializeComponent();

            // Assigne le nom de l'interface d'authentification à la fenêtre.
            Switcher.ChangeWindowTitle(Authen_Val.Default.AuthenTitle);

            // Assigne le nom d'utilisateur de la session Windows au champs de saisie correspondant.
            txt_username.Text = Environment.UserName;

            // Assigne le focus au champs de saisie du mot de passe.
            pwd_password.Focus();
        }
        #endregion

        #endregion

        #region Events

        #region cmd_connection_Click
        /// <summary>
        /// Action Lors du clic sur le bouton "cmd_connection".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_connection_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie si l'utilisateur est authentifié.
            if (Authen.IsUserAuthenticated(pwd_password.Password))
            {
                // Affiche l'interface d'accueil.
                usc_home usc_homeAuthen = new usc_home();
                Switcher.Switch(usc_homeAuthen);
            }
            else
            {
                // Affiche un message d'erreur.
                lbl_message.Content = Authen_Val.Default.NullPassword;
            }
        }
        #endregion

        #region usc_authen_KeyUp
        /// <summary>
        /// Action lors de la remontée d'une touche du clavier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usc_authen_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool isKeyEnterPess = (e.Key == Key.Enter);

            // Vérifie la remontée de la touche Enter.
            if (isKeyEnterPess)
            {
                // Actionne l'action du bouton "cmd_connection".
                cmd_connection_Click(sender, e);
            }
        }
        #endregion

        #endregion

    }
}
