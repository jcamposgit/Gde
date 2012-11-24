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

namespace ClubStarterKit.Infrastructure.ActionResults
{
    /// <summary>
    /// Result that triggers one of two actions, determined by
    /// the request
    /// </summary>
    public class AjaxDeterministicResult : ActionResult
    {
        /// <summary>
        /// Binds the result determined by whether the request is an AJAX request
        /// </summary>
        /// <param name="ajaxResult">
        /// Function that returns an action result to 
        /// execute if request is an ajax request
        /// </param>
        /// <param name="nonAjaxResult">
        /// Function that returns an action result to 
        /// execute if request is not an ajax request
        /// </param>
        public AjaxDeterministicResult(Func<ActionResult> ajaxResult, Func<ActionResult> nonAjaxResult)
        {
            if (ajaxResult == null)
                throw new ArgumentNullException("ajaxResult");

            if (nonAjaxResult == null)
                throw new ArgumentNullException("nonAjaxResult");

            AjaxResult = ajaxResult;
            NonAjaxResult = nonAjaxResult;
        }

        /// <summary>
        /// Binds the result determined by whether the result is an AJAX request
        /// </summary>
        /// <param name="ajaxResult">
        /// ActionResult that will be executed if 
        /// the request is an ajax request
        /// </param>
        /// <param name="nonAjaxResult">
        /// ActionResult that will be executed if 
        /// the request is not an ajax request
        /// </param>
        public AjaxDeterministicResult(ActionResult ajaxResult, ActionResult nonAjaxResult)
            :this(() => ajaxResult, () => nonAjaxResult)
        { }

        public Func<ActionResult> AjaxResult { get; set; }

        public Func<ActionResult> NonAjaxResult { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                if (AjaxResult == null)
                    throw new NullReferenceException("AjaxResult cannot be null");
                AjaxResult().ExecuteResult(context);
            }
            else
            {
                if (NonAjaxResult == null)
                    throw new NullReferenceException("NonAjaxResult cannot be null");
                NonAjaxResult().ExecuteResult(context);
            }
        }
    }
}
