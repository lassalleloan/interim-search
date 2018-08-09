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
using InterimSearch.User_Controls.Profile;
using InterimSearch.User_Controls.Results.Settings;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DialogResult = System.Windows.Forms.DialogResult;
using Label = System.Windows.Controls.Label;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using Orientation = System.Windows.Controls.Orientation;
#endregion

namespace InterimSearch.User_Controls.Results
{
    public partial class Results
    {
        #region Properties

        #region Criteria
        /// <summary>
        /// Contient les critères de recherches à afficher.
        /// </summary>
        public static List<string> Criteria { get; set; }
        #endregion

        #region TemporaryID
        /// <summary>
        /// Contient l'ID de l'intérimaire sélectionné.
        /// </summary>
        private static string TemporaryID { get; set; }
        #endregion

        #endregion

        #region Events

        #region lbl_labelBase_MouseEnter
        /// <summary>
        /// Action lors de l'entrée sur l'étiquette "lbl_labelBase".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_labelBase_MouseEnter(object sender, MouseEventArgs e)
        {
            Label lbl_labelBase = sender as Label;
            Storyboard storyboard = new Storyboard();

            // Assigne une échelle de transformation.
            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            lbl_labelBase.RenderTransformOrigin = new Point(1, 1);
            lbl_labelBase.RenderTransform = scale;

            // Assigne une animation de grossissement pour l'axe X.
            DoubleAnimation growAnimationX = new DoubleAnimation();
            growAnimationX.Duration = TimeSpan.FromMilliseconds(200);
            growAnimationX.To = 1.2;
            storyboard.Children.Add(growAnimationX);

            Storyboard.SetTargetProperty(growAnimationX, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimationX, lbl_labelBase);

            // Assigne une animation de grossissement pour l'axe Y.
            DoubleAnimation growAnimationY = new DoubleAnimation();
            growAnimationY.Duration = TimeSpan.FromMilliseconds(200);
            growAnimationY.To = 1.2;
            storyboard.Children.Add(growAnimationY);

            Storyboard.SetTargetProperty(growAnimationY, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growAnimationY, lbl_labelBase);

            storyboard.Begin();
        }
        #endregion

        #region lbl_labelBase_MouseLeave
        /// <summary>
        /// Action lors de la sortie sur l'étiquette "lbl_labelBase".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_labelBase_MouseLeave(object sender, MouseEventArgs e)
        {
            Label lbl_labelBase = sender as Label;
            Storyboard storyboard = new Storyboard();

            // Assigne une échelle de transformation.
            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            lbl_labelBase.RenderTransformOrigin = new Point(1, 1);
            lbl_labelBase.RenderTransform = scale;

            // Assigne une animation de grossissement pour l'axe X.
            DoubleAnimation growAnimationX = new DoubleAnimation();
            growAnimationX.Duration = TimeSpan.FromMilliseconds(200);
            growAnimationX.From = 1.2;
            growAnimationX.To = 1.0;
            storyboard.Children.Add(growAnimationX);

            Storyboard.SetTargetProperty(growAnimationX, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimationX, lbl_labelBase);

            // Assigne une animation de grossissement pour l'axe Y.
            DoubleAnimation growAnimationY = new DoubleAnimation();
            growAnimationY.Duration = TimeSpan.FromMilliseconds(200);
            growAnimationY.From = 1.2;
            growAnimationY.To = 1.0;
            storyboard.Children.Add(growAnimationY);

            Storyboard.SetTargetProperty(growAnimationY, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growAnimationY, lbl_labelBase);

            storyboard.Begin();
        }
        #endregion

        #region lbl_MenuItemSelected_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur le menuItem sélectionné du contextMenu du label "lbl_sendOffer".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_MenuItemSelected_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string serviceName = clickedMenuItem.Header.ToString();
            string temporaryTitle = InterimDatabase.GetTemporaryTitle(TemporaryID);
            string temporaryName = InterimDatabase.GetTemporaryName(TemporaryID);
            bool isCorrectServiceName = (!string.IsNullOrEmpty(serviceName));
            bool isCorrectTemporaryTitle = (!string.IsNullOrEmpty(temporaryTitle));
            bool isCorrectTemporaryName = (!string.IsNullOrEmpty(temporaryName));

            // Vérifie le contenu des informations de l'intérimaire.
            if (isCorrectTemporaryTitle && isCorrectTemporaryName && isCorrectServiceName)
            {
                string messageBox = Results_Val.Default.ConfirmSendEmail + temporaryTitle + " " + temporaryName + Results_Val.Default.ForServiceOf + serviceName + Results_Val.Default.Ask;

                // Message de confirmation.
                DialogResult dialogResult = MessageBox.Show(messageBox, Results_Val.Default.TitleConfirmSendEmail, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Vérifie la réponse au message de confirmation.
                if (dialogResult == DialogResult.Yes)
                {
                    string temporaryEmailAdress = InterimDatabase.GetTemporaryEmailAdress(TemporaryID);
                    bool isCorrectEmailAdress = (!string.IsNullOrEmpty(temporaryEmailAdress));

                    // Vérifie le contenu de l'adresse email récupérée.
                    if (isCorrectEmailAdress)
                    {
                        string dayName = InterimDatabase.DayName();
                        string dayNumber = InterimDatabase.DayNumber();
                        string month = InterimDatabase.MonthName();
                        string workTimetableStart = InterimDatabase.ReturnWorkTimetableStart();
                        string workTimetableEnd = InterimDatabase.ReturnWorkTimetableEnd();

                        List<string> offerInformations = new List<string>();

                        offerInformations.Add(temporaryEmailAdress);
                        offerInformations.Add(temporaryTitle);
                        offerInformations.Add(temporaryName);
                        offerInformations.Add(dayName);
                        offerInformations.Add(dayNumber);
                        offerInformations.Add(month);
                        offerInformations.Add(workTimetableStart);
                        offerInformations.Add(workTimetableEnd);
                        offerInformations.Add(serviceName);

                        // Crée un message de proposition de mission.
                        List<string> bodyMessageInformations = CreateBodyMessage(Results_Code.Default.TemplateOfferPath, offerInformations);
                        _MailItem message = CreateMessage(bodyMessageInformations);

                        // Vérifie la création du message, l'envoi de l'email de proposition de mission, l'enregistrement de la date d'envoi et du service de la proposition de mission.
                        if (IsCreateMessage(message) && IsSentEmail(message) && InterimDatabase.SetSendDateAndService(TemporaryID, serviceName))
                        {
                            // Affiche un message de réussite de l'envoi de l'email.
                            MessageBox.Show(Results_Val.Default.EmailSent, Results_Val.Default.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Rafraichit l'interface des résultats.
                            usc_results refreshResults = new usc_results();
                            Switcher.Switch(refreshResults);
                        }
                        else
                        {
                            // Affiche un message d'échec de l'envoi de l'email de proposition de mission.
                            MessageBox.Show(Results_Val.Default.NullSendEmail, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Affiche un message d'erreur.
                        MessageBox.Show(Results_Val.Default.NullEmailAdress, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Results_Val.Default.NullInformations, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region lbl_profile_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_profile".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_profile_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Label lbl_clickedLabel = sender as Label;
            string temporaryID = lbl_clickedLabel.Name.Substring(Results_Code.Default.TemporaryID.Length);

            // Affiche l'interface de profil d'un intérimaire.
            wdw_profile wdw_profileResults = new wdw_profile(temporaryID);
        }
        #endregion

        #region lbl_sendOffer_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_sendOffer".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_sendOffer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool isOutlookExist = (Type.GetTypeFromProgID(Results_Code.Default.OutlookApplication) != null);

            // Vérifie l'existence du client de messagerie Microsoft Outlook.
            if (isOutlookExist)
            {
                Label lbl_clickedLabel = sender as Label;
                bool isCorrectContextMenu = (lbl_clickedLabel.ContextMenu != null);
                TemporaryID = lbl_clickedLabel.Name.Substring(Results_Code.Default.TemporaryID.Length);

                // Vérifie le contenu du menu contextuel du label "lbl_sendOffer".
                if (isCorrectContextMenu)
                {
                    lbl_clickedLabel.ContextMenu.PlacementTarget = lbl_clickedLabel;
                    lbl_clickedLabel.ContextMenu.IsOpen = true;
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Results_Val.Default.NullOutlookApplication, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region Methods

        #region AddAnyProfilResumeControl
        /// <summary>
        /// Ajoute un contrôle si il n'existe aucun profil d'intérimaire répondant aux critères.
        /// </summary>
        /// <returns>Label : Etiquette pour afficher l'existance d'aucun profil d'intérimaire répondant aux critères.</returns>
        internal static Label AddAnyProfilResumeControl()
        {
            // Assigne à une étiquette des propriétés graphique.
            Label lbl_anyItem = LabelBaseBuild(Results_Val.Default.lbl_anyItem_Content, 540, 40);
            lbl_anyItem.Margin = new Thickness(285, 180, 0, 0);
            lbl_anyItem.FontSize = 20;
            lbl_anyItem.FontWeight = FontWeights.Bold;
            lbl_anyItem.HorizontalContentAlignment = HorizontalAlignment.Center;

            return lbl_anyItem;
        }
        #endregion

        #region AddProfilResumeControls
        /// <summary>
        /// Ajoute les contrôles pour les résumés des profils.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StackPanel de base</param>
        internal static void AddProfilResumeControls(StackPanel _stp_dynamicsBase)
        {
            // Assigne à une liste, les ID des intérimaires.
            List<string> temporaryIDList = InterimDatabase.GetTemporaryID();
            bool isCorrectIDList = IsCorrectList(temporaryIDList);

            // Vérifie le contenu de la liste.
            if (isCorrectIDList)
            {
                List<string> temporaryNameList = new List<string>();
                List<string> temporaryTitleList = new List<string>();
                List<string> temporaryFirstNameList = new List<string>();
                List<string> temporaryAgeList = new List<string>();
                List<string> temporaryNbrYearExpList = new List<string>();
                List<string> temporaryCityNameList = new List<string>();

                // Assigne à des listes, les informations de chaque intérimaire.
                foreach(string temporaryID in temporaryIDList)
                {
                    temporaryTitleList.Add(InterimDatabase.GetTemporaryTitle(temporaryID));
                    temporaryNameList.Add(InterimDatabase.GetTemporaryName(temporaryID));
                    temporaryFirstNameList.Add(InterimDatabase.GetTemporaryFirstName(temporaryID));
                    temporaryAgeList.Add(InterimDatabase.GetTemporaryAge(temporaryID));
                    temporaryNbrYearExpList.Add(InterimDatabase.GetTemporaryNbrYearExp(temporaryID));
                    temporaryCityNameList.Add(InterimDatabase.GetTemporaryCity(temporaryID));
                }

                bool isCorrectTitleList = IsCorrectList(temporaryTitleList);
                bool isCorrectNameList = IsCorrectList(temporaryNameList);
                bool isCorrectFirstNameList = IsCorrectList(temporaryFirstNameList);
                bool isCorrectAgeList = IsCorrectList(temporaryAgeList);
                bool isCorrectNbrYearExpList = IsCorrectList(temporaryNbrYearExpList);
                bool isCorrectCityNameList = IsCorrectList(temporaryCityNameList);

                // Vérifie le contenu des listes.
                if (isCorrectIDList && isCorrectTitleList && isCorrectNameList && isCorrectFirstNameList && isCorrectAgeList && isCorrectNbrYearExpList && isCorrectCityNameList)
                {
                    // Assigne à un stackPanel, les sections des intérimaires certifiés et non certifiés.
                    Sections(_stp_dynamicsBase);

                    for (int index = 0; index < temporaryNameList.Count; index++)
                    {
                        // Assigne à un stackPanel de contrôle de l'intérimaire, des propriétés graphiques.
                        StackPanel stp_TemporaryData = StackPanelBaseBuild(_stp_dynamicsBase, Orientation.Horizontal);

                        // Assigne à une étiquette, le titre de civilité d'un intérimaire.
                        TemporaryTitle(temporaryTitleList[index], stp_TemporaryData);

                        // Assigne à une étiquette, le nom d'un intérimaire.
                        TemporaryName(temporaryNameList[index], stp_TemporaryData);

                        // Assigne à une étiquette, le prénom d'un intérimaire.
                        TemporaryFirstName(temporaryFirstNameList[index], stp_TemporaryData);

                        // Assigne à une étiquette, l'âge d'un intérimaire.
                        TemporaryAge(temporaryAgeList[index], stp_TemporaryData);

                        // Assigne à une étiquette, le nombre d'années d'expérience d'un intérimaire.
                        TemporaryNbrYearExp(temporaryNbrYearExpList[index], stp_TemporaryData);

                        // Assigne à une étiquette, la ville d'un intérimaire.
                        TemporaryCity(temporaryCityNameList[index], stp_TemporaryData);

                        // Assigne à une étiquette, l'envoi d'une proposition de mission.
                        TemporarySendOffer(temporaryIDList[index], stp_TemporaryData);

                        // Assigne à une étiquette, la consultation du profil.
                        TemporaryProfile(temporaryIDList[index], stp_TemporaryData);
                    }
                }
            }
        }
        #endregion

        #region AddServicesControls
        /// <summary>s
        /// Ajoute des contrôles pour visualiser les services.
        /// </summary>
        /// <param name="_lbl_serviceNameContextMenu">Context Menu</param>
        private static void AddServicesControls(ContextMenu _lbl_serviceNameContextMenu)
        {
            // Assigne à une liste les noms des services.
            List<string> serviceName = InterimDatabase.GetService();
            bool isCorrectServices = (serviceName != null);

            // Vérifie le contenu de la liste des noms de services.
            if (isCorrectServices)
            {
                foreach (string element in serviceName)
                {
                    // Assigne à un menu contextuel, un menu d'éléments.
                    MenuItem lbl_MenuItemService = new MenuItem();

                    lbl_MenuItemService.Header = element;
                    lbl_MenuItemService.Click += new RoutedEventHandler(lbl_MenuItemSelected_MouseLeftButtonUp);
                    _lbl_serviceNameContextMenu.Items.Add(lbl_MenuItemService);
                }
            }
        }
        #endregion

        #region CertifiedString
        /// <summary>
        /// Interprète le contenu de la certification.
        /// </summary>
        /// <param name="_certifiedString">Certification</param>
        /// <returns>String : Chaîne de caractères spécifique pour le critère de certification.</returns>
        internal static string ConvertCertified(string _certifiedString)
        {
            string certified = _certifiedString;
            bool isCertified = (certified == Results_Code.Default.CertifiedString);

            // Vérifie le contenu d'une chaîne de caractères.
            if (isCertified)
            {
                return Results_Val.Default.CertifiedEmployer;
            }
            else
            {
                return Results_Val.Default.CertifiedAll;
            }
        }
        #endregion

        #endregion

    }
}
