namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class SupportOnlineDAL : RepositoryBase<WebEntities, SupportOnline> , ISupportOnlineDAL
    {
        public SupportOnlineDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ISupportOnlineDAL : IRepositoryBase<SupportOnline>
    {
    }
    
}
