namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleParamDAL : RepositoryBase<WebEntities, ModuleParam> , IModuleParamDAL
    {
        public ModuleParamDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleParamDAL : IRepositoryBase<ModuleParam>
    {
    }
    
}
