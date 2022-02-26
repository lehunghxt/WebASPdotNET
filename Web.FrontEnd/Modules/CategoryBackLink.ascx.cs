namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class CategoryBackLink : VITModule
    {
        private ArticleBLL _articleBLL;

        protected string BackLink;
        protected IList<DataSimpleModel> Categories;
        protected string ItemTitle = string.Empty; 

        protected void Page_Load(object sender, EventArgs e)
        {
            this._articleBLL = new ArticleBLL();

            // lay param
            var beginName = this.GetValueParam<string>("BeginName");
            var endName = this.GetValueParam<string>("EndName");
            var split = this.GetValueParam<string>("Split");
            
            var categoryId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId");
            var itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendProduct);
            if (itemId == 0) itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendArticle);
            if (itemId == 0) itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendDocument);
            var item = _articleBLL.GetArticle(this.Config.ID, this.Config.Language, itemId);
            if (item != null) categoryId = item.CATEGORYID;

            // get danh sach category tu bll
            Categories = this._articleBLL.GetAllParentId(this.Config.ID, this.Config.Language, categoryId);

            // tao link
            var link = new StringBuilder();
            link.AppendFormat("<a href='{0}' title='{1}'>{1}</a> ", "/", Language[beginName]);

            if (Categories != null)
            {
                Categories = Categories.Reverse().ToList();
                foreach (var dto in Categories)
                {
                        link.AppendFormat(
                            "{0} <a href='{1}' title='{2}'>{2}</a> ",
                            split,
                            HREF.LinkComponent(GetComponentByType(dto.TargetTag), "sCat/" + dto.ID + "/" + dto.Title.ConvertToUnSign()),
                            dto.Title);
                }
            }

            if (item != null)
            {
                link.AppendFormat("{0} {1}", split, item.TITLE);
                this.ItemTitle = item.TITLE;
            }

            if (!string.IsNullOrEmpty(endName))
                link.AppendFormat("{0} {1}", split, Language[endName]);

            this.BackLink = link.ToString();
        }

        protected string GetComponentByType(string type)
        {
            var component = this.GetValueParam<string>("ComponentList");
            if (!string.IsNullOrEmpty(component)) return component;
            switch(type)
            {
                case "PRO": return "Products";
                case "ART": return "Articles";
                case "DOC": return "Documents";
            }
            return "";
        }
    }
}