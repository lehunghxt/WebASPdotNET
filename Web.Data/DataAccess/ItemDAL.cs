namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemDAL : RepositoryBase<WebEntities, Item> , IItemDAL
    {
        public ItemDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemDAL : IRepositoryBase<Item>
    {
    }
    
}
