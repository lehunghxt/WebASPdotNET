namespace Web.Api
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Web.Business;

    public static class DataSource
    {
        private static IEnumerable<SelectListItem> DataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Bool", Text = "Bool" },
                new SelectListItem { Value = "Number", Text = "Number" },
                new SelectListItem { Value = "String", Text = "String" },
                new SelectListItem { Value = "Color", Text = "Color" },
                new SelectListItem { Value = "Date", Text = "Date" },
                new SelectListItem { Value = "ListCheck", Text = "ListCheck" },
                new SelectListItem { Value = "ListOption", Text = "ListOption" },
                new SelectListItem { Value = "ListSelect", Text = "ListSelect" },
            };
        public static List<SelectListItem> DataTyleCollection = DataTypes.ToList();

        private static IEnumerable<SelectListItem> ModuleTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "True", Text = "Template (cố định)" },
                new SelectListItem { Value = "False", Text = "Component (they đổi theo trang)" },
            };
        public static List<SelectListItem> ModuleTyleCollection = ModuleTypes.ToList();

        private static IEnumerable<SelectListItem> DataSources = new List<SelectListItem>
            {
                new SelectListItem { Value = "PRO", Text = "Sản phẩm" },
                new SelectListItem { Value = "ART", Text = "Bài viết" },
                new SelectListItem { Value = "LIN", Text = "Liên kết - Logo" },
                new SelectListItem { Value = "CAT", Text = "Danh mục" },
            };
        public static List<SelectListItem> DataSourceCollection = DataSources.ToList();

        public static List<SelectListItem> GetList(int companyId, string language, string key)
        {
            var data = new List<SelectListItem>();
            switch (key)
            {
                case "CategoryId":
                    var CategoryIdBLL = new ArticleBLL();
                    data = CategoryIdBLL.GetCategoryByType(companyId, language)
                        .Select( e => new SelectListItem
                        {
                            Value = e.ID.ToString(),
                            Text = e.NAME
                        }).ToList();
                    return data;
                case "CategoryArticleId":
                    var CategoryArticleIdBLL = new ArticleBLL();
                    data = CategoryArticleIdBLL.GetCategoryByType(companyId, language, "ART")
                        .Select(e => new SelectListItem
                        {
                            Value = e.ID.ToString(),
                            Text = e.NAME
                        }).ToList();
                    return data;
                case "CategoryType":
                    var CategoryTypeBLL = new ArticleBLL();
                    data = CategoryTypeBLL.GetCategoryTypes()
                        .Select(e => new SelectListItem
                        {
                            Value = e.ID,
                            Text = e.Name
                        }).ToList();
                    data.Insert(0, new SelectListItem
                    {
                        Value = "",
                        Text = ""
                    });
                    return data;
            }

            return data;
        }
    }
}