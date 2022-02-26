namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemImageDAL : RepositoryBase<WebEntities, ItemImage> , IItemImageDAL
    {
        public ItemImageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemImageDAL : IRepositoryBase<ItemImage>
    {
    }
    
}
