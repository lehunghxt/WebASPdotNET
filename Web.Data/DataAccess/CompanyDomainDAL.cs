namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyDomainDAL : RepositoryBase<WebEntities, CompanyDomain> , ICompanyDomainDAL
    {
        public CompanyDomainDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyDomainDAL : IRepositoryBase<CompanyDomain>
    {
    }
    
}
