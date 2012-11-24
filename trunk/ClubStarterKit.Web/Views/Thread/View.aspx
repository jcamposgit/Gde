<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Forum.MessageListViewData>" %>
<asp:Content ID="Full" ContentPlaceHolderID="FullContent" runat="server">
    <%: Html.PageJavascript(Links.Content.PageJavascript.ForumThreadView_js) %>
    <div class="fullwidth">
        <h2><%: Model.Title + (Model.Locked ? " (LOCKED)" : "") %></h2>
        <%: Html.RssLink(Website.Thread.Rss(Model.ThreadSlug)) %>
        
        <% if (UserHasElevatedPermission) {
               var action = Model.Locked ? Website.Thread.Unock(Model.ThreadSlug) : Website.Thread.Lock(Model.ThreadSlug);
               var text = Model.Locked ? "Unlock" : "Lock";
               %>
                <%: Html.ActionLink(text, action) %><span> </span>
                <%: Html.ActionLink("Delete", Website.Thread.Delete(Model.ThreadSlug)) %>
                <br />
        <%} %>
        
        <div class="dashedline"></div>
        <br />
        
        <div id="messages">
            <%: Html.DisplayFor(x => x.Messages, Website.Shared.Views.DisplayTemplates.ForumMessageList) %>    
        </div>    
    </div>

        <%if (!Model.Locked && User.Identity.IsAuthenticated) { %>
            <div class="fullwidth" id="message-add-block">
              <%using (Ajax.AsyncForm(Website.Message.Update(), new AsyncFormOptions(AsyncFormType.TargetUpdate, FormMethod.Post, "newMessages", targetUpdate: "messages", elementBlockId: "message-add-block", postRequestFunction: "afterMessageUpdate")))
              {%>
                    <h3>Add New Message</h3>
                    <br />
                    <%: Html.Wysiwyg("message") %>
                    <br />
                    <input type="hidden" value="<%: Model.ThreadSlug %>" name="thread" />
                    <input type="hidden" value="-1" name="messageId" />
                    <input type="submit" value="Add Message" />
              <%}%>
              
              <br />
              <a id="newmessage" href="#">New message</a>
            </div>
          <%} %>
</asp:Content>