#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Web.Mvc;
using System.Web.Security;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.Membership.Actions;
using ClubStarterKit.Web.ViewData.Membership;

namespace ClubStarterKit.Web.Controllers
{
    public partial class MembershipController : BaseController
    {
        #region Authentication / Login

        public virtual ActionResult Login()
        {
            ViewData.Title("Login");

            var data = new LoginViewData
            {
                ReturnUrl = Request[Constants.ReturnUrlRequestKey] ?? string.Empty
            };
            return View(Views.Login, new AuthenticationPageViewData(data));    
        }

        [HttpPost]
        public virtual ActionResult Authenticate(LoginViewData authData)
        {
            bool valid = ModelState.IsValid &&
                         new MembershipService().ValidateUser(authData.Username, authData.Password);

            if (Request.IsAjaxRequest())
                return HandleAjaxAuthentication(authData, valid);

            if (valid)
                return string.IsNullOrEmpty(authData.ReturnUrl) ? 
                            (ActionResult)RedirectToAction(Website.Home.Index()) :
                            (ActionResult)Redirect(authData.ReturnUrl);

            // user wasn't authenitcated, send the data back
            ModelState.AddModelError(LoginViewData.AuthenticationMessageKey, "Authentication failed. Please try again.");
            // reset password for security
            authData.Password = "";
            return View(Views.Login, new AuthenticationPageViewData(authData));
        }

        [NonAction]
        private ActionResult HandleAjaxAuthentication(LoginViewData authData, bool valid)
        {
            if (!valid)
                return Json(new { authentication = "fail" });

            string url = Url.Action(Website.Home.Index());
            if(!string.IsNullOrEmpty(authData.ReturnUrl))
                url = authData.ReturnUrl;

            return Json(new { url });
        }

        [Authorize]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(Website.Home.Index());
        }

        #endregion

        #region Forgot Password

        [HttpPost]
        public virtual ActionResult Forgot(string email)
        {
            // empty email
            if (string.IsNullOrEmpty(email))
                return HandleForgotResult("Email must be specified.");

            if (new ForgotAction(email).Execute()) // message sent
                return HandleForgotResult("Username and password were sent to " + email);
            // message wasn't sent
            return HandleForgotResult("Email address " + email + " was not found.");
        }

        [NonAction]
        private ActionResult HandleForgotResult(string message)
        {
            return new AjaxDeterministicResult
            (
                () => Content(message),
                () =>
                {
                    ViewData[LoginViewData.ForgotPasswordMessageKey] = message;
                    return View(Views.Login, new AuthenticationPageViewData());
                }
            );
        }

        #endregion

        #region User Registration

        [HttpPost]
        public virtual ActionResult Registration(RegistrationViewData registration)
        {
            if (registration == null)
                throw new Exception("Registration data is empty.");

            if (!ViewData.ModelState.IsValid)
                return HandleRegistrationData("Invalid registration request.");

            if (registration.Password != registration.PasswordRepeat)
                return HandleRegistrationData("Passwords must match.");

            if (new UserRegistractionAction(registration).Execute())
                return HandleRegistrationData("Registration accepted. You may now login.");
            return HandleRegistrationData("Invalid registration request.");
        }

        [NonAction]
        private ActionResult HandleRegistrationData(string message)
        {
            return new AjaxDeterministicResult
            (
                () => Content(message),
                () =>
                {
                    ViewData[RegistrationViewData.MessageViewDataKey] = message;
                    return View(Views.Login, new AuthenticationPageViewData());
                }
            );
        }

        #endregion

        #region Administration

        [Admin]
        public virtual ActionResult Delete(string id, string returnurl = "")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            new UserDeleteAction(id).Execute();

            if (string.IsNullOrEmpty(returnurl))
                return RedirectToAction(Website.Membership.Index());
            return Redirect(returnurl);
        }

        [Admin]
        public virtual ActionResult Email()
        {
            ViewData.Title("Send Email");
            return View(new SendEmailViewData());
        }

        [Admin, HttpPost]
        public virtual ActionResult SendEmail(SendEmailViewData data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var result = new AllMemberEmailAction(data).Execute();

            return RedirectToAction(Website.Membership.Index());
        }

        #endregion

        #region User Actions

        [Authorize]
        public virtual ActionResult Revoke()
        {
            new UserDeleteAction(User.Identity.Name).Execute();
            FormsAuthentication.SignOut();
            return RedirectToAction(Website.Home.Index());
        }

        [Authorize, ValidateInput(false)]
        public virtual ActionResult Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var result = new UserUpdateAction(user).Execute() ? "User profile updated" : "Profile update failed.";

            return new AjaxDeterministicResult
            (
                () => Content(result),
                () =>
                {
                    ViewData["UserUpdate"] = result;
                    ViewData.Title("Edit Profile");
                    return View(Views.Edit, new UserRetrievalAction(User.Identity.Name).Execute());
                }
            );
        }

        [Authorize]
        public virtual ActionResult List(int id = 1)
        {
            int index = id - 1;
            ViewData.Title("Members (Page " + id + ")");
            return View(Views.List, new UserListAction(index).Execute());
        }

        [Authorize]
        public virtual ActionResult Profile(string id = null)
        {
            id = string.IsNullOrEmpty(id) ? User.Identity.Name : id;

            var user = new UserRetrievalAction(id).Execute();

            ViewData.Title("Profile - " + user.Username);
            return View(Views.Profile, user);
        }

        [Authorize]
        public virtual ActionResult Edit()
        {
            ViewData.Title("Edit Profile");
            return View(Views.Edit, new UserRetrievalAction(User.Identity.Name).Execute());
        }

        [Authorize]
        public virtual ActionResult Index()
        {
            ViewData.Title("Members");
            return View(Views.List, new UserListAction(0).Execute());
        }

        [HttpPost, Authorize]
        public virtual ActionResult ChangePassword(string CurrentPassword, string NewPassword, string NewPasswordRepeat)
        {
            var result = new ChangePasswordAction(new ChangePasswordContext
            {
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword,
                NewPasswordRepeat = NewPasswordRepeat,
                Username = User.Identity.Name
            }).Execute();

            return new AjaxDeterministicResult
            (
                () => Content(result),
                () =>
                {
                    ViewData["ChangePwd"] = result;
                    ViewData.Title("Edit Profile");
                    return View(Views.Edit, new UserRetrievalAction(User.Identity.Name).Execute());
                }
            );
        }

        #endregion
    }
}