<%@ Control Language="C#" Inherits="ClubStarterKit.Infrastructure.View.BaseControl<ClubStarterKit.Domain.User>" %>

<div class="membercard">
   <div style="padding: 0pt 5px 0pt 0pt; float: left;">
		<%: GravatarHelper.GravatarImage(Model.Email) %>
	</div>
	<h3>
		<%: Model.FullName() %>
	</h3>
	<p>
		<a href="mailto:<%: Model.Email %>">
            <%: Model.Email %>
        </a>
	</p>
	<div class="clearcard"></div>
</div>