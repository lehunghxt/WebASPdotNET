namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class OrderTransactionDAL : RepositoryBase<WebEntities, OrderTransaction> , IOrderTransactionDAL
    {
        public OrderTransactionDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IOrderTransactionDAL : IRepositoryBase<OrderTransaction>
    {
    }
    
}
