<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.BlogPost>" %>
<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Membership.Identification" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        <% if (Model.Id == 0){ %>
            <h2>New Blog Post</h2>
        <%} else { %>
            <h2>Edit <%: Model.Title %> Blog Post</h2>
        <%} %>
        
        
        <div class="dashedlist"></div>
        
        <% using (Html.BeginForm(Website.Blog.Update(), FormMethod.Post, new { @class = "validateForm" }))
           { %>
           
            <p>
                <strong>Title</strong>
                <br />
                <%: Html.TextBox("Title", Model.Title, new { @class = "required" })%>
            </p>
            
            <p>
                <strong>Content</strong>
                <br />
                <%: Html.TextArea("Content", Model.Content, 20, 75, new { @class = "wysiwyg required" })%>
            </p>
            <%: Html.Hidden("PostDate", DateTimeOffset.Now)%>
            <input name="Id" type="hidden" value="<%: Model.Id %>" />
            <%: Html.HiddenDataValue("Author", Model.Author ?? UserRetrieval.GetUser(Html.ViewContext.HttpContext))%>
            <p>
                <input type="submit" value="Save" />
            </p>
        
        <%} %>
    </div>
</asp:Content>
