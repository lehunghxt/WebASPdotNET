namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemLikeDAL : RepositoryBase<WebEntities, ItemLike> , IItemLikeDAL
    {
        public ItemLikeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemLikeDAL : IRepositoryBase<ItemLike>
    {
    }
    
}
