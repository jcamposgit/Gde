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

using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class WysiwygHelper
    {
        /// <summary>
        /// WYSIWYG editor for a form 
        /// </summary>
        /// <typeparam name="TModel">ViewModel type</typeparam>
        /// <param name="helper"></param>
        /// <param name="prop">Property to access the value for the editor</param>
        /// <param name="cssClasses">CSS classes for the editor</param>
        /// <returns>WYSIWYG TextArea</returns>
        public static MvcHtmlString WysiwygFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, string>> prop, string cssClasses = "required")
        { 
            var classes = "wysiwyg " + cssClasses;
            return helper.TextAreaFor<TModel, string>(prop, 20, 75, new { @class = classes });
        }

        /// <summary>
        /// WYSIWYG editor for a form with a given value
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">Name of the TextArea for the editor</param>
        /// <param name="value">Initial value for the editor</param>
        /// <param name="cssClasses">CSS classes for the editor</param>
        /// <returns>WYSIWYG TextArea</returns>
        public static MvcHtmlString Wysiwyg(this HtmlHelper helper, string name, string value="", string cssClasses = "required")
        {
            var classes = "wysiwyg " + cssClasses;
            return helper.TextArea(name, value, 20, 75, new { @class = classes });
        }
    }
}