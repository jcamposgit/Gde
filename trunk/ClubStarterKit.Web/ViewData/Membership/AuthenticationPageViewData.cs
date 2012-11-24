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

using ClubStarterKit.Infrastructure;
using StructureMap;

namespace ClubStarterKit.Web.ViewData.Membership
{
    public class AuthenticationPageViewData
    {
        public AuthenticationPageViewData(LoginViewData loginData = null, RegistrationViewData registrationData = null)
        {
            LoginData = loginData ?? new LoginViewData();
            RegistrationData = registrationData ?? new RegistrationViewData();
            Config = ObjectFactory.GetInstance<ISiteConfigProvider>();
        }
        
        public LoginViewData LoginData { get; set; }
        public RegistrationViewData RegistrationData { get; set; }
        public ISiteConfigProvider Config { get; set; }
    }
}