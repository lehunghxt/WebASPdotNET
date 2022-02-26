namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class DistrictDAL : RepositoryBase<WebEntities, District> , IDistrictDAL
    {
        public DistrictDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IDistrictDAL : IRepositoryBase<District>
    {
    }
    
}
