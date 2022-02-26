namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TB_ThongKeDAL : RepositoryBase<WebEntities, TB_ThongKe> , ITB_ThongKeDAL
    {
        public TB_ThongKeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITB_ThongKeDAL : IRepositoryBase<TB_ThongKe>
    {
    }
    
}
