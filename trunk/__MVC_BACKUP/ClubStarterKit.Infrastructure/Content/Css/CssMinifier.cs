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
using System.Text.RegularExpressions;

namespace ClubStarterKit.Infrastructure.Content.Css
{
    /// <summary>
    /// Helper class that minifies given CSS string
    /// </summary>
    internal static class CssMinifier
    {
        internal static string Minify(string css)
        {
            string body = Regex.Replace(css, "/\\*.+?\\*/", "", RegexOptions.Singleline);
            body = body.Replace("  ", string.Empty);
            body = body.Replace(Environment.NewLine + Environment.NewLine + Environment.NewLine, string.Empty);
            body = body.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            body = body.Replace(Environment.NewLine, string.Empty);
            body = body.Replace('\n'.ToString(), string.Empty);
            body = body.Replace("\\t", string.Empty);
            body = body.Replace(" {", "{");
            body = body.Replace(" :", ":");
            body = body.Replace(": ", ":");
            body = body.Replace(", ", ",");
            body = body.Replace("; ", ";");
            body = body.Replace(";}", "}");
            body = Regex.Replace(body, "/\\*[^\\*]*\\*+([^/\\*]*\\*+)*/", "$1");
            body = Regex.Replace(body, "(?<=[>])\\s{2,}(?=[<])|(?<=[>])\\s{2,}(?=&nbsp;)|(?<=&ndsp;)\\s{2,}(?=[<])",
                                 string.Empty);
            return body;
        }
    }
}