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

using System.Web.Routing;
using ClubStarterKit.Infrastructure.Routing;

namespace ClubStarterKit.Web.Helper.Routing
{
    public class ErrorPageRouteRegistrant : IRouteRegistrant
    {
        public void Register(RouteCollection routeCollection)
        {
            routeCollection.MapRouteLowercase(
                "errorPageRoute",
                "error",
                new { controller = Website.Home.Name, action = Website.Home.Actions.Error });
        }
    }
}