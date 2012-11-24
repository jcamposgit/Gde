<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" ValidateRequest="false" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Announcement>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        <% if (ViewData.Title().Contains("Edit"))
           { %>
            <h2>Edit <%: Model.Title %> Announcement</h2>
        <%} else { %>
            <h2>New Announcement</h2>
        <%} %>
        <div class="dashedline">   </div>
        <%: Html.EditorForModel() %>
    </div>
</asp:Content>