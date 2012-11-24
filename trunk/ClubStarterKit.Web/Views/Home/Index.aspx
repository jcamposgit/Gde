<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Home.IndexViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="rightblock">
        <%: Html.DisplayFor(x=>x.News, Website.Home.Views.DisplayTemplates.HomeAnnouncement) %>
    </div>
    
    <div class="rightblock">
        <%: Html.DisplayFor(x=>x.Events, Website.Home.Views.DisplayTemplates.HomeEvents) %>
    </div>
    
    <div class="rightblock">
        <%: Html.DisplayFor(x=>x.BlogPosts, Website.Home.Views.DisplayTemplates.HomeBlogPosts) %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <%= Html.DisplayFor(model => model.SideSection) %>
    </div>
    
    <div id="poll-block" class="leftblock">
        <h2>Poll</h2>
        <div class="dashedline"></div>
        <%: Html.PageJavascript(Links.Content.PageJavascript.PagedList_js) %>
        <div id="poll">
            <div id="ajax-page-list" rel="{block: 'poll-block'}">
                <%: Html.Action(Website.Poll.Show()) %>
            </div>
        </div>
    </div>
    
    <% if(UserHasElevatedPermission){ %>
        <div class="leftblock">
            <%: Html.ActionLink("New Page", Website.ContentPage.New())%> <br />
            
        </div>
    <%} %>
</asp:Content>
