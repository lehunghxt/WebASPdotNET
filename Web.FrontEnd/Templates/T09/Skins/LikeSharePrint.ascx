<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/LikeSharePrint.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.LikeSharePrint" %>

<div class="SocialShare">
    <!-- AddThis Button BEGIN -->
    <div class="addthis_toolbox addthis_default_style addthis_32x32_style fleft" style="margin-right:20px;">    
        <a class="addthis_button_preferred_1"></a>
        <a class="addthis_button_preferred_2"></a>     
        <a class="addthis_button_preferred_3"></a>
        <a class="addthis_button_compact"></a>






    </div>
    <!-- AddThis Button END -->
    <asp:UpdatePanel runat="server" ID="udpComment" UpdateMode="Conditional">
        <ContentTemplate>
        
            <span class="content_like fleft">
              <span id="splike"><%=Like%></span>&nbsp;
                <input type="button" id="btnLike" name="btnLike" onclick="ExLike(true)"/>
                <input type="button" id="btnUnLike" name="btnUnLike" onclick="ExLike(false)"/>
              &nbsp;<span id="spUnLike"><%=DisLike%></span>
            </span> 

        </ContentTemplate>
    </asp:UpdatePanel>
        
    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4f6d234c226f965c"></script>
    <script language="javascript" type="text/javascript">
        function ExLike(islike) {
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/LoadLike",
                data: JSON.stringify({ productId: <%=ProductId %>, isLike: islike }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (islike) {
                        document.getElementById("splike").innerHTML = "<span id='splike'>"+ data.d.Like+"</span> &nbsp;";
                    } else {
                        document.getElementById("spUnLike").innerHTML = "<span id='spUnLike'>" + data.d.Unlike + "</span> &nbsp;";
                    }
                }
            });
        }
    </script>
    <div class="space"></div>
</div>