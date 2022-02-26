namespace Web.Backend
{
    /// <sURMmary>
    /// Class that managed application settings from every sources.
    /// </sURMmary>
    public class Constant
    {
        #region Constante
        public string AdminName = "Administrator";
        public string CookeiLogin = "Log{0}";
        public string CookeiTockenKey = "token";
        public string CookeiAppIdKey = "appId";
        public int CookeiTimeout = 7;
        #endregion

        #region Session
        public string SessionLicense = "vit-checkLicense";
        public string SessionCompanyConfig = "vit-company-config";
        public string SessionCompanyInfo = "vit-company-info";
        public string SessionGioHang = "vit-GioHang";
        public string SessionDomain = "ser-Domain";
        public string SesionToken = "SesionToken";
        #endregion

        #region Path
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

        #region Send Key
        public string SendCustomer = "sCus";
        public string SendOrder = "sOrd";
        public string SendCompany = "sCo";
        public string SendProduct = "sPro";
        public string SendArticle = "sArt";
        public string SendCategory = "sCat";
        public string SendColor = "sCol";
        public string SendStyle = "sTyl";
        public string SendMember = "sMem";
        public string SendModule = "sDul";
        public string SendTemplate = "sTem";
        public string SendComponent = "sCom";
        public string SendSkin = "Skin";
        public string SendManufacturer = "sMan";
        public string SendSupplier = "sSup";
        public string SendModel = "sMod";
        public string SendType = "sTyp";
        public string SendTag = "Tag";
        public string SendDomain = "sDom";
        public string SendClearData = "sCld";
        public string SendLanguage = "sLag";
        #endregion
    }
}