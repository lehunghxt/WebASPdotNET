<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSingle.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSingle" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library" %>

<div class="DataSingle">
    <%if(this.GetValueParam<string>("Source") == "Video" || this.GetValueParam<string>("Source") == "Audio")
      {%>
        <%if(!string.IsNullOrEmpty(this.Data.ImagePath))
          {%>
            <%if(this.GetValueParam<string>("Source") == "Video")
            {%>
                 <video <%=Width > 0 ? "Width='" + Width + "'": ""%> <%=Height > 0 ? "Height='" + Height + "'": ""%> controls>
                    <source src="<%=this.Data.ImagePath%>" type="video/mp4">
                </video>
            <%} else { %>
                <audio controls>
                      <source src="<%=this.Data.ImagePath%>" type="audio/mpeg">
                    </audio>
             <%}%>
        <%} else if(!string.IsNullOrEmpty(this.Data.URL)){ %>
            <%=Height > 0 ? this.Data.URL.Replace("<iframe", "<iframe height='" + Height + "'") : this.Data.URL%>






        <script>
            $("iframe").addClass("embed-responsive-item");
        </script>
    
    <div class="DataDescription">
        <%=this.Data.Description%>
    </div>
        <%}%>
    <%} else { %>
    <a href="<%=this.Data.URL%>" title="<%=Title %>">
        <img src="<%=this.Data.ImagePath%>" alt="<%=this.Data.Title%>" style='width:100%; display:<%=!string.IsNullOrEmpty(this.Data.ImagePath) ? "" : "none" %>'/>
        </a>
    <%} %>
</div>

<%--<a href="<%=HREF.LinkComponent(RederectComponent, RederectView, RederectSendKey + "/" + this.Data.Id + "/" + this.Data.Title.ConvertToUnSign())%>" title="<%=this.Data.Title %>">
    <%=this.Data.Description%>
</a>--%>