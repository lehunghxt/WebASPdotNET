namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleConfigParamDAL : RepositoryBase<WebEntities, ModuleConfigParam> , IModuleConfigParamDAL
    {
        public ModuleConfigParamDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleConfigParamDAL : IRepositoryBase<ModuleConfigParam>
    {
    }
    
}
