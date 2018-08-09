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
using InterimSearch.User_Controls.Sent_Offer.Settings;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using VerticalAlignment = System.Windows.VerticalAlignment;
#endregion

namespace InterimSearch.User_Controls.Sent_Offer
{
    public partial class SentOffer
    {

        #region Methods

        #region EmailInformations
        /// <summary>
        /// Obtient les informations pour l'email à envoyer.
        /// </summary>
        /// <param name="_avaibilityIDAndMessage">ID de la disponibilité et message à afficher</param>
        /// <returns>List : Réussite -> Informations de l'email. Echec -> Valeur null</returns>
        private static List<string> EmailInformations(List<string> _avaibilityIDAndMessage)
        {
            List<string> avaibilityIDAndMessage = _avaibilityIDAndMessage;
            string temporaryID = InterimDatabase.GetAvaibilityTemporaryID(avaibilityIDAndMessage[0]);
            bool isCorrectTemporaryID = (!string.IsNullOrEmpty(temporaryID));

            // Vérifie le contenu de l'ID de l'intérimaire.
            if (isCorrectTemporaryID)
            {
                string temporaryTitle = InterimDatabase.GetTemporaryTitle(temporaryID);
                string temporaryName = InterimDatabase.GetTemporaryName(temporaryID);
                bool isCorrectTemporaryTitle = (!string.IsNullOrEmpty(temporaryTitle));
                bool isCorrectTemporaryName = (!string.IsNullOrEmpty(temporaryName));

                // Vérifie le contenu des informations de l'intérimaire.
                if (isCorrectTemporaryTitle && isCorrectTemporaryName)
                {
                    string messageBox = avaibilityIDAndMessage[1] + temporaryTitle + " " + temporaryName + SentOffer_Val.Default.Ask;

                    // Message de confirmation.
                    DialogResult dialogResult = MessageBox.Show(messageBox, avaibilityIDAndMessage[2], MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Vérifie la réponse au message de confirmation.
                    if (dialogResult == DialogResult.Yes)
                    {
                        string temporaryEmailAdress = InterimDatabase.GetTemporaryEmailAdress(temporaryID);
                        string date = InterimDatabase.GetTemporaryDate(avaibilityIDAndMessage[0]);
                        bool isCorrectEmailAdress = (!string.IsNullOrEmpty(temporaryEmailAdress));
                        bool isCorrectDate = (!string.IsNullOrEmpty(date));

                        // Vérifie le contenu de l'adresse email.
                        if (isCorrectEmailAdress && isCorrectDate)
                        {
                            string dayName = DayName(date);
                            string dayNumber = DayNumber(date);
                            string month = MonthName(date);
                            string workTimetableStart = InterimDatabase.GetTemporaryWorkTimetableStart(avaibilityIDAndMessage[0]);
                            string workTimetableEnd = InterimDatabase.GetTemporaryWorkTimetableEnd(avaibilityIDAndMessage[0]);
                            string serviceName = InterimDatabase.GetTemporaryService(avaibilityIDAndMessage[0]);
                            string sendDate = InterimDatabase.GetTemporarySendDate(avaibilityIDAndMessage[0]);
                            bool isCorrectWorkTimetableStart = (!string.IsNullOrEmpty(workTimetableStart));
                            bool isCorrectWorkTimetableEnd = (!string.IsNullOrEmpty(workTimetableEnd));
                            bool isCorrectServiceName = (!string.IsNullOrEmpty(serviceName));
                            bool isCorrectSendDate = (!string.IsNullOrEmpty(sendDate));

                            // Vérifie le contenu des informations de la mission.
                            if (isCorrectWorkTimetableStart && isCorrectWorkTimetableEnd && isCorrectServiceName && isCorrectSendDate)
                            {
                                List<string> emailInformations = new List<string>();

                                emailInformations.Add(avaibilityIDAndMessage[0]);
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
                            MessageBox.Show(SentOffer_Val.Default.NullEmailAdressOrDate, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }

                    return null;
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

        #region IsCorrectEmailAdress
        /// <summary>
        /// Vérifie si l'adresse email est correcte.
        /// </summary>
        /// <param name="_emailAdress">Adresse email</param>
        /// <returns>Boolean : True -> Adresse email correcte. False -> Adresse email incorrecte.</returns>
        private static bool IsCorrectEmailAdress(string _emailAdress)
        {
            string emailAdress = _emailAdress;
            Regex regexEmail = new Regex(SentOffer_Code.Default.regexEmailAdress);

            // Vérifie si la syntaxe de l'email est correcte.
            if (regexEmail.IsMatch(emailAdress))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region IsCorrectList
        /// <summary>
        /// Vérifie le contenu d'une liste de chaînes de caractères.
        /// </summary>
        /// <param name="_list">Liste de chaînes de caractères</param>
        /// <returns>List : True -> Liste non null. False -> Liste null.</returns>
        private static bool IsCorrectList(List<string> _list)
        {
            List<string> list = _list;

            return (list != null);
        }
        #endregion

        #region IsCreateMessage
        /// <summary>
        /// Vérifie le contenu du message.
        /// </summary>
        /// <param name="_message">Message</param>
        /// <returns>Boolean : True -> Message non null. False -> Message null.</returns>
        private static bool IsCreateMessage(_MailItem _message)
        {
            _MailItem message = _message;
            bool isCorrectCreateMessage = (message != null);

            // Vérifie le contenu du message.
            if (isCorrectCreateMessage)
            {
                return true;
            }

            // Affichage d'un message d'erreur.
            MessageBox.Show(SentOffer_Val.Default.NullMessage, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        #endregion

        #region IsNullList
        /// <summary>
        /// Vérifie le contenu d'une liste de chaînes de caractères.
        /// </summary>
        /// <param name="_list">Liste de chaînes de caractères</param>
        /// <returns>List : True -> Liste null. False -> Liste non null.</returns>
        private static bool IsNullList(List<string> _list)
        {
            List<string> list = _list;

            return (!IsCorrectList(list));
        }
        #endregion

        #region IsSentMail
        /// <summary>
        /// Vérifie l'envoi de la proposition de mission.
        /// </summary>
        /// <param name="_message">Message</param>
        /// <returns>Boolean : True -> Réussite de l'envoi de l'email. False -> Echec de l'envoi de l'email</returns>
        private static bool IsSentEmail(_MailItem _message)
        {
            try
            {
                _MailItem message = _message;

                message.Send();
                return true;
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(SentOffer_Err.Default.IsSentEmail, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region LabelBaseBuild
        /// <summary>
        /// Assigne à une étiquette des propriétés graphiques basiques.
        /// </summary>
        /// <param name="_labelContent">Nom de l'étiquette</param>
        /// <param name="_labelWidth">Largeur de l'étiquette</param>
        /// <param name="_labelHeight">Hauteur de l'étiquette</param>
        /// <returns>Label : Etiquette affectée.</returns>
        private static Label LabelBaseBuild(string _labelContent, int _labelWidth, int _labelHeight = 26)
        {
            string labelContent = _labelContent;
            int labelWidth = _labelWidth;
            int labelHeight = _labelHeight;

            Label lbl_dataBase = new Label();

            // Assigne à une étiquette des propriétés graphiques basiques.
            lbl_dataBase.Content = labelContent;
            lbl_dataBase.HorizontalAlignment = HorizontalAlignment.Left;
            lbl_dataBase.VerticalAlignment = VerticalAlignment.Top;
            lbl_dataBase.Width = labelWidth;
            lbl_dataBase.Height = labelHeight;

            return lbl_dataBase;
        }
        #endregion

        #region LabelDataSection
        /// <summary>
        /// Assigne à une étiquette de section de données, des propriétés graphiques.
        /// </summary>
        /// <param name="_labelContent">Contenu de l'étiquette</param>
        /// <returns>Label : Réussite -> Etiquette affectée. Echec -> Valeur null.</returns>
        private static Label LabelDataSection(string _labelContent)
        {
            string labelContent = _labelContent;

            // Assigne à une étiquette, des propriétés graphiques.
            Label lbl_dataSection = LabelBaseBuild(labelContent, 300, 36);
            lbl_dataSection.FontSize = 16;
            lbl_dataSection.FontWeight = FontWeights.Bold;

            return lbl_dataSection;
        }
        #endregion

        #region MonthName
        /// <summary>
        /// Retourne le nom complet du mois de la date.
        /// </summary>
        /// <param name="_date">Date</param>
        /// <returns>String : Nom du mois.</returns>
        private static string MonthName(string _date)
        {
            string date = _date;
            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(date).Month);

            return char.ToUpper(month[0]) + month.Substring(1);
        }
        #endregion

        #region RejectEmail
        /// <summary>
        /// Envoi un email de refus de la mission.
        /// </summary>
        /// <param name="_rejectInformations">Informations de l'intérimaire</param>
        private static void RejectEmail(List<string> _rejectInformations)
        {
            List<string> rejectInformations = _rejectInformations;

            // Vérifie le contenu de la liste.
            if (IsCorrectList(rejectInformations))
            {
                // Crée un message de confirmation de refus de mission.
                List<string> bodyMessage = CreateBodyMessage(SentOffer_Code.Default.TemplateRejectPath, rejectInformations);

                // Vérifie le contenu du corps de message.
                if (IsCorrectList(bodyMessage))
                {
                    _MailItem message = CreateMessage(bodyMessage);

                    // Vérifie la création du message, l'envoi de l'email de confirmation et l'enregistrement du refus de mission.
                    if (IsCreateMessage(message) && IsSentEmail(message) && InterimDatabase.SetStateMission(rejectInformations[0], 0))
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

        #region ReplacementsMessage
        /// <summary>
        /// Remplace des valeurs spécifiques par des paramètres.
        /// </summary>
        /// <param name="_emailInformations">Liste des informations de l'intérimaire</param>
        /// <returns>ListDictionary : Dictionnaire contenant les paramètres à changer par des valeurs.</returns>
        private static ListDictionary ReplacementsMessage(List<string> _emailInformations)
        {
            List<string> emailInformations = _emailInformations;
            ListDictionary replacements = new ListDictionary();

            replacements.Add(SentOffer_Code.Default.Temporary_Title, emailInformations[2]);
            replacements.Add(SentOffer_Code.Default.Temporary_Name, emailInformations[3]);
            replacements.Add(SentOffer_Code.Default.Avaibility_DayName, emailInformations[4]);
            replacements.Add(SentOffer_Code.Default.Avaibility_DayNumber, emailInformations[5]);
            replacements.Add(SentOffer_Code.Default.Avaibility_Month, emailInformations[6]);
            replacements.Add(SentOffer_Code.Default.WorkTimetable_Start, emailInformations[7]);
            replacements.Add(SentOffer_Code.Default.WorkTimetable_End, emailInformations[8]);
            replacements.Add(SentOffer_Code.Default.Avaibility_Service, emailInformations[9]);
            replacements.Add(SentOffer_Code.Default.Avaibility_SendDate, emailInformations[10]);

            return replacements;
        }
        #endregion

        #region Sections
        /// <summary>
        /// Ajoute les étiquettes de sections à l'interface.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StacPanel de base de l'interface</param>
        /// <param name="_date">Date</param>
        private static void Sections(StackPanel _stp_dynamicsBase, string _date)
        {
            Label lbl_dataSection = null;
            string date = _date;
            string dayName = DayName(date);
            string dayNumber = DayNumber(date);
            string month = MonthName(date);
            string yearNumber = YearNumber(date);

            // Assigne à une étiquette, la date de la proposition de mission.
            date = dayName + " " + dayNumber + " " + month + " " + yearNumber;
            lbl_dataSection = LabelDataSection(date);

            // Ajoute l'étiquette de section à l'interface.
            _stp_dynamicsBase.Children.Add(lbl_dataSection);
        }
        #endregion

        #endregion

    }
}
