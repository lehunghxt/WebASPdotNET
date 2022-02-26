namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplatePositionDAL : RepositoryBase<WebEntities, TemplatePosition> , ITemplatePositionDAL
    {
        public TemplatePositionDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplatePositionDAL : IRepositoryBase<TemplatePosition>
    {
    }
    
}
