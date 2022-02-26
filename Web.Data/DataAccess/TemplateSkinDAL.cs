namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateSkinDAL : RepositoryBase<WebEntities, TemplateSkin> , ITemplateSkinDAL
    {
        public TemplateSkinDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateSkinDAL : IRepositoryBase<TemplateSkin>
    {
    }
    
}
