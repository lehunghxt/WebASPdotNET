namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ProductAttribute : Web.Asp.UI.VITModule
    {
        private ArticleBLL _articleBll;
        private ProductBLL productBLL;
        
        private int _categoryId;

        protected List<AttributeValueModel> Data;
        protected AttributeCategoryModel Category { get; set; }

        protected string ComponentProducts { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            productBLL = new ProductBLL();

            this._categoryId = this.GetValueParam<int>("AttributeCategoryId");
            ComponentProducts = this.GetValueParam<string>("ComponentProducts");

            this.Category = this.productBLL.GetAttributeCategory(this._categoryId, Config.ID);
            if (Category != null)
            {
                Data = productBLL.GetAttributeValues(Config.ID, this._categoryId).ToList();

                if (this.GetValueParam<bool>("OverWriteTitle") && !string.IsNullOrEmpty(Category.Name))
                {
                    this.Title = Category.Name;
                }
            }
            else Data = new List<AttributeValueModel>();
        }
    }
}