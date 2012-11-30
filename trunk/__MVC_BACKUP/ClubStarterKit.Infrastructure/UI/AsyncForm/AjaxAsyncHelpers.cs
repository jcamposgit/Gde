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
using System.Web.Mvc;

namespace ClubStarterKit.Infrastructure.UI.AsyncForm
{
    public static class AjaxAsyncHelpers
    {
        /// <summary>
        /// Asyncronous form (via custom jquery code)
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="url">Url of the form action</param>
        /// <param name="options">Ajax form options for the form</param>
        /// <returns>Disosable form</returns>
        /// <example>
        ///     using (Ajax.AsyncForm("/controller/action/id", new AsyncFormOptions(AsyncFormType.TargetUpdate, 
        ///             FormMethod.Post, "myform", targetUpdate: "block", preRequestFunction: "beforeFormSubmit"))
        ///     {
        ///         .....
        ///     }
        /// </example>
        public static IDisposable AsyncForm(this AjaxHelper ajax, string url, AsyncFormOptions options)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (options == null)
                throw new ArgumentNullException("options");
            
            return new AsyncFormBuilder(ajax, options, url);
        }

        /// <summary>
        /// Asyncronous form (via custom jquery code)
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="options">Ajax form options for the form</param>
        /// <param name="action">Controller action for the form's action</param>
        /// <param name="controller">Controller for the form's action</param>
        /// <param name="routeValues">Extra route values for the construction of the form's action</param>
        /// <returns>Disosable form</returns>
        /// <example>
        ///     using(Ajax.AsyncForm(new AsyncFormOptions(AsyncFormType.TargetUpdate, 
        ///                                               FormMethod.Post, "myform", targetUpdate: "block", 
        ///                                               preRequestFunction: "beforeFormSubmit")),
        ///                          "Action", "Controller", new { id = 4 }))
        ///     {
        ///         .....
        ///     }
        ///     
        /// </example>
        public static IDisposable AsyncForm(this AjaxHelper ajax, AsyncFormOptions options, string action, string controller, object routeValues)
        { 
            string url = new UrlHelper(ajax.ViewContext.RequestContext).Action(action, controller, routeValues);
            return ajax.AsyncForm(url, options);
        }
    }
}
