namespace URM.Data
{
	using Infrastructure;

    public partial class UserInfoDAL : RepositoryBase<URMEntities, UserInfo> , IUserInfoDAL
    {
        public UserInfoDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserInfoDAL : IRepositoryBase<UserInfo>
    {
    }
    
}
