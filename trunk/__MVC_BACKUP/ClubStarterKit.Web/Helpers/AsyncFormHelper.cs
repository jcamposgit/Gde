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

using ClubStarterKit.Infrastructure.UI.AsyncForm;

namespace System.Web.Mvc.Ajax
{
    public static class AsyncFormHelper
    {
        /// <summary>
        /// Asynchronous form for a given action
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="actionResult">Controller/Action to be executed</param>
        /// <param name="options">Options for the type of form</param>
        /// <returns>Disposable form</returns>
        public static IDisposable AsyncForm(this AjaxHelper ajax, ActionResult actionResult, AsyncFormOptions options)
        {
            var url = new UrlHelper(ajax.ViewContext.RequestContext).Action(actionResult);
            return ajax.AsyncForm(url, options);
        }
    }
}