namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CategoryLanguageDAL : RepositoryBase<WebEntities, CategoryLanguage> , ICategoryLanguageDAL
    {
        public CategoryLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICategoryLanguageDAL : IRepositoryBase<CategoryLanguage>
    {
    }
    
}
