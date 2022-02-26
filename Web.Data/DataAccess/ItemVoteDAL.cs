namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemVoteDAL : RepositoryBase<WebEntities, ItemVote> , IItemVoteDAL
    {
        public ItemVoteDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemVoteDAL : IRepositoryBase<ItemVote>
    {
    }
    
}
