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
using System.Web;
using System.Web.Mvc;

namespace ClubStarterKit.Infrastructure.UI.AsyncForm
{
    /// <summary>
    /// Form builder for the "async form"
    /// </summary>
    public class AsyncFormBuilder : IDisposable
    {
        private HttpResponseBase _httpResponse;
        private AsyncFormOptions _options;
        private string _url;

        public AsyncFormBuilder(AjaxHelper ajax, AsyncFormOptions options, string url)
        {
            _httpResponse = ajax.ViewContext.HttpContext.Response;
            _options = options;
            _url = url;
            RenderStartTag();
        }

        private void RenderStartTag()
        {
            string formTag = string.Format(@"<form id=""{0}"" action=""{1}"" method=""{2}"" rel=""{3}"" class=""async"">",
                                            _options.FormId, _url, _options.FormMethod.ToString().ToLower(), _options.ToString());
            _httpResponse.Write(formTag);
        }

        #region IDisposable Members
        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                // output end tag
                _httpResponse.Write("</form>");
            }
        }

        public void EndForm()
        {
            Dispose(true);
        }

        #endregion
    }
}
