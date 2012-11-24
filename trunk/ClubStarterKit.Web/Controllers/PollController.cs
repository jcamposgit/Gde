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
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;
using ClubStarterKit.Web.Infrastructure.Poll;

namespace ClubStarterKit.Web.Controllers
{
    public partial class PollController : BaseController
    {
        [ActionName("View")]
        public virtual ActionResult Show(int id /*PAGE*/ = 1)
        {
            var poll = new PollListAction(id - 1).Execute();
            if (poll.Count == 0)
                return View(Views.NoPoll);

            if (PollUtils.CanVote(HttpContext, poll[0]))
                return View(Views.Vote, poll);
            return View(Views.ViewResults, poll);
        }

        [Authorize]
        public virtual ActionResult Vote(int poll, int answer)
        {
            // find user
            var user = UserRetrieval.GetUser(HttpContext);
            if (user.IsNothing)
                throw new Exception("Invalid user principal");
            var result = new PollVoteAction(poll, answer, user.Return()).Execute();

            if (!result)
                throw new Exception("Vote was not persisted to the data store.");
            return RedirectToAction(Website.Poll.Show(poll));
        }

        [Admin]
        public virtual ActionResult Add()
        {
            ViewData.Title("New Poll");
            return View(Views.Edit, new PollQuestion());
        }

        [Admin, HttpPost]
        public virtual ActionResult UpdateQuestion(PollQuestion question)
        {
            if (question == null)
                throw new ArgumentNullException("question");
            question.CreationDate = DateTimeOffset.Now;
            question.Owner = UserRetrieval.GetUser(HttpContext).Return();
            var result = new PollQuestionUpdateAction(question).Execute();

            if (!result)
                throw new Exception("Poll question was not added");

            return View(Views.AddAnswers, question);
        }

        [Admin, HttpPost]
        public virtual ActionResult AddAnswer(string answer, int poll, int pos)
        {
            var result = new PollAddAnswerAction(answer, poll, pos).Execute();

            return Json(new { success = result });
        }

        [Admin]
        public virtual ActionResult New()
        {
            // find user
            var user = UserRetrieval.GetUser(HttpContext);
            if (user.IsNothing)
                throw new Exception("Invalid user principal");

            var question = new PollQuestion
            {
                Question = " ",
                CreationDate = DateTimeOffset.Now,
                Owner = user.Return(),
                Hidden = false
            };
            
            ViewData.Title("New Poll");
            return View(Views.Edit, question);;
        }
    }
}