<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.PhotoComment>>" %>


<h3>Comments</h3>
<br />

<% ClubStarterKit.Domain.Photo photo = null; %>

<% foreach(var item in Model){ %>
    <% if (photo == null)
       {
           photo = item.Photo;
       }%>
    <div class="comment">
        <div class="commentGravatar">
            <%if (item.Commenter.ShowGravatar)
              { %>
                <%: GravatarHelper.GravatarImage(item.Commenter.Email)%>
            <%}
              else
              { %>
                <%: Html.SiteImage(Links.Content.Images.nophoto_gif) %>
            <%} %>
        </div>
        <div class="commentText">
            <h4><%: item.Commenter.FullName() %></h4>
            <strong><%: item.CommentDate.ToFriendlyDateString(true) %></strong>
            <br />
            <%: item.CommentText %>
        </div>
        
        <% if(UserHasElevatedPermission){ 
               using (Ajax.AsyncForm(Website.Photos.DeleteComment(), new AsyncFormOptions(AsyncFormType.TargetUpdate, 
                                                                                        formid: "deleteComment" + item.Id, 
                                                                                        targetUpdate: "comments",
                                                                                        elementBlockId: "comments"))){ %>
                <%: Html.HiddenDataValue("comment", item) %>
                <%: Html.HiddenDataValue("comment.Photo", photo) %>
                <%: Html.HiddenDataValue("comment.Commenter", item.Commenter)%>
                
                <input type="submit" value="Delete" />
            <%} 
        } %>
        
        <div class="clearcard dashedline"></div>
    </div>
<%} %>
