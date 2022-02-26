namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ArticleRelatiedDAL : RepositoryBase<WebEntities, ArticleRelatied> , IArticleRelatiedDAL
    {
        public ArticleRelatiedDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IArticleRelatiedDAL : IRepositoryBase<ArticleRelatied>
    {
    }
    
}
