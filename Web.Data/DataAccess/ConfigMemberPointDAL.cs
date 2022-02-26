namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ConfigMemberPointDAL : RepositoryBase<WebEntities, ConfigMemberPoint> , IConfigMemberPointDAL
    {
        public ConfigMemberPointDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IConfigMemberPointDAL : IRepositoryBase<ConfigMemberPoint>
    {
    }
    
}
