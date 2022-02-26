namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProvinceDAL : RepositoryBase<WebEntities, Province> , IProvinceDAL
    {
        public ProvinceDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProvinceDAL : IRepositoryBase<Province>
    {
    }
    
}
