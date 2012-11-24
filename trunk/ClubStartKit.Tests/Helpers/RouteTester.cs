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

namespace ClubStarterKit.Tests.Helpers
{
    public static class RouteTester
    {
        /// <summary>
        /// Route data value for the controller
        /// </summary>
        /// <example>RouteTester.Test(...).Values[RouteTester.ActionValue]</example>
        public const string ActionValue = "action";

        /// <summary>
        /// Route data value for the controller
        /// </summary>
        /// <example>RouteTester.Test(...).Values[RouteTester.ControllerValue]</example>
        public const string ControllerValue = "controller";

        /// <summary>
        /// Determines the route data from a fake HttpContext with a URL
        /// </summary>
        /// <param name="registrant">Route registrant to test routes against</param>
        /// <param name="url">URL to determine route data</param>
        /// <returns>RouteData object containing values for given URL</returns>
        public static RouteData Test(IRouteRegistrant registrant, string url)
        {
            var routes = new RouteCollection();
            registrant.Register(routes);

            var context = new FakeHttpContext(url);
            return routes.GetRouteData(context);
        }
    }
}