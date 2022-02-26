namespace Web.Asp.UI
{
    using Security;
    using Web.Asp.Provider;
    using Web.Model;

    interface IWebUI
    {
        URL HREF { get; }
        LanguageHelper Language { get; }
        UserPrincipal UserContext { get; }

        CompanyConfigModel Config { get; }
    }
}
