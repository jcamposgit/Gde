<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.ContentPage>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        
        <% if (Model.Id > 0)
           { %>
            <h2>Edit <%: Model.PageTitle%> Page</h2>
        <%} else { %>
            <h2>New Page</h2>
        <%} %>
        
        <div class="dashedlist"></div>
        
        <% using (Html.BeginForm(Website.ContentPage.Update(), FormMethod.Post, new { @class = "validateForm" }))
           { %>
            <p>
                <strong>Page Title</strong><br />
                <%: Html.TextBox("PageTitle", Model.PageTitle) %>
            </p>   
            <p>
                <strong>Page Url</strong><br />
                <%: Html.TextBox("PageUrl", Model.PageUrl)%>
            </p>
            <p>
                <input type="hidden" value="<%: Model.Id %>" name="Id" />
                <input value="Save" type="submit" />
            </p>
        <%} %>
    </div>
</asp:Content>
