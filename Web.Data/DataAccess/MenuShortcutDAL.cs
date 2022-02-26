namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class MenuShortcutDAL : RepositoryBase<WebEntities, MenuShortcut> , IMenuShortcutDAL
    {
        public MenuShortcutDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IMenuShortcutDAL : IRepositoryBase<MenuShortcut>
    {
    }
    
}
