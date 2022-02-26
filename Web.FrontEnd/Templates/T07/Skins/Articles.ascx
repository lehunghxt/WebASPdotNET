<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<article class="content">
            <div class="t_cont">
                <h1 class="h_t_cont"><%=Title %></h1>






            </div><!-- End .t_cont -->
            <div class="m_cont">
                <ul class="ul_dm_bv">
                    <%foreach(var item in this.Data) 
  {%>  
                	                    <li>
                        <a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                            <figure>
                               <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title%>">
                            </figure>
                            <div class="m_ul_dm_bv">
                                <h3><%=item.Title%></h3>
                                <ol class="sty_date">
                                    <li>
                                        <svg class="svg-inline--fa fa-calendar-alt fa-w-14" aria-hidden="true" focusable="false" data-prefix="far" data-icon="calendar-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M148 288h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm108-12v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 96v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm192 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96-260v352c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h48V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h128V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h48c26.5 0 48 21.5 48 48zm-48 346V160H48v298c0 3.3 2.7 6 6 6h340c3.3 0 6-2.7 6-6z"></path></svg><!-- <i class="far fa-calendar-alt"></i> -->
                                        Ngày đăng: <%=string.Format("{0:dd/MM/yyyy}", item.CreateDate) %>                                 </li>
                                </ol>
                                <p><%=item.Description %></p>
                                <span>
                                    Chi tiết
                                    <svg class="svg-inline--fa fa-long-arrow-right fa-w-14" aria-hidden="true" focusable="false" data-prefix="fal" data-icon="long-arrow-right" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M311.03 131.515l-7.071 7.07c-4.686 4.686-4.686 12.284 0 16.971L387.887 239H12c-6.627 0-12 5.373-12 12v10c0 6.627 5.373 12 12 12h375.887l-83.928 83.444c-4.686 4.686-4.686 12.284 0 16.971l7.071 7.07c4.686 4.686 12.284 4.686 16.97 0l116.485-116c4.686-4.686 4.686-12.284 0-16.971L328 131.515c-4.686-4.687-12.284-4.687-16.97 0z"></path></svg><!-- <i class="fal fa-long-arrow-right"></i> -->
                                </span>
                            </div>
                        </a>
                    </li>
                    <%} %>
                                    </ul><!-- End .ul_dm_bv --> 
                <div class="page">
                    <div class="PageNum">
                                            </div>
                    <div class="clear"></div>
                </div><!-- End .page -->
            </div><!-- End .m_cont -->
        </article>
