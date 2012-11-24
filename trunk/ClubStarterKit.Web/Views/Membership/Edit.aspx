<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Domain.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.PageJavascript(Links.Content.PageJavascript.ProfileEdit_js) %>
    <div class="rightblock" id="updateBlock">
        <h2><%: Model.Username %></h2>
        <br />
        
        <%: Html.InfoMessage("UserUpdate", "updateUserMsg", "updateUserMsgTxt") %>
        
        <% using (Ajax.AsyncForm(Website.Membership.Update(), new AsyncFormOptions(AsyncFormType.TargetUpdate,
               postRequestFunction: "showUserUpdateMessage",
               formid: "updateUser", targetUpdate: "updateUserMsgTxt", elementBlockId: "updateBlock"))){ %>
            
            <p>
                <strong>First Name</strong><br />
                <%: Html.TextBox("FirstName", Model.FirstName, new{ @class = "required"}) %>
            </p>
            
            <p>
                <strong>Last Name</strong><br />
                <%: Html.TextBox("LastName", Model.LastName, new{ @class = "required"}) %>
            </p>
            
            <p>
                <strong>Phone</strong><br />
                <%: Html.TextBox("Phone", Model.Phone, new{ @class = "required phoneUS"}) %>
            </p>
            
            <p>
                <strong>Address</strong><br />
                <%: Html.TextArea("Address", Model.Address, 2, 20, null) %>
            </p>
            
            <p>
                <strong>Email</strong><br />
                <%: Html.TextBox("Email", Model.Email, new{ @class = "required email"}) %>
            </p>
            
            <p>
                <strong>Show Email</strong><br />
                <%: Html.CheckBox("ShowEmail", Model.ShowEmail) %>
            </p>
            
            <p>
                <strong>Show Gravatar</strong><br />
                <%: Html.CheckBox("ShowGravatar", Model.ShowGravatar) %>
            </p>
            
            <p>
                <strong>Send Newsletter</strong><br />
                <%: Html.CheckBox("SendNewsletter", Model.SendNewsletter) %>
            </p>
            
            <p>
                <strong>Signature</strong><br />
                <%: Html.TextArea("Signature", Model.Signature, 20, 50, new { @class= "wysiwyg" })%>
            </p>
            
            <p>
                <strong>Bio</strong><br />
                <%: Html.TextArea("Bio", Model.Bio, 20, 50, new { @class= "wysiwyg" })%>
            </p>
            
            <%: Html.Hidden("Username", Model.Username) %>
            <%: Html.Hidden("MessagesPerPage", Model.MessagesPerPage)%>
            <%: Html.Hidden("PasswordSalt", Model.PasswordSalt)%>
            <%: Html.Hidden("SaltedPassword", Model.SaltedPassword)%>
            <input type="hidden" value="<%: Model.Id %>" name="Id" />
            <p>
                <input type="submit" value="Update" />
            </p>
        <%} %>
        
    </div>

    <div class="rightblock" id="changePasswordBlock">
        <h3>Change Password</h3>
        <div class="dashedline"></div>
        
        <%: Html.InfoMessage("ChangePwd", "changePwdMsg", "changePwdMsgTxt") %>
        
        <% using (Ajax.AsyncForm(Website.Membership.ChangePassword(), new AsyncFormOptions(AsyncFormType.TargetUpdate,
               formid: "changePassword", targetUpdate: "changePwdMsgTxt",
               postRequestFunction: "showChangePasswordMessage",
               elementBlockId: "changePasswordBlock")))
           { %>
                <p>
                    <strong>Current Password</strong>
                    <br />
                    <input name="CurrentPassword" value="" class="required" type="password" />
                </p>
                
                <p>
                    <strong>New Password</strong>
                    <br />
                    <input id="chngPwdPwd" name="NewPassword" class="required" value="" type="password" />
                </p>
                
                <p>
                    <strong>New Password Repeat</strong>
                    <br />
                    <input name="NewPasswordRepeat" equalto="#chngPwdPwd" class="required" value="" type="password" />
                </p>
                
                <p>
                    <input value="Change Password" type="submit" />
                </p>
        <%} %>
    
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        <h2>Edit Profile</h2>
        <div class="dashedline"></div>
        Use this page to ammend your personal profile.
        
    </div>
</asp:Content>