<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Domain.ContentSection>" %>

<div class="contentsection <%: Model.Name %>">
    <%= Model.Content %>
    
    <% if (UserHasElevatedPermission)
       { %>
       <br />
       <%: Html.ActionLink("Edit", Website.ContentSection.Edit(Model.Name)) %>
    <%} %>
</div>