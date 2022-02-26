<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Source == "PRO") { %>
<section class="dv_spec">
        <div class="min_wrap">
            <h2 class="t_h"><%=Title %></h2>
            <ul class="ul_dv_spec clearfix">
                 <%foreach(var item in this.Data) 
  {%>  
                                <li>
                                    <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                        <figure class="img_dv_spec">
                            <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                        </figure>
                        <h3 class="n_dv_spec"><%=item.Title %></h3>
                    </a>
                </li>
                <%} %>
                            </ul><!-- End .ul_dv_spec -->






        </div><!-- End .min_wrap -->
    </section>
<%} %>

<%if(Source == "ART") { %>
<div class="block_sb">
        <div class="t_sb"><%=Title %></div>
        <div class="m_sb">
            <ul class="news_sb">
                <%foreach(var item in this.Data) 
  {%>  
                				<li>
					<a title="<%=item.Title %>" href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
						<figure>
							<img src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>" class="img_object_fit">
						</figure>
						<h4><%=item.Title %></h4>
					</a>
				</li>
                <%} %>
				            </ul>
        </div><!-- End .m_sb -->
    </div>
<%} %>

<%if(Source == "LIN") { %>
<section class="dt_h">

        <div class="min_wrap">

            <div class="swiper-container swiper3 swiper-container-initialized swiper-container-horizontal">

                <div class="swiper-wrapper" style="transition-duration: 0ms; transform: translate3d(-199px, 0px, 0px);">

                     
                    <%foreach(var item in this.Data) 
                    {%>  
                	    <div class="swiper-slide swiper-slide-prev" style="width: 175px;">
                        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                            <figure>
                                <img class="swiper-lazy img_object_fit swiper-lazy-loaded" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                            </figure>
                        </a>
                    </div>
                <%} %>
					
                </div><!-- End .swiper-wrapper -->

            <span class="swiper-notification" aria-live="assertive" aria-atomic="true"></span>

            </div><!-- End .swiper3 -->

        </div><!-- End .min_wrap -->

    </section>

<link rel="stylesheet" type="text/css" href="/Templates/T07/style/swiper.css" />
    <script src="/Templates/T07/js/swiper.js"></script>
<%} %>