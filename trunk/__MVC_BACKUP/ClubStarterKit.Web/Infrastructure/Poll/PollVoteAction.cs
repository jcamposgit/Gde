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
using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Poll
{
    public class PollVoteAction : IDataAction<PollVoteAction.PollVoteContext, bool>
    {
        public class PollVoteContext
        {
            public int Poll { get; set; }
            public int Answer { get; set; }
            public User User { get; set; }
        }

        public PollVoteAction(int poll, int answer, User user)
        {
            Context = new PollVoteContext
            {
                User = user,
                Answer = answer,
                Poll = poll
            };
        }

        public PollVoteContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    var answer = scope.UnitOfWork.RepositoryFor<PollAnswer>()
                                                 .First(x=>x.Id == Context.Answer);
                    var vote = new PollVote
                    {
                        Answer = answer,
                        DateVoted = DateTimeOffset.Now,
                        User = Context.User
                    };
                    scope.UnitOfWork.RepositoryFor<PollVote>().Save(vote);
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<PollVote>(Context.Poll.ToString());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}