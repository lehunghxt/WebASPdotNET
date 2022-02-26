namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CategoryFixDAL : RepositoryBase<WebEntities, CategoryFix> , ICategoryFixDAL
    {
        public CategoryFixDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICategoryFixDAL : IRepositoryBase<CategoryFix>
    {
    }
    
}
