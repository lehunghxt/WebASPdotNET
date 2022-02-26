namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class FileDocumentDAL : RepositoryBase<WebEntities, FileDocument> , IFileDocumentDAL
    {
        public FileDocumentDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IFileDocumentDAL : IRepositoryBase<FileDocument>
    {
    }
    
}
