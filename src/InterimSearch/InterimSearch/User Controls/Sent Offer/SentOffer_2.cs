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
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using Orientation = System.Windows.Controls.Orientation;
using Outlook = Microsoft.Office.Interop.Outlook;
#endregion

namespace InterimSearch.User_Controls.Sent_Offer
{
    public partial class SentOffer
    {

        #region Methods

        #region AddAvaibilityDateControls
        /// <summary>
        /// Ajoute les contrôles pour les propositions de missions envoyées.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StackPanel de base</param>
        internal static void AddSentOfferControls(StackPanel _stp_dynamicsBase)
        {
            List<string> avaibilityDate = InterimDatabase.GetAvaibilityDate();
            bool isCorrectAvaibilityDateList = IsCorrectList(avaibilityDate);
            List<TimeSpan> workTimetableList = new List<TimeSpan>();
            bool isCorrecyWorkTimetableList = false;

            List<string> avaibilityStartTime = new List<string>();
            List<string> avaibilityEndTime = new List<string>();
            string workTimetable = string.Empty;

            List<string> avaibilityIDAll = new List<string>();
            string avaibilityTemporaryID = string.Empty;
            bool isCorrectAvaibilityTemporaryID = false;

            string temporaryName = string.Empty;
            bool isCorrectTemporaryName = false;

            string temporaryTitle = string.Empty;
            string temporaryFirstName = string.Empty;
            string temporaryService = string.Empty;
            string tamporarySendDate = string.Empty;

            // Vérifie le contenu de la liste de données.
            if (isCorrectAvaibilityDateList)
            {
                for (int indexDate = 0; indexDate < avaibilityDate.Count; indexDate++)
                {
                    StackPanel stp_DateData = StackPanelBaseBuild(_stp_dynamicsBase);
                    Sections(stp_DateData, avaibilityDate[indexDate]);

                    workTimetableList = InterimDatabase.GetAvaibilityWorkTimetable(avaibilityDate[indexDate]);
                    isCorrecyWorkTimetableList = (workTimetableList != null);

                    // Vérifie le contenu des horaires de travail.
                    if (isCorrecyWorkTimetableList)
                    {
                        // Sépare les horaires de travail en début et fin.
                        for (int index = 0; index < workTimetableList.Count; index += 2)
                        {
                            avaibilityStartTime.Add(workTimetableList[index].ToString().Substring(0, 5));
                            avaibilityEndTime.Add(workTimetableList[index + 1].ToString().Substring(0, 5));
                        }

                        for (int indexWorkTimetable = 0; indexWorkTimetable < avaibilityStartTime.Count; indexWorkTimetable++)
                        {
                            // Assigne à un stackPanel de contrôles, pour les données de chaque horaires de travail.
                            workTimetable = avaibilityStartTime[indexWorkTimetable] + SentOffer_Val.Default.BetweenTime + avaibilityEndTime[indexWorkTimetable];

                            StackPanel stp_WorkTimetableData = DayWorkTimetable(workTimetable, _stp_dynamicsBase);

                            avaibilityIDAll = InterimDatabase.GetAvaibilityIDAll(avaibilityDate[indexDate], avaibilityStartTime[indexWorkTimetable], avaibilityEndTime[indexWorkTimetable]);

                            // Vérifie le contenu des ID des disponibilités.
                            if (IsCorrectList(avaibilityIDAll))
                            {
                                foreach (string avaibilityID in avaibilityIDAll)
                                {
                                    avaibilityTemporaryID = InterimDatabase.GetAvaibilityTemporaryID(avaibilityID);
                                    isCorrectAvaibilityTemporaryID = (!string.IsNullOrEmpty(avaibilityTemporaryID));

                                    // Vérifie le contenu de l'ID de l'intérimaire.
                                    if (isCorrectAvaibilityTemporaryID)
                                    {
                                        // Assigne à des chaînes de caractères, les informations de chaque intérimaires ayant reçu une propositions de missions.
                                        temporaryName = InterimDatabase.GetTemporaryName(avaibilityTemporaryID);
                                        isCorrectTemporaryName = (!string.IsNullOrEmpty(temporaryName));

                                        // Vérifie le contenu du nom de l'intérimaire.
                                        if (isCorrectTemporaryName)
                                        {
                                            temporaryTitle = InterimDatabase.GetTemporaryTitle(avaibilityTemporaryID);
                                            temporaryFirstName = InterimDatabase.GetTemporaryFirstName(avaibilityTemporaryID);
                                            temporaryService = InterimDatabase.GetTemporaryService(avaibilityID);
                                            tamporarySendDate = InterimDatabase.GetTemporarySendDate(avaibilityID);

                                            // Assigne à un stackPanel de contrôles, pour les données de chaque intérimaires.
                                            StackPanel stp_TemporaryData = StackPanelBaseBuild(_stp_dynamicsBase, Orientation.Horizontal);

                                            // Assigne à une étiquette des propriétés graphiques.
                                            stp_TemporaryData.Margin = new Thickness(40, 10, 0, 10);

                                            // Assigne le titre de civilité de l'intérimaire.
                                            TemporaryTitle(stp_TemporaryData, temporaryTitle);

                                            // Assigne le nom de l'intérimaire.
                                            TemporaryName(stp_TemporaryData, temporaryName);

                                            // Assigne le prénom de l'intérimaire.
                                            TemporaryFirstName(stp_TemporaryData, temporaryFirstName);

                                            // Assigne le service de l'intérimaire.
                                            TemporaryService(stp_TemporaryData, temporaryService);

                                            // Assigne la date d'envoi de la proposition de mission de l'intérimaire.
                                            TemporarySendDate(stp_TemporaryData, tamporarySendDate);

                                            // Assigne les étiquettes de l'état de l'acceptation de proposition de mission.
                                            TemporaryStateMission(stp_TemporaryData, avaibilityID);
                                        }
                                    }
                                }
                            }
                        }

                        avaibilityStartTime.Clear();
                        avaibilityEndTime.Clear();
                    }
                }
            }
        }
        #endregion

        #region AnswerOffer
        /// <summary>
        /// Assigne un label de réponse pour la proposition de mission.
        /// </summary>
        /// <param name="_stp_TemporaryData">StackPanel</param>
        /// <param name="_avaibilityID">ID disponibilité</param>
        /// <param name="_labelContent">Contenu de l'étiquette</param>
        /// <returns>Label : Etiquette pour répondre à la propoition de mission.</returns>
        private static Label AnswerOffer(StackPanel _stp_TemporaryData, string _avaibilityID, string _labelContent)
        {
            string avaibilityID = _avaibilityID;
            string labelContent = _labelContent;
            Label lbl_answerOffer = LabelBaseBuild(labelContent, 1);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_answerOffer.Width = double.NaN;
            lbl_answerOffer.Margin = new Thickness(20, 0, 0, 0);
            lbl_answerOffer.BorderBrush = Brushes.Black;
            lbl_answerOffer.BorderThickness = new Thickness(0, 0, 0, 1);
            lbl_answerOffer.Name = SentOffer_Code.Default.lbl_answerOffer_Name + avaibilityID;

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_answerOffer);

            return lbl_answerOffer;
        }
        #endregion

        #region CreateBodyMessage
        /// <summary>
        /// Crée le contenu d'un message.
        /// </summary>
        /// <remarks>
        /// Crée le contenu d'un message avec un template HTML.
        /// </remarks>
        /// <param name="_pathFile">Chemin du fichier</param>
        /// <param name="_emailInformations">Informations de l'email</param>
        /// <returns>String : Réussite -> Corps, destinataire et sujet du message. Echec -> Valeur null.</returns>
        /// <exception cref="StreamReader, MailDefinition, MailMessage">
        /// Exception levée par l'objet StreamReader, MailDefinition ou MailMessage.
        /// </exception>
        private static List<string> CreateBodyMessage(string _filePath, List<string> _emailInformations)
        {
            string filePath = _filePath;
            List<string> emailInformations = _emailInformations;

            // Vérifie le contenu de la liste.
            if (IsNullList(emailInformations))
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Val.Default.NullEmailAdress, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                string templateConfirm = string.Empty;
                List<string> bodyMessageInformations = new List<string>();
                MailDefinition mailDefinition = new MailDefinition();
                MailMessage mailMessage = new MailMessage();

                // Assigne des valeurs spécifiques par des paramètres.
                ListDictionary replacements = ReplacementsMessage(emailInformations);

                // Lit le contenu du coprs du template HTML.
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    templateConfirm = streamReader.ReadToEnd();
                }

                // Assigne le sujet du message.
                int indexSubject = (templateConfirm.IndexOf(SentOffer_Code.Default.OpenTitleBalise) + SentOffer_Code.Default.OpenTitleBalise.Length);
                int lengthSubject = (templateConfirm.LastIndexOf(SentOffer_Code.Default.CloseTitleBalise) - indexSubject);
                string subject = templateConfirm.Substring(indexSubject, lengthSubject);

                // Crée le corps du message modifié.
                mailDefinition.From = SentOffer_Val.Default.FakeEmailAdress;
                mailMessage = mailDefinition.CreateMailMessage(emailInformations[1], replacements, templateConfirm, new System.Web.UI.Control());

                // Assigne le destinataire, le corps et le sujet du message.
                bodyMessageInformations.Add(mailMessage.To.ToString());
                bodyMessageInformations.Add(mailMessage.Body);
                bodyMessageInformations.Add(subject);

                return bodyMessageInformations;
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(SentOffer_Err.Default.CreateBodyMessage, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region CreateMessage
        /// <summary>
        /// Crée un message avec des paramètres spécifques.
        /// </summary>
        /// <remarks>
        /// Crée un message avec un template HTML.
        /// </remarks>
        /// <param name="_bodyMessage">Corps, destinataire et sujet du message</param>
        /// <returns>_MailItem : Réussite -> Message créée. Echec -> Valeur null.</returns>
        /// <exception cref="Outlook.Application, StreamReader, MailDefinition, MailMessage">
        /// Exception levée par l'objet Outlook.Application, StreamReader, MailDefinition ou MailMessage.
        /// </exception>
        private static _MailItem CreateMessage(List<string> _bodyMessage)
        {
            List<string> bodyMessage = _bodyMessage;

            try
            {
                // Crée l'application Outlook.
                Outlook.Application outlookApp = new Outlook.Application();

                // Crée un nouvel email.
                Outlook.MailItem message = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                // Ajoute des destinataires.
                Outlook.Recipients recipients = (Outlook.Recipients)message.Recipients;
                Outlook.Recipient recipient = (Outlook.Recipient)recipients.Add(bodyMessage[0]);
                recipient.Resolve();

                // Assigne l'objet du message.
                message.Subject = bodyMessage[2];

                // Assigne le contenu de l'email.
                message.BodyFormat = OlBodyFormat.olFormatHTML;
                message.HTMLBody = bodyMessage[1];
                message.ReadReceiptRequested = true;

                return message;
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(SentOffer_Err.Default.CreateMessage, SentOffer_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region DayName
        /// <summary>
        /// Retourne le nom complet du jour de la date.
        /// </summary>
        /// <param name="_date">Date</param>
        /// <returns>String : Nom du jour.</returns>
        private static string DayName(string _date)
        {
            string date = _date;
            string dayName = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(date).DayOfWeek);

            return char.ToUpper(dayName[0]) + dayName.Substring(1);
        }
        #endregion

        #region DayNumber
        /// <summary>
        /// Retourne le numéro du jour de la date.
        /// </summary>
        /// <param name="_date">Date</param>
        /// <returns>String : Numéro du jour.</returns>
        private static string DayNumber(string _date)
        {
            string date = _date;

            return date.Substring(0, 2);
        }
        #endregion

        #region DayWorkTimetable
        /// <summary>
        /// Retourne le stackPanel de l'horaire de travail.
        /// </summary>
        /// <remarks>
        /// Retourne le stackPanel de l'horaire de travail et
        /// assigne à un stackPanel de contrôles, pour les données de chaque horaires de travail.
        /// </remarks>
        /// <param name="_workTimetable">Horaires de trvail</param>
        /// <param name="_stp_DateData">StackPanel</param>
        /// <returns>StackPanel : StackPanel de l'horaire de travail.</returns>
        private static StackPanel DayWorkTimetable(string _workTimetable, StackPanel _stp_DateData)
        {
            string workTimetable = _workTimetable;
            StackPanel stp_WorkTimetableData = StackPanelBaseBuild(_stp_DateData);
            Label lbl_workTimetable = LabelBaseBuild(workTimetable, 100);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_workTimetable.FontSize = 12;
            lbl_workTimetable.FontWeight = FontWeights.Bold;
            stp_WorkTimetableData.Margin = new Thickness(20, 0, 0, 0);
            stp_WorkTimetableData.Children.Add(lbl_workTimetable);

            return stp_WorkTimetableData;
        }
        #endregion

        #endregion

    }
}
