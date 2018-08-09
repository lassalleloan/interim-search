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
using InterimSearch.Interim_Database.Settings;
using System;
using System.Collections.Generic;
#endregion

namespace InterimSearch.Interim_Database
{
    public partial class InterimDatabase
    {

        #region Global Variables

        private static string ageYearOld = string.Empty;
        private static string ageYearRecent = string.Empty;
        private static string date = string.Empty;
        private static string nbrYearExpOld = string.Empty;
        private static string nbrYearExpRecent = string.Empty;
        private static string profession = string.Empty;
        private static string workTimetableEnd = string.Empty;
        private static string workTimetableStart = string.Empty;

        #endregion

        #region Properties

        #region AgeYearRecent
        /// <summary>
        /// Contient l'année la plus récente par rapport à la catégorie d'âge.
        /// </summary>
        private static string AgeYearRecent
        {
            get
            {
                return ageYearRecent;
            }
            set
            {
                IsParticularAge = (value != Interim_Val.Default.All);

                // Vérifie le contenu du boulean.
                if (IsParticularAge)
                {
                    int minimumAge = Convert.ToInt32(value.Substring(0, 2));
                    int currentYear = DateTime.Now.Year;

                    // Assigne l'année la plus récente par rapport à l'âge minimum.
                    ageYearRecent = Convert.ToString(currentYear - minimumAge);
                }
                else
                {
                    ageYearRecent = string.Empty;
                }
            }
        }
        #endregion

        #region AgeYearOld
        /// <summary>
        /// Contient l'année la plus ancienne par rapport à la catégorie d'âge.
        /// </summary>
        private static string AgeYearOld
        {
            get
            {
                return ageYearOld;
            }
            set
            {
                IsParticularAge = (value != Interim_Val.Default.All);

                // Vérifie le contenu du boolean.
                if (IsParticularAge)
                {
                    int maximumAge = Convert.ToInt32(value.Substring(5, 2));
                    int currentYear = DateTime.Now.Year;

                    // Assigne l'année la plus ancienne par rapport à l'âge maximum.
                    ageYearOld = Convert.ToString(currentYear - maximumAge);
                }
                else
                {
                    ageYearOld = string.Empty;
                }
            }
        }
        #endregion

        #region Date
        /// <summary>
        /// Contient la date au format yyyy-mm-dd.
        /// </summary>
        private static string Date
        {
            get
            {
                return date;
            }
            set
            {
                DateTime dateTime = Convert.ToDateTime(value);

                date = string.Format(Interim_Code.Default.DateTimeFormatDatabase, dateTime);
            }
        }
        #endregion

        #region IsCertified
        /// <summary>
        /// Contient l'état de la recherche de disponibilité par rapport à la certification.
        /// </summary>
        public static bool IsCertified { get; set; }
        #endregion

        #region IsParticularAge
        /// <summary> 
        /// Contient l'état de la recherche de disponibilité par rapport à la catégorie d'âge.
        /// </summary>
        private static bool IsParticularAge { get; set; }
        #endregion

        #region IsParticularNbrYearExp
        /// <summary>
        /// Contient l'état de la recherche de disponibilité par rapport à la catégorie d'années d'expériences.
        /// </summary>
        private static bool IsParticularNbrYearExp { get; set; }
        #endregion

        #region IsParticularProfession
        /// <summary>
        /// Contient l'état de la recherche de disponibilité par rapport à la profession.
        /// </summary>
        private static bool IsParticularProfession { get; set; }
        #endregion

        #region NbrYearExpOld
        /// <summary>
        /// Contient l'année la plus ancienne par rapport à la catégorie d'années d'expériences.
        /// </summary>
        private static string NbrYearExpOld
        {
            get
            {
                return nbrYearExpOld;
            }
            set
            {
                IsParticularNbrYearExp = (value != Interim_Val.Default.All);

                // Vérifie le contenu du boolean.
                if (IsParticularNbrYearExp)
                {
                    int nbrYearExpStart = Convert.ToInt32(value.Substring(0, 2).Trim());
                    int MaximumNbrYearExpEnd = -1;
                    int currentYear = DateTime.Now.Year;
                    bool isNbrYearExpStartN = ((nbrYearExpStart != 0) && (nbrYearExpStart != 5));
                    int startIndex = 4;

                    // Vérifie le contenu d'un nombre.
                    if (isNbrYearExpStartN)
                    {
                        // Assigne le StartIndex de la SubString.
                        startIndex++;
                    }

                    // Assigne l'année la plus ancienne par rapport au nombre d'années d'expériences maximum.
                    MaximumNbrYearExpEnd = Convert.ToInt32(value.Substring(startIndex, 2));
                    nbrYearExpOld = Convert.ToString(currentYear - MaximumNbrYearExpEnd);
                }
                else
                {
                    nbrYearExpOld = string.Empty;
                }
            }
        }
        #endregion

        #region NbrYearExpRecent
        /// <summary>
        /// Contient l'année la plus récente par rapport à la catégorie d'années d'expériences.
        /// </summary>
        private static string NbrYearExpRecent
        {
            get
            {
                return nbrYearExpRecent;
            }
            set
            {
                IsParticularNbrYearExp = (value != Interim_Val.Default.All);

                // Vérifie le contenu du boolean.
                if (IsParticularNbrYearExp)
                {
                    int minimumNbrYearExp = Convert.ToInt32(value.Substring(0, 2).Trim());
                    int currentYear = DateTime.Now.Year;
                    bool isNbrYearExpStartN = ((minimumNbrYearExp != 0) && (minimumNbrYearExp != 5));
                    int startIndex = 4;

                    // Vérifie le contenu d'un nombre.
                    if (isNbrYearExpStartN)
                    {
                        // Assigne le StartIndex de la SubString.
                        startIndex++;
                    }

                    // Assigne l'année la plus récente par rapport au nombre d'années d'expériences minimum.
                    nbrYearExpRecent = Convert.ToString(currentYear - minimumNbrYearExp);
                }
                else
                {
                    nbrYearExpRecent = string.Empty;
                }
            }
        }
        #endregion

        #region Profession
        /// <summary>
        /// Contient la profession.
        /// </summary>
        private static string Profession
        {
            get
            {
                return profession;
            }
            set
            {
                IsParticularProfession = (value != Interim_Val.Default.AllProfession);

                // Vérifie le contenu du boolean.
                if (IsParticularProfession)
                {
                    profession = value;
                }
                else
                {
                    profession = string.Empty;
                }
            }
        }
        #endregion

        #region WorkTimetableEnd
        /// <summary>
        /// Contient l'heure de fin de travail.
        /// </summary>
        private static string WorkTimetableEnd
        {
            get
            {
                return workTimetableEnd;
            }
            set
            {
                workTimetableEnd = value.Substring(8);
            }
        }
        #endregion

        #region WorkTimetableStart
        /// <summary>
        /// Contient l'heure de début de travail.
        /// </summary>
        private static string WorkTimetableStart
        {
            get
            {
                return workTimetableStart;
            }
            set
            {
                workTimetableStart = value.Substring(0, 5);
            }
        }
        #endregion

        #endregion

        #region Methods

        #region DatabaseCriteria
        /// <summary>
        /// Assigne aux propriétés les critères sélectionnés formatés.
        /// </summary>
        /// <param name="_criteriaList">Liste de critères</param>
        public static void DatabaseCriteria(List<string> _criteriaList)
        {
            List<string> criteria = _criteriaList;

            Date = criteria[0];
            WorkTimetableStart = criteria[1];
            WorkTimetableEnd = criteria[1];
            Profession = criteria[2];
            AgeYearOld = criteria[4];
            AgeYearRecent = criteria[4];
            NbrYearExpOld = criteria[5];
            NbrYearExpRecent = criteria[5];
        }
        #endregion

        #region NbrSpendYear
        /// <summary>
        /// Calcul le nombre d'années écoulés entre une année et l'année en court.
        /// </summary>
        /// <param name="_year">Année de départ</param>
        /// <returns>String : Nombre d'années écoulées.</returns>
        private static string NbrSpendYear(string _year)
        {
            int year = Convert.ToInt32(_year);

            return Convert.ToString(DateTime.Now.Year - year);
        }
        #endregion

        #region ReturnWorkTimetableStart
        /// <summary>
        /// Obtient la valeur de la propriété WorkTimetableStart.
        /// </summary>
        /// <returns>String : Valeur de la porpriété WorkTimetableStart</returns>
        public static string ReturnWorkTimetableStart()
        {
            return WorkTimetableStart;
        }
        #endregion

        #endregion

    }
}
