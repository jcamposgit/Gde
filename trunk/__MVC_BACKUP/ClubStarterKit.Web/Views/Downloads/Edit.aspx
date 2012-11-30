<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div class="fullwidth">
        <h2>Add Download</h2>
        
        <div class="dashedline"></div>
        
        <%using (Html.BeginForm(Website.Downloads.Upload(), FormMethod.Post, new { enctype="multipart/form-data", @class = "validateForm" }))
          { %>
            <p>
                <strong>Title</strong>
                <br />
                <%: Html.TextBox("Title", string.Empty, new { @class = "required" })%>
            </p>
            
            <p>
                <strong>File</strong>
                <br />
                <input type="file" id="uploadFile" class="required" name="uploadFile" />
            </p>
            
            <p>
                <input type="submit" value="Upload" />
            </p>
            
        <%} %>
    </div>
</asp:Content>
