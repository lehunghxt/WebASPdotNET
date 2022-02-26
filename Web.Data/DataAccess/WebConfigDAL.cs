namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class WebConfigDAL : RepositoryBase<WebEntities, WebConfig> , IWebConfigDAL
    {
        public WebConfigDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IWebConfigDAL : IRepositoryBase<WebConfig>
    {
    }
    
}
