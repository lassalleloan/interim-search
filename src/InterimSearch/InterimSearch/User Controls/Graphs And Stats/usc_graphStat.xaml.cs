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
using InterimSearch.User_Controls.Graphs_And_Stats.Settings;
using InterimSearch.User_Controls.Home;
using System.Collections.Generic;
using System.Windows.Controls;
#endregion

namespace InterimSearch.User_Controls.Graphs_And_Stats
{
    /// <summary>
    /// Logique d'interaction pour usc_graphStat.xaml
    /// </summary>
    public partial class usc_graphStat : UserControl
    {

        #region Constructors

        #region usc_graphStat
        /// <summary>
        /// Initialise une nouvelle instance de la classe usc_graphStat.
        /// </summary>
        public usc_graphStat()
        {
            InitializeComponent();

            // Assigne le nom de l'interface des graphiques et statistiques à la fenêtre.
            Switcher.ChangeWindowTitle(GraphStat_Val.Default.GraphStatTitle);
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
            usc_home usc_homeHome = new usc_home();
            Switcher.Switch(usc_homeHome);
        }
        #endregion

        #region cbo_dataToGraph_SelectionChanged
        /// <summary>
        /// Action lors du changement d'élément de la liste dérouante "cbo_dataToGraph".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_dataToGraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cboItemSelected = (ComboBoxItem)cbo_dataToGraph.SelectedItem;
            int selectedIndex = cbo_dataToGraph.SelectedIndex;

            cht_GraphBase.Title = cboItemSelected.Content.ToString();

            // Vérifie l'élément sélectionné.
            switch (selectedIndex)
            {
                // Nombre d'intérimaire par profession.
                case 0:
                    cht_GraphBase.Series.Clear();

                    List<string>[] countTemporaryProfession = InterimDatabase.GetCountTemporaryProfession();
                    GraphStat.AddSerie(cht_GraphBase, countTemporaryProfession, GraphStat_Val.Default.TemporaryProfession);

                    break;

                // Nombre de missions acceptées par service.
                case 1:
                    cht_GraphBase.Series.Clear();

                    List<string>[] countAcceptedMissionService = InterimDatabase.GetCountAcceptedMissionService();
                    GraphStat.AddSerie(cht_GraphBase, countAcceptedMissionService, GraphStat_Val.Default.AcceptedMissionService);

                    break;

                // Nombre d'intérimaire par agence d'intérim.
                case 2:
                    cht_GraphBase.Series.Clear();

                    List<string>[] countTemporaryEmploymentAgency = InterimDatabase.GetCountTemporaryEmploymentAgency();
                    GraphStat.AddSerie(cht_GraphBase, countTemporaryEmploymentAgency, GraphStat_Val.Default.TemporaryEmploymentAgency);

                    break;

                // Nombre de missions acceptées, refusées et en attente par agence d'intérim.
                case 3:
                    cht_GraphBase.Series.Clear();

                    List<string>[] countAcceptedMissionEmploymentAgency = InterimDatabase.GetCountAcceptedMissionEmploymentAgency();
                    GraphStat.AddSerie(cht_GraphBase, countAcceptedMissionEmploymentAgency, GraphStat_Val.Default.AcceptedMission);

                    List<string>[] countRejectedMissionEmploymentAgency = InterimDatabase.GetCountRejectedMissionEmploymentAgency();
                    GraphStat.AddSerie(cht_GraphBase, countRejectedMissionEmploymentAgency, GraphStat_Val.Default.RejectedMission);

                    List<string>[] countWaitedMissionEmploymentAgency = InterimDatabase.GetCountWaitedMissionEmploymentAgency();
                    GraphStat.AddSerie(cht_GraphBase, countRejectedMissionEmploymentAgency, GraphStat_Val.Default.WaitedMission);

                    break;
            }
        }
        #endregion

        #endregion

    }
}
