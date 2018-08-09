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
using InterimSearch.User_Controls.Profile.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
#endregion

namespace InterimSearch.User_Controls.Profile
{
    public static class Profile
    {

        #region Properties

        #region TemporaryID
        /// <summary>
        /// Contient l'ID de l'intérimaire.
        /// </summary>
        internal static string TemporaryID { get; set; }
        #endregion

        #endregion

        #region Methods

        #region AddPaths
        /// <summary>
        /// Assigne les différents chemins d'accès du rapport de travail.
        /// </summary>
        /// <param name="_temporaryInformations">Informations de l'intérimaire</param>
        internal static void AddPaths(List<string> _temporaryInformations)
        {
            string myDocuments = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryFilePath = myDocuments + Profile_Code.Default.TemporaryWorkReports + _temporaryInformations[10] + Profile_Code.Default.Underscore + _temporaryInformations[11];
            string pdfFileName = _temporaryInformations[10] + Profile_Code.Default.Underscore + _temporaryInformations[11] + Profile_Code.Default.Underscore
                                + DateTime.Now.Day + Profile_Code.Default.Underscore + DateTime.Now.Month + Profile_Code.Default.Underscore + DateTime.Now.Year + Profile_Code.Default.DotPDF;
            string pdfFullPath = directoryFilePath + Profile_Code.Default.Slash + pdfFileName;

            _temporaryInformations.Add(directoryFilePath);
            _temporaryInformations.Add(pdfFullPath);
        }
        #endregion

        #region AvaibilityTableData
        /// <summary>
        /// Obtient et format en HTML un tableau pour contenir les missions effectuées.
        /// </summary>
        /// <returns>List[] : Tableau de données en HTML</returns>
        private static string AvaibilityTableData(List<string>[] _workReportInformations)
        {
            List<string>[] workReportInformations = _workReportInformations;
            string tableData = string.Empty;

            // Ajoute un ligne à un tableau en HTML.
            for (int row = 0; row < workReportInformations[0].Count; row++)
            {
                tableData += "<tr align=\"center\">";

                // Ajoute une cellule à un tableau en HTML.
                for (int column = 0; column < workReportInformations.Length - 1; column++)
                {
                    tableData += "<td>" + workReportInformations[column][row] + "</td>";
                }

                tableData += "</tr>";
            }

            return tableData;
        }
        #endregion

        #region CreateWorkReport
        /// <summary>
        /// Crée le contenu d'un rapport de travail.
        /// </summary>
        /// <remarks>
        /// Crée le contenu d'un rapport de travail avec un template HTML et le replacements de valeurs spécifiques.
        /// </remarks>
        /// <param name="_workReportInformations">Liste des informations pour le rapport de travail</param>
        /// <returns>List : Réussite -> Rapport de travail. Echec -> Valeur vide.</returns>
        /// <exception cref="StreamReader, MailDefinition, MailMessage">
        /// Exception levée par l'objet StreamReader, MailDefinition ou MailMessage.
        /// </exception>
        internal static string CreateWorkReport(List<string>[] _workReportInformations)
        {
            List<string>[] workReportInformations = _workReportInformations;

            try
            {
                MailDefinition mailDefinition = new MailDefinition();
                MailMessage mailMessage = new MailMessage();
                string templateWorkReport = string.Empty;

                // Assigne des valeurs spécifiques par des paramètres.
                ListDictionary replacements = ReplacementsWorkReport(workReportInformations);

                // Lit le contenu du coprs du template HTML.
                using (StreamReader streamReader = new StreamReader(Profile_Code.Default.TemplateWorkReport))
                {
                    templateWorkReport = streamReader.ReadToEnd();
                }

                // Crée le corps du rapport de travail.
                mailDefinition.From = Profile_Val.Default.FakeEmailAdress;
                mailMessage = mailDefinition.CreateMailMessage(Profile_Val.Default.FakeEmailAdress, replacements, templateWorkReport, new System.Web.UI.Control());

                return mailMessage.Body;
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Profile_Err.Default.CreateWorkReport, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
        #endregion

        #region IsCorrectEmailAdress
        /// <summary>
        /// Vérifie si l'adresse email est correcte.
        /// </summary>
        /// <param name="_emailAdress">Adresse email</param>
        /// <returns>Boolean : True -> Adresse email correcte. False -> Adresse email Incorrecte.</returns>
        internal static bool IsCorrectEmailAdress(string _emailAdress)
        {
            string emailAdress = _emailAdress;
            Regex regexEmail = new Regex(Profile_Code.Default.regexEmailAdress);

            // Vérifie le contenu de l'adresse email.
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
        /// <returns>Boolean : True -> Liste non null. False -> Liste null.</returns>
        internal static bool IsCorrectList(List<string> _list)
        {
            List<string> list = _list;

            return (list != null);
        }
        #endregion

        #region IsNullList
        /// <summary>
        /// Vérifie le contenu d'une liste de chaînes de caractères.
        /// </summary>
        /// <param name="_list">Liste de chaînes de caractères</param>
        /// <returns>List : True -> Liste null. False -> Liste non null.</returns>
        internal static bool IsNullList(List<string> _list)
        {
            List<string> list = _list;

            return (!IsCorrectList(list));
        }
        #endregion
        
        #region ReplacementsWorkReport
        /// <summary>
        /// Remplace des valeurs spécifiques par des paramètres.
        /// </summary>
        /// <param name="_workReportInformations">Liste des informations de l'intérimaire</param>
        /// <returns>ListDictionary : Dictionnaire contenant les paramètres à changer par des valeurs.</returns>
        private static ListDictionary ReplacementsWorkReport(List<string>[] _workReportInformations)
        {
            List<string>[] workReportInformations = _workReportInformations;
            ListDictionary replacements = new ListDictionary();

            replacements.Add(Profile_Code.Default.EmployementAgency_Name, workReportInformations[5][0]);
            replacements.Add(Profile_Code.Default.EmployementAgency_Number, workReportInformations[5][1]);
            replacements.Add(Profile_Code.Default.EmployementAgency_Street, workReportInformations[5][2]);
            replacements.Add(Profile_Code.Default.EmployementAgency_PostCode, workReportInformations[5][3]);
            replacements.Add(Profile_Code.Default.EmployementAgency_City, workReportInformations[5][4]);
            replacements.Add(Profile_Code.Default.EmployementAgency_Region, workReportInformations[5][5]);
            replacements.Add(Profile_Code.Default.EmployementAgency_Contry, workReportInformations[5][6]);
            replacements.Add(Profile_Code.Default.EmployementAgency_PhoneNumber, workReportInformations[5][7]);
            replacements.Add(Profile_Code.Default.EmployementAgency_EmailAdress, workReportInformations[5][8]);
            replacements.Add(Profile_Code.Default.Temporary_Title, workReportInformations[5][9]);
            replacements.Add(Profile_Code.Default.Temporary_Name, workReportInformations[5][10]);
            replacements.Add(Profile_Code.Default.Temporary_FirstName, workReportInformations[5][11]);
            replacements.Add(Profile_Code.Default.Temporary_BirthDate, workReportInformations[5][12]);
            replacements.Add(Profile_Code.Default.Temporary_Profession, workReportInformations[5][13]);
            replacements.Add(Profile_Code.Default.Generate_Date, workReportInformations[5][14]);
            replacements.Add(Profile_Code.Default.Avaibility_Data, AvaibilityTableData(workReportInformations));
            replacements.Add(Profile_Code.Default.WorkTimetable_FinaleSum, WorkTimetableFinaleSum(workReportInformations[4]));

            return replacements;
        }
        #endregion

        #region SaveWorkReport
        /// <summary>
        /// Enregistre le contenu HTML dans un fichier PDF.
        /// </summary>
        /// <param name="_workReportInformations">Informations du rapport de travail</param>
        /// <returns>Boolean : True -> Fichier créé. False -> Fichier non créé.</returns>
        internal static bool SaveWorkReport(List<string> _workReportInformations)
        {
            List<string> workReportInformations = _workReportInformations;

            try
            {
                byte[] pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(workReportInformations[17]);
                File.WriteAllBytes(workReportInformations[16], pdfBytes);
                return true;
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Profile_Err.Default.SaveWorkReport, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region TemporaryInformations
        /// <summary>
        /// Assigne aux contrôles les informations de l'intérimaire.
        /// </summary>
        /// <param name="_temporaryID">Id de l'intérimaire</param>
        internal static List<string> TemporaryInformations()
        {
            List<string> temporaryInformations = new List<string>();

            temporaryInformations.Add(InterimDatabase.GetTemporaryTitle(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryName(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryFirstName(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryAge(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryNumber(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryStreet(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryPostCode(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryCity(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryRegion(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryPhoneNumber(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryEmailAdress(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryProfession(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryNbrYearExp(TemporaryID));
            temporaryInformations.Add(InterimDatabase.GetTemporaryEmploymentAgency(TemporaryID));

            return temporaryInformations;
        }
        #endregion

        #region TryOpenFile
        /// <summary>
        /// Essaye d'ouvrir le fichier avec le programme par défaut.
        /// </summary>
        /// <param name="_filePath">Chemin du fichier</param>
        internal static void TryOpenFile(string _filePath)
        {
            string filePath = _filePath;

            try
            {
                Process.Start(filePath);
            }
            catch
            {
                // Affiche un message d'erreur.
                MessageBox.Show(Profile_Err.Default.TryOpenFile, Profile_Err.Default.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        #endregion

        #region WorkTimetableFinaleSum
        /// <summary>
        /// Calcul la total des totaux des heures de travail.
        /// </summary>
        /// <param name="_workTimetableSum">Totaux des heures de travail</param>
        /// <returns>String : Total des totaux des heures de travail.</returns>
        private static string WorkTimetableFinaleSum(List<string> _workTimetableSum)
        {
            List<string> workTimetableSum = _workTimetableSum;
            string workTimetableFinaleSum = string.Empty;
            double hours = 0;
            double minutes = 0;

            for (int index = 0; index < workTimetableSum.Count; index++)
            {
                minutes += ((Convert.ToDateTime(workTimetableSum[index]).Minute * 0.5) / 30);
                hours += Convert.ToDateTime(workTimetableSum[index]).Hour + minutes;

                workTimetableFinaleSum = hours.ToString();
            }

            return workTimetableFinaleSum;
        }
        #endregion

        #endregion

    }
}
