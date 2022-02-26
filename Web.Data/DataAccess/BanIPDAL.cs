namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class BanIPDAL : RepositoryBase<WebEntities, BanIP> , IBanIPDAL
    {
        public BanIPDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IBanIPDAL : IRepositoryBase<BanIP>
    {
    }
    
}
