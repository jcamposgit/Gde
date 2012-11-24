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

using System.Web.Mvc;
using System.Web.Routing;
using ClubStarterKit.Infrastructure.Routing;

namespace ClubStarterKit.Web.Helper.Routing
{
    public class ForumRouteRegistrant : IRouteRegistrant
    {
        public void Register(RouteCollection routeCollection)
        {
            routeCollection.MapRouteLowercase(
                "ForumMsgRoute",
                "forum/message/{action}/{id}",
                new { controller = Website.Message.Name, id = string.Empty },
                new { action = new NotEmptyConstraint() }
            );

            routeCollection.MapRouteLowercase(
                "ThreadView",
                "forum/thread/{id}/{action}",
                new { controller = Website.Thread.Name, action = Website.Thread.Actions.ViewThread },
                new { id = new NotEmptyConstraint() } 
            );

            routeCollection.MapRouteLowercase(
                "ThreadWithAction",
                "forum/thread/{action}",
                new { controller = Website.Thread.Name },
                new { action = new NotEmptyConstraint() }
            );

            // ALWAYS ignore thread controller routes that don't
            // start with the forum root
            routeCollection.IgnoreRoute("thread/{*rest}");
            routeCollection.IgnoreRoute("message/{*rest}");
        }
    }
}