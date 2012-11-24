<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Forum.EditTopicViewData>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
<div class="fullwidth">
    <%: Html.PageJavascript(Links.Content.PageJavascript.ForumTopicEdit_js) %>
    <% if (string.IsNullOrEmpty(Model.Topic.TopicSlug))
       { %>
        <h2>New Topic</h2>
    <%}
       else
       { %>
        <h2>Edit Topic</h2>
    <%} %>
    
    <div class="dashedline"></div>
    
    <% using(Html.BeginForm(Website.Forum.Update(), FormMethod.Post, new { @class = "validateForm"})){ %>
        <p>
            <strong>Title</strong><br />
            <%: Html.TextBoxFor(x=>x.Topic.Title) %>
        </p>
        
        <p>
            <strong>Title</strong><br />
            <%: Html.WysiwygFor(x=>x.Topic.Description) %>
        </p>
        
        <p>
            <strong>Group</strong><br />
            <%: Html.TextBoxFor(x => x.Topic.Group, new { @class = "required" })%>
            <br />
            <i>Current Groups</i><br />
            <%foreach(var group in Model.Groups){ %>
                <a href="#Topic.Group" class="cur-group"><%: group %></a><span> </span>
            <%} %>
        </p>
        
        <p>
            <strong>Visible to Anonymous Users</strong><br />
            <%: Html.CheckBox("Topic.VisibleToAnonymous", Model.Topic.VisibleToAnonymous)%>
        </p>
        
        <p>
            <%: Html.HiddenFor(x=>x.Groups) %>
            <input name="Topic.Id" type="hidden" value="<%: Model.Topic.Id %>" />
            <input type="submit" value="Save" />
        </p>
    <%} %>
</div>
</asp:Content>