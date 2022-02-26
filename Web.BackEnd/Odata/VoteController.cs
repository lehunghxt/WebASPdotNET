namespace Web.Backend.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    public class VoteController : OdataBaseController<ItemVoteModel, int>
    {
        private ItemBLL bll;
        public VoteController()
        {
            this.bll = new ItemBLL();
        }
        protected override ItemVoteModel UpdateEntity(int key, ItemVoteModel model)
        {
            model = bll.UpdateVote(model);
            return model;
        }
    }
}
