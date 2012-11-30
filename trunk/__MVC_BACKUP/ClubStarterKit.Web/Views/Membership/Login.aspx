<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Membership.AuthenticationPageViewData>" %>
<%@ Import Namespace="ClubStarterKit.Web.ViewData.Membership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.PageJavascript(Links.Content.PageJavascript.Login_js) %>

    <div class="rightblock" id="loginBlock">
        <h2>Login to <%: Model.Config.ApplicationName %></h2>
        <div class="dashedline"></div>
        <% using (Ajax.AsyncForm(Website.Membership.Authenticate(), new AsyncFormOptions(AsyncFormType.General, 
                                 formid: "loginForm", postRequestFunction: "afterLogin", elementBlockId: "loginBlock",
                                 returnType: AjaxReturnType.Json)))
        {%>
                <%: Html.AlertMessage("<strong>Login Failed:</strong> Please try again.", LoginViewData.AuthenticationMessageKey, "loginResult")%>
                
                <p>
                    <strong>Username</strong>
                    <br />
                    <%: Html.TextBox("Username", Model.LoginData.Username, new { @class = "required" })%>
                </p>
                
                <p>
                    <strong>Password</strong>
                    <br />
                    <%: Html.Password("Password", Model.LoginData.Password, new { @class = "required" })%>
                </p>
                <p>
                    <strong>Remember Me</strong>
                    <br />
                    <%: Html.CheckBox("RememberMe", Model.LoginData.RememberMe)%>
                </p>
                
                <%: Html.Hidden("ReturnUrl", Model.LoginData.ReturnUrl)%>
                
                <p>
                    <input type="submit" value="Login" />
                </p>

        <% } %>
    </div>
    
    <div class="rightblock" id="forgotBlock">
        <h2>Forgot Username or Password</h2>
        <div class="dashedline"></div>
        
        <% using(Ajax.AsyncForm(Website.Membership.Forgot(), new AsyncFormOptions(AsyncFormType.TargetUpdate, 
                                formid: "forgotInfo", postRequestFunction: "showForgotMessage", 
                                elementBlockId: "forgotBlock", targetUpdate: "forgotMessage")))
            { %>
            Enter your email to have your username and password 
            sent to your email address.<br /><br />
            
            <%: Html.InfoMessage(LoginViewData.ForgotPasswordMessageKey, "forgotEmailMsg", "forgotMessage")%>
            
            <p>
                <strong>Email</strong>
                <br />
                <%: Html.TextBox("email") %>
            </p>
            
            <p>
                <input type="submit" value="Send" />
            </p>
        <%} %>
    </div>
    
    <div class="rightblock" id="regBlock">
        <h3>Registration</h3>
        <div class="dashedline"></div>
        <% using(Ajax.AsyncForm(Website.Membership.Registration(), 
                       new AsyncFormOptions(AsyncFormType.TargetUpdate, 
                                formid: "register", postRequestFunction: "showRegistrationMessage",
                                elementBlockId: "regBlock", targetUpdate: "regMsgTxt")))
        { %>
                    <%: Html.InfoMessage(RegistrationViewData.MessageViewDataKey, "regMsg", "regMsgTxt")%>
                    
                    <p>
                        <strong>Username</strong>
                        <br />
                        <%: Html.TextBox("Username", Model.RegistrationData.Username, new { @class = "required", minlength = "4" })%>
                    </p>
                    
                    <p>
                        <strong>Password</strong>
                        <br />
                        <%: Html.Password("Password", Model.RegistrationData.Password, new { id = "regPwd", @class = "required", minlength = "6" })%>
                    </p>
                    
                    <p>
                        <strong>Repeat Password</strong>
                        <br />
                        <%: Html.Password("PasswordRepeat", Model.RegistrationData.PasswordRepeat, new { equalto = "#regPwd", @class = "required", minlength = "6" })%>
                    </p>
                    
                    <p>
                        <strong>Email</strong>
                        <br />
                        <%: Html.TextBox("Email", Model.RegistrationData.Email, new { @class = "required email" })%>
                    </p>
                    
                    <p>
                        <strong>First Name</strong>
                        <br />
                        <%: Html.TextBox("FirstName", Model.RegistrationData.FirstName, new { @class = "required", minlength = "2" })%>
                    </p>
                    
                    <p>
                        <strong>Last Name</strong>
                        <br />
                        <%: Html.TextBox("LastName", Model.RegistrationData.LastName, new { @class = "required", minlength = "2" })%>
                    </p>
                    
                    <p>
                        <input type="submit" value="Register" />
                    </p>
        <%} %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <a name="content_start" id="content_start"></a>
    <div class="leftblock">
        <%= Html.ContentSection("members_login") %>
    </div>
</asp:Content>

