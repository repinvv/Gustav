﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gustav.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Red")]
        public global::System.Drawing.Color BodyColor {
            get {
                return ((global::System.Drawing.Color)(this["BodyColor"]));
            }
            set {
                this["BodyColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color GunColor {
            get {
                return ((global::System.Drawing.Color)(this["GunColor"]));
            }
            set {
                this["GunColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color RadarColor {
            get {
                return ((global::System.Drawing.Color)(this["RadarColor"]));
            }
            set {
                this["RadarColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("GreenYellow")]
        public global::System.Drawing.Color BulletColor {
            get {
                return ((global::System.Drawing.Color)(this["BulletColor"]));
            }
            set {
                this["BulletColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public long ScanExpiration {
            get {
                return ((long)(this["ScanExpiration"]));
            }
            set {
                this["ScanExpiration"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("70")]
        public double MaxPowerDistance {
            get {
                return ((double)(this["MaxPowerDistance"]));
            }
            set {
                this["MaxPowerDistance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.3")]
        public double MinPower {
            get {
                return ((double)(this["MinPower"]));
            }
            set {
                this["MinPower"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("120")]
        public double DistanceReductionRate {
            get {
                return ((double)(this["DistanceReductionRate"]));
            }
            set {
                this["DistanceReductionRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public double TargettingTolerance {
            get {
                return ((double)(this["TargettingTolerance"]));
            }
            set {
                this["TargettingTolerance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.001")]
        public double ComparisionTolerance {
            get {
                return ((double)(this["ComparisionTolerance"]));
            }
            set {
                this["ComparisionTolerance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public double ScanArc {
            get {
                return ((double)(this["ScanArc"]));
            }
            set {
                this["ScanArc"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("loyalist.log")]
        public string Logfile {
            get {
                return ((string)(this["Logfile"]));
            }
            set {
                this["Logfile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public double RotationReductionRate {
            get {
                return ((double)(this["RotationReductionRate"]));
            }
            set {
                this["RotationReductionRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.8")]
        public double TargetSpeedAdjust {
            get {
                return ((double)(this["TargetSpeedAdjust"]));
            }
            set {
                this["TargetSpeedAdjust"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("600")]
        public double MaxFireDistance {
            get {
                return ((double)(this["MaxFireDistance"]));
            }
            set {
                this["MaxFireDistance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public double TurnVelocity {
            get {
                return ((double)(this["TurnVelocity"]));
            }
            set {
                this["TurnVelocity"] = value;
            }
        }
    }
}
