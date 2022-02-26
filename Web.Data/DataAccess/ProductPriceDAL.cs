namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductPriceDAL : RepositoryBase<WebEntities, ProductPrice> , IProductPriceDAL
    {
        public ProductPriceDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductPriceDAL : IRepositoryBase<ProductPrice>
    {
    }
    
}
