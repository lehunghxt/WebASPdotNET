namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyLanguageDAL : RepositoryBase<WebEntities, CompanyLanguage> , ICompanyLanguageDAL
    {
        public CompanyLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyLanguageDAL : IRepositoryBase<CompanyLanguage>
    {
    }
    
}
