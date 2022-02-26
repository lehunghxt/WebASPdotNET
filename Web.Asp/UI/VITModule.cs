namespace Web.Asp.UI
{
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using Web.Asp.Controls;
    using Web.Asp.Provider;
    using Web.Business;
    using Web.Model;
    using System.Web.UI;
    using System.Text;

    abstract public class VITModule : UserControl, IWebUI
    {
        private readonly CompanyBLL _companyProvider;

        // giao dien
        public int Id { get; set; }
        public string Title { get; set; }

        public Dictionary<string, string> Params { get; set; }

        protected VITModule()
        {
            this._companyProvider = new CompanyBLL();

            this._url = new URL();
            this._language = new LanguageHelper();
        }

        /// <summary>
        /// Lay gia tri tu Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueParam<T>(string key)
        {
            if(Params!=null && Params.ContainsKey(key))
            {
                try
                {
                    var value = Convert.ChangeType(Params[key], typeof(T));
                    return value is T ? (T)value : default(T);
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueRequest<T>(string keyRequest)
        {
            if (this.Page.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
            {
                try
                {
                    var value = Convert.ChangeType(this.Page.Request.Params.Get(keyRequest), typeof(T));
                    if (value is T) return (T)value;
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu request, neu ko co thi lay gia tri Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="keyRequest">Key cua Request</param>
        /// <param name="keyParam">Key cua Param</param>
        /// <returns>
        /// Gia tri voi kieu du lieu cu the
        /// </returns>
        protected T GetRequestThenParam<T>(string keyRequest, string keyParam)
        {
            try
            {
                if (this.Page.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
                {
                    var value = Convert.ChangeType(this.Page.Request.Params.Get(keyRequest), typeof(T));
                    if (value is T) return (T)value;
                }

                if (Params != null && Params.ContainsKey(keyParam))
                {
                    var value = Convert.ChangeType(Params[keyParam], typeof(T));
                    return value is T ? (T)value : default(T);
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu param, neu ko co thi lay gia tri request
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="keyRequest">Key cua Request</param>
        /// <param name="keyParam">Key cua Param</param>
        /// <returns>
        /// Gia tri voi kieu du lieu cu the
        /// </returns>
        protected T GetParamThenRequest<T>(string keyRequest, string keyParam)
        {
            try
            {
                if (Params != null && Params.ContainsKey(keyParam))
                {
                    var value = Convert.ChangeType(Params[keyParam], typeof(T));
                    if(value is T) return (T)value;
                }

                if (this.Page.Request.Params.AllKeys.Select(c => c.ToLower()).Contains(keyRequest.ToLower()))
                {
                    var valueParam = Convert.ChangeType(this.Page.Request.Params.Get(keyRequest), typeof(T));
                    return valueParam is T ? (T)valueParam : default(T);
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);
        }

        // hàm addmodule vào module
        protected void LoadModule(Position position, string moduleName, Dictionary<string, string> pars, string title = "")
        {
            try
            {
                var pathModule = new StringBuilder();
                pathModule.Append(HREF.TemplatePath);
                pathModule.Append("Skins/");
                pathModule.Append(moduleName);
                pathModule.Append(".ascx");
                var module = LoadControl(pathModule.ToString()) as VITModule;
                if (module == null)
                {
                    var mesage = new Label();
                    mesage.Text = string.Format("Không load được {0}", pathModule);
                    position.Controls.Add(mesage);
                    return;
                }

                position.Controls.Add(module);

                module.Title = title;
                module.Params = pars;
            }
            catch (Exception ex)
            {
                var mesage = new Label();
                mesage.Text = string.Format("Không load được module {0}: {1}", moduleName, ex.Message);
                position.Controls.Add(mesage);
            }
        }

        /// <summary>
        /// hàm add module vào component
        /// </summary>
        /// <param name="positionId">Id server của Position</param>
        /// <param name="moduleName">Tên module</param>
        /// <param name="pars">Param truyền vào module</param>
        /// <param name="title">Tên module</param>
        /// <returns>True: load thành công</returns>
        protected void LoadModule(string positionId, string moduleName, Dictionary<string, string> pars, string title = "")
        {//LoadModule
            var position = this.FindControl(positionId) as Position;
            if (position != null)
            {
                this.LoadModule(position, moduleName, pars, title);
            }
        }

        #region inherit interface
        private URL _url;
        public URL HREF
        {
            get
            {
                return this._url;
            }
        }

        private LanguageHelper _language;
        public LanguageHelper Language
        {
            get
            {
                return this._language;
            }
        }

        public UserPrincipal UserContext
        {
            get
            {
                return this.Page.User as UserPrincipal;
            }
        }

        public CompanyConfigModel Config
        {
            get
            {
                var config = (new CompanyBLL()).GetCompanyByDomain(HREF.Domain);
                var language = Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language)) config.Language = language;
                if (string.IsNullOrEmpty(config.Language)) config.Language = SettingsManager.Constants.DefauleLanguage;

                return config;
            }
        }
        #endregion
    }
}
