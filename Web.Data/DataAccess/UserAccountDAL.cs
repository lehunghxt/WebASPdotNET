namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class UserAccountDAL : RepositoryBase<WebEntities, UserAccount> , IUserAccountDAL
    {
        public UserAccountDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserAccountDAL : IRepositoryBase<UserAccount>
    {
    }
    
}
