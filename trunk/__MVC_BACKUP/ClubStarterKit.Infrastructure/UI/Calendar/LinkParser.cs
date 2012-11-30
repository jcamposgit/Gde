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

namespace ClubStarterKit.Infrastructure.UI.Calendar
{
    public class LinkParser
    {
        public LinkParser(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public IEventImporter Calendar
        {
            get
            {
                if (string.IsNullOrEmpty(Id))
                    throw new Exception("ID must be specified");

                if (Id.StartsWith("month-"))
                    return ParseMonth(Id.Remove(0, "month-".Length));

                if (Id.StartsWith("week-"))
                    return ParseWeek(Id.Remove(0, "week-".Length));

                throw new Exception("Action not specified from string");
            }
        }

        private static CalendarMonth ParseMonth(string query)
        {
            if (query.Length != "9999-99".Length)
                throw new Exception("Identifier is not properly formed");

            int year = 0, month = 0;
            string working = "";
            query.Foreach(ch =>
                {
                    if (ch == '-')
                    {
                        year = int.Parse(working);
                        working = string.Empty;
                    }
                    else
                    {
                        if (!Char.IsNumber(ch))
                            throw new Exception("Characters must be numeric");

                        working += ch;
                    }
                });

            month = int.Parse(working);
            return new CalendarService().FillMonthCalendar(year, month);
        }

        private static CalendarWeek ParseWeek(string query)
        {
            if (query.Length != "9999-99-99".Length)
                throw new Exception("Identifier is not properly formed");

            int year = 0, month = 0, day = 0;
            bool yearWorking = true;
            string working = "";
            query.Foreach(ch =>
            {
                if (ch == '-')
                {
                    if (yearWorking)
                    {
                        year = int.Parse(working);
                        yearWorking = false;
                        working = string.Empty;
                    }
                    else
                    {
                        month = int.Parse(working);
                        working = string.Empty;
                    }
                }
                else
                {
                    if (!Char.IsNumber(ch))
                        throw new Exception("Characters must be numeric");

                    working += ch;
                }
            });

            day = int.Parse(working);
            return new CalendarService().FillWeekCalendar(year, month, day);
        }
    }
}
