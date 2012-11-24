﻿<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<System.Collections.Generic.IEnumerable<ClubStarterKit.Domain.Announcement>>" %>

<h2>
    News
    <%= Html.RssLink(Website.News.Rss()) %>
</h2>

<div class="dashedline">  </div>
    <%foreach(var item in Model){ %>
        <span class="listitem">
            <h3>
                <%: Html.ActionLink(item.Title, Website.News.Show(item.Slug))%>
            </h3>
            
            <strong><%: item.ItemDate.ToFriendlyDateString() %></strong>
            
            <p>
                <%: item.Description.Truncate(Constants.TruncateLength)%>
            </p>
            
            <div class="clearlist">
            </div>
        </span>
    <%} %>
    
<div class="dashedline">  </div>
<%: Html.ActionLink("View All Announcements »", Website.News.Index())%>
