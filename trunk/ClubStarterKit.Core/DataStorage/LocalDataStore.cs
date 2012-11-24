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
using System.Collections;
using System.Web;

namespace ClubStarterKit.Core.DataStorage
{
    /// <summary>
    /// Thread data storage
    /// </summary>
    public class LocalDataStore : DataStore
    {
        [ThreadStatic] private static Hashtable _internalStorage;

        protected override bool UseLocking
        {
            get { return false; }
        }

        protected override object LockInstance
        {
            get { return null; }
        }

        protected override Hashtable GetInternalHashtable()
        {
            if (IsWebApplication)
            {
                var internalStorage = HttpContext.Current.Items[typeof (LocalDataStore).FullName] as Hashtable;
                if (internalStorage == null)
                    HttpContext.Current.Items[typeof (LocalDataStore).FullName] = internalStorage = new Hashtable();
                return internalStorage;
            }
            return _internalStorage ?? (_internalStorage = new Hashtable());
        }
    }
}