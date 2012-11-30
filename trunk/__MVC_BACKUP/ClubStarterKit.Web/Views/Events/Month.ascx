<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Infrastructure.UI.Calendar.CalendarMonth>" %>

<%: Html.PageJavascript(Links.Content.PageJavascript.CalendarMonth_js) %>
<input type="hidden" id="currentLink" value="<%: Model.CurrentLink %>" />

<!-- Header -->
 <table width="100%">
    <tbody>
        <tr align="center">
            <td>
                <%: Html.ActionLink("< Prev", Website.Events.Calendar(Model.PrevLink), new { rev = Model.PrevLink })  %>
            </td>
            <td>
                <h3><%: Model.MonthName + " " + Model.Year %> </h3>
            </td>
            <td>
                <%: Html.ActionLink("Week", Website.Events.Calendar(Model.WeekLink), new { rev = Model.WeekLink })%>
                <span>    </span>
                <%: Html.ActionLink("Next >", Website.Events.Calendar(Model.NextLink), new { rev = Model.NextLink })%>
            </td>
        </tr>
    </tbody>
</table>
    
<!-- Content -->
<table class="cal-month" width="100%">
    <thead>
		<tr>
			<th>Sunday</th>
            <th>Monday</th>
            <th>Tuesday</th>
            <th>Wednesday</th>
			<th>Thursday</th>
            <th>Friday</th>
            <th>Saturday</th>
		</tr>
	</thead>
    <tbody>
        <%foreach(var week in Model.Weeks){ %>
            <tr>
                <% foreach(var day in new ClubStarterKit.Infrastructure.UI.Calendar.CalendarDay[] 
                   { week.Sunday, week.Monday, week.Tuesday, week.Wednesday, week.Thursday, week.Friday, week.Saturday}){ %>
                    <% if (!day.IsOtherMonth)
                      {
                          var hasEvents = day.Events.Count() > 0;
                          var _class = hasEvents ? "date_has_event" : "";
                          var asterisk = hasEvents ? "*" : "";
                          _class += day.IsToday ? "today" : ""; %>
                          
                          <td class="<%= _class %>">
                            <%= day.Date.Day + asterisk%>
                                <div class="events">
                    				<ul>
                                        <% foreach (var @event in day.Events)
                                           { %>
                    						<li>
                    							<span class="title"><%: Html.ActionLink(@event.Title, Website.Events.ViewEvent(@event.Id)) %></span>
                                                <span class="desc">Start: <%: @event.StartDate.ToString(Constants.EventDateFormat) %> <br /> 
                                                                   End: <%: @event.EndDate.ToString(Constants.EventDateFormat)%></span>
                    						</li>
                                        <%} %>
                    				</ul>
                    			</div>
                          </td>
                    <% } else { %>
                        <td class="padding"></td>        
                    <%}%>
                <%} %>
            </tr>
        <%} %>
    </tbody>
</table>