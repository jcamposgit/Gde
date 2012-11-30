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
using System.Web.Routing;

namespace ClubStarterKit.Infrastructure.Routing
{
    public static class MapLowercaseRoute
    {
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRouteLowercase(name, url, defaults, null);
        }

        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults,
                                             object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var route = new LowercaseRoute(url, new MvcRouteHandler())
                            {
                                Defaults = new RouteValueDictionary(defaults),
                                Constraints = new RouteValueDictionary(constraints)
                            };

            if (string.IsNullOrEmpty(name))
                routes.Add(route);
            else
            {
                try
                {
                    routes.Add(name, route);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}