<%@ Page Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.PollQuestion>" %>

<%: Html.PageJavascript(Links.Content.PageJavascript.PollAddAnswer_js) %>

<h2>Add Answers</h2>

<br />

<ul id="answers">
    
</ul>
<br />
<div>
    <% using (Ajax.AsyncForm(Website.Poll.AddAnswer(), 
           new AsyncFormOptions(AsyncFormType.General, FormMethod.Post, "addAnswer", AjaxReturnType.Json, 
                                postRequestFunction: "UpdateList", elementBlockId: "question-block")))
       { %>
            <h3>Add Answer</h3>
            <br />
            <%: Html.TextBox("answer") %>
            <br />
            <input type="hidden" name="pos" value="0" /> <!--TODO: add position support-->
            <input type="hidden" name="poll" value="<%: Model.Id %>" />
            <input type="submit" value="Add Answer" />
    <% } %>
</div>