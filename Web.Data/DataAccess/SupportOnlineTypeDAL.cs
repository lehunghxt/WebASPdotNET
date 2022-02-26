namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class SupportOnlineTypeDAL : RepositoryBase<WebEntities, SupportOnlineType> , ISupportOnlineTypeDAL
    {
        public SupportOnlineTypeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ISupportOnlineTypeDAL : IRepositoryBase<SupportOnlineType>
    {
    }
    
}
