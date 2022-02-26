<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<section id="product-categories" class="section-products-carousel-tabs product-carousel">
                                <div class="section-products-carousel-tabs-wrap">
                                    <header class="section-header">
                                        <h2 class="section-title"><%=Title %></h2>
                                        <ul class="nav justify-content-end" role="tablist">
                                            <li class="nav-item"> <a data-toggle="tab" href="#" class="nav-link view-all">Xem tất cả </a></li>
                                        </ul>
                                    </header>
                                    <div class="product-categories product-categories-carousel">
                                        <div class="woocommerce columns-5 swiper-container swiper-container-initialized swiper-container-horizontal">
                                            <div class="x-carousel product-list swiper-wrapper" style="transform: translate3d(-1637.33px, 0px, 0px); transition-duration: 0ms;">
                                                <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                                                <div class="swiper-slide product-category product"> 
                                                <a href="<%=this.Data[i].URL != null ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+ this.Data[i].Title.ConvertToUnSign())%>" tabindex="-1">
                                                    <img src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>" width="224" height="197">
                                                        <h2 class="woocommerce-loop-category__title"> <%=this.Data[i].Title%></h2>






                                                    </a></div>
            <%} %>	
</div>
                                            <a href="#" class="arrow left desktop-only"></a>

                                            <a href="#" class="arrow right desktop-only"></a>
                                        
                                    </div>
                                </div>

                            </section>

                

