namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyUserDAL : RepositoryBase<WebEntities, CompanyUser> , ICompanyUserDAL
    {
        public CompanyUserDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyUserDAL : IRepositoryBase<CompanyUser>
    {
    }
    
}
