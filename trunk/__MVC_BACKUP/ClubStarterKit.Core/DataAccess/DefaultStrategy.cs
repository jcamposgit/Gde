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

using StructureMap;

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Default strategy for all repository strategies
    /// </summary>
    /// <remarks>
    ///     This is simply a placeholder strategy. 
    ///     There is no transformation that occurs 
    ///     on the repository
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class DefaultStrategy<T> : IDataStrategy<T>
        where T : IDataModel
    {
        public IRepository<T> Transform(IRepository<T> repository)
        {
            return repository;
        }
    }

    public class DefaultStrategyBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            ObjectFactory.Configure(config => config.ForRequestedType(typeof(IDataStrategy<>))
                                                    .TheDefaultIsConcreteType(typeof(DefaultStrategy<>)));
        }
    }
}
