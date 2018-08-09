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
using System;
using System.Runtime.InteropServices;
#endregion

namespace InterimSearch.User_Controls.Authentication
{
    public static class Authen
    {

        #region DllImport

        // Fournit les informations nécessaires pour appeler une fonction exportée à partir d'une DLL non managée.

        #region advapi32
        [DllImport("advapi32.dll")]
        private static extern bool LogonUser(string name, string domain, string password, int logType, int logPV, ref IntPtr pht);
        #endregion

        #endregion

        #region Methods

        #region IsUserAuthenticated
        /// <summary>
        /// Authentifie l'utilisateur avec ses identifiants de session Windows.
        /// </summary>
        /// <param name="_password">Mot de passe</param>
        /// <returns>Boolean : True -> Identifiants de connexion corrects. False -> Identifiants de connexion incorrects.</returns>
        internal static bool IsUserAuthenticated(string _password)
        {
            IntPtr th = IntPtr.Zero;
            string password = _password;
            bool isConnected = LogonUser(Environment.UserName, Environment.UserDomainName, password, 2, 0, ref th);

            return isConnected;
        }
        #endregion

        #endregion

    }
}
