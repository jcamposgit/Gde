<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.Event>" %>

<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Downloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="rightblock">
    <h2><%: Model.Title %></h2>
    <strong>Start</strong>: <%: Model.StartTime.ToString(Constants.EventDateFormat)%><br />
    <strong>End</strong>: <%: Model.EndTime.ToString(Constants.EventDateFormat)%><br />
    
    <% if(!string.IsNullOrEmpty(Model.LinkUrl)){ %>
        <br />
        <a href="<%: Model.LinkUrl %>" target="_blank">Link</a>
    <%} %>
    
    <div class="dashedline"></div>
    
    <br />
    <%= Model.Description %>
    <br />
    
    <%if (UserHasElevatedPermission)
      { %>
        <%: Html.ActionLink("Delete", Website.Events.Delete(Model.Slug)) %>
        <br />
        <%: Html.ActionLink("Edit", Website.Events.Edit(Model.Slug)) %>
    <%} %>
    
</div>

<%--<%if (UserHasElevatedPermission)
      { %>
    <div class="rightblock">
        <h3>Add Download</h3>
        <%using (Html.BeginForm(Website.Events. %>
            <%: Html.DropDownList("addDownload", new DownloadListAction().Execute().Select(x => new SelectListItem() 
                                                                                            { 
                                                                                                Selected = false, 
                                                                                                Text = x.Title, 
                                                                                                Value = x.Id 
                                                                                            })); %>
        
    </div>      
<%} %>--%>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">

<% if (Model.Downloads != null && Model.Downloads.Count > 0)
   { %>
    <div class="leftblock">
        <h3>Downloads</h3>
        <br />
        <% foreach(var download in Model.Downloads){ %>
            <%: Html.ActionLink(download.Title, Website.Downloads.Download(download.Id)) %>
            <br />
        <%} %>
    </div>
<%} %>

<% if(Model.EventLocation != null){ %>
    <div class="leftblock">
        <h3>Location</h3>
        <strong><%: Model.EventLocation.Title %></strong>
        <br />
        <%: Model.EventLocation.Description %>
        <br /><br />
        <strong>Address</strong><br />
        <%: Model.EventLocation.Address %>
    </div>
<%} %>

<%if (Model.AllowRsvp && User.Identity.IsAuthenticated)
  { %>
  <div class="leftblock">
    <h3>RSVP</h3>
    <br />
    <!-- TODO: RSVP -->
  </div>
<%} %>
</asp:Content>
