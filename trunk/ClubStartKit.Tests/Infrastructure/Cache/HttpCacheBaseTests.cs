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
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Cache
{
    public class HttpCacheBaseTests
    {
        [Fact]
        public void HttpCacheBase_InsertCalled_ForInitialGrab()
        {
            string strToInsert = "heloWolrDloD";

            CacheBase mockCache = new MockCache();
            HttpContextBase mockHttpContext = new Mock<HttpContextBase>().Object;
            var mockHttpCache = new MockHttpCacheBase(mockHttpContext, mockCache, "123");
            
            mockHttpCache.Value = strToInsert;
            Assert.Equal(strToInsert, mockHttpCache.CachedValue);
        }

        [Fact]
        public void HttpCacheBase_ForceRefresh_RefreshesValue()
        {
            string strToInsert = "heloWolrDloD";
            string strToInsert2 = strToInsert + "2";

            HttpContextBase mockHttpContext = new Mock<HttpContextBase>().Object;
            CacheBase mockCache = new MockCache();

            var mockHttpCache = new MockHttpCacheBase(mockHttpContext, mockCache, "1234");

            mockHttpCache.Value = strToInsert;
            Assert.Equal(strToInsert, mockHttpCache.CachedValue);

            mockHttpCache.Value = strToInsert2;
            mockHttpCache.ForceRefresh();
            Assert.Equal(strToInsert2, mockHttpCache.CachedValue);
        }

        #region Helper Classes

        #region Nested type: MockCache

        private class MockCache : CacheBase
        {
            private readonly IDictionary<string, object> dictionary = new Dictionary<string, object>();

            public override int Count
            {
                get { return dictionary.Count; }
            }

            public override long EffectivePercentagePhysicalMemoryLimit
            {
                get { throw new NotImplementedException(); }
            }

            public override long EffectivePrivateBytesLimit
            {
                get { throw new NotImplementedException(); }
            }

            public override object this[string key]
            {
                get
                {
                    try
                    {
                        return dictionary[key];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                set { dictionary[key] = value; }
            }

            public override object Add(string key, object value, CacheDependency dependencies,
                                       DateTime absoluteExpiration, TimeSpan slidingExpiration,
                                       CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
            {
                dictionary.Add(key, value);
                return value;
            }

            public override object Get(string key)
            {
                try
                {
                    return dictionary[key];
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public override IDictionaryEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public override void Insert(string key, object value)
            {
                dictionary.Add(key, value);
            }

            public override void Insert(string key, object value, CacheDependency dependencies)
            {
                dictionary.Add(key, value);
            }

            public override void Insert(string key, object value, CacheDependency dependencies,
                                        DateTime absoluteExpiration, TimeSpan slidingExpiration)
            {
                dictionary.Add(key, value);
            }

            public override void Insert(string key, object value, CacheDependency dependencies,
                                        DateTime absoluteExpiration, TimeSpan slidingExpiration,
                                        CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
            {
                dictionary.Add(key, value);
            }

            public override object Remove(string key)
            {
                return dictionary.Remove(key);
            }
        }

        #endregion

        #region Nested type: MockHttpCacheBase

        private class MockHttpCacheBase : HttpCacheBase<string>
        {
            public MockHttpCacheBase(HttpContextBase context, string appId)
                : base(context, new SimpleApplicationIdProvider(appId))
            {
            }

            public MockHttpCacheBase(HttpContextBase context, CacheBase cache, string appId)
                : base(context, cache, new SimpleApplicationIdProvider(appId))
            {
            }

            public string Value { get; set; }

            public override string ContentType
            {
                get { return "sample"; }
            }

            protected override string Grab()
            {
                return Value;
            }
        }

        #endregion

        #endregion
    }
}