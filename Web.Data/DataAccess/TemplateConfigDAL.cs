namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateConfigDAL : RepositoryBase<WebEntities, TemplateConfig> , ITemplateConfigDAL
    {
        public TemplateConfigDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateConfigDAL : IRepositoryBase<TemplateConfig>
    {
    }
    
}
