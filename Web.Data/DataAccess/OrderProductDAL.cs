namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class OrderProductDAL : RepositoryBase<WebEntities, OrderProduct> , IOrderProductDAL
    {
        public OrderProductDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IOrderProductDAL : IRepositoryBase<OrderProduct>
    {
    }
    
}
