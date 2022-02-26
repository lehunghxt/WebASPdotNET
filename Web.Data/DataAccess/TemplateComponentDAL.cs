namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateComponentDAL : RepositoryBase<WebEntities, TemplateComponent> , ITemplateComponentDAL
    {
        public TemplateComponentDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateComponentDAL : IRepositoryBase<TemplateComponent>
    {
    }
    
}
