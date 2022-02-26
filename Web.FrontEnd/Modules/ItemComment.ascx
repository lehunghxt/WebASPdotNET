<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemComment.ascx.cs" Inherits="Web.FrontEnd.Modules.ItemComment" %>
<script src='<%=HREF.BaseUrl%>Includes/bbeditor/jquery.bbcode.js' type='text/javascript'></script>

<div class="row">
     <div id="myScrollContainer">
         <%for (int i = 0; i < this.Comments.Count; i++)
             {%>
                    <div onmouseover="comment_over('<%=i %>')" onmouseout="comment_out('<%=i %>')" class="<%= i % 2 == 0 ? "comment_even" : "comment_odd" %>">
                        <span class="comment_date"><%# Convert.ToDateTime(Eval("Time")).ToString("dd/MM") %></span>
                        <%=this.Comments[i].Like%> <i class="glyphicon glyphicon-thumbs-up" /> &nbsp; <%=this.Comments[i].DisLike%> <i class="glyphicon glyphicon-thumbs-down" />
                        <span class="comment_title"><%# Eval("Name") %></span> &nbsp;
                        <span id="comment_like<%=i %>" style="display:none">
                            <span alt="like" onclick="like(<%=this.Comments[i].ID %>)"><i class="glyphicon glyphicon-thumbs-up" /></span>
                            <span alt="like" onclick="dislike(<%=this.Comments[i].ID %>)"><i class="glyphicon glyphicon-thumbs-down" /></span>
                        </span> 
                        <div class="comment_content"><%= this.Comments[i].CONTENT %></div>
                    </div>
         <%} %>
    </div>

    <div class="main_panel_header">Ý kiến của bạn</div>
    <div class="odd">
        <asp:TextBox runat="server" ID="txtNameComment" placeholder="Tên"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtPhone" placeholder="Điện thoại"></asp:TextBox>
        
        <div class="comment_captcha">
            <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="email"></asp:TextBox>
                    <img id="imgCaptcha" runat="server" alt="Confirm Code"/>
                    <asp:UpdateProgress runat="server" ID="UdtProgressCap" AssociatedUpdatePanelID="udpchange">
                        <ProgressTemplate>
                            <%--Đang gửi...--%>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:TextBox ID="txtCode" ClientIDMode="Static" runat="server" CssClass="input_form validate[required,length[6,6]]" placeholder="Nhập mã xác nhận (*)" ></asp:TextBox>
                    <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" OnClick="btnDoiMa_Click" CssClass="ChangeCode" ViewStateMode="Disabled" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:TextBox ID="txtContents" TextMode="MultiLine" ClientIDMode="Static" runat="server" CssClass="txtContents"></asp:TextBox>
    <div style="text-align:center">
    <asp:Button runat="server" CssClass="sendcommend" ID="btnSendComment" OnClick="btnSendComment_Click" Text="Gửi" OnClientClick="$('#frmMain').validationEngine();" />
        </div>
</div>



<script language="javascript" type="text/javascript">
    function comment_over(id) {
        $('#comment_like' + id).css("display", "");
    }
    function comment_out(id) {
        $('#comment_like' + id).css("display", "none");
    }
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#myScrollContainer").niceScroll();
        $("#txtContents").bbcode({ tag_bold: true, tag_italic: true, tag_underline: true, tag_link: true, tag_image: true, button_image: true });
        //process();
    });
    //                var bbcode = "";
    //                function process() {
    //                    if (bbcode != $("#test").val()) {
    //                        bbcode = $("#test").val();
    //                        $.get('../../Includes/bbeditor/bbParser.php',
    //			{
    //			    bbcode: bbcode
    //			},
    //			function (txt) {
    //			    $("#preview").html(txt);
    //			})

    //                    }
    //                    setTimeout("process()", 2000);

    //                }
</script>