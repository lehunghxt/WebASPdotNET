namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class SEODAL : RepositoryBase<WebEntities, SEO> , ISEODAL
    {
        public SEODAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ISEODAL : IRepositoryBase<SEO>
    {
    }
    
}
