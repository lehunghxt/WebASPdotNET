namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeDAL : RepositoryBase<WebEntities, Attribute> , IAttributeDAL
    {
        public AttributeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeDAL : IRepositoryBase<Attribute>
    {
    }
    
}
