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
using System.Web.Mvc;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Infrastructure.UI.Calendar;
using ClubStarterKit.Web.Infrastructure.Calendar;
using ClubStarterKit.Web.Infrastructure.Membership;

namespace ClubStarterKit.Web.Controllers
{
    public partial class EventsController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewData.Title("Events");
            SetRss();
            return View(Views.CalendarMonth, new CalendarService().FillMonthCalendar(DateTime.Now.Year, DateTime.Now.Month));
        }

        public virtual ActionResult Rss()
        {
            return new RssResult(new EventRssAction(Url).Execute());
        }

        protected void SetRss()
        {
            this.Rss(Website.Events.Rss(), "Events");
        }

        public virtual ActionResult Calendar(string id)
        {
            var cal = new LinkParser(id).Calendar;
            ViewData.Title("Events");
            SetRss();

            if(cal is CalendarMonth)
                return new AjaxDeterministicResult
                (
                    () => View(Views.Month, cal as CalendarMonth),
                    () => View(Views.CalendarMonth, cal as CalendarMonth)
                );

            return new AjaxDeterministicResult
            (
                () => View(Views.Week, cal as CalendarWeek),
                () => View(Views.CalendarWeek, cal as CalendarWeek)
            );
        }

        [ActionName("View")]
        public virtual ActionResult ViewEvent(string id)
        {
            var evnt = new EventRetrievalAction(id).Execute();
            ViewData.Title("View Event - " + evnt.Title);
            return View(Views.View, evnt);
        }

        #region Administration

        [Admin]
        public virtual ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            var result = new EventDeleteAction(id).Execute();
            return RedirectToAction(Website.Events.Index());
        }

        [Admin, ValidateInput(false), HttpPost]
        public virtual ActionResult Update(Event evnt)
        {
            if (evnt == null)
                throw new ArgumentNullException("evnt");

            // find event from form
            int locId = 0;
            bool parsed = int.TryParse(Request.Form["EventLocation.Location"], out locId);
            if (parsed)
                evnt.EventLocation = new LocationListAction().Execute().First(x => x.Id == locId);

            var result = new EventUpdateAction(evnt).Execute();
            return RedirectToAction(Website.Events.ViewEvent(evnt.Slug));
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ViewData.Title("Edit Event");

            return View(new EventRetrievalAction(id).Execute());
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New Event");
            return View(Views.Edit, new Event
            {
                StartTime = DateTimeOffset.Now,
                EndTime = DateTimeOffset.Now
            });
        }

        [Admin]
        public virtual ActionResult NewLocation()
        {
            ViewData.Title("New Location");
            return View(Views.EditLocation, new Location());
        }

        [Admin, HttpPost]
        public virtual ActionResult UpdateLocation(Location location)
        {
            new LocationUpdateAction(location).Execute();
            return RedirectToAction(Website.Events.Index());
        }

        #endregion
    }
}