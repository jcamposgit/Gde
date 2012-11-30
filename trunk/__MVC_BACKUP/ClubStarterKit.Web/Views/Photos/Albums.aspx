<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.Album>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="rightblock">
    <h2>Photo Albums</h2>
    <div class="dashedline"></div>
    <br />
    <% foreach(var album in Model){ %>
        <h3><%:Html.ActionLink(album.Title, Website.Photos.Actions.ViewAlbum, new { id = album.Slug })%></h3>
        <strong><%: album.DateCreated.ToString("d") %></strong>
        <br />
        <br />
    <%} %>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
<div class="leftblock">
    <%: Html.ContentSection("albums_left") %>
    
    <% if(UserHasElevatedPermission){ %>
        <br />
        <%: Html.ActionLink("New Album", Website.Photos.New()) %>
    <%} %>
</div>
</asp:Content>