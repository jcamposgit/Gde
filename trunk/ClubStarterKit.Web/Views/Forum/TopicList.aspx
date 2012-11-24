<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<System.Collections.Generic.IEnumerable<ClubStarterKit.Web.ViewData.Forum.TopicGroupItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="rightblock">
    <h2>Forum</h2>
    <div class="dashedline"></div>
    <%foreach (var group in Model) { %>
        <h3><%: group.Group %></h3>
        <% foreach(var topic in group.Topics){ %>
            
            <div class="forum-topic">
                <strong><%: Html.ActionLink(topic.Title, Website.Forum.ViewTopic(topic.TopicSlug, null)) %></strong>
                <br />
                Last updated: <%: (topic.LastUpdate ?? DateTimeOffset.Now).ToString(Constants.EventDateFormat) %>
                <br />
                Threads: <%: topic.TotalThreads %>
                <br />
                <%= topic.Description %>
                <br />
                <% if (UserHasElevatedPermission){ %>
                    <%: Html.ActionLink("Edit", Website.Forum.Edit(topic.TopicSlug)) %>
                    <%: Html.ConfirmationLink("Delete", Website.Forum.Delete(topic.TopicSlug), "Are you sure?", "Are you sure you want to delete the " + topic.Title + " topic?") %>
                    <br />
                <%} %>
                <br />
            </div>
            
        <%} %>
    <%} %>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
<div class="leftblock">
    <%: Html.ContentSection("forum_index") %>
    <% if (UserHasElevatedPermission){ %>
        <br />
        <%: Html.ActionLink("New Topic", Website.Forum.New()) %>
    <%} %>
</div>
</asp:Content>