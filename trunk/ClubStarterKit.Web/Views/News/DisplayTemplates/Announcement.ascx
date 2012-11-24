<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Domain.Announcement>" %>

<h2>
    <%: Model.Title %>
</h2>

<div class="dashedline">   </div>

<strong>
    <% if(!string.IsNullOrEmpty(Model.StaticUrl)){ %>
        <a href="<%: Model.StaticUrl %>">External Url</a>
    <%} %>
</strong>

<div class="itemdetails">
    <p>
        <%: Model.ItemDate.ToFriendlyDateString() %>
    </p>
</div>

<%= Model.Description %>

<div class="dashedline">   </div>
<% if(UserHasElevatedPermission){ %>
    <p>
        <%: Html.ActionLink("Edit", Website.News.Edit(Model.Slug)) %>
        <span> </span>
        <%: Html.ActionLink("Delete", Website.News.Delete(Model.Slug)) %>
    </p>
<%} %>

<!-- DOWNLOADS -->