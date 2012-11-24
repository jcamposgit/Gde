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

using System.Configuration;
using ClubStarterKit.Data.NHibernate;
using StructureMap;
using IBootstrapper = ClubStarterKit.Core.IBootstrapper;

namespace ClubStarterKit.Web.Infrastructure.Application
{
    public class SessionBuilder : SessionBuilderBase
    {
        protected override string ConnectionString
        {
            get 
            {
                return ConfigurationManager.ConnectionStrings["csk"].ConnectionString;
            }
        }
        public override FluentNHibernate.Cfg.Db.IPersistenceConfigurer DataConfiguration
        {
            get
            {
                return FluentNHibernate.Cfg.Db.MsSqlConfiguration
                                              .MsSql2008
                                              .ConnectionString(ConnectionString);
            }
        }
    }

    public class SessionBuilderBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            ObjectFactory.Configure(x => x.ForRequestedType<SessionBuilderBase>()
                                          .TheDefaultIsConcreteType<SessionBuilder>());
        }
    }


}