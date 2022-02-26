namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class RoleDAL : RepositoryBase<WebEntities, Role> , IRoleDAL
    {
        public RoleDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IRoleDAL : IRepositoryBase<Role>
    {
    }
    
}
