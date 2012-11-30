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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubStarterKit.Infrastructure.Cache
{
    /// <summary>
    /// Storage device for deleting all the keys in the cache store
    /// for a given type
    /// </summary>
    public static class CacheKeyStore
    {
        private static IDictionary<Type, IList<string>> _cacheKeys;
        
        internal static void Reset()
        {
            _cacheKeys = new Dictionary<Type, IList<string>>();
        }

        internal static IList<string> KeysFor(Type type)
        { 
            if(_cacheKeys == null || !_cacheKeys.ContainsKey(type))
                return new List<string>();
            return _cacheKeys[type];
        }

        internal static IList<string> KeysFor<T>()
        {
            return KeysFor(typeof(T));
        }

        internal static void AddKey<T>(string key)
        {
            AddKey(typeof(T), key);
        }

        internal static void AddKey(Type type, string key)
        {
            // create dictionary if null
            if(_cacheKeys == null)
                Reset();

            if (type.IsGenericType)
            {
                type.GetGenericArguments().Foreach(t => AddKey(t, key));
                return;
            }

            // if exists, add to keys
            // if not, add new list containing the key
            if(!_cacheKeys.ContainsKey(type))
                _cacheKeys.Add(type, new List<string> { key });
            else if (!_cacheKeys[type].Contains(key)) // add only if not contained
                _cacheKeys[type].Add(key);
        }

        /// <summary>
        /// Expires all keys for a given type
        /// </summary>
        /// <param name="with">Removes keys that contain this value</param>
        /// <param name="cache">CacheBase to remove key values form</param>
        /// <typeparam name="T"></typeparam>
        public static void ExpireWithType<T>(string with = "", CacheBase cache = null)
        {
            var type = typeof (T);

            // if no keys, nothing to do
            if (_cacheKeys == null || !_cacheKeys.ContainsKey(type))
                return;

            cache = cache ?? new CacheWrapper(HttpContext.Current.Cache);

            var keys = _cacheKeys[type].Where(k => k.ToLower().Contains(with.ToLower())).ToList();
            keys.Foreach(key =>
            {
                cache.Remove(key);
                _cacheKeys[type].Remove(key);
            });
        }
    }
}
