namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ArticleLinkDAL : RepositoryBase<WebEntities, ArticleLink> , IArticleLinkDAL
    {
        public ArticleLinkDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IArticleLinkDAL : IRepositoryBase<ArticleLink>
    {
    }
    
}
