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
using InterimSearch.User_Controls.Results.Settings;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using _MailItem = Microsoft.Office.Interop.Outlook._MailItem;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using Orientation = System.Windows.Controls.Orientation;
using Outlook = Microsoft.Office.Interop.Outlook;
using VerticalAlignment = System.Windows.VerticalAlignment;
#endregion

namespace InterimSearch.User_Controls.Results
{
    public partial class Results
    {

        #region Methods

        #region CreateBodyMessage
        /// <summary>
        /// Crée le contenu d'un message.
        /// </summary>
        /// <remarks>
        /// Crée le contenu d'un message avec un template HTML et le replacements de valeurs spécifiques.
        /// </remarks>
        /// <param name="_offerInformations">Liste des informations pour l'envoi</param>
        /// <returns>List : Réussite -> Corps, destinataire et sujet du message. Echec -> Valeur null.</returns>
        /// <exception cref="StreamReader, MailDefinition, MailMessage">
        /// Exception levée par l'objet StreamReader, MailDefinition ou MailMessage.
        /// </exception>
        private static List<string> CreateBodyMessage(string _filePath, List<string> _offerInformations)
        {
            string filePath = _filePath;
            List<string> offerInformations = _offerInformations;

            // Vérifie le contenu de la liste.
            if (IsNullList(offerInformations))
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Results_Val.Default.NullEmailAdress, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                List<string> bodyMessageInformations = new List<string>();
                MailDefinition mailDefinition = new MailDefinition();
                MailMessage mailMessage = new MailMessage();
                string templateOffer = string.Empty;

                // Assigne des valeurs spécifiques par des paramètres.
                ListDictionary replacements = ReplacementsMessage(offerInformations);

                // Lit le contenu du coprs du template HTML.
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    templateOffer = streamReader.ReadToEnd();
                }

                // Assigne le sujet de l'email.
                int indexSubject = (templateOffer.IndexOf(Results_Code.Default.OpenTitleBalise) + Results_Code.Default.OpenTitleBalise.Length);
                int lengthSubject = (templateOffer.LastIndexOf(Results_Code.Default.CloseTitleBalise) - indexSubject);
                string subject = templateOffer.Substring(indexSubject, lengthSubject);

                // Crée le corps du message modifié.
                mailDefinition.From = Results_Val.Default.FakeEmailAdress;
                mailMessage = mailDefinition.CreateMailMessage(offerInformations[0], replacements, templateOffer, new System.Web.UI.Control());

                // Assigne le corps et le destinaire à une liste.
                bodyMessageInformations.Add(mailMessage.To.ToString());
                bodyMessageInformations.Add(mailMessage.Body);
                bodyMessageInformations.Add(subject);

                return bodyMessageInformations;
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Results_Err.Default.CreateBodyMessage, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region CreateMessage
        /// <summary>
        /// Crée un message avec des paramètres spécifques.
        /// </summary>
        /// <remarks>
        /// Crée un message avec un template HTML et le replacements de valeurs spécifiques.
        /// </remarks>
        /// <param name="_bodyMessageInformations">Corps, destinataire et sujet du message</param>
        /// <returns>_MailItem : Réussite -> Message créée. Echec -> Valeur null.</returns>
        /// <exception cref="Outlook.Application, StreamReader, MailDefinition, MailMessage">
        /// Exception levée par l'objet Outlook.Application, StreamReader, MailDefinition ou MailMessage.
        /// </exception>
        private static _MailItem CreateMessage(List<string> _bodyMessageInformations)
        {
            List<string> bodyMessageInformations = _bodyMessageInformations;

            // Vérifie le contenu de la chaîne de caractères.
            if (IsNullList(bodyMessageInformations))
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Results_Val.Default.CreateMessage, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                // Crée l'application Outlook.
                Outlook.Application outlookApp = new Outlook.Application();

                // Crée un nouvel email.
                Outlook.MailItem message = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                // Ajoute des destinataires.
                Outlook.Recipients recipients = (Outlook.Recipients)message.Recipients;
                Outlook.Recipient recipient = (Outlook.Recipient)recipients.Add(bodyMessageInformations[0]);
                recipient.Resolve();

                // Assigne l'objet du message.
                message.Subject = bodyMessageInformations[2];

                // Assigne le contenu de l'email.
                message.BodyFormat = OlBodyFormat.olFormatHTML;
                message.HTMLBody = bodyMessageInformations[1];
                message.ReadReceiptRequested = true;

                return message;
            }
            catch
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Results_Err.Default.CreateMessage, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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
            MessageBox.Show(Results_Val.Default.IsNullMessage, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(Results_Err.Default.IsSendEmail, Results_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region ReplacementsMessage
        /// <summary>
        /// Remplace des valeurs spécifiques par des paramètres.
        /// </summary>
        /// <param name="_temporaryOfferInformations">Liste des informations de l'intérimaire</param>
        /// <returns>ListDictionary : Dictionnaire contenant les paramètres à changer par des valeurs.</returns>
        private static ListDictionary ReplacementsMessage(List<string> _temporaryOfferInformations)
        {
            List<string> temporaryOfferInformations = _temporaryOfferInformations;
            ListDictionary replacements = new ListDictionary();

            replacements.Add(Results_Code.Default.Temporary_Title, temporaryOfferInformations[1]);
            replacements.Add(Results_Code.Default.Temporary_Name, temporaryOfferInformations[2]);
            replacements.Add(Results_Code.Default.Avaibility_DayName, temporaryOfferInformations[3]);
            replacements.Add(Results_Code.Default.Avaibility_DayNumber, temporaryOfferInformations[4]);
            replacements.Add(Results_Code.Default.Avaibility_Month, temporaryOfferInformations[5]);
            replacements.Add(Results_Code.Default.WorkTimetable_Start, temporaryOfferInformations[6]);
            replacements.Add(Results_Code.Default.WorkTimetable_End, temporaryOfferInformations[7]);
            replacements.Add(Results_Code.Default.Avaibility_Service, temporaryOfferInformations[8]);

            return replacements;
        }
        #endregion

        #region Sections
        /// <summary>
        /// Ajoute les étiquettes de sections à l'interface.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StackPanel de base de l'interface</param>
        private static void Sections(StackPanel _stp_dynamicsBase)
        {
            Label lbl_dataSection = null;

            // Vérifie si le critère "Certification" a été sélectionné.
            if (InterimDatabase.IsCertified)
            {
                // Assigne à une étiquette, le nom de section "Certifiés CHUV".
                lbl_dataSection = LabelDataSection(Results_Val.Default.lbl_CertidiedSection_Content);
            }
            else
            {
                // Assigne à une étiquette, le nom de section "Non Certifiés".
                lbl_dataSection = LabelDataSection(Results_Val.Default.lbl_UncertidiedSection_Content);
            }

            // Ajoute l'étiquette de section à l'interface.
            _stp_dynamicsBase.Children.Add(lbl_dataSection);
        }
        #endregion

        #region StackPanelBaseBuild
        /// <summary>
        /// Assigne à un StackPanel des propriétés graphiques basiques.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StackPanel de base</param>
        /// <param name="_orientation">Orientation du Stackpanel</param>
        /// <returns>StackPanel : StackPanel pour les données de chaque intérimaire.</returns>
        private static StackPanel StackPanelBaseBuild(StackPanel _stp_dynamicsBase, Orientation _orientation = Orientation.Vertical)
        {
            // Assigne à un stackPanel de contrôles, pour les données de chaque intérimaire.
            StackPanel stp_TemporaryData = new StackPanel();
            Orientation orientation = _orientation;

            stp_TemporaryData.Width = 1110;
            stp_TemporaryData.Height = 36;
            stp_TemporaryData.Orientation = orientation;
            _stp_dynamicsBase.Children.Add(stp_TemporaryData);

            return stp_TemporaryData;
        }
        #endregion

        #region TemporaryAge
        /// <summary>
        /// Assigne à des étiquettes, l'âge de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryAge">L'age de l'intérimaire</param>
        /// <param name="_stp_TemporaryData">Stackpanel de l'intérimaire</param>
        private static void TemporaryAge(string _temporaryAge, StackPanel _stp_TemporaryData)
        {
            string temporaryAge = _temporaryAge;

            // Assigne à une étiquette, l'âge de l'intérimaire.
            Label lbl_temporaryAge = LabelBaseBuild(Results_Val.Default.lbl_temporaryAge, 38);
            Label lbl_temporaryAgeValue = LabelBaseBuild(temporaryAge + Results_Val.Default.Year, 52);

            // Assigne à une étiquette une propriété graphique.
            lbl_temporaryAge.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute les étiquettes au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryAge);
            _stp_TemporaryData.Children.Add(lbl_temporaryAgeValue);
        }
        #endregion

        #endregion

    }
}
