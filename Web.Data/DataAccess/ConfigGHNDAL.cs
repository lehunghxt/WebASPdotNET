namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ConfigGHNDAL : RepositoryBase<WebEntities, ConfigGHN> , IConfigGHNDAL
    {
        public ConfigGHNDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IConfigGHNDAL : IRepositoryBase<ConfigGHN>
    {
    }
    
}
