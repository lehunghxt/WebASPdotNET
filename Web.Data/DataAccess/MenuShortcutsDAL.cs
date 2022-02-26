namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class MenuShortcutsDAL : RepositoryBase<WebEntities, MenuShortcuts> , IMenuShortcutsDAL
    {
        public MenuShortcutsDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IMenuShortcutsDAL : IRepositoryBase<MenuShortcuts>
    {
    }
    
}
