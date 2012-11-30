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
using System.Web;
using ClubStarterKit.Infrastructure.Application;
using StructureMap;
using IBootstrapper = ClubStarterKit.Core.IBootstrapper;

namespace ClubStarterKit.Web.Infrastructure.Application
{
    public class ApplicationIdProvider : IApplicationIdProvider
    {
        private const string ApplicationKey = "Application.ApplicationId";
        private object _appsetlock = new object();

        public string ApplicationId
        {
            get 
            {
                var app = HttpContext.Current.Application;

                if (app[ApplicationKey] == null)
                {
                    var now = DateTime.Now;
                    app[ApplicationKey] = now.Year.ToString() +
                                          now.Month.ToString() +
                                          now.Day.ToString() +
                                          now.Hour.ToString() +
                                          now.Minute.ToString() +
                                          now.Second.ToString() +
                                          now.Millisecond.ToString();
                }

                return app[ApplicationKey].ToString();
            }
        }


        public void Expire()
        {
            HttpContext.Current.Application[ApplicationKey] = null;
        }
    }

    public class ApplicationIdProviderBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            ObjectFactory.Configure(config => config.ForRequestedType<IApplicationIdProvider>().TheDefaultIsConcreteType<ApplicationIdProvider>());
        }
    }
}