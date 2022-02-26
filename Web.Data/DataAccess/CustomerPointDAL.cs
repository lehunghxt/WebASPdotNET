namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CustomerPointDAL : RepositoryBase<WebEntities, CustomerPoint> , ICustomerPointDAL
    {
        public CustomerPointDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICustomerPointDAL : IRepositoryBase<CustomerPoint>
    {
    }
    
}
