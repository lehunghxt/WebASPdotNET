namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ArticleLanguageDAL : RepositoryBase<WebEntities, ArticleLanguage> , IArticleLanguageDAL
    {
        public ArticleLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IArticleLanguageDAL : IRepositoryBase<ArticleLanguage>
    {
    }
    
}
