﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterimSearch.User_Controls.Home.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Home_Val : global::System.Configuration.ApplicationSettingsBase {
        
        private static Home_Val defaultInstance = ((Home_Val)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Home_Val())));
        
        public static Home_Val Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Accueil")]
        public string HomeTitle {
            get {
                return ((string)(this["HomeTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Plus de critères")]
        public string ShowCriteria {
            get {
                return ((string)(this["ShowCriteria"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Moins de critères")]
        public string HideCriteria {
            get {
                return ((string)(this["HideCriteria"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("La date sélectionnée est incorrecte.")]
        public string NullDate {
            get {
                return ((string)(this["NullDate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Toutes")]
        public string AllProfession {
            get {
                return ((string)(this["AllProfession"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Impossible de charger les noms des professions.")]
        public string ProfessionListNull {
            get {
                return ((string)(this["ProfessionListNull"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("La profession sélectionnée est incorrecte.")]
        public string NullProfession {
            get {
                return ((string)(this["NullProfession"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Impossible de charger les horaires de travail.")]
        public string WorkTimetableListNull {
            get {
                return ((string)(this["WorkTimetableListNull"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("L\'horaire de travail sélectionné est incorrect.")]
        public string NullWorkTimetable {
            get {
                return ((string)(this["NullWorkTimetable"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(" - ")]
        public string BetweenTime {
            get {
                return ((string)(this["BetweenTime"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("La connexion avec la base de données a été interrompu.\r\nVeuillez relancer l\'appli" +
            "cation")]
        public string LaunchAgain {
            get {
                return ((string)(this["LaunchAgain"]));
            }
        }
    }
}