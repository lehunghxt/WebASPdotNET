namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleConfigDAL : RepositoryBase<WebEntities, ModuleConfig> , IModuleConfigDAL
    {
        public ModuleConfigDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleConfigDAL : IRepositoryBase<ModuleConfig>
    {
    }
    
}
