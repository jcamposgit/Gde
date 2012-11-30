<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Blogs.PostViewData>" %>
<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Blogs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        <h2><%: Model.Post.Title%></h2>
        <strong><%: Html.ActionLink(Model.Post.Author.FullName(), Website.Blog.Author(Model.Post.Author.Slug, null))%></strong><br />
        <strong><%: Model.Post.PostDate.ToFriendlyDateString()%></strong>
        
        <div class="dashedline">   </div>
        
        <%= Model.Post.Content %>
        <% if(UserHasElevatedPermission){ %>
            <br />
            <%: Html.ActionLink("Edit", Website.Blog.Edit(Model.Post.Slug)) %>
            <span> </span>
            <%: Html.ActionLink("Delete", Website.Blog.Delete(Model.Post.Slug)) %>
        <%} %>
        
        <br /><br />
        
        
            <%: Html.DisplayFor(x=>x.Comments) %>
            <br />
            <% if (HttpContext.Current.User.Identity.IsAuthenticated)
               { %>
               <div id="commentSection">
                    <h4>Add Comment</h4>
                    <% using (Ajax.AsyncForm(Website.Blog.AddComment(), new AsyncFormOptions(AsyncFormType.TargetUpdate, 
                                                                                             formid: "commentsForm", 
                                                                                             targetUpdate: "comments",
                                                                                             postRequestFunction: "function(){$('#commentText').val(''); return true;}",
                                                                                             elementBlockId: "commentSection")))
                       { %>
                        <textarea name="commentText" id="commentText" rows="5"></textarea>
                        <br />
                        <input type="submit" value="Add Comment"></input>
                        <%: Html.HiddenDataValue("post", Model.Post)%>
                        
                    <% } %>
                </div>
            <%} %>
            
    </div>
</asp:Content>

