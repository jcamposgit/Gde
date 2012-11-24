<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.ContentPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rightblock">
        <h2><%: Model.PageTitle %></h2>
        <div class="dashedline"></div>
        <%= Html.ContentSection(Model.PageUrl + "_main") %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <%= Html.ContentSection(Model.PageUrl + "_side") %>
        
        <% if(UserHasElevatedPermission){ %>
            <br />
            <%: Html.ActionLink("Edit", Website.ContentPage.Edit(Model.PageUrl))%>
            <span> </span>
            <%: Html.ActionLink("Delete", Website.ContentPage.Delete(Model.PageUrl))%>
        <%} %>
    </div>
</asp:Content>