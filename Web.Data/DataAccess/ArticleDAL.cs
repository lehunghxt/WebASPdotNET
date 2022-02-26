namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ArticleDAL : RepositoryBase<WebEntities, Article> , IArticleDAL
    {
        public ArticleDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IArticleDAL : IRepositoryBase<Article>
    {
    }
    
}
