<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleDetailWithView.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.ArticleDetailWithView" %>
<!-- post content -->
<div class="group blog-big-image row">
    <div class="span9">
        <div class="blog-big-image-content row">
            <div class="post-footer meta span3">
                <p>
                    <span class="author"><%=this.Language["Posted"] %>:</span> <%=String.Format("{0:dd/MM/yyyy}", dto.Time)%>
                </p>

                <p>
                    <span class="author"><%=this.Language["Views"] %>:</span> <%=String.Format("{0,0:d}", dto.Views)%>
                </p>

                <p>
                    <span class="tags">Tag:</span>
                    <%for (int i = 0; i < this.Tags.Count; i++ )
                      {%>
                        <%=this.Tags[i]%>&nbsp; 
                    <%} %>
                </p>






            </div>
            <div class="the-content-post">
                <!-- post title -->
                <h1 class="post-title upper"><%=dto.Title%></h1>

                <div class="content-post">
                    <%=dto.Contents%>
                </div>
            </div>

        </div>
        <div class="clear"></div>
    </div>
</div>