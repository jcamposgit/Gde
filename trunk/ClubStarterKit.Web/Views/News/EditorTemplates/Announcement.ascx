<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Domain.Announcement>" %>

    <%: Html.ValidationSummary("Save was unsuccessful. Please correct the errors and try again.") %>
    

    <% using (Html.BeginForm(Website.News.Update(), FormMethod.Post)) {%>
            <p>
                <strong>Title</strong><br />
                <%: Html.TextBox("Title", Model.Title) %>
                <%: Html.ValidationMessage("Title", "*") %>
            </p>
            
            <p>
                <strong>Static Url</strong><br />
                <%: Html.TextBox("StaticUrl", Model.StaticUrl) %>
                <%: Html.ValidationMessage("StaticUrl", "*") %>
            </p>
            
            <p>
                <strong>Description</strong><br />
                <%: Html.TextArea("Description", Model.Description, 20, 75, new { @class = "wysiwyg" })%>
                <%: Html.ValidationMessage("Description", "*") %>
            </p>
            
            <p>
                <label>Article Visible Date:</label>
                <%: Html.EditorFor(model => model.ItemDate)%>
            </p>
            
             <input type="hidden" name="Id" value="<%: Model.Id %>" />
            
            <p>
                <input type="submit" value="Save" />
            </p>

    <% } %>