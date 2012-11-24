<%@ Page Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.PollQuestion>>" %>
<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Poll" %>

<% using (Ajax.AsyncForm(Website.Poll.Vote(), new AsyncFormOptions(AsyncFormType.TargetUpdate, FormMethod.Post, "pollVote", 
                                                                   targetUpdate: "poll", elementBlockId: "poll-block", 
                                                                   postRequestFunction: "replacePagerHrefs")))
   { %>
    <h3><%: Model[0].Question %></h3>
    <br />
    <ul>
        <% foreach(var answer in new PollAnswersRetrieval(Model[0].Id).Execute()){ %>
            <li>
                <%:Html.RadioButton("answer", answer.Id)  %>
                <strong><%: answer.Answer %></strong>
            </li>
        <%} %>
    </ul>
    <input type="hidden" name="poll" value="<%: Model[0].Id %>" />
    <input type="submit" value="Vote" />
<%} %>

<script type="text/javascript">
    setupForms();
</script>

<%: Html.Pager(page=> Website.Poll.Show(page), 0, "Poll") %>

<%if(UserHasElevatedPermission){ %>
    <br />
    <%: Html.ActionLink("New Poll", Website.Poll.New()) %>
<%} %>