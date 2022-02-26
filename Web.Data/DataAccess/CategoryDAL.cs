namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CategoryDAL : RepositoryBase<WebEntities, Category> , ICategoryDAL
    {
        public CategoryDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICategoryDAL : IRepositoryBase<Category>
    {
    }
    
}
