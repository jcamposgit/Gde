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

using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace ClubStarterKit.Tests.Helpers
{
    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext(ControllerBase controller)
            : this(controller, string.Empty, null, null, null, null, null, null)
        {
        }

        public FakeControllerContext(ControllerBase controller, HttpCookieCollection cookies)
            : this(controller, string.Empty, null, null, null, null, cookies, null)
        {
        }

        public FakeControllerContext(ControllerBase controller, SessionStateItemCollection sessionItems)
            : this(controller, string.Empty, null, null, null, null, null, sessionItems)
        {
        }


        public FakeControllerContext(ControllerBase controller, NameValueCollection formParams)
            : this(controller, string.Empty, null, null, formParams, null, null, null)
        {
        }


        public FakeControllerContext(ControllerBase controller, NameValueCollection formParams,
                                     NameValueCollection queryStringParams)
            : this(controller, string.Empty, null, null, formParams, queryStringParams, null, null)
        {
        }


        public FakeControllerContext(ControllerBase controller, string userName)
            : this(controller, string.Empty, userName, null, null, null, null, null)
        {
        }


        public FakeControllerContext(ControllerBase controller, string userName, string[] roles)
            : this(controller, string.Empty, userName, roles, null, null, null, null)
        {
        }


        public FakeControllerContext
            (
            ControllerBase controller,
            string relativeUrl,
            string userName,
            string[] roles,
            NameValueCollection formParams,
            NameValueCollection queryStringParams,
            HttpCookieCollection cookies,
            SessionStateItemCollection sessionItems
            )
            : base(
                new FakeHttpContext(relativeUrl, new FakePrincipal(new FakeIdentity(userName), roles), formParams,
                                    queryStringParams, cookies, sessionItems), new RouteData(), controller)
        {
        }
    }
}