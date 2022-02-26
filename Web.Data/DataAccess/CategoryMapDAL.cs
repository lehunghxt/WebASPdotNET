namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CategoryMapDAL : RepositoryBase<WebEntities, CategoryMap> , ICategoryMapDAL
    {
        public CategoryMapDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICategoryMapDAL : IRepositoryBase<CategoryMap>
    {
    }
    
}
