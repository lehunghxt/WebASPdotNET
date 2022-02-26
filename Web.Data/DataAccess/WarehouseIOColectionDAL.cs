namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class WarehouseIOColectionDAL : RepositoryBase<WebEntities, WarehouseIOColection> , IWarehouseIOColectionDAL
    {
        public WarehouseIOColectionDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IWarehouseIOColectionDAL : IRepositoryBase<WarehouseIOColection>
    {
    }
    
}
