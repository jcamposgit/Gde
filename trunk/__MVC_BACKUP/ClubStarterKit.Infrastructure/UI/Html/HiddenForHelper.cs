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

namespace System.Web.Mvc.Html
{
    public static class HiddenForHelper
    {
        private const string HiddenInputFormat = @"<input type=""hidden"" name=""{0}.{1}"" value=""{2}"" />";
        public static MvcHtmlString HiddenDataValue(this HtmlHelper html, string name, object val)
        {
            Type t = val.GetType();

            var hiddenValues = from prop in t.GetProperties()
                               let value = prop.GetValue(val, null).ToString()
                               where value != prop.PropertyType.ToString()
                               select string.Format(HiddenInputFormat, name, prop.Name, value);

            return MvcHtmlString.Create(hiddenValues.ConcatAll());
        }
    }
}
