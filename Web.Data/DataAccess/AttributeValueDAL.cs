namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeValueDAL : RepositoryBase<WebEntities, AttributeValue> , IAttributeValueDAL
    {
        public AttributeValueDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeValueDAL : IRepositoryBase<AttributeValue>
    {
    }
    
}
