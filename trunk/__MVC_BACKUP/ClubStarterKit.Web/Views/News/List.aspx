<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.Announcement>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rightblock" id="list-block">
        <%: Html.PageJavascript(Links.Content.PageJavascript.PagedList_js)%>
        <h2>
            News Announcements
            <%= Html.RssLink(Website.News.Rss()) %>
        </h2>
        <div id="ajax-page-list" rel="{block: 'list-block'}">
            <%: Html.DisplayForModel(Website.News.Views.DisplayTemplates.PagedNews) %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <%= Html.ContentSection("news_list") %>
        
        <% if(UserHasElevatedPermission){ %>
            <br />
            <%: Html.ActionLink("New Announcement", Website.News.New()) %>
        <%} %>
    </div>
</asp:Content>