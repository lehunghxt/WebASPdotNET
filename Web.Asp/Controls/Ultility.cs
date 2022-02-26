using System.Data;
using System.Web.UI.WebControls;

namespace Web.Asp.Controls
{
    public static class Ultility
    {
        #region static method
        private static TreeNode AddNode(DataTable table, string columnId, string columnText, string parentId, string columnParentName = "ParentId")
        {

            var node = new TreeNode();
            var root = table.Select(columnId + " = " + parentId, columnText);
            node.Text = root[0][columnText].ToString();
            node.Value = root[0][0].ToString();

            var subcats = table.Select("ParentId = " + parentId);
            if (subcats.Length == 0) return node;

            foreach (var subcat in subcats)
            {
                var nodetemp = new TreeNode();
                nodetemp = AddNode(table, columnId, columnText, subcat[columnId].ToString(), columnParentName);
                    node.ChildNodes.Add(nodetemp);
            }
            return node;
        }
        #endregion 

        #region TreeView
        public static void LoadData(this TreeView treeView)
        {
            var subcats = treeView.Table.Select(treeView.ColumnParent + " = " + treeView.RootId, treeView.ColumnText);
            foreach (var subcat in subcats)
                treeView.Nodes.Add(AddNode(treeView.Table, treeView.ColumnValue, treeView.ColumnText, subcat[treeView.ColumnValue].ToString(), treeView.ColumnParent));
        }
        #endregion
    }
}
