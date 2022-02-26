<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ItemComment.ascx.cs" Inherits="Web.FrontEnd.Modules.ItemComment" %>






<script src='<%=HREF.BaseUrl%>Includes/bbeditor/jquery.bbcode.js' type='text/javascript'></script>
<script src='<%=HREF.BaseUrl%>Includes/jquery.nicescroll.v282/jquery.nicescroll.min.js' type='text/javascript'></script>

<div class="row">
     <div id="myScrollContainer">
         <%foreach (var comment in this.Comments)
             {%>
                    <div>
                        <%= Convert.ToDateTime(comment.Date).ToString("dd/MM") %> &nbsp;
                        <span id="like<%=comment.ID %>"><%=comment.Like%></span> <i class="glyphicon glyphicon-thumbs-up" ></i> &nbsp; 
                        <span id="dislike<%=comment.ID %>"><%=comment.DisLike%></span> <i class="glyphicon glyphicon-thumbs-down" ></i>
                        <span class="comment_title"><%=comment.NAME %></span> &nbsp;
                        <span id="commentlike<%=comment.ID %>" class="comment_like">
                            <span onclick="like(<%=comment.ID %>, true)"><i class="glyphicon glyphicon-thumbs-up" ></i></span> &nbsp; 
                            <span onclick="like(<%=comment.ID %>, false)"><i class="glyphicon glyphicon-thumbs-down" ></i></span>
                        </span> 
                        <div class="comment_content"><%=comment.CONTENT %></div>
                    </div>
         <%} %>
    </div>

    <div class="main_panel_header">Ý kiến của bạn</div>
    <div class="row">
        <div class="col-md-4"><input type="text" id="commentname" name="commentname" class="form-control" placeholder="Tên"/></div>
        <div class="col-md-4"><input type="text" id="commentphone" name="commentphone" class="form-control" placeholder="Điện thoại"/></div>
        <div class="col-md-4"><input type="text" id="commentemail" name="commentemail" class="form-control" placeholder="Email"/></div>
        <div class="col-md-12"><textarea id="commentcontent" name="commentcontent" class="form-control" placeholder="Nội dung"></textarea></div>
    </div>
    <div style="text-align:center">
        <div class="comment_captcha">
            <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                <ContentTemplate>
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
    <input type="button" class="btn btn-primary" value="Gửi" onclick="Comment_post()"/>
    </div>
</div>

<script>
    function Comment_post() {
        var name = $('#commentname').val();
        var phone = $('#commentphone').val();
        var email = $('#commentemail').val();
        var content = $('#commentcontent').val();
        var capt = <%=this.GetValueParam<int>("CaptchaLength") > 0 ? txtCode.Text : "''"%>;
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/Comment",
            data: JSON.stringify({id: <%=vId%>, name: name, phone: phone, email: email, content: content, captcha: capt }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == -1) alert('Sai mã xác nhận !');
                else if (data.d == 0) alert('Server đang bảo trì, hiện tại không thể gửi bình luận.');
                else {
                    var idea = '<div> &nbsp;';
                    idea += '<span id="like' + data.d + '">0</span> <i class="glyphicon glyphicon-thumbs-up" ></i> &nbsp; ';
                    idea += '<span id="dislike' + data.d + '">0</span> <i class="glyphicon glyphicon-thumbs-down" ></i> &nbsp; ';
                    idea += '<span class="comment_title">' + name + '</span> &nbsp;';
                    idea += '<div class="comment_content">' + content + '</div>';
                    idea += '</div>';
                    $("#myScrollContainer").prepend(idea);
                }
             }
        });
    }

    function like(id, like) {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/Like",
            data: JSON.stringify({ id: id, like: like }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d.ID == id) {
                    $("#like" + data.d.ID).html(data.d.LIKES);
                    $("#dislike" + data.d.ID).html(data.d.UNLIKES);
                    $("#commentlike" + data.d.ID).remove();
                }
            }
        });
    }
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#myScrollContainer").niceScroll();
        $("#commentcontent").bbcode({ tag_bold: true, tag_italic: true, tag_underline: true, tag_link: true, tag_image: true, button_image: true });
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