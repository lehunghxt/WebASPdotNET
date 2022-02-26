namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductGrouponDAL : RepositoryBase<WebEntities, ProductGroupon> , IProductGrouponDAL
    {
        public ProductGrouponDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductGrouponDAL : IRepositoryBase<ProductGroupon>
    {
    }
    
}
