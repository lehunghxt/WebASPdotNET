namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemViewDAL : RepositoryBase<WebEntities, ItemView> , IItemViewDAL
    {
        public ItemViewDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemViewDAL : IRepositoryBase<ItemView>
    {
    }
    
}
