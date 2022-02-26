namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleConfigLanguageDAL : RepositoryBase<WebEntities, ModuleConfigLanguage> , IModuleConfigLanguageDAL
    {
        public ModuleConfigLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleConfigLanguageDAL : IRepositoryBase<ModuleConfigLanguage>
    {
    }
    
}
