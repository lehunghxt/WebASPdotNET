namespace URM.Data
{
	using Infrastructure;

    public partial class GroupDAL : RepositoryBase<URMEntities, Group> , IGroupDAL
    {
        public GroupDAL(IDatabaseFactory<URMEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IGroupDAL : IRepositoryBase<Group>
    {
    }
    
}
