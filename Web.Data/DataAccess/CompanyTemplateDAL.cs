namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyTemplateDAL : RepositoryBase<WebEntities, CompanyTemplate> , ICompanyTemplateDAL
    {
        public CompanyTemplateDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyTemplateDAL : IRepositoryBase<CompanyTemplate>
    {
    }
    
}
