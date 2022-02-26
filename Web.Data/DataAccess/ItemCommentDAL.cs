namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemCommentDAL : RepositoryBase<WebEntities, ItemComment> , IItemCommentDAL
    {
        public ItemCommentDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemCommentDAL : IRepositoryBase<ItemComment>
    {
    }
    
}
