namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateDemoDAL : RepositoryBase<WebEntities, TemplateDemo> , ITemplateDemoDAL
    {
        public TemplateDemoDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateDemoDAL : IRepositoryBase<TemplateDemo>
    {
    }
    
}
