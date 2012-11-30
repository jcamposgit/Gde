﻿#region license

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
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Routing;

namespace ClubStarterKit.Web
{
    public class CskApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            // run the bootsrap method on all the bootstrappers
            //new BinPartLoader<IBootstrapper>().WithAssemblyOf(this)
            //new BinPartLoader<IBootstrapper>().WithAssemblyOf(this)
            //                                  .Items
            //                                  .Foreach(i => i.Bootstrap());

            // Uncomment this line *ON YOUR FIRST RUN* to hydrate the database wtih tables
             //new ClubStarterKit.Data.NHibernate.NHDataProvider(new ClubStarterKit.Web.Infrastructure.Application.SessionBuilder()).Hydrate();



        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            // register routes from the registrants
            //new BinPartLoader<IRouteRegistrant>().WithAssemblyOf<CskApplication>()
            //                                     .Items
            //                                     .Foreach(r => r.Register(routes));

            // because this mapping needs to be last
            // this mapping MUST be after all registrant 
            // registrations
            routes.MapRouteLowercase(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );
        }
    }
}