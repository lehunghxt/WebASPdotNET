namespace URM.Data
{
	using Infrastructure;

    public partial class AppInfoDAL : RepositoryBase<URMEntities, AppInfo> , IAppInfoDAL
    {
        public AppInfoDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAppInfoDAL : IRepositoryBase<AppInfo>
    {
    }
    
}
