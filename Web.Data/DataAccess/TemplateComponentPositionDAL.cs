namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateComponentPositionDAL : RepositoryBase<WebEntities, TemplateComponentPosition> , ITemplateComponentPositionDAL
    {
        public TemplateComponentPositionDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateComponentPositionDAL : IRepositoryBase<TemplateComponentPosition>
    {
    }
    
}
