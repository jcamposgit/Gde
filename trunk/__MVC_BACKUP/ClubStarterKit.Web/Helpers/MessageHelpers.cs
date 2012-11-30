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

using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class MessageHelpers
    {
        private const string messageFormat = @"<div id=""{0}""{2} class=""ui-widget""><div class=""{3} ui-corner-all"" style=""padding: 0pt 0.7em;"">
            <p><span style=""float: left; margin-right: 0.3em;"" class=""ui-icon {4}""></span><span{5}>{1}</span></p></div></div>";

        /// <summary>
        /// Inline alert-styled message 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="message">Message of the alert</param>
        /// <param name="show">Show the alert?</param>
        /// <param name="messageId">ID of the message container</param>
        /// <returns></returns>
        public static MvcHtmlString AlertMessage(this HtmlHelper html, string message, bool show, string messageId)
        {
            return MvcHtmlString.Create(string.Format(messageFormat, messageId, message, GetStyle(show), "ui-state-error", "ui-icon-alert", ""));
        }

        /// <summary>
        /// Inline alert-styled message 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="message">Message of the alert</param>
        /// <param name="modelValidationName">Name of the Model Validation key</param>
        /// <param name="messageId">ID of the message container</param>
        /// <returns></returns>
        public static MvcHtmlString AlertMessage(this HtmlHelper html, string message, string modelValidationName, string messageId)
        {
            return html.AlertMessage(message, ShowMessage(html, modelValidationName), messageId);
        }

        /// <summary>
        /// Inline info-styled message 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="message">Message of the information</param>
        /// <param name="show">Show the message?</param>
        /// <param name="messageId">ID of the message container</param>
        /// <param name="messageSpanId">ID of the message's inner container</param>
        /// <returns></returns>
        public static MvcHtmlString InfoMessage(this HtmlHelper html, string message, bool show, string messageId, string messageSpanId = "")
        {
            return MvcHtmlString.Create(string.Format(messageFormat, messageId, message, GetStyle(show), "ui-state-highlight", "ui-icon-info", GetSpanId(messageSpanId)));
        }

        /// <summary>
        /// Inline info-styled message 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="viewDataValue">ViewData key of the message</param>
        /// <param name="messageId">ID of the message container</param>
        /// <param name="messageSpanId">ID of the message's inner container</param>
        /// <returns></returns>
        public static MvcHtmlString InfoMessage(this HtmlHelper html, string viewDataValue, string messageId, string messageSpanId = "")
        {
            bool showMessage = html.ViewData[viewDataValue] != null;
            string message = showMessage ? html.ViewData[viewDataValue].ToString() : string.Empty;
            return html.InfoMessage(message, showMessage, messageId, messageSpanId);
        }

        private static string GetSpanId(string messageSpanId)
        {
            if (string.IsNullOrEmpty(messageSpanId))
                return string.Empty;
            return @" id=""" + messageSpanId + @"""";
        }

        private static bool ShowMessage(HtmlHelper html, string modelValidationName)
        {
            return html.ValidationMessage(modelValidationName).HasValue();
        }

        private static string GetStyle(bool show)
        {
            if (show)
                return string.Empty;
            return @" style=""display:none;""";
        }
    }
}