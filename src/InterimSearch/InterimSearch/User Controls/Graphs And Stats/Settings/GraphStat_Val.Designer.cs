﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterimSearch.User_Controls.Graphs_And_Stats.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class GraphStat_Val : global::System.Configuration.ApplicationSettingsBase {
        
        private static GraphStat_Val defaultInstance = ((GraphStat_Val)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GraphStat_Val())));
        
        public static GraphStat_Val Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Graphiques et statistiques")]
        public string GraphStatTitle {
            get {
                return ((string)(this["GraphStatTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Nombre\r\nd\'intérimaires\r\npar profession")]
        public string TemporaryProfession {
            get {
                return ((string)(this["TemporaryProfession"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Missions\r\nacceptées\r\npar service")]
        public string AcceptedMissionService {
            get {
                return ((string)(this["AcceptedMissionService"]));
            }
            set {
                this["AcceptedMissionService"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Nombre\r\nd\'intérimaires par\r\nagence d\'intérim")]
        public string TemporaryEmploymentAgency {
            get {
                return ((string)(this["TemporaryEmploymentAgency"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Missions acceptées")]
        public string AcceptedMission {
            get {
                return ((string)(this["AcceptedMission"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Missions refusées")]
        public string RejectedMission {
            get {
                return ((string)(this["RejectedMission"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Mission en attente")]
        public string WaitedMission {
            get {
                return ((string)(this["WaitedMission"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Impossible de charger les informations pour construire le graphique.")]
        public string NullGraphInformations {
            get {
                return ((string)(this["NullGraphInformations"]));
            }
        }
    }
}