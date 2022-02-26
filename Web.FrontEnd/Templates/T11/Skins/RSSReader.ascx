<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/RSSReader.ascx.cs" Inherits="Web.FrontEnd.Modules.RSSReader" %>

<div>
    <div class="header_menu_right">
              <h3 style="color: white;font-size: 1.4em;"><%=Title %></h3>
            </div>
    <div style="height:400px; overflow: scroll;">
        <ul class="sub_menu_right">
            <%if (Feed != null && Feed.Items != null && Feed.Items.Count() > 0)
                {%>
        <%foreach (var item in Feed.Items)
            {%>
             <li>
                        <a class="simpletitle" href="<%=item.Links[0].Uri.AbsoluteUri %>" target="_blank" title="<%=item.Title.Text %>">
                        <i class="icons icon-right-dir"></i> <strong><%=item.Title.Text %></strong></a>
                 <div style="text-align:right; font-style: italic;font-size:small"><%=item.PublishDate.ToString("dd/MM/yyyy hh:mm:ss") %></div>
                 <div style="margin-top:10px">
                     <%=item.Summary.Text.Replace("\n","<br />") %>
                 </div>
                 <hr />
            </li>
        
  <%}
      }%>
            </ul>
        </div>
    </div>