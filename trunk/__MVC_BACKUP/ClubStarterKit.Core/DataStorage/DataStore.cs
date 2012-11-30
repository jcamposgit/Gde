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
    /// Provifes an in-memory store for storing application / thread specific data.
    /// </summary>
    public abstract class DataStore
    {
        protected static readonly object AppStorageLock = new object();
        protected static readonly object LocalStorageLock = new object();
        protected static readonly object SessionStorageLock = new object();

        private static AppDataStore _appStorage;
        private static LocalDataStore _localStorage;
        private static HttpSessionDataStore _sessionStorage;

        public static bool IsWebApplication
        {
            get { return HttpContext.Current != null; }
        }

        public static DataStore Local
        {
            get
            {
                if (_localStorage == null)
                {
                    lock (LocalStorageLock)
                    {
                        if (_localStorage == null)
                            _localStorage = new LocalDataStore();
                    }
                }
                return _localStorage;
            }
        }

        public static DataStore Application
        {
            get
            {
                if (_appStorage == null)
                {
                    lock (AppStorageLock)
                    {
                        if (_appStorage == null)
                            _appStorage = new AppDataStore();
                    }
                }
                return _appStorage;
            }
        }

        public static DataStore Session
        {
            get
            {
                if (_sessionStorage == null)
                {
                    lock (SessionStorageLock)
                    {
                        if (_sessionStorage == null)
                            _sessionStorage = new HttpSessionDataStore();
                    }
                }
                return _sessionStorage;
            }
        }

        protected abstract bool UseLocking { get; }

        protected abstract object LockInstance { get; }

        public T Get<T>(object key)
        {
            if (UseLocking)
            {
                lock (LockInstance)
                    return (T) GetInternalHashtable()[key];
            }
            return (T) GetInternalHashtable()[key];
        }

        public void Set<T>(object key, T value)
        {
            if (UseLocking)
            {
                lock (LockInstance)
                    GetInternalHashtable()[key] = value;
            }
            else
                GetInternalHashtable()[key] = value;
        }

        public bool Contains(object key)
        {
            if (UseLocking)
            {
                lock (LockInstance)
                    return GetInternalHashtable().ContainsKey(key);
            }
            return GetInternalHashtable().ContainsKey(key);
        }

        public void Remove(object key)
        {
            if (UseLocking)
            {
                lock (LockInstance)
                    GetInternalHashtable().Remove(key);
            }
            else
                GetInternalHashtable().Remove(key);
        }

        public void Clear()
        {
            if (UseLocking)
            {
                lock (LockInstance)
                    GetInternalHashtable().Clear();
            }
            else
                GetInternalHashtable().Clear();
        }

        protected abstract Hashtable GetInternalHashtable();
    }
}