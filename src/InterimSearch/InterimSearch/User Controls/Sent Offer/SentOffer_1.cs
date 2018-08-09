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
using InterimSearch.User_Controls.Sent_Offer.Settings;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;

#endregion

namespace InterimSearch.User_Controls.Sent_Offer
{
    public partial class SentOffer
    {

        #region Events

        #region lbl_abortMission_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_abortMission".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_abortMission_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            bool isOutlookExist = (Type.GetTypeFromProgID("Outlook.Application") != null);

            // Vérifie l'existence du client de messagerie Microsoft Outlook.
            if (isOutlookExist)
            {
                MenuItem clickedMenuItem = sender as MenuItem;
                string avaibilityID = clickedMenuItem.Name.Substring(SentOffer_Code.Default.lbl_answerOffer_Name.Length);
                List<string> avaibilityIDAndMessage = new List<string>();

                avaibilityIDAndMessage.Add(avaibilityID);
                avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmAbortMission);
                avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmAbortMissionTitle);

                // Obtient les informations d'annulation de l'email à envoyer.
                List<string> abortInformations = EmailInformations(avaibilityIDAndMessage);

                // Vérifie le contenu de la liste.
                if (IsCorrectList(abortInformations))
                {
                    // Envoi l'email d'annulation de mission.
                    AbortEmail(abortInformations, avaibilityID);
                }

                // Rafraichit l'interface des propositions envoyées.
                usc_sentOffer refreshSentOffer = new usc_sentOffer();
                Switcher.Switch(refreshSentOffer);
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Val.Default.NullOutlookApplication, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region lbl_acceptMission_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_acceptMission".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_acceptMission_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool isOutlookExist = (Type.GetTypeFromProgID(SentOffer_Code.Default.OutlookApplication) != null);

            // Vérifie l'existence du client de messagerie Microsoft Outlook.
            if (isOutlookExist)
            {
                Label lbl_clickedLabel = sender as Label;
                string avaibilityID = lbl_clickedLabel.Name.Substring(SentOffer_Code.Default.lbl_answerOffer_Name.Length);
                List<string> avaibilityIDAndMessage = new List<string>();

                avaibilityIDAndMessage.Add(avaibilityID);
                avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmAcceptMission);
                avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmAcceptMissionTitle);

                // Obtient les informations d'acceptation de l'email à envoyer.
                List<string> acceptInformations = EmailInformations(avaibilityIDAndMessage);

                // Vérifie le contenu de la liste.
                if (IsCorrectList(acceptInformations))
                {
                    // Envoi l'email de confirmation d'acceptation.
                    AcceptEmail(acceptInformations, avaibilityID);

                    // Obtient les ID des disponibilités annulées.
                    List<string> abortAvaibilityIDList = InterimDatabase.GetAbortAvibilityID(avaibilityID);

                    // Vérifie le contenu de la liste.
                    if (IsCorrectList(abortAvaibilityIDList))
                    {
                        foreach (string abortAvaibilityID in abortAvaibilityIDList)
                        {
                            // Obtient les informations de l'email à envoyer.
                            List<string> abortInformations = AbortEmailInformations(abortAvaibilityID);

                            AbortEmail(abortInformations, abortAvaibilityID, false);
                        }
                    }
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Val.Default.NullOutlookApplication, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Rafraichit l'interface des propositions envoyées.
            usc_sentOffer refreshSentOffer = new usc_sentOffer();
            Switcher.Switch(refreshSentOffer);
        }
        #endregion

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

        #region lbl_otherActionMission_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_otherActionMission".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_otherActionMission_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool isOutlookExist = (Type.GetTypeFromProgID(SentOffer_Code.Default.OutlookApplication) != null);

            // Vérifie l'existence du client de messagerie Microsoft Outlook.
            if (isOutlookExist)
            {
                Label lbl_clickedLabel = sender as Label;
                bool isCorrectContextMenu = (lbl_clickedLabel.ContextMenu != null);

                // Vérifie le contenu du menu contextuel du label "lbl_otherActionMission".
                if (isCorrectContextMenu)
                {
                    lbl_clickedLabel.ContextMenu.PlacementTarget = lbl_clickedLabel;
                    lbl_clickedLabel.ContextMenu.IsOpen = true;
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Val.Default.NullOutlookApplication, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region lbl_rejectMission_MouseLeftButtonUp
        /// <summary>
        /// Action lors du clic sur l'étiquette "lbl_rejectMission".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void lbl_rejectMission_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = sender as MenuItem;
            string avaibilityID = clickedMenuItem.Name.Substring(SentOffer_Code.Default.lbl_answerOffer_Name.Length);
            List<string> avaibilityIDAndMessage = new List<string>();

            avaibilityIDAndMessage.Add(avaibilityID);
            avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmRejectMission);
            avaibilityIDAndMessage.Add(SentOffer_Val.Default.ConfirmRejectMissionTitle);

            // Obtient les informations de refus de mission de l'email à envoyer.
            List<string> rejectInformations = EmailInformations(avaibilityIDAndMessage);            

            // Vérifie le contenu de la liste.
            if (IsCorrectList(rejectInformations))
            {
                // Envoi l'email de confirmation refus de mission.
                RejectEmail(rejectInformations);
            }

            // Rafraichit l'interface des propositions envoyées.
            usc_sentOffer refreshSentOffer = new usc_sentOffer();
            Switcher.Switch(refreshSentOffer);
        }
        #endregion

        #endregion

        #region Methods

        #region AbortEmail
        /// <summary>
        /// Envoi un email d'annulation de la mission.
        /// </summary>
        /// <param name="_abortInformations">Informations de l'intérimaire</param>
        /// <param name="_avaibilityID">ID avibility</param>
        /// <param name="_oneSend">Un seul envoi</param>
        private static void AbortEmail(List<string> _abortInformations, string _avaibilityID, bool _oneSend = true)
        {
            List<string> abortInformations = _abortInformations;
            bool onseSend = _oneSend;

            if (IsCorrectList(abortInformations))
            {
                // Crée un message de confirmation d'accepation de mission.
                List<string> bodyMessage = CreateBodyMessage(SentOffer_Code.Default.TemplateAbortPath, abortInformations);

                // Vérifie le contenu du corps de message.
                if (IsCorrectList(bodyMessage))
                {
                    _MailItem message = CreateMessage(bodyMessage);

                    // Vérifie la création du message, l'envoi de l'email de confirmation et l'enregistrement de l'acceptation de mission.
                    if (IsCreateMessage(message) && IsSentEmail(message) && InterimDatabase.SetAbortMission(abortInformations[0]))
                    {
                        if (onseSend)
                        {
                            // Affiche un message de réussite de l'envoi de l'email.
                            MessageBox.Show(SentOffer_Val.Default.EmailSent, SentOffer_Val.Default.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Affiche un message d'erreur.
                        MessageBox.Show(SentOffer_Val.Default.NullSendEmail, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Affiche un message d'erreur.
                    MessageBox.Show(SentOffer_Err.Default.CreateMessage, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region AbortEmailInformations
        /// <summary>
        /// Obtient les informations pour les emails d'annulation à envoyer.
        /// </summary>
        /// <param name="_avaibilityID">ID de la diponibilité</param>
        /// <returns>List : Réussite -> Informations de l'email. Echec -> Valeur null</returns>
        private static List<string> AbortEmailInformations(string _avaibilityID)
        {
            string avaibilityID = _avaibilityID;
            string temporaryID = InterimDatabase.GetAvaibilityTemporaryID(avaibilityID);
            bool isCorrectTemporaryID = (!string.IsNullOrEmpty(temporaryID));

            // Vérifie le contenu de l'ID de l'intérimaire.
            if (isCorrectTemporaryID)
            {
                string temporaryTitle = InterimDatabase.GetTemporaryTitle(temporaryID);
                string temporaryName = InterimDatabase.GetTemporaryName(temporaryID);
                string temporaryEmailAdress = InterimDatabase.GetTemporaryEmailAdress(temporaryID);
                string date = InterimDatabase.GetTemporaryDate(avaibilityID);
                bool isCorrectTemporaryTitle = (!string.IsNullOrEmpty(temporaryTitle));
                bool isCorrectTemporaryName = (!string.IsNullOrEmpty(temporaryName));
                bool isCorrectEmailAdress = (!string.IsNullOrEmpty(temporaryEmailAdress));
                bool isCorrectDate = (!string.IsNullOrEmpty(date));

                // Vérifie le contenu des informations de l'intérimaire.
                if (isCorrectTemporaryTitle && isCorrectTemporaryName && isCorrectEmailAdress && isCorrectDate)
                {
                    string dayName = DayName(date);
                    string dayNumber = DayNumber(date);
                    string month = MonthName(date);
                    string workTimetableStart = InterimDatabase.GetTemporaryWorkTimetableStart(avaibilityID);
                    string workTimetableEnd = InterimDatabase.GetTemporaryWorkTimetableEnd(avaibilityID);
                    string serviceName = InterimDatabase.GetTemporaryService(avaibilityID);
                    string sendDate = InterimDatabase.GetTemporarySendDate(avaibilityID);
                    bool isCorrectWorkTimetableStart = (!string.IsNullOrEmpty(workTimetableStart));
                    bool isCorrectWorkTimetableEnd = (!string.IsNullOrEmpty(workTimetableEnd));
                    bool isCorrectServiceName = (!string.IsNullOrEmpty(serviceName));
                    bool isCorrectSendDate = (!string.IsNullOrEmpty(sendDate));

                    // Vérifie le contenu des informations de la mission.
                    if (isCorrectWorkTimetableStart && isCorrectWorkTimetableEnd && isCorrectServiceName && isCorrectSendDate)
                    {
                        List<string> emailInformations = new List<string>();

                        emailInformations.Add(avaibilityID);
                        emailInformations.Add(temporaryEmailAdress);
                        emailInformations.Add(temporaryTitle);
                        emailInformations.Add(temporaryName);
                        emailInformations.Add(dayName);
                        emailInformations.Add(dayNumber);
                        emailInformations.Add(month);
                        emailInformations.Add(workTimetableStart);
                        emailInformations.Add(workTimetableEnd);
                        emailInformations.Add(serviceName);
                        emailInformations.Add(sendDate);

                        return emailInformations;
                    }
                    else
                    {
                        // Affiche un message d'erreur.
                        MessageBox.Show(SentOffer_Val.Default.NullAvaibilityInformations, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    // Affiche un message d'erreur.
                    MessageBox.Show(SentOffer_Val.Default.NullInformations, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Val.Default.NullTemporaryID, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        #endregion

        #region AcceptedOrRejectedMission
        /// <summary>
        /// Assigne à une étiquette, l'état acceptée ou refusée à la proposition de mission.
        /// </summary>
        /// <param name="_stp_TemporaryData">StackPanel</param>
        /// <param name="_temporaryAcceptedMission">Etat de la proposition de mission</param>
        private static void AcceptedOrRejectedMission(StackPanel _stp_TemporaryData, string _temporaryAcceptedMission)
        {
            string temporaryAcceptedMission = _temporaryAcceptedMission;
            bool isAcceptedMission = (temporaryAcceptedMission == SentOffer_Code.Default.True);
            string stateMissionContent = string.Empty;
            Label lbl_stateMission = new Label();
            SolidColorBrush color = new SolidColorBrush();

            // Vérifie le contenu du statut de l'acceptation de la mission.
            if (isAcceptedMission)
            {
                stateMissionContent = SentOffer_Val.Default.AcceptedMission;
                color = Brushes.Green;
            }
            else
            {
                stateMissionContent = SentOffer_Val.Default.RejectedMission;
                color = Brushes.Red;
            }

            // Assigne à une étiquette des propriétés graphiques.
            lbl_stateMission = LabelBaseBuild(stateMissionContent, 220);
            lbl_stateMission.Margin = new Thickness(10, 0, 0, 0);
            lbl_stateMission.FontWeight = FontWeights.Bold;
            lbl_stateMission.Foreground = color;
            lbl_stateMission.HorizontalContentAlignment = HorizontalAlignment.Center;

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_stateMission);
        }
        #endregion

        #region AcceptEmail
        /// <summary>
        /// Envoi un email de confirmation de la mission.
        /// </summary>
        /// <param name="_acceptInformations">Informations de l'intérimaire</param>
        /// <param name="_avaibilityID">ID avibility</param>
        private static void AcceptEmail(List<string> _acceptInformations, string _avaibilityID)
        {
            List<string> acceptInformations = _acceptInformations;

            // Vérifie le contenu de la liste.
            if (IsCorrectList(acceptInformations))
            {
                // Crée un message de confirmation d'accepation de mission.
                List<string> bodyMessage = CreateBodyMessage(SentOffer_Code.Default.TemplateAcceptPath, acceptInformations);

                // Vérifie le contenu du corps de message.
                if (IsCorrectList(bodyMessage))
                {
                    _MailItem message = CreateMessage(bodyMessage);

                    // Vérifie la création du message, l'envoi de l'email de confirmation et l'enregistrement de l'acceptation de mission.
                    if (IsCreateMessage(message) && IsSentEmail(message) && InterimDatabase.SetStateMission(acceptInformations[0], 1))
                    {
                        // Affiche un message de réussite de l'envoi de l'email.
                        MessageBox.Show(SentOffer_Val.Default.EmailSent, SentOffer_Val.Default.InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Affiche un message d'erreur.
                        MessageBox.Show(SentOffer_Val.Default.NullSendEmail, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Affiche un message d'erreur.
                    MessageBox.Show(SentOffer_Err.Default.CreateMessage, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region AddAnyAvaibilityDateControl
        /// <summary>
        /// Ajoute un contrôle si il n'existe aucun profil d'intérimaire ayant reçu le proposition de mission.
        /// </summary>
        /// <returns>Label : Etiquette pour afficher l'existance d'aucun profil d'intérimaire ayant reçu le proposition de mission.</returns>
        internal static Label AddAnyAvaibilityDateControl()
        {
            // Assigne à une étiquette des propriétés graphique.
            Label lbl_anyItem = LabelBaseBuild(SentOffer_Val.Default.lbl_anyItem_Content, 540, 40);
            lbl_anyItem.Margin = new Thickness(285, 180, 0, 0);
            lbl_anyItem.FontSize = 20;
            lbl_anyItem.FontWeight = FontWeights.Bold;
            lbl_anyItem.HorizontalContentAlignment = HorizontalAlignment.Center;

            return lbl_anyItem;
        }
        #endregion

        #endregion

    }
}
