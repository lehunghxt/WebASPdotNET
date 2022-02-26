namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CustomerAddressDAL : RepositoryBase<WebEntities, CustomerAddress> , ICustomerAddressDAL
    {
        public CustomerAddressDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICustomerAddressDAL : IRepositoryBase<CustomerAddress>
    {
    }
    
}
