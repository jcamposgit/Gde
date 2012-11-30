<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Membership.SendEmailViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <h2>SendEmail</h2>
    <br />
    <% using (Html.BeginForm(Website.Membership.SendEmail(), FormMethod.Post, new { @class = "validateForm" })) {%>

            <p>
                <%= Html.TextBoxFor(x => x.Subject, new { @class="required" })%>
            </p>
            <p>
                <%= Html.TextAreaFor(x=> x.Message, 20, 75, new { @class = "wysiwyg required" }) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>

    <% } %>
</div>
</asp:Content>