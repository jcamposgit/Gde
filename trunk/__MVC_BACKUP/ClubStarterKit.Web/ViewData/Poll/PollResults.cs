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
using System.Collections.Generic;
using System.Linq;
using ClubStarterKit.Domain;
using ClubStarterKit.Web.Infrastructure.Poll;

namespace ClubStarterKit.Web.ViewData.Poll
{
    public class PollResults
    {
        public PollResults(PollQuestion question)
        {
            var answers = new PollAnswersRetrieval(question.Id).Execute();
            var votes = new PollVotesRetrievalAction(question.Id).Execute();
            Question = question;
            TotalVotes = votes.Count();
            Results = answers.Select(a => new PollResult(a, this, votes)).ToList();
        }

        public PollQuestion Question { get; private set; }
        public IEnumerable<PollResult> Results { get; private set; }
        public int TotalVotes { get; set; }
    }

    public class PollResult
    {
        private readonly PollResults _results;
        public PollResult(PollAnswer answer, PollResults results, IEnumerable<PollVote> votes)
        {
            Answer = answer;
            _results = results;
            _votes = votes.Where(x => x.Answer.Id == answer.Id).Count();
        }

        public PollAnswer Answer { get; set; }

        private int _votes = -1;
        public int Votes 
        {
            get
            {
                return _votes;
            }
        }

        public int Percentage
        {
            get
            {
                if (_results.TotalVotes == 0)
                    return 0;
                return (int)Math.Round(Votes / (_results.TotalVotes * 1.0) * 100.0);
            }
        }
    }
}