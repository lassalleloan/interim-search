﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterimSearch.User_Controls.Sent_Offer.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class SentOffer_Err : global::System.Configuration.ApplicationSettingsBase {
        
        private static SentOffer_Err defaultInstance = ((SentOffer_Err)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SentOffer_Err())));
        
        public static SentOffer_Err Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Erreur au sein du programme")]
        public string ErrorTitle {
            get {
                return ((string)(this["ErrorTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Une erreur s\'est produite lors de l\'accès à la méthode \"SentOffer.CreateBodyMessa" +
            "ge\".")]
        public string CreateBodyMessage {
            get {
                return ((string)(this["CreateBodyMessage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Une erreur s\'est produite lors de l\'accès à la méthode \"SentOffer.CreateMessage\"." +
            "")]
        public string CreateMessage {
            get {
                return ((string)(this["CreateMessage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Une erreur s\'est produite lors de l\'accès à la méthode \"SentOffer.SentEmail\".")]
        public string IsSentEmail {
            get {
                return ((string)(this["IsSentEmail"]));
            }
        }
    }
}
