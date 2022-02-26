namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductAtrributeDynamicDAL : RepositoryBase<WebEntities, ProductAtrributeDynamic> , IProductAtrributeDynamicDAL
    {
        public ProductAtrributeDynamicDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductAtrributeDynamicDAL : IRepositoryBase<ProductAtrributeDynamic>
    {
    }
    
}
