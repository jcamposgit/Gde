<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.BlogPost>>" %>

<h2>
    Blog Posts
    <%= Html.RssLink(Website.Blog.Rss()) %>
</h2>

<div class="dashedline">  </div>
    <%foreach(var item in Model){ %>
        <span class="listitem">
            <h3>
                <%: Html.ActionLink(item.Title, Website.Blog.Show(item.Slug))%>
            </h3>
            
            <strong><%: item.PostDate.ToFriendlyDateString() %></strong>
            
            <p>
                <%: item.Content.Truncate(Constants.TruncateLength)%>
            </p>
            
            <div class="clearlist">
            </div>
        </span>
    <%} %>
    
<div class="dashedline">  </div>
<%: Html.ActionLink("View All Blog Posts »", Website.Blog.Index())%>