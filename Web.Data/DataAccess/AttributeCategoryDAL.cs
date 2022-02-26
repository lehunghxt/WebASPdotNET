namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeCategoryDAL : RepositoryBase<WebEntities, AttributeCategory> , IAttributeCategoryDAL
    {
        public AttributeCategoryDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeCategoryDAL : IRepositoryBase<AttributeCategory>
    {
    }
    
}
