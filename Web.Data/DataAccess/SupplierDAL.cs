namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class SupplierDAL : RepositoryBase<WebEntities, Supplier> , ISupplierDAL
    {
        public SupplierDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ISupplierDAL : IRepositoryBase<Supplier>
    {
    }
    
}
