namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class FileDAL : RepositoryBase<WebEntities, File> , IFileDAL
    {
        public FileDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IFileDAL : IRepositoryBase<File>
    {
    }
    
}
