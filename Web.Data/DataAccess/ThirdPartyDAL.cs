namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ThirdPartyDAL : RepositoryBase<WebEntities, ThirdParty> , IThirdPartyDAL
    {
        public ThirdPartyDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IThirdPartyDAL : IRepositoryBase<ThirdParty>
    {
    }
    
}
