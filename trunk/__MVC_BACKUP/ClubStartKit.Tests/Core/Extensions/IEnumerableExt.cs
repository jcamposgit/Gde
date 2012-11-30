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

namespace ClubStarterKit.Tests.Core.Extensions
{
    public class IEnumerableExt
    {
        [Fact]
        public void IEnumberbaleExt_ConcatAll_OnEmptyEnumerable_WithoutDeliminator_ReturnsEmptyString()
        {
            IEnumerable<string> strings = new List<string>();
            Assert.Equal(string.Empty, strings.ConcatAll());
        }

        [Fact]
        public void IEnumberbaleExt_ConcatAll_OnEmptyEnumerable_WithDeliminator_ReturnsEmptyString()
        {
            IEnumerable<string> strings = new List<string>();
            Assert.Equal(string.Empty, strings.ConcatAll());
        }

        [Fact]
        public void IEnumberbaleExt_ConcatAll_OnListOfAFewStrings_WithoutDeliminator_ReturnsConcatedStrings()
        {
            string s1 = "hello there", s2 = "hi there";
            IEnumerable<string> strings = new List<string> { s1, s2 };
            Assert.Equal(s1 + s2, strings.ConcatAll());
        }

        [Fact]
        public void IEnumberbaleExt_ConcatAll_OnListOfAFewStrings_WithSpaceDeliminator_ReturnsConcatedStringsWithSpaceBetween()
        {
            string s1 = "hello there", s2 = "hi there", delim = " ";
            IEnumerable<string> strings = new List<string> { s1, s2 };
            Assert.Equal(s1 + delim + s2, strings.ConcatAll(delim));
        }

        [Fact]
        public void IEnumberbaleExt_SplitWith_GivenEmptyString_ReturnsEnumerableWithNoElements()
        {
            Assert.Equal(0, string.Empty.SplitWith('-').Count());
        }

        [Fact]
        public void IEnumberbaleExt_SplitWith_GivenStringWithoutTheDeliminator_ReturnsJustOneElement()
        {
            var text = "this is a random test string without the delim";
            char delim = '-';

            Assert.Equal(1, text.SplitWith(delim).Count());
        }

        [Fact]
        public void IEnumberbaleExt_SplitWith_GivenStringWithoutTheDeliminator_FirstElementEqualToGivenText()
        {
            var text = "this is a random test string without the delim";
            char delim = '-';

            Assert.Equal(text, text.SplitWith(delim).First());
        }

        [Fact]
        public void IEnumberbaleExt_SplitWith_GivenStringWithDeliminatorInMultipleSpots_ReturnsEnumerableWithCountEqualToNumberOfParts()
        {
            var text = "a,b,c,d,e,f";
            char delim = ',';

            Assert.Equal(6, text.SplitWith(delim).Count());
        }

        [Fact]
        public void IEnumberbaleExt_SplitWith_GivenStringWithDeliminatorInMultipleSpots_AndAtEnd_ReturnsEnumerableWithCountEqualToNumberOfParts()
        {
            var text = "a,b,c,d,e,f,";
            char delim = ',';

            Assert.Equal(6, text.SplitWith(delim).Count());
        }
    }
}
