namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class WarehouseIODAL : RepositoryBase<WebEntities, WarehouseIO> , IWarehouseIODAL
    {
        public WarehouseIODAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IWarehouseIODAL : IRepositoryBase<WarehouseIO>
    {
    }
    
}
