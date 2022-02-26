<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ItemComment.ascx.cs" Inherits="VIT.Pre.Modules.ItemComment" %>






<script src='<%=HREF.BaseUrl%>Includes/bbeditor/jquery.bbcode.js' type='text/javascript'></script>
<script src='<%=HREF.BaseUrl%>Includes/jquery.nicescroll.v282/jquery.nicescroll.min.js' type='text/javascript'></script>

<div id="divBinhluan" class="share" style='<%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'>
    <div id="comment_header" class="main_panel_header">Các ý kiến về Shop</div>
     <div id="myScrollContainer">
        <asp:UpdatePanel runat="server" ID="udpComment" UpdateMode="Conditional">
            <ContentTemplate>
            <span>
                <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpComment">
                    <ProgressTemplate>
                        Đang gửi...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </span>
            <asp:ListView runat="server" ID="lsvComment">
                <ItemTemplate>
                    <div onmouseover="comment_over('<%#Container.DataItemIndex %>')" onmouseout="comment_out('<%#Container.DataItemIndex %>')" class="<%# Container.DataItemIndex % 2 == 0 ? "comment_even" : "comment_odd" %>">
                        <span class="comment_date"><%# Convert.ToDateTime(Eval("Time")).ToString("dd/MM") %></span>
                        <%#getLike(Convert.ToInt32(Eval("Id")))%> &nbsp; 
                        <span class="comment_title"><%# Eval("Name") %></span> &nbsp;
                        <span id="comment_like<%#Container.DataItemIndex %>" style="display:none">
                            <asp:ImageButton ID="ImageButton1" ImageUrl='<%#HREF.TemplatePath  + "images/like.png"%>' CommandName='Like' CommandArgument='<%#Eval("Id")%>' OnClick="Like_Click" runat="server" /> &nbsp;
                            <asp:ImageButton ID="ImageButton2" ImageUrl='<%#HREF.TemplatePath + "images/dislike.png"%>' CommandName='UnLike' CommandArgument='<%#Eval("Id")%>' OnClick="Like_Click" runat="server" />
                        </span> 
                        <div class="comment_content"><%# Eval("Description") %></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                    <asp:TextBox ID="txtCode" runat="server" CssClass="input_form validate[required,length[6,6]]" placeholder="Nhập mã xác nhận (*)" ></asp:TextBox>
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