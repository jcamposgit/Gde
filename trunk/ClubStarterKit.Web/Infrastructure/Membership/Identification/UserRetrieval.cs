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

using System.Web;
using ClubStarterKit.Core;
using ClubStarterKit.Domain;

namespace ClubStarterKit.Web.Infrastructure.Membership.Identification
{
    public static class UserRetrieval
    {
        public static Maybe<User> GetUser(HttpContextBase context)
        {
            var sessionValue = context.Session[Constants.UserSessionKey] as User;

            if (sessionValue != null)
                return sessionValue;

            var cookieValue = context.Request.Cookies[Constants.UserIdCookieKey];

            if (cookieValue == null)
            {
                if (!context.User.Identity.IsAuthenticated)
                    return null;

                var user = new UserRetrievalAction(context.User.Identity.Name).Execute();
                context.Session[Constants.UserSessionKey] = user;
                return user;
            }

            return new UserRetrievalAction(cookieValue.Value).Execute();            
        }
    }
}