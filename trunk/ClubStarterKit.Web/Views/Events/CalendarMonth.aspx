<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Infrastructure.UI.Calendar.CalendarMonth>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <%: Html.PageJavascript(Links.Content.PageJavascript.Calendar_js) %>
    <div class="fullwidth">
        <%= Html.ContentSection("events") %>
        
        <%if(UserHasElevatedPermission){ %>
            <br />
            <%: Html.ActionLink("New Event", Website.Events.New()) %>
        <%} %>
        
        <%= Html.RssLink(Website.Events.Rss()) %>
    </div>
    
    <div class="fullwidth" id="calendar">
        <% Html.RenderPartial(Website.Events.Views.Month, Model); %>
    </div>
</asp:Content>
