<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/RSSReader.ascx.cs" Inherits="Web.FrontEnd.Modules.RSSReader" %>

<style>
    .tags img{width:100%}
</style>

<div class="tags">
    <h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h3>
    <div style="height:400px; overflow: scroll;<%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>">
            <%if (Feed != null && Feed.Items != null && Feed.Items.Count() > 0)
                {%>
        <%foreach (var item in Feed.Items)
            {%>
             <div>
                        <a href="<%=item.Links[0].Uri.AbsoluteUri %>" target="_blank" title="<%=item.Title.Text %>">
                        <i class="icons icon-right-dir"></i> <strong><%=item.Title.Text %></strong></a>
                 <div style="text-align:right; font-style: italic;font-size:small"><%=item.PublishDate.ToString("dd/MM/yyyy hh:mm:ss") %></div>
                 <div style="margin-top:10px">
                     <%=item.Summary.Text.Replace("\n","<br />") %>
                 </div>
                 <hr />
            </div>
        
  <%}
      }%>
        </div>
    </div>