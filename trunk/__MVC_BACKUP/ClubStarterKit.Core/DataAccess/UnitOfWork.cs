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
using ClubStarterKit.Core.DataStorage;
using StructureMap;

namespace ClubStarterKit.Core.DataAccess
{
    public static class UnitOfWork
    {
        private const string UnitOfWorkSessionKey = "UnitOfWorkCurrent";

        public static bool InSession
        {
            get { return DataStore.Local.Contains(UnitOfWorkSessionKey); }
        }

        /// <summary>
        /// Current unit of work
        /// </summary>
        public static IUnitOfWork Current
        {
            get
            {
                if (!InSession)
                    return null;

                return DataStore.Local.Get<IUnitOfWork>(UnitOfWorkSessionKey);
            }
            set
            {
                if (value == null)
                    DataStore.Local.Remove(UnitOfWorkSessionKey);
                else
                    DataStore.Local.Set(UnitOfWorkSessionKey, value);
            }
        }

        /// <summary>
        /// Starts a new unit of work or not
        /// </summary>
        /// <returns></returns>
        public static IUnitOfWork Start()
        {
            // create new unit of work if it hasn't started
            if (!InSession)
                Current = ObjectFactory.GetInstance<IUnitOfWorkFactory>().Create();
            return Current;
        }

        /// <summary>
        /// Finishes the current unit of work
        /// </summary>
        /// <exception cref="InvalidOperationException">When there there is not a current unit of work to finish</exception>
        /// <param name="flushTransaction"></param>
        public static void Finish(bool flushTransaction)
        {
            if (Current == null)
                throw new InvalidOperationException("No unit of work to finish.");

            if (flushTransaction)
                Current.TransactionalFlush();

            Current.Dispose();
            Current = null;
        }
    }
}