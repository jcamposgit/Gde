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

using System.Text;
using System.Web.UI;

namespace ClubStarterKit.Infrastructure.View
{
    public static class BaseViewHelper
    {
        /// <summary>
        /// Removes whitespace from a given HtmlTextWriter and
        /// sets the new, cleaner version to the destination HtmlTextWriter
        /// </summary>
        /// <param name="sourceWriter">Source Html Writer</param>
        /// <param name="destinationWriter">Html writer to write cleaned HTML</param>
        public static void RemoveWhitespace(HtmlTextWriter sourceWriter, HtmlTextWriter destinationWriter)
        {
            string html = sourceWriter.InnerWriter.ToString();

            html = Constants.DuplicateSpaceFilter.Replace(html, " ");
            var sb = new StringBuilder(html);
            sb.Replace("\r\n", string.Empty);
            sb.Replace("\r", string.Empty);
            sb.Replace("\n", string.Empty);
            sb.Replace("\t", string.Empty);
            sb.Replace("//<![CDATA[", "//<![CDATA[\n");
            sb.Replace("//]]>", "//]]>\n");
            html = sb.ToString();
            destinationWriter.Write(html.Trim());
        }
    }
}