namespace Web.Asp.Provider
{
    /// <summary>
    /// Class that managed application settings from every sources.
    /// </summary>
    public class Constant
    {
        #region Cache Application
        public string AppModuleCache = "ModuleData";
        public string AppTemplateCache = "TemplateData";
        public string AppSEOLinkCache = "SEOLinkData";
        public string AppCompanyDomainMapCache = "CompanyDomainMapData";
        public string LanguageCache = "LanguageData";
        public string AllDataCache = "AllDataCache";
        public string AllMapCache = "AllMapCache";
        public string CheckConfigCache = "CheckConfigData";
        #endregion

        #region Cache Session
        public string SessionLicense = "vit-checkLicense";
        public string SessionCompanyConfig = "vit-company-config";
        public string SessionCompanyFullConfig = "vit-company-fullconfig";
        public string SessionCompanyInfo = "vit-company-info";
        public string SessionLanguage = "vit-company-language";
        public string SessionGioHang = "vit-GioHang";
        public string SessionDaXem = "vit-DaXem";
        public string SessionDomain = "ser-Domain";
        public string SesionToken = "SesionToken";
        #endregion

        #region Defaule value
        public string DefauleLanguage = "vi-VN";
        #endregion

        #region Constante
        public string ConstParamChangeTemplate = "tpl";
        public string JsonPage = "JsonPost.aspx";
        public string PermissonUpdateKey = "UpdateKey";
        public string PermissonLoginAdmin = "LoginAdmin";
        public string PermissonLoginCRM = "CRMControl";

        public int MaxLengthTitle = 60;
        public int MaxLengthDescription = 150;

        public string CookeiLogin = "Log{0}";
        public string CookeiTockenKey = "token";
        public string CookeiAppIdKey = "appId";
        public int CookeiTimeout = 7;
        #endregion

        #region Send Key
        public string SendCustomer = "sCus";
        public string SendOrder = "sOrd";
        public string SendCompany = "sCo";
        public string SendProduct = "sPro";
        public string SendArticle = "sArt";
        public string SendDocument = "sDoc";
        public string SendCategory = "sCat";
        public string SendColor = "sCol";
        public string SendMember = "sMem";
        public string SendModule = "sDul";
        public string SendTemplate = "sTem";
        public string SendComponent = "sCom";
        public string SendSkin = "Skin";
        public string SendAttributeId = "sAtrId";
        public string SendAttributeValue = "sAtrVl";
        public string SendSupplier = "sSup";
        public string SendModel = "sMod";
        public string SendTag = "Tag";
        public string SendDomain = "sDom";
        public string SendClearData = "sCld";
        public string SendLanguage = "sLag";
        public string SendDate = "sDat";
        public string SendGcseSearch = "sq";
        public string SendMedia = "sMid";
        #endregion

        #region Link
        public string PathArticleImage = "Articles/";
        public string PathMediaFile = "Medias/";
        public string PathDocumentFile = "Documents/";
        public string PathProductImage = "Products/";

        public string PathStyleImage = "Styles/";
        public string PathColorImage = "Colors/";

        public string PathCategoryImage = "Categories/";

        public string PathItemImage = "Items/";

        public string PathCompanyImage = "Webs/";

        public string PathTemplateImage = "Templates/";

        public string PathLanguageFile = "Languages/";

        public string FolderTemp = "Temps/";
        #endregion
    }
}