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
using System.Web;
using System.Web.Caching;
using ClubStarterKit.Core.Cache;
using ClubStarterKit.Infrastructure.Application;
using StructureMap;

namespace ClubStarterKit.Infrastructure.Cache
{
    public abstract class HttpSessionCacheBase<T> : ICacheController<T>
        where T : class
    {
        public HttpSessionCacheBase(HttpContextBase httpContext = null)
        {
            ApplicationIdProvider = ObjectFactory.GetInstance<IApplicationIdProvider>();
            HttpContext = httpContext ?? new HttpContextWrapper(System.Web.HttpContext.Current);
        }

        public IApplicationIdProvider ApplicationIdProvider { get; private set; }
        public HttpContextBase HttpContext { get; set; }

        #region Abstract

        /// <summary>
        /// Absolute expiration time from the point of 
        /// construction of the cache item
        /// </summary>
        /// <remarks>Default value is 1 day</remarks>
        public virtual TimeSpan Expiration
        {
            get { return TimeSpan.FromDays(1); }
        }

        /// <summary>
        /// Type of content to cache. This is used primarily for naming
        /// </summary>
        public abstract string ContentType { get; }

        /// <summary>
        /// Cache item priority
        /// </summary>
        public virtual CacheItemPriority Priority
        {
            get { return CacheItemPriority.Normal; }
        }

        protected abstract T Grab();

        #endregion

        #region HttpCache Accessors

        /// <summary>
        /// Inserts the value from the Grab method into the cache
        /// </summary>
        protected void Insert()
        {
            T val = Grab();
            Expire();
            if (val == null)
                return;

            HttpContext.Session.Add(ToString(), val);
        }

        #endregion

        #region ICacheController<T> Members

        /// <summary>
        /// Value in the cache
        /// </summary>
        public T CachedValue
        {
            get
            {
                if (HttpContext.Session[ToString()] == null)
                    Insert();
                return (T)HttpContext.Session[ToString()];
            }
        }

        /// <summary>
        /// Forces a refresh of this cache object
        /// </summary>
        public void ForceRefresh()
        {
            Insert();
        }

        /// <summary>
        /// Removes the item from the cache
        /// </summary>
        public void Expire()
        {
            HttpContext.Session.Remove(ToString());
        }

        #endregion

        public override string ToString()
        {
            return ContentType + "_" + ApplicationIdProvider.ApplicationId;
        }
    }
}
