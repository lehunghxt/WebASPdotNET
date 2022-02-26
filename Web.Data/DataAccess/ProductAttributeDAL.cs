namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductAttributeDAL : RepositoryBase<WebEntities, ProductAttribute> , IProductAttributeDAL
    {
        public ProductAttributeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductAttributeDAL : IRepositoryBase<ProductAttribute>
    {
    }
    
}
