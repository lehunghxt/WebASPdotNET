namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class OrderDAL : RepositoryBase<WebEntities, Order> , IOrderDAL
    {
        public OrderDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IOrderDAL : IRepositoryBase<Order>
    {
    }
    
}
