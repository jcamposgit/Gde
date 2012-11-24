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
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace ClubStarterKit.Tests.Helpers
{
    public class FakeHttpContext : HttpContextBase
    {
        private readonly HttpCookieCollection _cookies;
        private readonly NameValueCollection _formParams;
        private readonly FakePrincipal _principal;
        private readonly NameValueCollection _queryStringParams;
        private readonly string _relativeUrl;
        private readonly SessionStateItemCollection _sessionItems;


        public FakeHttpContext(string relativeUrl)
            : this(relativeUrl, null, null, null, null, null)
        {
        }

        public FakeHttpContext(string relativeUrl, FakePrincipal principal, NameValueCollection formParams,
                               NameValueCollection queryStringParams, HttpCookieCollection cookies,
                               SessionStateItemCollection sessionItems)
        {
            _relativeUrl = relativeUrl;
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _sessionItems = sessionItems;
        }

        public override HttpRequestBase Request
        {
            get { return new FakeHttpRequest(_relativeUrl, _formParams, _queryStringParams, _cookies); }
        }

        public override IPrincipal User
        {
            get { return _principal; }
            set { throw new NotImplementedException(); }
        }

        public override HttpSessionStateBase Session
        {
            get { return new FakeHttpSessionState(_sessionItems); }
        }
    }
}