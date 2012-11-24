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
    /// CacheBase representation of the System.Web.Caching.Cache class
    /// </summary>
    public class CacheWrapper : CacheBase
    {
        private readonly Cache _cache;

        public CacheWrapper(Cache cache)
        {
            _cache = cache;
        }

        public override int Count
        {
            get { return _cache.Count; }
        }

        public override long EffectivePercentagePhysicalMemoryLimit
        {
            get { return _cache.EffectivePercentagePhysicalMemoryLimit; }
        }

        public override long EffectivePrivateBytesLimit
        {
            get { return _cache.EffectivePrivateBytesLimit; }
        }

        public override object this[string key]
        {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }

        public override object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                   TimeSpan slidingExpiration, CacheItemPriority priority,
                                   CacheItemRemovedCallback onRemoveCallback)
        {
            return _cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority,
                              onRemoveCallback);
        }

        public override object Get(string key)
        {
            return _cache.Get(key);
        }

        public override IDictionaryEnumerator GetEnumerator()
        {
            return _cache.GetEnumerator();
        }

        public override void Insert(string key, object value)
        {
            _cache.Insert(key, value);
        }

        public override void Insert(string key, object value, CacheDependency dependencies)
        {
            _cache.Insert(key, value, dependencies);
        }

        public override void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                    TimeSpan slidingExpiration)
        {
            _cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration);
        }

        public override void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                                    TimeSpan slidingExpiration, CacheItemPriority priority,
                                    CacheItemRemovedCallback onRemoveCallback)
        {
            _cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public override object Remove(string key)
        {
            return _cache.Remove(key);
        }
    }
}