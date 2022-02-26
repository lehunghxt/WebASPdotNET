namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleDAL : RepositoryBase<WebEntities, Module> , IModuleDAL
    {
        public ModuleDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleDAL : IRepositoryBase<Module>
    {
    }
    
}
