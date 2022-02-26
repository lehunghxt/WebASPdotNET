namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CategoryTypeDAL : RepositoryBase<WebEntities, CategoryType> , ICategoryTypeDAL
    {
        public CategoryTypeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICategoryTypeDAL : IRepositoryBase<CategoryType>
    {
    }
    
}
