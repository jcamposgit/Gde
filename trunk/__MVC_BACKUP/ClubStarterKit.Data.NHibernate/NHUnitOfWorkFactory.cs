#region license

//Copyright 2008 Ritesh Rao 

//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using ClubStarterKit.Core.DataAccess;
using StructureMap;

namespace ClubStarterKit.Data.NHibernate
{
    /// <summary>
    /// Implements the <see cref="IUnitOfWorkFactory"/> interface to provide an implementation of a factory
    /// that creates <see cref="NHUnitOfWork"/> instances.
    /// </summary>
    public class NHUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private SessionBuilderBase _builder;
        protected SessionBuilderBase Builder
        {
            get
            {
                if (_builder == null)
                    _builder = ObjectFactory.GetInstance<SessionBuilderBase>();

                return _builder;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork Create()
        {
            if (Builder == null)
                throw new InvalidOperationException(
                    "A ISession provider has not been specified. Please specify set a " +
                    "provider before creating NHUnitOfWork instances");
            return new NHUnitOfWork(Builder.GetSession());
        }
    }
}