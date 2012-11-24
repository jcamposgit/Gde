<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Event>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <% if (Model.Id == 0)
       { %>
       <h2>New Event</h2>
    <%} else { %>
        <h2>Edit Event</h2>
    <%} %>
    <%: Html.ActionLink("New Location", Website.Events.NewLocation()) %>
    <br />
    <%using (Html.BeginForm(Website.Events.Update(), FormMethod.Post, new { @class = "validateForm" }))
      { %>
        <p>
            <strong>Title</strong><br />
            <%: Html.TextBoxFor(x=>x.Title, new{@class="required"}) %>
        </p>
        <p>
            <strong>Description</strong><br />
            <%: Html.TextAreaFor(x=>x.Description, 20, 75, new { @class = "wysiwyg required" })%>
        </p>
        <p>
            <strong>Start</strong><br />
            <%: Html.EditorFor(x=>x.StartTime) %>
        </p>
        <p>
            <strong>End</strong><br />
            <%: Html.EditorFor(x=>x.EndTime) %>
        </p>
        <p>
            <strong>Allow RSVP</strong><br />
            <%: Html.CheckBox("AllowRsvp", Model.AllowRsvp) %>
        </p>
        <p>
            <strong>LinkUrl</strong><br />
            <%: Html.TextBoxFor(x=>x.LinkUrl) %>
        </p>
        <p>
            <strong>Location</strong><br />
            <%: Html.EditorFor(x=>x.EventLocation) %>
        </p>
        <p>
            <input name="Id" value="<%: Model.Id %>" type="hidden" />
            <input value="Save" type="submit" />
        </p>
    <%} %>
</div>
</asp:Content>