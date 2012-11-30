<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Core.IPagedList<ClubStarterKit.Domain.Announcement>>" %>
<div class="dashedline">  </div>

<% foreach(var item in Model){ %>
    <div class="listitem">
        <!-- Photo -->
        <%--<div class="thumbnail">
            <a href='<%# "View.aspx?Articleid=" &Cstr( Eval("ID"))%>'>
                <Club:ImageThumbnail ID="ImageThumbnail1" runat="server" PhotoID='<%# Eval("photo") %>'
                    />
            </a>
        </div>--%>
        
        <h3>
            <%: Html.ActionLink(item.Title, Website.News.Show(item.Slug)) %>
        </h3>
        <strong><%: item.ItemDate.ToFriendlyDateString() %></strong>
        
        <p>
            <%: item.Description.Truncate(Constants.TruncateLength) %>
        </p>
        
        <% if(UserHasElevatedPermission){ %>
        <p>
            <%: Html.ActionLink("Edit", Website.News.Edit(item.Slug)) %>
            <span> </span>
            <%: Html.ActionLink("Delete", Website.News.Delete(item.Slug)) %>
        </p>
        <%} %>
        
        <div class="clearlist">
        </div>
        
        <br />
    </div>
<%} %>

<div class="dashedline">  </div>

<%= Html.RssLink(Website.News.Rss()) %>
<%= Html.Pager(page => Website.News.List(page)) %>