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

using ClubStarterKit.Core;
using StructureMap;
using IBootstrapper = ClubStarterKit.Core.IBootstrapper;

namespace ClubStarterKit.Infrastructure
{
    /// <summary>
    /// Sets the default specification to be one in which always returns TRUE
    /// </summary>
    /// <remarks>To user a custom specification, inject a named instance into StructureMap.ObjectFactory</remarks>
    public class DefaultSpecificationBootstrapper : IBootstrapper
    {
        #region IBootstrapper Members

        public void Bootstrap()
        {
            ObjectFactory.Configure(conf => conf.ForRequestedType(typeof (ISpecification<>))
                                                .TheDefaultIsConcreteType(typeof (DefaultSpecification<>)));
        }

        #endregion
    }

    /// <summary>
    /// By default, everything is satisfied by the DefaultSpecification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultSpecification<T> : ISpecification<T>
    {
        #region ISpecification<T> Members

        public bool IsSatisfiedBy(T entity)
        {
            return true;
        }

        #endregion
    }
}