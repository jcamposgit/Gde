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

using System.Collections;
using System.Web.Caching;

namespace System.Web
{
    /// <summary>
    /// Abstract wrapper for System.Web.Caching.Cache. This is 
    /// used for testing purposes and is used to decentralize the
    /// cache behavor. 
    /// </summary>
    public abstract class CacheBase : IEnumerable
    {
        // these methods come directly from System.Web.Caching.Cache

        public abstract int Count { get; }
        public abstract long EffectivePercentagePhysicalMemoryLimit { get; }
        public abstract long EffectivePrivateBytesLimit { get; }
        public abstract object this[string key] { get; set; }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public abstract object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                   TimeSpan slidingExpiration, CacheItemPriority priority,
                                   CacheItemRemovedCallback onRemoveCallback);

        public abstract object Get(string key);
        public abstract IDictionaryEnumerator GetEnumerator();
        public abstract void Insert(string key, object value);
        public abstract void Insert(string key, object value, CacheDependency dependencies);

        public abstract void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                    TimeSpan slidingExpiration);

        public abstract void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                    TimeSpan slidingExpiration, CacheItemPriority priority,
                                    CacheItemRemovedCallback onRemoveCallback);

        public abstract object Remove(string key);
    }
}