<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.Photo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="rightblock" id="list-block">
    <%: Html.PageJavascript(Links.Content.PageJavascript.PagedList_js) %>
    <div id="ajax-page-list" rel="{block: 'list-block', afterChange: setupForms}">
       <%: Html.DisplayForModel(Website.Photos.Views.DisplayTemplates.Photo) %>
    </div>
</div>

<% if(UserHasElevatedPermission){ %>
    <div class="rightblock">
        <h3>Add Photo</h3>
        <br />
        <%using (Html.BeginForm(Website.Photos.Upload(), FormMethod.Post, new { enctype="multipart/form-data", @class = "validateForm" }))
          { %>
            <p>
                <strong>Title</strong>
                <br />
                <%: Html.TextBox("title", string.Empty, new { @class = "required" })%>
            </p>
            
            <p>
                <strong>File</strong>
                <br />
                <input type="file" id="uploadFile" class="required" name="uploadFile" />
            </p>
            <input name="album" value="<%:(ViewData["Album"] as ClubStarterKit.Domain.Album).Slug %>" type="hidden"/>
            <p>
                <input type="submit" value="Upload" />
            </p>
            
        <% } %>
   </div>
<%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
<% var album = (ViewData["Album"] as ClubStarterKit.Domain.Album) ?? new ClubStarterKit.Domain.Album(); %>
<div class="leftblock">
    <h2><%= album.Title%></h2>
    <div class="dashedline"></div>
    <%= album.Description%>
    
    <% if(UserHasElevatedPermission){ %>
        <br />
        <%: Html.ActionLink("Edit", Website.Photos.Edit(album.Slug)) %>
    <%} %>
</div>
</asp:Content>