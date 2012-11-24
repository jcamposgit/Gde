<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.BlogPost>>" %>

<% var viewData = new ClubStarterKit.Web.ViewData.Blogs.ListViewData(ViewData); %>
<div class="dashedline">  </div>

<% foreach(var item in Model){ %>
    <div class="listitem">
        <h3><%: Html.ActionLink(item.Title, Website.Blog.Show(item.Slug)) %></h3>
        <strong><%: Html.ActionLink(item.Author.FullName(), Website.Blog.Author(item.Author.Slug, null)) %></strong><br />
        <strong><%: item.PostDate.ToFriendlyDateString() %></strong><br />
        
        <br />
        
        <p>
            <%: item.Content.Truncate(Constants.TruncateLength) %>
        </p>
        
        <% var httpcontext = Html.ViewContext.HttpContext;
           if (UserHasElevatedPermission)
           { %>
            <br />
            <%: Html.ActionLink("Edit", Website.Blog.Edit(item.Slug)) %>
            <span> </span>
            <%: Html.ActionLink("Delete", Website.Blog.Delete(item.Slug))%>
        <%} %>
        
        <div class="clearlist"></div>
        <br />
    </div>
<%} %>

<div class="dashedline">  </div>

<%= Html.Pager(Model, viewData.Action) %>