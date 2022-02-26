namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class InventoryDAL : RepositoryBase<WebEntities, Inventory> , IInventoryDAL
    {
        public InventoryDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IInventoryDAL : IRepositoryBase<Inventory>
    {
    }
    
}
