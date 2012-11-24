<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.Download>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rightblock">
        <h2>Downloads</h2>
        <div class="dashedline"></div>
        <% bool isAdmin = UserHasElevatedPermission;
           foreach (var item in Model)
           { %>
            <h3><%: Html.ActionLink(item.Title, Website.Downloads.Download(item.Id)) %></h3>
            
            <% if(isAdmin){ %>
                <br />
                <%: Html.ActionLink("Delete", Website.Downloads.Delete(item.Id)) %>
            <%} %>
            
        <%} %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <%= Html.ContentSection("download_side") %>
        
        <%if (UserHasElevatedPermission)
          { %>
            <br />
            <%: Html.ActionLink("New Download", Website.Downloads.New()) %>        
        <%} %>
    </div>
</asp:Content>