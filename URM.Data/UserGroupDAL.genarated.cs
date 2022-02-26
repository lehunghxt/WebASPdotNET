namespace URM.Data
{
	using Infrastructure;

    public partial class UserGroupDAL : RepositoryBase<URMEntities, UserGroup> , IUserGroupDAL
    {
        public UserGroupDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserGroupDAL : IRepositoryBase<UserGroup>
    {
    }
    
}
