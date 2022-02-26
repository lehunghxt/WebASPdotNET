namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class CommentController : OdataBaseController<ITEMCOMMENTModel, int>
    {
        private ItemBLL bll;
        public CommentController()
        {
            this.bll = new ItemBLL();
        }
        [EnableQuery]
        public override IQueryable<ITEMCOMMENTModel> Get()
        {
            var param = this.GetParameter();
            var id = 0;
            if (param.ContainsKey("Id")) int.TryParse(param["Id"], out id);
            var data = this.bll.GetComments(id, this.Web.ID).OrderByDescending(e => e.Date);

            return data;
        }

        protected override ITEMCOMMENTModel CreateEntity(ITEMCOMMENTModel model)
        {
            model.ID = bll.CreateComment(model, this.Web.ID);
            return model;
        }
    }
}
