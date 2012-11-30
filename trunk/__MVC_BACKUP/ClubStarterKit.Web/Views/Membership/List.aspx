<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rightblock">
        <% foreach(var member in Model){ %>
            
            <div class="membercard">
                <div style="float: left;padding: 0px 5px 0px 0px">
                    <%: GravatarHelper.GravatarImage(member.Email) %>
                </div>
                <h3>
                    <%: Html.ActionLink(member.FullName(), Website.Membership.Profile(member.Username)) %>
                </h3>
                
                <a href="mailto:<%: member.Email %>"><%: member.Email %></a>
                
                <% if(UserHasElevatedPermission){ %>
                    <br /><br />
                    <%: Html.ActionLink("Delete", Website.Membership.Delete(member.Username)) %>
                <%} %>
                
                <div class="clearcard"></div>
            </div>
        
        <%} %>
        <div class="dashedline"></div>
        <%: Html.Pager(x=>Website.Membership.List(x)) %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <%: Html.ContentSection("memberslist_side") %>
        <br />
        <%: Html.ActionLink("My Membership Details", Website.Membership.Profile()) %>
        <br />
        <%: Html.ActionLink("Edit Membership Details", Website.Membership.Edit()) %>
        <br />
        <%: Html.ConfirmationLink("Revoke Membership", Website.Membership.Revoke(), "Confirm Membership Revoke", "Are you sure you want to cancel your membership?") %>
    </div>
</asp:Content>