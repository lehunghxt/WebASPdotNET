namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductDAL : RepositoryBase<WebEntities, Product> , IProductDAL
    {
        public ProductDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductDAL : IRepositoryBase<Product>
    {
    }
    
}
