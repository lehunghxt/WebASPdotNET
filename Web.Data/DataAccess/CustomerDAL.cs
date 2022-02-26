namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CustomerDAL : RepositoryBase<WebEntities, Customer> , ICustomerDAL
    {
        public CustomerDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICustomerDAL : IRepositoryBase<Customer>
    {
    }
    
}
