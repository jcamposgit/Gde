﻿<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<System.DateTimeOffset>" %>

<%: Html.TextBox("", Model.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"), new { @class = "jquery-ui-datetime text-box single-line" })%>