<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Domain.Location>" %>
<%@ Import Namespace="ClubStarterKit.Web.Infrastructure.Calendar" %>

<% var locations = new LocationListAction().Execute(); %>

<%: Html.DropDownList("Location", locations.Select(x=>new SelectListItem()
    {
        Selected = Model != null && Model.Id == x.Id,
        Text =x.Title,
        Value = x.Id.ToString()
    }))%>