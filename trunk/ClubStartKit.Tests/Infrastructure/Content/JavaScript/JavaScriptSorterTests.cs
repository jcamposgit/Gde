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
using ClubStarterKit.Infrastructure.Content.Javascript;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Content
{
    public class JavaScriptSorterTests
    {
        [Fact]
        public void JavaScriptSorter_Sort_SortedValuesAppearBeforeUnsorted()
        {
            string[] fileContent = { "this is unsorted.", "// 1 \n this is sorted." };
            var target = new JavaScriptSorter(fileContent);
            string expected = new List<string> { fileContent[1], fileContent[0] }.ConcatAll();
            string actual = target.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void JavaScriptSorter_Sort_LowerSortValuesAppearBeforeHigherValues()
        {
            string[] fileContent = { "// 2 \n this is sorted2.", "// 1 \n this is sorted." };
            var target = new JavaScriptSorter(fileContent);
            string expected = new List<string> { fileContent[1], fileContent[0] }.ConcatAll();
            string actual = target.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void JavaScriptSorter_Sort_GivenNoJavaScriptFiles_ReturnsEmptyString()
        {
            string[] fileContent = new string[0];
            var target = new JavaScriptSorter(fileContent);
            Assert.Equal(string.Empty, target.ToString());
        }

        [Fact]
        public void JavaScriptSorter_Sort_DoesntIncludeExcludedFiles()
        {
            string[] fileContent = { "this is unsorted.", "// exclude alert something", "// 1 \n this is sorted." };
            var target = new JavaScriptSorter(fileContent);
            string expected = new List<string> { fileContent[2], fileContent[0] }.ConcatAll();
            string actual = target.ToString();
            Assert.Equal(expected, actual);
        }
    }
}
