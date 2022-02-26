namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyDAL : RepositoryBase<WebEntities, Company> , ICompanyDAL
    {
        public CompanyDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyDAL : IRepositoryBase<Company>
    {
    }
    
}
