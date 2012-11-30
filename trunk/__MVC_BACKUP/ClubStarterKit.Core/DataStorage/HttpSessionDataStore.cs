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
using System.Diagnostics.Contracts;
using System.Web;

namespace ClubStarterKit.Core.DataStorage
{
    /// <summary>
    /// HTTP Session data store
    /// </summary>
    public class HttpSessionDataStore : DataStore
    {
        protected override bool UseLocking
        {
            get { return true; }
        }

        protected override object LockInstance
        {
            get { return SessionStorageLock; }
        }

        protected override Hashtable GetInternalHashtable()
        {
            Contract.Requires(IsWebApplication);
            Contract.Requires(HttpContext.Current.Session != null);

            var internalStorage = HttpContext.Current.Session[typeof (HttpSessionDataStore).FullName] as Hashtable;
            if (internalStorage == null)
            {
                lock (SessionStorageLock)
                {
                    internalStorage = HttpContext.Current.Session[typeof (HttpSessionDataStore).FullName] as Hashtable;
                    if (internalStorage == null)
                        HttpContext.Current.Session[typeof (HttpSessionDataStore).FullName] =
                            internalStorage = new Hashtable();
                }
            }
            return internalStorage;
        }
    }
}