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
using InterimSearch.User_Controls.Results.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace InterimSearch.User_Controls.Results
{
    public partial class Results
    {

        #region Methods

        #region TemporaryCity
        /// <summary>
        /// Assigne à une étiquette, nom de la ville de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryCityNameList">Nom de la ville de l'intérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporaryCity(string _temporaryCityName, StackPanel _stp_TemporaryData)
        {
            string temporaryCityName = _temporaryCityName;
            Label lbl_temporaryCity = LabelBaseBuild(temporaryCityName, 130);

            // Assigne à une étiquette une propriété graphique.
            lbl_temporaryCity.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryCity);
        }
        #endregion

        #region TemporaryFirstName
        /// <summary>
        /// Assigne à une étiquette, le prénom de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryFirstName">Prénom de l'intrérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporaryFirstName(string _temporaryFirstName, StackPanel _stp_TemporaryData)
        {
            string temporaryFirstName = _temporaryFirstName;
            Label lbl_temporaryFirstName = LabelBaseBuild(temporaryFirstName, 130);

            // Assigne à une étiquette une propriété graphique.
            lbl_temporaryFirstName.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryFirstName);
        }
        #endregion

        #region TemporaryName
        /// <summary>
        /// Assigne à une étiquette, le nom de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryName">Nom de l'intrérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporaryName(string _temporaryName, StackPanel _stp_TemporaryData)
        {
            string temporaryName = _temporaryName;
            Label lbl_temporaryName = LabelBaseBuild(temporaryName, 147);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_temporaryName.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryName);
        }
        #endregion

        #region TemporaryNbrYearExp
        /// <summary>
        /// Assigne à des étiquettes, Nombre d'années d'expériences de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryNbrYearExp">Nombre d'années d'expériences de l'intrérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporaryNbrYearExp(string _temporaryNbrYearExp, StackPanel _stp_TemporaryData)
        {
            string temporaryNbrYearExp = _temporaryNbrYearExp;

            // Assigne à une étiquette, le nombre d'années d'expériences de l'intérimaire.
            Label lbl_temporaryNbrYearExp = LabelBaseBuild(Results_Val.Default.lbl_temporaryNbrYearExp, 180);
            Label lbl_temporaryNbrYearExpValue = LabelBaseBuild(temporaryNbrYearExp + Results_Val.Default.Year, 52);

            // Assigne à une étiquette une propriété graphique.
            lbl_temporaryNbrYearExp.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute les étiquettes au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryNbrYearExp);
            _stp_TemporaryData.Children.Add(lbl_temporaryNbrYearExpValue);
        }
        #endregion

        #region TemporaryProfile
        /// <summary>
        /// Assigne à une étiquette, la consultation du profil.
        /// </summary>
        /// <param name="_temporaryID">Liste des ID des intrérimaires</param>
        /// <param name="_stp_TemporaryData">Stackpanel de chaque intérimaire</param>
        private static void TemporaryProfile(string _temporaryID, StackPanel _stp_TemporaryData)
        {
            string temporaryID = _temporaryID;
            Label lbl_profile = LabelBaseBuild(Results_Val.Default.lbl_profile, 108);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_profile.Width = double.NaN;
            lbl_profile.Margin = new Thickness(22, 0, 0, 0);
            lbl_profile.BorderBrush = Brushes.Black;
            lbl_profile.BorderThickness = new Thickness(0, 0, 0, 1);
            lbl_profile.Name = Results_Code.Default.TemporaryID + temporaryID;
            lbl_profile.MouseLeftButtonUp += new MouseButtonEventHandler(lbl_profile_MouseLeftButtonUp);
            lbl_profile.MouseEnter += new MouseEventHandler(lbl_labelBase_MouseEnter);
            lbl_profile.MouseLeave += new MouseEventHandler(lbl_labelBase_MouseLeave);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_profile);
        }
        #endregion

        #region TemporarySendOffer
        /// <summary>
        /// Assigne à une étiquette, l'envoi d'une proposition de mission.
        /// </summary>
        /// <param name="_temporaryID">ID de l'intérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporarySendOffer(string _temporaryID, StackPanel _stp_TemporaryData)
        {
            string temporaryID = _temporaryID;
            Label lbl_sendOffer = LabelBaseBuild(Results_Val.Default.lbl_sendOffer, 130);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_sendOffer.Width = double.NaN;
            lbl_sendOffer.Margin = new Thickness(20, 0, 0, 0);
            lbl_sendOffer.BorderBrush = Brushes.Black;
            lbl_sendOffer.BorderThickness = new Thickness(0, 0, 0, 1);
            lbl_sendOffer.Name = Results_Code.Default.TemporaryID + temporaryID;
            lbl_sendOffer.MouseLeftButtonUp += new MouseButtonEventHandler(lbl_sendOffer_MouseLeftButtonUp);
            lbl_sendOffer.MouseEnter += new MouseEventHandler(lbl_labelBase_MouseEnter);
            lbl_sendOffer.MouseLeave += new MouseEventHandler(lbl_labelBase_MouseLeave);

            // Assigne à une étiquette, un menu contextuel.
            ContextMenu lbl_serviceNameContextMenu = new ContextMenu();
            lbl_sendOffer.ContextMenu = lbl_serviceNameContextMenu;

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_sendOffer);

            // Ajoute les contrôles pour les services.
            AddServicesControls(lbl_serviceNameContextMenu);
        }
        #endregion

        #region TemporaryTitle
        /// <summary>
        /// Assigne à une étiquette, le titre de civilité de l'intérimaire.
        /// </summary>
        /// <param name="temporaryTitle">Titres de civilité de l'intrérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de chaque intérimaire</param>
        private static void TemporaryTitle(string _temporaryTitle, StackPanel _stp_TemporaryData)
        {
            string temporaryTitle = _temporaryTitle;
            Label lbl_temporaryTitle = LabelBaseBuild(temporaryTitle, 40);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryTitle);
        }
        #endregion

        #endregion

    }
}
