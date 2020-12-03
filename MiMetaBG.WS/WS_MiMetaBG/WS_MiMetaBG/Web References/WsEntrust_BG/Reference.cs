﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WS_MiMetaBG.WsEntrust_BG {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="FuncionesExtSoap", Namespace="http://tempuri.org/")]
    public partial class FuncionesExt : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback OTPgenerationOperationCompleted;
        
        private System.Threading.SendOrPostCallback OTPvalidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertaTablaInvitadosOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FuncionesExt() {
            this.Url = global::WS_MiMetaBG.Properties.Settings.Default.WS_MiMetaBG_WsEntrust_BG_FuncionesExt;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event OTPgenerationCompletedEventHandler OTPgenerationCompleted;
        
        /// <remarks/>
        public event OTPvalidateCompletedEventHandler OTPvalidateCompleted;
        
        /// <remarks/>
        public event InsertaTablaInvitadosCompletedEventHandler InsertaTablaInvitadosCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/OTPgeneration", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string OTPgeneration(string aplicacion, string servicio, string identificador, ref string cretorno, ref string err, string canal, string opid, string terminal) {
            object[] results = this.Invoke("OTPgeneration", new object[] {
                        aplicacion,
                        servicio,
                        identificador,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal});
            cretorno = ((string)(results[1]));
            err = ((string)(results[2]));
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void OTPgenerationAsync(string aplicacion, string servicio, string identificador, string cretorno, string err, string canal, string opid, string terminal) {
            this.OTPgenerationAsync(aplicacion, servicio, identificador, cretorno, err, canal, opid, terminal, null);
        }
        
        /// <remarks/>
        public void OTPgenerationAsync(string aplicacion, string servicio, string identificador, string cretorno, string err, string canal, string opid, string terminal, object userState) {
            if ((this.OTPgenerationOperationCompleted == null)) {
                this.OTPgenerationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnOTPgenerationOperationCompleted);
            }
            this.InvokeAsync("OTPgeneration", new object[] {
                        aplicacion,
                        servicio,
                        identificador,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal}, this.OTPgenerationOperationCompleted, userState);
        }
        
        private void OnOTPgenerationOperationCompleted(object arg) {
            if ((this.OTPgenerationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.OTPgenerationCompleted(this, new OTPgenerationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/OTPvalidate", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void OTPvalidate(string aplicacion, string servicio, string identificador, string key, ref string cretorno, ref string err, string canal, string opid, string terminal) {
            object[] results = this.Invoke("OTPvalidate", new object[] {
                        aplicacion,
                        servicio,
                        identificador,
                        key,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal});
            cretorno = ((string)(results[0]));
            err = ((string)(results[1]));
        }
        
        /// <remarks/>
        public void OTPvalidateAsync(string aplicacion, string servicio, string identificador, string key, string cretorno, string err, string canal, string opid, string terminal) {
            this.OTPvalidateAsync(aplicacion, servicio, identificador, key, cretorno, err, canal, opid, terminal, null);
        }
        
        /// <remarks/>
        public void OTPvalidateAsync(string aplicacion, string servicio, string identificador, string key, string cretorno, string err, string canal, string opid, string terminal, object userState) {
            if ((this.OTPvalidateOperationCompleted == null)) {
                this.OTPvalidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnOTPvalidateOperationCompleted);
            }
            this.InvokeAsync("OTPvalidate", new object[] {
                        aplicacion,
                        servicio,
                        identificador,
                        key,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal}, this.OTPvalidateOperationCompleted, userState);
        }
        
        private void OnOTPvalidateOperationCompleted(object arg) {
            if ((this.OTPvalidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.OTPvalidateCompleted(this, new OTPvalidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertaTablaInvitados", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertaTablaInvitados(string identificacion, ref string cretorno, ref string err, string canal, string opid, string terminal) {
            object[] results = this.Invoke("InsertaTablaInvitados", new object[] {
                        identificacion,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal});
            cretorno = ((string)(results[1]));
            err = ((string)(results[2]));
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void InsertaTablaInvitadosAsync(string identificacion, string cretorno, string err, string canal, string opid, string terminal) {
            this.InsertaTablaInvitadosAsync(identificacion, cretorno, err, canal, opid, terminal, null);
        }
        
        /// <remarks/>
        public void InsertaTablaInvitadosAsync(string identificacion, string cretorno, string err, string canal, string opid, string terminal, object userState) {
            if ((this.InsertaTablaInvitadosOperationCompleted == null)) {
                this.InsertaTablaInvitadosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertaTablaInvitadosOperationCompleted);
            }
            this.InvokeAsync("InsertaTablaInvitados", new object[] {
                        identificacion,
                        cretorno,
                        err,
                        canal,
                        opid,
                        terminal}, this.InsertaTablaInvitadosOperationCompleted, userState);
        }
        
        private void OnInsertaTablaInvitadosOperationCompleted(object arg) {
            if ((this.InsertaTablaInvitadosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertaTablaInvitadosCompleted(this, new InsertaTablaInvitadosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void OTPgenerationCompletedEventHandler(object sender, OTPgenerationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OTPgenerationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal OTPgenerationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string cretorno {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string err {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void OTPvalidateCompletedEventHandler(object sender, OTPvalidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OTPvalidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal OTPvalidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string cretorno {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string err {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void InsertaTablaInvitadosCompletedEventHandler(object sender, InsertaTablaInvitadosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsertaTablaInvitadosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsertaTablaInvitadosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string cretorno {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string err {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
}

#pragma warning restore 1591