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

using System.Web.Caching;

namespace ClubStarterKit.Core.Cache
{
    /// <summary>
    /// Contorller for a cache implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheController<T>
    {
        /// <summary>
        /// Type of content the <see cref="ICacheController{T}"/> is controlling
        /// </summary>
        /// <remarks>Used for generation of the cache key</remarks>
        string ContentType { get; }

        /// <summary>
        /// Valuee in the cache 
        /// </summary>
        /// <remarks>
        /// If the value is not in the cache, it will be 
        /// generated and injected in the cache. This is
        /// only a side-effect and will not hinder the
        /// CachedValue behavior
        /// </remarks>
        T CachedValue { get; }

        /// <summary>
        /// Priority to keep the object in cache
        /// </summary>
        CacheItemPriority Priority { get; }

        /// <summary>
        /// Forces a refresh of the cache object
        /// </summary>
        void ForceRefresh();

        /// <summary>
        /// Removes the value from the cache
        /// </summary>
        void Expire();
    }
}