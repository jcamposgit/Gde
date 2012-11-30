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
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class DisplayForListExt
    {
        internal static string GenerateViewDataKey(string seed)
        {
            return seed + Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Display model for an IEnumerable view model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayForList<T>(this HtmlHelper<IEnumerable<T>> html)
        {
            StringBuilder returnHtml = new StringBuilder();
            var viewData = html.ViewData;
            string seed = typeof(T).FullName;

            viewData.Model.Foreach(val =>
                {
                    string key = GenerateViewDataKey(seed);
                    viewData[key] = val;
                    string valHtml = html.Display(key).ToHtmlString();
                    returnHtml.Append(valHtml);
                });

            return MvcHtmlString.Create(returnHtml.ToString());
        }
    }
}