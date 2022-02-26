namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductColorDAL : RepositoryBase<WebEntities, ProductColor> , IProductColorDAL
    {
        public ProductColorDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductColorDAL : IRepositoryBase<ProductColor>
    {
    }
    
}
