<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Location>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <%if (Model.Id == 0)
      { %>
      <h2>New Location</h2>
    <%} else { %>
        <h2>Edit Location</h2>
    <%} %>
    
    <%using (Html.BeginForm(Website.Events.UpdateLocation(), FormMethod.Post, new { @class = "validateForm" }))
      { %>
        <p>
            <strong>Title</strong><br />
            <%:Html.TextBoxFor(x=>x.Title, new{@class="required"}) %>
        </p>
        <p>
            <strong>Description</strong><br />
            <%:Html.TextAreaFor(x=>x.Description, new{@class="required"}) %>
        </p>
        <p>
            <strong>Address</strong><br />
            <%:Html.TextAreaFor(x => x.Address, new { @class = "required" })%>
        </p>
        <p>
            <strong>Link</strong><br />
            <%:Html.TextBoxFor(x => x.Link)%>
        </p>
        <p>
            <strong>Show Map</strong><br />
            <%:Html.CheckBox("ShowMap", Model.ShowMap)%>
        </p>
        <p>
            <input type="hidden" value="<%: Model.Id %>" name="Id" />
            <input type="submit" value="Save" />
        </p>
    <%} %>
</div>
</asp:Content>