
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class CompanyInfoModel
    {
		public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string IMAGE { get; set; }
        public string PathImage { get; set; }
        public string WARDID { get; set; }
        public string FULLNAME { get; set; }
		public string DISPLAYNAME { get; set; }
		public string DESCRIPTION { get; set; }
		public string ABOUTUS { get; set; }
		public string ADDRESS { get; set; }
		public string SLOGAN { get; set; }
        public string Certificate { get; set; }

        public string DefaultLanguage { get; set; }
        public string WebIcon { get; set; }
        public string PathWebIcon { get; set; }
        public string GoogleAnalytics { get; set; }
        public string VerifyOutside { get; set; }
        public string Background { get; set; }
        public string BackgroundRepeat { get; set; }
        public string BackgroundPosition { get; set; }
        public string HeaderBackground { get; set; }
        public string HeaderFontColor { get; set; }
        public int HeaderFontSize { get; set; }
        public string FooterBackground { get; set; }
        public string FooterFontColor { get; set; }
        public int FooterFontSize { get; set; }
        public bool IsRightClick { get; set; }
        public bool IsSelectCoppy { get; set; }
        public string DefaultTemplate { get; set; }
        public string TemplateConfigBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyByUser { get; set; }
        public string Keys { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> RegisDate { get; set; }
        public Nullable<System.DateTime> ExperDate { get; set; }
        public string LinkGoPublic { get; set; }
        public bool Hierarchy { get; set; }
        public string GoogleApiKey { get; set; }
        public string GoogleMapAddress { get; set; }
        public string GooglePlus { get; set; }
        public string FacebookPersonalId { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookFanpage { get; set; }
        public Nullable<bool> MailEnableSSL { get; set; }
        public string MailServer { get; set; }
        public Nullable<int> MailPort { get; set; }
        public string MailAccount { get; set; }
        public string GHNUserName { get; set; }
        public Nullable<int> GHNFromDistrict { get; set; }
        public string GHTKAddressId { get; set; }
        public string Twitter { get; set; }
        public string Pinterest { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Youtube { get; set; }
        public string Zalo { get; set; }
        public string Whatsapp { get; set; }
    }
}  
