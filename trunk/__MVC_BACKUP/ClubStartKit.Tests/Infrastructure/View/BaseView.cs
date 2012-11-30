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

using System.IO;
using System.Text;
using System.Web.UI;
using ClubStarterKit.Infrastructure.View;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.View
{
    public class BaseViewHelperTests
    {
        private string ExecuteRemoveWhitespaceHelper(string value)
        {
            var destinationWriter = new HtmlTextWriter(new StringWriter());
            var sourceWrite = new HtmlTextWriter(new StringWriter(new StringBuilder(value)));
            BaseViewHelper.RemoveWhitespace(sourceWrite, destinationWriter);
            return destinationWriter.InnerWriter.ToString();
        }

        [Fact]
        public void BaseViewHelper_RemoveWhitespace_RemovesTabCharacters()
        {
            string val = ExecuteRemoveWhitespaceHelper("samplehtml\t");

            Assert.Equal("samplehtml", val);
        }

        [Fact]
        public void BaseViewHelper_RemoveWhitespace_RemovesNewlineCharacters()
        {
            string val = ExecuteRemoveWhitespaceHelper("samplehtml\n");

            Assert.Equal("samplehtml", val);
        }

        [Fact]
        public void BaseViewHelper_RemoveWhitespace_RemovesCarriageReturnCharacters()
        {
            string val = ExecuteRemoveWhitespaceHelper("samplehtml\r");

            Assert.Equal("samplehtml", val);
        }

        [Fact]
        public void BaseViewHelper_RemoveWhitespace_ExcessInbetweenTagWhitespaceToOneSpace()
        {
            string expected = "<html> <head> <title>Sample Title</title> </head> </html>";
            string test = "<html>    <head>    <title>Sample Title</title>    </head>       </html>";
            Assert.Equal(expected, ExecuteRemoveWhitespaceHelper(test));
        }
    }
}