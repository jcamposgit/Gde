<%@ Page Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseView" %>

There are currently no polls to display.

<%if(UserHasElevatedPermission){ %>
    <%: Html.ActionLink("New Poll", Website.Poll.New()) %>
<%} %>