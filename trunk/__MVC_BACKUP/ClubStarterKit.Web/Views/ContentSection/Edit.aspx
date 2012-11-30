<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.ContentSection>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        <h2>Edit <%: Model.Name %> Content Section</h2>
        <br />
        <% using (Html.BeginForm(Website.ContentSection.Update(), FormMethod.Post, new { @class = "validateForm" }))
           { %>
            <p>
                <%: Html.TextArea("Content", Model.Content, 20, 75, new { @class = "wysiwyg required" })%>
            </p>
            <p>
                <%: Html.Hidden("Name", Model.Name) %>
                <input value="<%: Model.Id %>" type="hidden" name="Id" />
                <input value="Save" type="submit" />
            </p>
        <%} %>
    </div>
</asp:Content>
