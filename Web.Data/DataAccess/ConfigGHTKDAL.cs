namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ConfigGHTKDAL : RepositoryBase<WebEntities, ConfigGHTK> , IConfigGHTKDAL
    {
        public ConfigGHTKDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IConfigGHTKDAL : IRepositoryBase<ConfigGHTK>
    {
    }
    
}
