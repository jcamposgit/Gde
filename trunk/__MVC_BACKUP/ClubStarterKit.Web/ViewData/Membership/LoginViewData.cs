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

using System.ComponentModel.DataAnnotations;

namespace ClubStarterKit.Web.ViewData.Membership
{
    public class LoginViewData
    {
        public const string ForgotPasswordMessageKey = "forgotPasswordMessage";
        public const string AuthenticationMessageKey = "__Authentication__";

        [Required(ErrorMessage="Username is required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage="Password is required."), DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 5, ErrorMessage="Password is required.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}