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

namespace ClubStarterKit.Web.Helpers
{
    public static class ConfirmationLinkHelper
    {
        /// <summary>
        /// Link used to confirm a user action
        /// </summary>
        /// <param name="html"></param>
        /// <param name="linkText">Text of the link</param>
        /// <param name="action">Controller/Action to use as the URL of the link</param>
        /// <param name="dialogTitle">Confirmation dialog title</param>
        /// <param name="dialogText">Confirmation dialog text</param>
        /// <returns>Link with confirmation metadata used to confirm a user action</returns>
        public static MvcHtmlString ConfirmationLink(this HtmlHelper html, string linkText, ActionResult action, string dialogTitle = "", string dialogText = "")
        {
            var rel = "{dialogTitle: " + (string.IsNullOrEmpty(dialogTitle) ? "null" : "'" + dialogTitle + "'") +
                ", dialogText: " + (string.IsNullOrEmpty(dialogText) ? "null" : "'" + dialogText + "'") + "}";
            return html.ActionLink(linkText, action, new { @class = "confirmation", rel = rel, id = Guid.NewGuid().ToString().Replace("-", "") });               
        }
    }
}