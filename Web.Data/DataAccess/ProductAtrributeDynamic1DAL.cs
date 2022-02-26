namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ProductAtrributeDynamic1DAL : RepositoryBase<WebEntities, ProductAtrributeDynamic1> , IProductAtrributeDynamic1DAL
    {
        public ProductAtrributeDynamic1DAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IProductAtrributeDynamic1DAL : IRepositoryBase<ProductAtrributeDynamic1>
    {
    }
    
}
