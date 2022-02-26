namespace Web.Backend
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Web.Business;
    using Web.Model;

    public static class DataSource
    {
        private static IEnumerable<SelectListItem> DataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Bool", Text = "Bool" },
                new SelectListItem { Value = "Number", Text = "Number" },
                new SelectListItem { Value = "String", Text = "String" },
                new SelectListItem { Value = "Color", Text = "Color" },
                new SelectListItem { Value = "Date", Text = "Date" },
                new SelectListItem { Value = "Dynamic", Text = "Dynamic" },
                new SelectListItem { Value = "ListCheck", Text = "ListCheck" },
                new SelectListItem { Value = "ListOption", Text = "ListOption" },
                new SelectListItem { Value = "ListSelect", Text = "ListSelect" },
            };
        public static List<SelectListItem> DataTyleCollection = DataTypes.ToList();

        private static IEnumerable<SelectListItem> ModuleTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "True", Text = "Template (cố định)" },
                new SelectListItem { Value = "False", Text = "Component (thay đổi theo trang)" },
            };
        public static List<SelectListItem> ModuleTyleCollection = ModuleTypes.ToList();

        private static IEnumerable<SelectListItem> Sources = new List<SelectListItem>
            {
                new SelectListItem { Value = "PRO", Text = "Sản phẩm" },
                new SelectListItem { Value = "ART", Text = "Bài viết" },
                new SelectListItem { Value = "CAT", Text = "Danh mục" },
                new SelectListItem { Value = "LIN", Text = "Liên kết" },
                new SelectListItem { Value = "MID", Text = "Media" },
                new SelectListItem { Value = "DOC", Text = "Tài liệu" },
                //new SelectListItem { Value = "ATR", Text = "Thuộc tính" }, // có module riêng, ko dùng module datasimple
                new SelectListItem { Value = "COR", Text = "Màu sắc" },
                new SelectListItem { Value = "PTG", Text = "Tag sản phâm" },
                new SelectListItem { Value = "ATG", Text = "Tag bài viết" }
            };
        public static List<SelectListItem> SourceCollection = Sources.ToList();

        private static IEnumerable<SelectListItem> ProductSameTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "categoty", Text = "Danh mục" },
                new SelectListItem { Value = "tag", Text = "Tag" },
            };
        public static List<SelectListItem> ProductSameCollection = ProductSameTypes.ToList();

        public static List<SelectListItem> GetList(int companyId, string language, string key)
        {
            var data = new List<SelectListItem>();
            switch (key)
            {
                case "CategoryId":
                    var CategoryIdBLL = new ArticleBLL();
                    var temp = CategoryIdBLL.GetCategoryByType(companyId, language).ToList();
                    var tempSort = SortTable(temp, 0);
                    data = tempSort.Select( e => new SelectListItem
                        {
                            Value = e.ID.ToString(),
                            Text = e.Blank + e.NAME
                        }).ToList();
                    data.Insert(0, new SelectListItem
                    {
                        Value = "0",
                        Text = "-- Chọn danh mục --"
                    });
                    return data;
                case "Attributes":
                case "SendProperties":
                    var attBLL = new ProductBLL();
                    var attributes = attBLL.GetAttributes(companyId).ToList();
                    data = attributes.Select(e => new SelectListItem
                    {
                        Value = e.ID.ToString(),
                        Text = e.Name
                    }).ToList();
                    return data;
                case "AttributeCategoryId":
                    var attCatBLL = new ProductBLL();
                    var cats = attCatBLL.GetAttributeCategories(companyId).ToList();
                    data = cats.Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Name
                    }).ToList();
                    data.Insert(0, new SelectListItem
                    {
                        Value = "0",
                        Text = "-- Chọn danh mục --"
                    });
                    return data;
                case "CategoryType":
                    var CategoryTypeBLL = new ArticleBLL();
                    data = CategoryTypeBLL.GetCategoryTypes()
                        .Select(e => new SelectListItem
                    {
                        Value = e.ID,
                        Text = e.Name
                    }).ToList();
                    return data;
                case "DataSource":
                case "Source": return SourceCollection;
            }

            return data;
        }

        public static List<SelectListItem> GetListForAttribute(int companyId, int categoryId)
        {
            var productBLL = new ProductBLL();
            var category = productBLL.GetAttributeCategory(categoryId, companyId);
            if (category != null)
            {
                var data = productBLL.GetSimpleAttributes(companyId, categoryId)
                            .Select(e => new SelectListItem
                            {
                                Value = e.ID.ToString(),
                                Text = e.Title
                            }).ToList();
                data.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = category.Type
                });
                return data;
            }
            return new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "" } };
        }

        private static IList<CATEGORYLANGUAGEModel> SortTable(IList<CATEGORYLANGUAGEModel> table, int parentId, string space = "", string distance = "....")
        {
            var rows = table.Where(dto => dto.PARENTID == parentId).ToList();
            if (!rows.Any()) return new List<CATEGORYLANGUAGEModel>();
            var sortData = new List<CATEGORYLANGUAGEModel>();
            foreach (var row in rows)
            {
                var spaceNext = space + distance;
                var dt = SortTable(table, row.ID, spaceNext, distance);
                row.Blank = space;
                //row.Title = space + row.Title;
                sortData.Add(row);

                if (dt.Count > 0) sortData.AddRange(dt);
            }
            return sortData;
        }
    }
}