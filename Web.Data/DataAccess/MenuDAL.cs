namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class MenuDAL : RepositoryBase<WebEntities, Menu> , IMenuDAL
    {
        public MenuDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IMenuDAL : IRepositoryBase<Menu>
    {
    }
    
}
