namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class UserInfoDAL : RepositoryBase<WebEntities, UserInfo> , IUserInfoDAL
    {
        public UserInfoDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserInfoDAL : IRepositoryBase<UserInfo>
    {
    }
    
}
