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

using System.Collections;
using System.Web;

namespace ClubStarterKit.Core.DataStorage
{
    /// <summary>
    /// Data storage for application data store
    /// </summary>
    public class AppDataStore : DataStore
    {
        private static Hashtable _internalStorage;

        protected override bool UseLocking
        {
            get { return true; }
        }

        protected override object LockInstance
        {
            get { return AppStorageLock; }
        }

        protected override Hashtable GetInternalHashtable()
        {
            if (IsWebApplication)
            {
                //This code is executing under a WebSite. Use the Application context to retrieve the hash table.
                var internalHashtable = HttpContext.Current.Application[typeof (AppDataStore).FullName] as Hashtable;
                if (internalHashtable == null)
                {
                    lock (AppStorageLock)
                    {
                        internalHashtable = HttpContext.Current.Application[typeof (AppDataStore).FullName] as Hashtable;
                        if (internalHashtable == null)
                            HttpContext.Current.Application[typeof (AppDataStore).FullName] =
                                internalHashtable = new Hashtable();
                    }
                }
                return internalHashtable;
            }

            //The code is running under a normal windows application. Use the static property.
            if (_internalStorage == null)
            {
                lock (SessionStorageLock)
                {
                    if (_internalStorage == null)
                        _internalStorage = new Hashtable();
                }
            }
            return _internalStorage;
        }
    }
}