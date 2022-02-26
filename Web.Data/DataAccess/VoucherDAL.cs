namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class VoucherDAL : RepositoryBase<WebEntities, Voucher> , IVoucherDAL
    {
        public VoucherDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IVoucherDAL : IRepositoryBase<Voucher>
    {
    }
    
}
