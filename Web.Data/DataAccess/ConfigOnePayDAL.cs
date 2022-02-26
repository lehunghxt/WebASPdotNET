namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ConfigOnePayDAL : RepositoryBase<WebEntities, ConfigOnePay> , IConfigOnePayDAL
    {
        public ConfigOnePayDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IConfigOnePayDAL : IRepositoryBase<ConfigOnePay>
    {
    }
    
}
