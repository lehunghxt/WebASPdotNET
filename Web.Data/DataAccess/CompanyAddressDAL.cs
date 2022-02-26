namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyAddressDAL : RepositoryBase<WebEntities, CompanyAddress> , ICompanyAddressDAL
    {
        public CompanyAddressDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyAddressDAL : IRepositoryBase<CompanyAddress>
    {
    }
    
}
