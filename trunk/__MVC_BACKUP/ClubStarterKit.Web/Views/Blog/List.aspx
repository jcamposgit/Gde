<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.BlogPost>>" %>
<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Blogs" %>
<%@ Import Namespace="ClubStarterKit.Web.ViewData.Blogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% var viewData = new ListViewData(ViewData); %>
<%: Html.PageJavascript(Links.Content.PageJavascript.PagedList_js) %>
    <div class="rightblock" id="list-block">
        <h2>
            <%: viewData.Header%>
            <%: Html.RssLink(viewData.RssAction)%>
        </h2>
                
         <div id="ajax-page-list" rel="{block: 'list-block'}">
            <%: Html.DisplayForModel(Website.Blog.Views.DisplayTemplates.PagedBlogPosts) %>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">

    <div class="leftblock">
        <%= Html.ContentSection("bloglist_left") %>

        <% if(UserHasElevatedPermission){ %>
            <br />
            <%: Html.ActionLink("New Blog Post", Website.Blog.New()) %>
        <%} %>
    </div>
</asp:Content>