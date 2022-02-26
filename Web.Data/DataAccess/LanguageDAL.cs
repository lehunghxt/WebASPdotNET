namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class LanguageDAL : RepositoryBase<WebEntities, Language> , ILanguageDAL
    {
        public LanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ILanguageDAL : IRepositoryBase<Language>
    {
    }
    
}
