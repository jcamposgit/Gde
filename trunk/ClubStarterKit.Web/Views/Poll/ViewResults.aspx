<%@ Page Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.PollQuestion>>" %>
<%@ Import Namespace="ClubStarterKit.Web.ViewData.Poll" %>

<%: Html.PageJavascript(Links.Content.PageJavascript.PollResults_js) %>
<h3>
    <%: Model[0].Question %>
</h3>
<br />
<% var results = new PollResults(Model[0]); %>
<ul>
    <%foreach (var answer in results.Results)
      { %>
        <li>
            <strong><%: answer.Answer.Answer %> (<%: answer.Votes %> votes)</strong>
            <br />
            <div class="poll-result" value="<%: answer.Percentage %>"></div>
        </li>
    <%} %>
</ul>
<br />
<%: Html.Pager(page=> Website.Poll.Show(page), 0, "Poll") %>

<%if(UserHasElevatedPermission){ %>
    <br />
    <%: Html.ActionLink("New Poll", Website.Poll.New()) %>
<%} %>