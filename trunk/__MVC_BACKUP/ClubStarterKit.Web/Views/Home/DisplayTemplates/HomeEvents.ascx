<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.Event>>" %>

<h2>
    Upcoming Events
    <%= Html.RssLink(Website.Events.Rss()) %>
</h2>
<div class="dashedline">  </div>

<% foreach (var evnt in Model)
   { %>
   <strong><%: Html.ActionLink(evnt.Title, Website.Events.ViewEvent(evnt.Slug)) %></strong>
   <br />
   <%: evnt.StartTime.ToString(Constants.EventDateFormat) %> to <%: evnt.EndTime.ToString(Constants.EventDateFormat) %>
   
   <p>
        <%: evnt.Description.Truncate(Constants.TruncateLength) %>
   </p>
   
   <div class="clearlist">
            </div>
<% } %>

<div class="dashedline">  </div>
<%: Html.ActionLink("View All Events »", Website.Events.Index())%>