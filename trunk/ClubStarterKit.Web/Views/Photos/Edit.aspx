<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Album>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <%if(Model.Id == 0){ %>
        <h2>Add Album</h2>
    <%} else{ %>
        <h2>Edit Album</h2>
    <%} %>
    
    <br />
    
    <% using (Html.BeginForm(Website.Photos.Update())){ %>
        <p>
            <strong>Title</strong><br />
            <%: Html.TextBoxFor(x=>x.Title, new{@class = "required"}) %>
        </p>
        
        <p>
            <strong>Allow Anonymous</strong><br />
            <%: Html.CheckBox("AllowAnonymous", Model.AllowAnonymous) %>
        </p>
        
        <p>
            <strong>Description</strong><br />
            <%: Html.TextAreaFor(x=>x.Description, 20, 75, new { @class = "wysiwyg required" }) %>
        </p>
        
        <%: Html.HiddenDataValue("Owner", Model.Owner) %>
        <input name="Id" type="hidden" value="<%: Model.Id %>" />
        <input name="DateCreated" type="hidden" value="<%:Model.DateCreated.ToString() %>" />
        <input type="submit" value="Save" />
    <% } %>
</div>
</asp:Content>