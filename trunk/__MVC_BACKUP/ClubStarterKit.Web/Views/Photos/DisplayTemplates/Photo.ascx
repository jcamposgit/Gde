<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.Photo>>" %>

<% if(Model.Count > 0){ %>
    <h3><%: Model[0].Title %></h3>
    <br />
    
    <img src="<%: Url.Action(Website.Photos.Photo(Model[0].PhotoLocation, Url.ApplicationId())) %>" alt="<%: Model[0].Title %>" />
    
    <% if(UserHasElevatedPermission){ %>
        <%: Html.PageJavascript(Links.Content.PageJavascript.PhotoAdmin_js) %>
        <br />
        <a href="#photo-1" class="deletePhoto" loc="<%: Url.Action(Website.Photos.Delete(Model[0].Id, Model[0].Album.Id)) %>">Delete Photo</a>
    <%} %>
    
    <br />
    
    <div id="comments">
        <%: Html.Action(Website.Photos.Comments(Model[0].Id)) %>
    </div>
    
    
    <% if(Context.User.Identity.IsAuthenticated){ %>
        <br />
        <h3>Add Comment</h3>
        <br />
        <% using(Ajax.AsyncForm(Website.Photos.AddComment(), new AsyncFormOptions(AsyncFormType.TargetUpdate, 
                                                                                            formid: "addComment",
                                                                                            targetUpdate: "comments",
                                                                                            elementBlockId: "comments"))){ %>
            <textarea name="comment" id="commentText" rows="5"></textarea>
            <input type="hidden" value="<%: Model[0].Id %>" name="photo" />
            <br />
            <input type="submit" value="Add Comment"></input>
            <%: Html.HiddenDataValue("post", Model[0])%>
        <%} %>                                                                                      
    <%} %>
    
    <br />
    
    <%: Html.Pager(page=>Website.Photos.ViewAlbum(Model[0].Album.Slug, page), prefix: "Photo") %>
<%} else { %>
    There are current no photos in this album.
<%} %>