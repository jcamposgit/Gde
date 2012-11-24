<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Forum.ThreadListViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%: Html.PageJavascript(Links.Content.PageJavascript.PagedList_js) %>
<div class="rightblock" id="ajax-page-list" rel="{block: 'ajax-page-list'}"> 
    <%: Html.DisplayForModel(Website.Forum.Views.DisplayTemplates.ThreadList)%>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
<div class="leftblock">
    <h3><%: Model.Topic.Title %></h3>
    <%: Html.RssLink(Website.Forum.Rss(Model.Topic.TopicSlug)) %>
    <br />
    <%= Model.Topic.Description %>
    
    <% if(User.Identity.IsAuthenticated){ %>
        <br />
        <%: Html.ActionLink("New Thread", Website.Thread.New(Model.Topic.TopicSlug)) %>
    <%} %>
</div>
</asp:Content>