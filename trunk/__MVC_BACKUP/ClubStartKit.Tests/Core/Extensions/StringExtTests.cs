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
using System.Linq;
using Xunit;

namespace ClubStarterKit.Tests.Core.Extensions
{
    public class StringExtTests
    {
        [Fact]
        public void StringExt_Truncate_ReturnsEmptyString_WhenGivenEmptyString()
        {
            Assert.Equal(string.Empty, string.Empty.Truncate(100));
        }

        [Fact]
        public void StringExt_Truncate_ReturnsSameString_WhenLengthLessThanGiven()
        {
            string testString = "this is a simple test string";
            Assert.Equal(testString, testString.Truncate(testString.Length + 1));
        }

        [Fact]
        public void StringExt_Truncate_ReturnsStringWithElipseAtEnd_WhenStringTruncated()
        {
            string testString = "this is a simple test string";
            string trunked = testString.Truncate(4);

            Assert.Equal("...", trunked.Substring(trunked.Length - 3, trunked.Length - 1));
        }

        [Fact]
        public void StringExt_Truncate_ReturnsStringWithoutHtml()
        {
            string truncated = "this is a <b>simple</b> test string".Truncate(27);

            Assert.False(truncated.Contains('<'));
        }
    }
}
