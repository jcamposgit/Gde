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

using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Data.NHibernate.Conventions;
using ClubStarterKit.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace ClubStarterKit.Data.NHibernate
{
    public static class DomainUtilities
    {
        public static ISessionFactory GenerateSessionFactory(IPersistenceConfigurer db)
        {
            return GenerateFluent(db).BuildSessionFactory();
        }

        private static FluentConfiguration GenerateFluent(IPersistenceConfigurer db)
        {
            return Fluently.Configure()
                .Database(db)
                .Mappings(m =>
                        m.AutoMappings.Add(AutoMap.AssemblyOf<User>()
                                                  .Conventions
                                                  .AddFromAssemblyOf<RequiredConvention>()
                                                  .Where(t => t.GetInterface(typeof(IDataModel).Name) != null))
                    );
        }

        public static Configuration GenerateConfiguration(IPersistenceConfigurer db)
        {
            return GenerateFluent(db).BuildConfiguration();
        }
    }
}