namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateDAL : RepositoryBase<WebEntities, Template> , ITemplateDAL
    {
        public TemplateDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateDAL : IRepositoryBase<Template>
    {
    }
    
}
