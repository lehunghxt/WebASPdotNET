<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSearch.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSearch" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>

<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>


<asp:ListView ID="rpt" runat="server">
    <ItemTemplate>
        <%#Eval("ID")%>
        <%#Eval("Title").ToString().ConvertToUnSign()%>
        <%#Eval("ImageThumbName")%>
        <%#Eval("ImageThumdPath")%>
        <%#Eval("Description")%>
        <%#Eval("Price")%>
        <%#Eval("Sale")%>
        <%#Eval("VoteNumber")%>
        <%#Eval("VoteRate")%>
        <%#Eval("Vote")%>
        <script type="text/javascript" language="javascript">
            generate_stars(10, 'rate<%#Eval("Id") %>');
            current('rate<%#Eval("Id") %>', <%#Eval("Vote") %>, <%=VoteNumber%>);
        </script> 
    </ItemTemplate>
</asp:ListView>
 <VIT:Pager ID="pager" runat="server"/> 

<script type="text/javascript">

            // Send the rating information somewhere using Ajax or something like that.
            function sendRate(sel) {
                var rid = sel.id.split('_');
                var id = rid[0].replace("rate", '');

                $.ajax({
                    type: "POST",
                    url: "<%=HREF.BaseUrl %>/JsonPost.aspx/UpdateVote",
             data: JSON.stringify({ Id: id, Rate: rid[1] }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data != "") {
                     current(rid[0], data.d.VoteRate, <%=VoteNumber/2%>);
                 }
             }

         })
            }
</script>