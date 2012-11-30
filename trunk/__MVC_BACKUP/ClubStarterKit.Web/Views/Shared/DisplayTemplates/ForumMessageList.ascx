<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<System.Collections.Generic.IEnumerable<ClubStarterKit.Web.ViewData.Forum.MessageViewItem>>" %>

<% foreach (var message in Model) { %>
    <div class="forum-message" id="<%: message.Id %>">
        <div class="sidecard">
            <%: GravatarHelper.GravatarImage(message.Member.Email) %>
            <br />
            <%: Html.ActionLink(message.Member.Username, Website.Membership.Profile(message.Member.Username)) %>
        </div>
        
        <div class="forum-message-block">
            <i>Posted<%: message.DateAdded.ToString(Constants.EventDateFormat) %></i><br /><br />
            <div class="msg">
                <%= message.Body %>
            </div>
            
            <% if(Context.User.Identity.IsAuthenticated){ %>
                <br />
                <a href="#" title="<%: Url.Action(Website.Message.MarkSpam(message.Id)) %>" class="markspam">Mark as Spam</a>
            
                <% if(Context.User.Identity.Name.Equals(message.Member.Username, StringComparison.OrdinalIgnoreCase)){ %>
                    <br />
                    <a href="#" class="editmessage" rel="{message: '<%: message.Id %>'}">Edit</a>
                <%} %>
                <% if(UserHasElevatedPermission){ %>                    
                    <a rev="<%: message.Id %>" class="deletemessage" href="<%: Url.Action(Website.Message.Delete(message.Id)) %>">Delete</a>
                <%} %>
            <%} %>            
        </div>
        
        <div class="clearcard dashedline"></div>
    </div>
<%} %>