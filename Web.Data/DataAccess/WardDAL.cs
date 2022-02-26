namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class WardDAL : RepositoryBase<WebEntities, Ward> , IWardDAL
    {
        public WardDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IWardDAL : IRepositoryBase<Ward>
    {
    }
    
}
