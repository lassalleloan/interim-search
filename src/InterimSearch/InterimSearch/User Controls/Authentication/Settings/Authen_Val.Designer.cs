﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterimSearch.User_Controls.Authentication.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Authen_Val : global::System.Configuration.ApplicationSettingsBase {
        
        private static Authen_Val defaultInstance = ((Authen_Val)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Authen_Val())));
        
        public static Authen_Val Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Le mot de passe est incorrect.")]
        public string NullPassword {
            get {
                return ((string)(this["NullPassword"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Connexion")]
        public string AuthenTitle {
            get {
                return ((string)(this["AuthenTitle"]));
            }
        }
    }
}
