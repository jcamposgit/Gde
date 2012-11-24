<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rightblock">
        <h2><%: Model.FullName() %></h2>
        <div class="dashedline"></div>
        
        <p>
            <strong>Address</strong>
            <br />
            <%: Model.Address %>
        </p>
        
        <p>
            <strong>Phone</strong>
            <br />
            <%: Model.Phone %>
        </p>        
        
        <% if(Model.ShowEmail){ %>
            <p>
                <strong>Email</strong><br />
                <a href="mailto:<%: Model.Email %>"><%: Model.Email %></a>
            </p>
        <%} %>
    </div>
    
    <% if(!string.IsNullOrEmpty(Model.Bio)){ %>
        <div class="rightblock">
            <h3>Bio</h3>
            <br />
            <%= Model.Bio %>
        </div>
    
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <h2>Profile</h2>
        <%: GravatarHelper.GravatarImage(Model.Email) %>
        
        <br />
        <% if(UserHasElevatedPermission){ %>
            <%: Html.ActionLink("Delete", Website.Membership.Delete(Model.Username)) %>
        <%} %>
    </div>
</asp:Content>