namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ConfigAccessTradeDAL : RepositoryBase<WebEntities, ConfigAccessTrade> , IConfigAccessTradeDAL
    {
        public ConfigAccessTradeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IConfigAccessTradeDAL : IRepositoryBase<ConfigAccessTrade>
    {
    }
    
}
