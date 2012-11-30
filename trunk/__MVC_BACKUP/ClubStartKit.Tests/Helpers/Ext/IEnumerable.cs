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

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace System.Collections
{
    public static class IEnumerableExten
    {
        public static void DoesntContainItemWithType<T>(this IEnumerable enumerable)
        {
            bool contained = false;

            foreach (object value in enumerable)
                if (value is T)
                    contained = true;

            Assert.False(contained, string.Format("List contains {0} type.", typeof (T).FullName));
        }

        public static void ContainItemWithType<T>(this IEnumerable enumerable)
        {
            bool contained = false;

            foreach (object value in enumerable)
                if (value is T)
                    contained = true;

            Assert.True(contained, string.Format("List doesn't contain {0} type.", typeof (T).FullName));
        }

        public static int NumberOfItemsWithType<T>(this IEnumerable enumerable)
        {
            int count = 0;

            foreach (object item in enumerable)
                if (item is T)
                    count++;

            return count;
        }

        public static void AssertInTypedOrder<T>(this IEnumerable<T> enumerable, params T[] order)
        {
            Assert.Equal(enumerable.Count(), order.Length);
            T[] enum_array = enumerable.ToArray();

            for (int i = 0; i < enum_array.Length; i++)
                Assert.IsAssignableFrom(order[i].GetType(), enum_array[i]);
        }
    }
}