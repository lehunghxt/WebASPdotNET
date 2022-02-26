namespace URM.Data
{
	using Infrastructure;

    public partial class UserAccountDAL : RepositoryBase<URMEntities, UserAccount> , IUserAccountDAL
    {
        public UserAccountDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserAccountDAL : IRepositoryBase<UserAccount>
    {
    }
    
}
