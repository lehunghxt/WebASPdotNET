namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductAddOnDAL : RepositoryBase<WebEntities, ProductAddOn> , IProductAddOnDAL
    {
        public ProductAddOnDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductAddOnDAL : IRepositoryBase<ProductAddOn>
    {
    }
    
}
