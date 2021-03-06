<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleBilinguals.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleBilinguals" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>
 
<%if(!string.IsNullOrEmpty(this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag"))) {%>
<h1 title="<%=this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag") %>">Tag:<strong> <%=this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag") %></strong></h1>
<%} %>
<div class="articles" style="margin-top: 30px;">
  <%if(this.Data.Count > 0) { %>
<div id="article<%=this.Data[0].ID %>">
<h2 class="bd-title" title="<%=this.Data[0].Title1 %>"><a href="<%=this.Data[0].Link%>" title="<%=this.Data[0].Title1 %>"><%=this.Data[0].Title1 %></a></h2>
    <%if (!string.IsNullOrEmpty(Data[0].ImagePath))
        { %>
    <a href="<%=this.Data[0].Link%>" title="<%=this.Data[0].Title1 %>">
        <img alt="<%=this.Data[0].Title1 %>" src="<%=Data[0].ImagePath %>" style="width:250px; float:left; margin: 0px 20px 20px 0px"/>
    </a>
    <%} %>
	<div class="articlecontent1"><%=this.Data[0].Content1 %></div>
<button type="button" class="btn btn-secondary" onclick="$('#Translate<%=this.Data[0].ID %>').toggle(500);"><%=TextTranslate %></button>
  <div class="jumbotron mt-3" id="Translate<%=this.Data[0].ID %>" style="display:none">
      <%if (!string.IsNullOrEmpty(this.Data[0].Title2)){ %>
    <h3 title="<%=this.Data[0].Title2 %>"><%=this.Data[0].Title2 %></h3>
    <div class="articlecontent2"><%=this.Data[0].Content2 %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[0].ID %>', 1)"><%=TextFixTranslationYet %></button>
	    </div>
      <%} else { %>
      <div><%=TextNoTranslationYet %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[0].ID %>', 0)"><%=TextSendTranslationYet %></button>
	    </div>
	
      <%} %>
  </div>
    </div>

	<!--<iframe style='border:none; max-width: 100%;margin-top:10px' src='https://smartlink.adpia.vn/adpia-ads.php?width=728&height=90&cate=A&a_id=A100047119&u_id=' height='90' width='100%'></iframe><script>window.addEventListener('load', function (event) { if(document.getElementsByTagName('iframe')[0].offsetWidth < document.getElementsByTagName('iframe')[0].width) { document.getElementsByTagName('iframe')[0].style.height = (document.getElementsByTagName('iframe')[0].height * document.getElementsByTagName('iframe')[0].offsetWidth / document.getElementsByTagName('iframe')[0].width) + 'px' } })</script>-->
	<br/>
	<script type="text/javascript" src="https://uprimp.com/bnr.php?section=General&pub=972236&format=728x90&ga=g&bg=1"></script>
<noscript><a href="https://yllix.com/publishers/972236" target="_blank"><img src="//ylx-aff.advertica-cdn.com/pub/728x90.png" style="border:none;margin:0;padding:0;vertical-align:baseline;" alt="ylliX - Online Advertising Network" /></a></noscript>

<%for(int i = 1; i< this.Data.Count; i++) 
  {%> 
  <hr />
  <div id="article<%=this.Data[i].ID %>">
  <h2 class="bd-title" title="<%=this.Data[i].Title1 %>"><a href="<%=this.Data[i].Link%>" title="<%=this.Data[i].Title1 %>"><%=this.Data[i].Title1 %></a></h2>
	<%if (!string.IsNullOrEmpty(Data[i].ImagePath))
        { %>
    <a href="<%=this.Data[i].Link%>" title="<%=this.Data[i].Title1 %>">
        <img alt="<%=this.Data[i].Title1 %>" src="<%=Data[i].ImagePath %>" style="width:250px; float:left; margin: 0px 20px 20px 0px"/>
    </a>
    <%} %>
      <div class="articlecontent1"><%=this.Data[i].Content1 %></div>
<button type="button" class="btn btn-secondary" onclick="$('#Translate<%=this.Data[i].ID %>').toggle(500);"><%=TextTranslate %></button>
  <div class="jumbotron mt-3" id="Translate<%=this.Data[i].ID %>" style="display:none">
      <%if (!string.IsNullOrEmpty(this.Data[i].Title2)){ %>
    <h3 title="<%=this.Data[i].Title2 %>"><%=this.Data[i].Title2 %></h3>
    <div class="articlecontent2">
        <%=this.Data[i].Content2 %>
	</div>
      <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[i].ID %>', 1)"><%=TextFixTranslationYet %></button>
	    </div>
      <%} else { %>
      <div><%=TextNoTranslationYet %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data[i].ID %>', 0)"><%=TextSendTranslationYet %></button>
	    </div>
	
      <%} %>
  </div>
      </div>
  <%} %>

<div id="LoadErea"></div>

<input type="hidden" value="<%= Total%>" id="articlesTotal"/>
<input type="hidden" value="<%= this.GetValueParam<int>("Top")%>" id="articlesCurrent"/>

<div style="margin: 40px;display:block; text-align:center" id="btnLoad">
  <button type="button" class="btn btn-primary" onclick="LoadMore();"><%=Language["LoadMore"] %></button>
</div>
 <%} %>

<!-- Modal -->
<div class="modal fade" id="sendTranslate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel"><%=TextTranslate %></h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
      <input type="hidden" id="articleid"/>
      <h3></h3>
      <div class="articlecontent"></div>
       <div class="form-group row">
    <label for="sendName" class="col-sm-2 col-form-label"><%=Language["YourName"] %></label>
    <div class="col-sm-10">
      <input class="form-control" id="sendName" placeholder="<%=Language["YourName"] %>" required="required">
    </div>
  </div>
      <textarea id="w3review" name="w3review" rows="10" style="width:100%"> </textarea>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Language["Close"] %></button>
        <button type="button" class="btn btn-primary" onclick="SendTranlation()"><%=Language["Send"] %></button>
      </div>
    </div>
  </div>
</div>
</div>
<script src='<%=HREF.BaseUrl%>Includes/bbeditor/jquery.bbcode.js' type='text/javascript'></script>
<script>
    checkload();
    function checkload()
    {
        $("#sendTranslate #w3review").bbcode({ tag_bold: true, tag_italic: true, tag_underline: true, tag_link: true, tag_image: true, button_image: true });
        $(document).ready(function () {
            var total = $("#articlesTotal").val();
            var current = $("#articlesCurrent").val();
            if (parseInt(current, 10) >= parseInt(total, 10)) $("#btnLoad").hide();
        });
    }
</script>
<script>
    function SetData(id, type)
    {
        var title1 = $("#article" + id + " h2 a").html();
        var content1 = $("#article" + id + " .articlecontent1").html();
        $("#sendTranslate h3").html(title1);
        $("#sendTranslate .articlecontent").html(content1);
        $("#sendTranslate #articleid").val(id);
        //if(type == 1)
        //{
            var title2 = $("#article" + id + " h3").html();
            var content2 = $("#article" + id + " .articlecontent2").html();
            //$("#sendTranslate #w3review").html(title2 + "<br/>" + content2);
        //}
    }

    function LoadMore()
    {
        var total = $("#articlesTotal").val();
        var currentbefore = $("#articlesCurrent").val();
		var key = '<%=this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag")%>';
		var orderby = '<%=this.GetRequestThenParam<string>("sort", "OrderBy")%>';
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/LoadMoreArticleBilinguals",
            data: JSON.stringify({key: '<%= Session.SessionID%>', cat: <%= CategoryId%>, skip: currentbefore, take: <%= this.GetValueParam<int>("Top")%>, order: orderby, tag: key}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    var currentafter = parseInt(currentbefore, 10) + <%= this.GetValueParam<int>("Top")%>;
                    $("#articlesCurrent").val(currentafter); 
                    if(currentafter >=  parseInt(total, 10)) $("#btnLoad").hide();

                    var content = "";
                    for (var i = 0; i < data.d.length; i++) {
                        content +="<hr />";
                        content +="<div id='"+data.d[i].ID+"'>";
                        content +="<h2 class='bd-title' title='" + data.d[i].Title1 + "'><a href='" + data.d[i].Link + "' title='" + data.d[i].Title1 +"'>"+data.d[i].Title1+"</a></h2>";
                        if (data.d[i].ImagePath)
                        {
                            content +="<a href='" + data.d[i].Link + "' title='" + data.d[i].Title1 +"'>";
                            content +="<img alt='" + data.d[i].Title1 +"' src='"+data.d[i].ImagePath+"' style='width:250px; float:left; margin: 0px 20px 20px 0px'/>";
                            content +="</a>";
                        }
                        content +="<div class='articlecontent1'>" + data.d[i].Content1 + "</div>";
                        content +="<button type='button' class='btn btn-secondary' onclick='$(\"#Translate" + data.d[i].ID + "\").toggle();'><%=TextTranslate %></button>";
                        content +="<div class='jumbotron mt-3' id='Translate"+data.d[i].ID+"' style='display:none'>";
                        if(data.d[i].Title2)
                        {
							content +="<h3 title='" + data.d[i].Title2 + "'>" + data.d[i].Title2 + "</h3>";
                            content +="<div class='articlecontent2'>";
                            content +="<div class='articlecontent2'>" + data.d[i].Content2 + "</div>";
                            content +="<div style='text-align:right; margin-top:20px'>";
                            content +="<button type='button' class='btn btn-success' data-toggle='modal' data-target='#sendTranslate' onclick='SetData('"+data.d[i].ID+"', 1)'><%=TextFixTranslationYet %></button>";
                            content +="</div>";
                        }
                        else
                        {
                             content +="<div><%=TextNoTranslationYet %></div>";
                            content +="<div style='text-align:right; margin-top:20px'>";
                            content +="<button type='button' class='btn btn-success' data-toggle='modal' data-target='#sendTranslate' onclick='SetData('"+data.d[i].ID+"', 0)'><%=TextSendTranslationYet %></button>";
                            content +="</div>";
                        }
                        content +="</div></div>";
                    }
                    $("#LoadErea").append(content);
                    $('html,body').animate({
                        scrollTop: $("#" + data.d[0].ID).offset().top
                    }, 'fast');
                }
            }
        });
    }

    function SendTranlation()
    {
        var aid = $("#sendTranslate #articleid").val();
        var sendname = $("#sendTranslate #sendName").val();
        var sendcontent = $("#sendTranslate #w3review").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/Comment",
            data: JSON.stringify({ id: aid, name: sendname, phone: '', email:'', content: sendcontent, captcha:'' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == -1) alert('Sai mã xác nhận !');
                else if (data.d == 0) alert('<%=Language["ErrorSend"] %>');
                else alert('<%=Language["ThankSent"] %>');
            }
        });
    }
</script>