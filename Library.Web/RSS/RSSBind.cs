using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Net;

namespace Library.Web.RSS
{
    public class RSSBind
    {
        public RSSBind()
        {
        }

        private DataTable CreateDataTable()
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "title";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "link";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "description";
            myDataTable.Columns.Add(myDataColumn);
            return myDataTable;
        }

        private void AddDataToTable(string Title, string link, string Descriptions, DataTable myTable)
        {
            DataRow row;
            row = myTable.NewRow();
            row["title"] = Title;
            row["link"] = link;
            row["description"] = Descriptions;
            myTable.Rows.Add(row);
        }

        public DataTable BindRSSItem(string rssURL)
        {
            DataTable myDataTable = this.CreateDataTable();
            try
            {
                WebRequest myRequest = WebRequest.Create(rssURL);
                WebResponse myResponse = myRequest.GetResponse();

                Stream rssStream = myResponse.GetResponseStream();
                XmlDocument rssDoc = new XmlDocument();
                rssDoc.Load(rssStream);
                XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
                string title = "";
                string link = "";
                string description = "";
                for (int i = 0; i < rssItems.Count; i++)
                {
                    XmlNode rssDetail;
                    rssDetail = rssItems.Item(i).SelectSingleNode("title");
                    if (rssDetail != null)
                    {
                        title = rssDetail.InnerText;
                    }
                    else
                    {
                        title = "";
                    }
                    rssDetail = rssItems.Item(i).SelectSingleNode("link");
                    if (rssDetail != null)
                    {
                        link = rssDetail.InnerText;
                    }
                    else
                    {
                        link = "";
                    }

                    rssDetail = rssItems.Item(i).SelectSingleNode("description");
                    if (rssDetail != null)
                    {
                        description = rssDetail.InnerText;
                    }
                    else
                    {
                        description = "";
                    }
                    AddDataToTable(title, link, description, myDataTable);
                }
            }
            catch { }
            return myDataTable;
        }
    }
}
