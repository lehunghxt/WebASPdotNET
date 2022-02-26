namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class GroupDAL : RepositoryBase<WebEntities, Group> , IGroupDAL
    {
        public GroupDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IGroupDAL : IRepositoryBase<Group>
    {
    }
    
}
