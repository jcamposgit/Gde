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

using System.Linq;
using System.Web;
using ClubStarterKit.Domain;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;

namespace ClubStarterKit.Web.Infrastructure.Poll
{
    public static class PollUtils
    {
        public static bool CanVote(HttpContextBase httpContext, PollQuestion question)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            var maybeUser = UserRetrieval.GetUser(httpContext);

            if (maybeUser.IsNothing)
                return false;

            var resultColl = from vote in new PollVotesRetrievalAction(question.Id).Execute()
                             where vote.User.Id == maybeUser.Return().Id
                             select vote;

            return resultColl.Count() == 0;
        }
    }
}