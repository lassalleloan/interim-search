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
using InterimSearch.User_Controls.Home.Settings;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion

namespace InterimSearch.User_Controls.Home
{
    public static class Home
    {

        #region Methods

        #region CriteriaList
        /// <summary>
        /// Regroupe les critères dans une liste de chaînes de caractères.
        /// </summary>
        /// <param name="_dateString">Date</param>
        /// <param name="_workTimetableString">La plage horaire de travail</param>
        /// <param name="_professionString">Profession</param>
        /// <param name="_certifiedString">Certification</param>
        /// <param name="_ageString">L'âge</param>
        /// <param name="_nbrYearExpString">Expérience</param>
        internal static void CriteriaList(string _dateString, string _workTimetableString, string _professionString, string _certifiedString, string _ageString, string _nbrYearExpString)
        {
            string date = _dateString;
            string workTimetable = _workTimetableString;
            string profession = _professionString;
            string certified = _certifiedString;
            string age = _ageString;
            string nbrYearExp = _nbrYearExpString;
            List<string> criteriaList = new List<string>();

            // Ajoute les criètres à la liste.
            criteriaList.Add(date);
            criteriaList.Add(workTimetable);
            criteriaList.Add(profession);
            criteriaList.Add(certified);
            criteriaList.Add(age);
            criteriaList.Add(nbrYearExp);

            // Assigne aux différentes propriétés.
            Results.Results.Criteria = criteriaList;
            InterimDatabase.DatabaseCriteria(criteriaList);
        }
        #endregion

        #region IsNullDate
        /// <summary>
        /// Vérifie le contenu et le format de la date.
        /// </summary>
        /// <param name="_date">Date</param>
        /// <returns>Boolean : True -> Contenu et format de la date incorrecte. False -> Contenu et format de la date correcte.</returns>
        internal static bool IsNullDate(string _date)
        {
            string date = _date;
            Regex regex = new Regex(Home_Code.Default.regexDate);

            // Vérifie le contenu et le format de la date.
            if (regex.IsMatch(date))
            {
                return false;
            }

            return true;

        }
        #endregion

        #region MergeWorkTimetable
        /// <summary>
        /// Obtient les plages horaires de travail.
        /// </summary>
        /// <param name="_cbo_workTimetable">ComboBox</param>
        internal static void MergeWorkTimetable(ComboBox _cbo_workTimetable)
        {
            List<TimeSpan> workTimetable = InterimDatabase.GetWorkTimetable();
            bool isCorrecyWorkTimetableList = (workTimetable != null);

            // Vérifie le contenu de la liste.
            if (isCorrecyWorkTimetableList)
            {
                List<string> mergeWorkTimetableList = new List<string>();

                for (int index = 0; index < workTimetable.Count; index += 2)
                {
                    mergeWorkTimetableList.Add(workTimetable[index].ToString().Substring(0, 5) + Home_Val.Default.BetweenTime + workTimetable[index + 1].ToString().Substring(0, 5));
                }

                // Assigne les horaires de travail et sélectionne la première.
                _cbo_workTimetable.ItemsSource = mergeWorkTimetableList;
                _cbo_workTimetable.SelectedItem = mergeWorkTimetableList[0];
            }
            else
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Home_Val.Default.WorkTimetableListNull, Home_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ProfessionList
        /// <summary>
        /// Assigne les professions contenues dans la base de données aux contrôles.
        /// </summary>
        /// <param name="_cbo_profession">ComboBox</param>
        internal static void ProfessionList(ComboBox _cbo_profession)
        {
            List<string> professionsName = InterimDatabase.GetProfession();
            bool isCorrecyPofessionNameList = (professionsName != null);

            // Vérifie le contenu de la liste des noms de professions.
            if (isCorrecyPofessionNameList)
            {
                // Assigne les professions.
                _cbo_profession.ItemsSource = professionsName;

                // Sélectionne la premièrer profesion "Toutes".
                _cbo_profession.SelectedItem = professionsName[0];
            }
            else
            {
                // Affichage d'un message d'erreur.
                MessageBox.Show(Home_Val.Default.ProfessionListNull, Home_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

    }
}
