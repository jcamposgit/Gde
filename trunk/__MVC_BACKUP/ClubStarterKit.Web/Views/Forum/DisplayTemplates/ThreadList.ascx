<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Web.ViewData.Forum.ThreadListViewData>" %>

<% foreach(var thread in Model.Threads){ %>
    
    <h3><%: Html.ActionLink(thread.Title, Website.Thread.ViewThread(thread.ThreadSlug)) %></h3>
    Messages: <%: thread.MessageCount %>
    <br />
    Last Message: <%: (thread.LastUpdated ?? DateTimeOffset.Now).ToString(Constants.EventDateFormat) %>
    <br /><br />
    
<%} %>

<br />
<%: Html.Pager(Model.Threads, page=>Website.Forum.ViewTopic(Model.Topic.TopicSlug, page))%>