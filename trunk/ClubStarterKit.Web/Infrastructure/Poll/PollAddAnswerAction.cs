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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Poll
{
    public class PollAddAnswerContext
    {
        public int PollId { get; set; }
        public string Answer { get; set; }
        public int Position { get; set; }
    }

    public class PollAddAnswerAction : IDataAction<PollAddAnswerContext, bool>
    {
        public PollAddAnswerAction(string answer, int poll, int pos)
        {
            Context = new PollAddAnswerContext
            {
                PollId = poll,
                Position = pos,
                Answer = answer
            };
        }

        public PollAddAnswerContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    var question = scope.UnitOfWork.RepositoryFor<PollQuestion>()
                                                   .FirstOrDefault(x => x.Id == Context.PollId);

                    var answer = new PollAnswer
                    {
                        Answer = Context.Answer,
                        Position = Context.Position,
                        Question = question
                    };

                    scope.UnitOfWork.RepositoryFor<PollAnswer>().Save(answer);
                    scope.Commit();
                }
                CacheKeyStore.ExpireWithType<PollAnswer>(Context.PollId.ToString());
                CacheKeyStore.ExpireWithType<PollVote>(Context.PollId.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
