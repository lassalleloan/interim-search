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
using InterimSearch.User_Controls.Graphs_And_Stats.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Controls.DataVisualization.Charting;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion

namespace InterimSearch.User_Controls.Graphs_And_Stats
{
    public static class GraphStat
    {

        #region Methods

        #region AddSerie
        /// <summary>
        /// Assigne une série de données au graphique de type histogramme.
        /// </summary>
        /// <param name="_chtGraphBase">Graphique de base</param>
        /// <param name="_dataSerie">Données de la série</param>
        /// <param name="titleSerie">Titre de la série</param>
        internal static void AddSerie(Chart _chtGraphBase, List<string>[] _dataSerie, string _titleSerie)
        {
            List<KeyValuePair<string, int>> DataGraphSerie = new List<KeyValuePair<string, int>>();
            List<string>[] dataSerie = _dataSerie;
            bool isCorrectDataSerie = (dataSerie != null);

            // Vérifie le contenu du tableau.
            if (isCorrectDataSerie)
            {
                for (int index = 0; index < dataSerie[0].Count; index++)
                {
                    DataGraphSerie.Insert(0, new KeyValuePair<string, int>(dataSerie[0][index], Convert.ToInt32(dataSerie[1][index])));
                }

                ColumnSeries typeSerie = new ColumnSeries();

                typeSerie.Title = _titleSerie;
                typeSerie.DependentValuePath = "Value";
                typeSerie.IndependentValuePath = "Key";
                typeSerie.ItemsSource = DataGraphSerie;
                _chtGraphBase.Series.Add(typeSerie);
            }
            else
            {
                // Affiche un message d'erreur.
                MessageBox.Show(GraphStat_Val.Default.NullGraphInformations, GraphStat_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

    }
}
