<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.PollQuestion>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="FullContent" runat="server">
    <div id="question-block" class="fullwidth">
        <%if(Model.Id == 0){ %>
            <h3>New Question</h3>
        <%} else{ %>
            <h3>Edit Question</h3>
        <%} %>
        
        <br />
        <% using (Ajax.AsyncForm(Website.Poll.UpdateQuestion(), 
                    new AsyncFormOptions(AsyncFormType.TargetUpdate, formid: "question-block", targetUpdate: "question-block"))){ %>
            <input type="hidden" value="<%: Model.Id %>" name="question.Id" />
            <input type="hidden" value="false" name="question.Hidden" />
            <input type="hidden" value="<%: DateTimeOffset.Now.ToString() %>" name="question.CreationDate" />
            <%: Html.TextBox("question.Question", Model.Question) %>
            <br />
            <input value="Save" type="submit" />
        <%} %>
    </div>
</asp:Content>
