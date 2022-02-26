namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class UserGroupDAL : RepositoryBase<WebEntities, UserGroup> , IUserGroupDAL
    {
        public UserGroupDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserGroupDAL : IRepositoryBase<UserGroup>
    {
    }
    
}
