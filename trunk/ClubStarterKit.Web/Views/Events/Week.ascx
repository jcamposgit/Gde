<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Infrastructure.UI.Calendar.CalendarWeek>" %>
<%@ Import Namespace="ClubStarterKit.Infrastructure.UI.Calendar" %>

<input type="hidden" id="currentLink" value="<%: Model.CurrentLink %>" />

<h2>Week of <%: Model.Sunday.Date.Date.ToShortDateString() %></h2>
<br />
<%: Html.ActionLink("< Prev", Website.Events.Calendar(Model.PrevLink), new{ rev = Model.PrevLink}) %>
<span> </span>
<%: Html.ActionLink("Month", Website.Events.Calendar(Model.MonthLink), new { rev = Model.MonthLink })%>
<span> </span>
<%: Html.ActionLink("Next >", Website.Events.Calendar(Model.NextLink), new { rev = Model.NextLink })%>
<br />
<br />
<% foreach(var day in new ClubStarterKit.Infrastructure.UI.Calendar.CalendarDay[] 
           { Model.Sunday, Model.Monday, Model.Tuesday, Model.Wednesday, Model.Thursday, Model.Friday, Model.Saturday}){ %>
    <h3><%: day.Date.ToString("MMMM dd, yyyy") %></h3>
    <br />

    <% foreach (var evnt in day.Events)
       { %>
        <strong><%: Html.ActionLink(evnt.Title, Website.Events.ViewEvent(evnt.Id)) %></strong>
        <br />
        <%: evnt.StartDate.ToString(Constants.EventDateFormat)%><span> - </span>
        <%: evnt.EndDate.ToString(Constants.EventDateFormat)%>
        <br /><br />
    <%} %>
    <br />
<%} %>