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

using System.Collections.Generic;
using System.Web.Routing;

namespace ClubStarterKit.Infrastructure.Routing
{
    public static class WwwReasignment
    {
        public static void ReAssignHandler(RouteCollection routes)
        {
            using (routes.GetReadLock())
                AssignRoute301GlobalHandler(routes);
        }

        private static void AssignRoute301GlobalHandler(IEnumerable<RouteBase> routes)
        {
            foreach (RouteBase routeBase in routes)
                AssignRoute301GlobalHandler(routeBase);
        }

        private static void AssignRoute301GlobalHandler(RouteBase routeBase)
        {
            var route = routeBase as Route;

            if (route == null || route is LegacyRoute)
                return;

            if (!(route.RouteHandler is WwwRouteHandler))
                route.RouteHandler = new WwwRouteHandler();
        }
    }
}