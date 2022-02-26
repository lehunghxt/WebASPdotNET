namespace URM.Data
{
	using Infrastructure;

    public partial class RoleDAL : RepositoryBase<URMEntities, Role> , IRoleDAL
    {
        public RoleDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IRoleDAL : IRepositoryBase<Role>
    {
    }
    
}
