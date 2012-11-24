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
using System.Web;
using ClubStarterKit.Infrastructure.Cache;
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Cache
{
    public class CacheKeyStoreTests
    {
        public CacheKeyStoreTests()
        {
            CacheKeyStore.Reset();
        }

        [Fact]
        public void CacheKeyStore_AddKey_Generic_WithTypeNotAdd_AddsSingleKey()
        {
            var key = "sample_key";
            CacheKeyStore.AddKey<string>(key);
            

            var keys_for_string = CacheKeyStore.KeysFor<string>();
            Assert.Equal(1, keys_for_string.Count);
            Assert.Equal(key, keys_for_string[0]);
        }

        [Fact]
        public void CacheKeyStore_AddKey_NonGeneric_WithTypeNotAdd_AddsSingleKey()
        {
            var key = "sample_key";
            CacheKeyStore.AddKey(typeof(string), key);

            var keys_for_string = CacheKeyStore.KeysFor<string>();
            Assert.Equal(1, keys_for_string.Count);
            Assert.Equal(key, keys_for_string[0]);
        }

        [Fact]
        public void CacheKeyStore_AddKey_GivenGenericType_AddsKeysForEachGenericTypeArgument()
        {
            var generic_type = typeof(Dictionary<DateTime, DateTimeOffset>);
            var key = "mykey";
            CacheKeyStore.AddKey(generic_type, key);

            Assert.Equal(1, CacheKeyStore.KeysFor<DateTime>().Count);
            Assert.Equal(1, CacheKeyStore.KeysFor<DateTimeOffset>().Count);
        }

        [Fact]
        public void CacheKeyStore_AddKey_DoesntAddTheSameKeyMoreThanOnce()
        {
            var key = "mykey";
            CacheKeyStore.AddKey<DateTime>(key);
            CacheKeyStore.AddKey<DateTime>(key);

            Assert.Equal(1, CacheKeyStore.KeysFor<DateTime>().Count);
        }

        [Fact]
        public void CacheKeyStore_ExpireWithType_DoesntThrowException_WhenTypeIsNotInKeyStore()
        {
            Assert.DoesNotThrow(() => CacheKeyStore.ExpireWithType<string>());
        }

        [Fact]
        public void CacheKeyStore_ExpireWithType_WithoutContraint_RemovesAllKeys()
        {
            var mock_cache = new Mock<CacheBase>();
            var removed_keys = new List<string>();
            mock_cache.Setup(x => x.Remove(It.IsAny<string>()))
                      .Callback((string key) => removed_keys.Add(key));

            string key1 = "mykey1", key2 = "mykey2";
            CacheKeyStore.AddKey<string>(key1);
            CacheKeyStore.AddKey<string>(key2);

            CacheKeyStore.ExpireWithType<string>(cache: mock_cache.Object);

            Assert.True(removed_keys.Contains(key1));
            Assert.True(removed_keys.Contains(key2));
        }

        [Fact]
        public void CacheKeyStore_ExpireWithType_WithContraint_RemovesKeysThatHaveTheConstraint()
        {
            var mock_cache = new Mock<CacheBase>();
            var removed_keys = new List<string>();
            mock_cache.Setup(x => x.Remove(It.IsAny<string>()))
                      .Callback((string key) => removed_keys.Add(key));

            string key1 = "mykey1", key2 = "mykey2";
            CacheKeyStore.AddKey<string>(key1);
            CacheKeyStore.AddKey<string>(key2);

            CacheKeyStore.ExpireWithType<string>("1", mock_cache.Object);

            Assert.True(removed_keys.Contains(key1));
            Assert.False(removed_keys.Contains(key2));
        }
    }
}

