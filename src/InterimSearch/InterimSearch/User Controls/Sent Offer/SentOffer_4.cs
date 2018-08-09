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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Label = System.Windows.Controls.Label;
#endregion

namespace InterimSearch.User_Controls.Sent_Offer
{
    public partial class SentOffer
    {

        #region Methods

        #region StackPanelBaseBuild
        /// <summary>
        /// Assigne à un StackPanel des prorpiétés graphiques basiques.
        /// </summary>
        /// <param name="_stp_dynamicsBase">StackPanel</param>
        /// <param name="_orientation">Orientation du StackPanel</param>
        /// <returns>StackPanel : StackPanel pour les données de chaque élément.</returns>
        private static StackPanel StackPanelBaseBuild(StackPanel _stp_dynamicsBase, Orientation _orientation = Orientation.Vertical)
        {
            // Assigne à un stackPanel de contrôles, pour les données de chaque élément.
            StackPanel stp_TemporaryData = new StackPanel();

            Orientation orientation = _orientation;
            stp_TemporaryData.Width = 1110;
            stp_TemporaryData.Orientation = orientation;
            _stp_dynamicsBase.Children.Add(stp_TemporaryData);

            return stp_TemporaryData;
        }
        #endregion

        #region TemporaryFirstName
        /// <summary>
        /// Assigne le prénom de l'intérimaire.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_temporaryFirstName">Prénom</param>
        private static void TemporaryFirstName(StackPanel _stp_TemporaryData, string _temporaryFirstName)
        {
            string temporaryFirstName = _temporaryFirstName;
            Label lbl_temporaryFirstName = LabelBaseBuild(temporaryFirstName, 130);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_temporaryFirstName.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryFirstName);
        }
        #endregion

        #region TemporaryName
        /// <summary>
        /// Assigne le nom de l'intérimaire.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_temporaryName">Nom</param>
        private static void TemporaryName(StackPanel _stp_TemporaryData, string _temporaryName)
        {
            string temporaryName = _temporaryName;
            Label lbl_temporaryName = LabelBaseBuild(temporaryName, 158);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_temporaryName.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryName);
        }
        #endregion

        #region TemporarySendDate
        /// <summary>
        /// Assigne date d'envoi de la proposition de mission de l'intérimaire.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_tamporarySendDate">Date d'envoi</param>
        private static void TemporarySendDate(StackPanel _stp_TemporaryData, string _tamporarySendDate)
        {
            string tamporarySendDate = _tamporarySendDate;
            Label lbl_temporarySendDate = LabelBaseBuild(SentOffer_Val.Default.lbl_temporarySendDate, 238);
            Label lbl_temporarySendDateValue = LabelBaseBuild(tamporarySendDate, 72);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_temporarySendDate.Margin = new Thickness(10, 0, 0, 0);
            lbl_temporarySendDateValue.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporarySendDate);
            _stp_TemporaryData.Children.Add(lbl_temporarySendDateValue);
        }
        #endregion

        #region TemporaryService
        /// <summary>
        /// Assigne le service de l'intérimaire.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_temporaryService">Service</param>
        private static void TemporaryService(StackPanel _stp_TemporaryData, string _temporaryService)
        {
            string temporaryService = _temporaryService;
            Label lbl_temporaryService = LabelBaseBuild(temporaryService, 140);

            // Assigne à une étiquette des propriétés graphiques.
            lbl_temporaryService.Margin = new Thickness(10, 0, 0, 0);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryService);
        }
        #endregion

        #region TemporaryStateMission
        /// <summary>
        /// Assigne les étiquettes de l'état d'acceptation de proposition de mission.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_avaibilityID">ID disponibilité</param>
        private static void TemporaryStateMission(StackPanel _stp_TemporaryData, string _avaibilityID)
        {
            string avaibilityID = _avaibilityID;
            string temporaryAcceptedMission = InterimDatabase.GetTemporaryAcceptedMission(avaibilityID);
            bool isAcceptMission = (string.IsNullOrEmpty(temporaryAcceptedMission));

            // Vérifie si la mission est déjà acceptée, refusée ou en attente de réponse.
            if (isAcceptMission)
            {
                Label lbl_acceptMission = AnswerOffer(_stp_TemporaryData, avaibilityID, SentOffer_Val.Default.AcceptMission);
                lbl_acceptMission.MouseLeftButtonUp += new MouseButtonEventHandler(lbl_acceptMission_MouseLeftButtonUp);
                lbl_acceptMission.MouseEnter += new MouseEventHandler(lbl_labelBase_MouseEnter);
                lbl_acceptMission.MouseLeave += new MouseEventHandler(lbl_labelBase_MouseLeave);

                Label lbl_otherActionMission = AnswerOffer(_stp_TemporaryData, avaibilityID, SentOffer_Val.Default.OtherActionMission);
                lbl_otherActionMission.MouseLeftButtonUp += new MouseButtonEventHandler(lbl_otherActionMission_MouseLeftButtonUp);
                lbl_otherActionMission.MouseEnter += new MouseEventHandler(lbl_labelBase_MouseEnter);
                lbl_otherActionMission.MouseLeave += new MouseEventHandler(lbl_labelBase_MouseLeave);

                // Assigne à une étiquette, un menu contextuel.
                ContextMenu lbl_otherActionMissionContextMenu = new ContextMenu();
                lbl_otherActionMission.ContextMenu = lbl_otherActionMissionContextMenu;

                // Assigne à un menu contextuel, un menu d'éléments.
                MenuItem lbl_abortMission = new MenuItem();

                lbl_abortMission.Header = SentOffer_Val.Default.AbortMission;
                lbl_abortMission.Name = SentOffer_Code.Default.lbl_answerOffer_Name + avaibilityID;
                lbl_abortMission.Click += new RoutedEventHandler(lbl_abortMission_MouseLeftButtonUp);
                lbl_otherActionMissionContextMenu.Items.Add(lbl_abortMission);

                // Assigne à un menu contextuel, un menu d'éléments.
                MenuItem lbl_rejectMission = new MenuItem();

                lbl_rejectMission.Header = SentOffer_Val.Default.RejectMission;
                lbl_rejectMission.Name = SentOffer_Code.Default.lbl_answerOffer_Name + avaibilityID;
                lbl_rejectMission.Click += new RoutedEventHandler(lbl_rejectMission_MouseLeftButtonUp);
                lbl_otherActionMissionContextMenu.Items.Add(lbl_rejectMission);
            }
            else
            {
                // Assigne à une étiquette, l'état acceptée ou refusée à la proposition de mission.
                AcceptedOrRejectedMission(_stp_TemporaryData, temporaryAcceptedMission);
            }
        }
        #endregion

        #region TemporaryTitle
        /// <summary>
        /// Assigne le titre de civilité de l'intérimaire.
        /// </summary>
        /// <param name="_stp_TemporaryData">Stackpanel</param>
        /// <param name="_temporaryTitle">Titre de civilité</param>
        private static void TemporaryTitle(StackPanel _stp_TemporaryData, string _temporaryTitle)
        {
            string temporaryTitle = _temporaryTitle;
            Label lbl_temporaryTitle = LabelBaseBuild(temporaryTitle, 40);

            // Ajoute l'étiquette au stackPanel de l'intérimaire.
            _stp_TemporaryData.Children.Add(lbl_temporaryTitle);
        }
        #endregion

        #region YearNumber
        /// <summary>
        /// Retour l'année de la date
        /// </summary>
        /// <param name="_date">Date</param>
        /// <returns>String : Année</returns>
        private static string YearNumber(string _date)
        {
            string date = _date;

            return date.Substring(6);
        }
        #endregion

        #endregion

    }
}
