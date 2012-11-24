<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Announcement>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="rightblock">
        <%: Html.DisplayFor(model => model) %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <h2>News Article</h2>
        <br />
        <%: Html.ActionLink("Article List", Website.News.Index()) %>
    </div>
    
    <!-- PHOTOS -->
</asp:Content>
