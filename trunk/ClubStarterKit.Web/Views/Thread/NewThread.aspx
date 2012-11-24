<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Forum.NewThreadViewData>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <h2>New Forum Post</h2>
    <% using (Html.BeginForm(Website.Thread.Add())){ %>
        <p>
            <strong>Title</strong>
            <br />
            <input type="text" name="title" />
        </p>
        <p>
            <%: Html.Wysiwyg("message") %>
        </p>
        <p>
            <input type="hidden" name="topic" value="<%: Model.TopicSlug %>" />
            <input type="submit" value="Add Thread" />
        </p>
    <%} %>
</div>
</asp:Content>