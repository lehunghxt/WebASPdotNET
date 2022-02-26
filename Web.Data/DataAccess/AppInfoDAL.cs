namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AppInfoDAL : RepositoryBase<WebEntities, AppInfo> , IAppInfoDAL
    {
        public AppInfoDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAppInfoDAL : IRepositoryBase<AppInfo>
    {
    }
    
}
