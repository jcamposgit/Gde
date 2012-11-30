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

namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines if the day, month, and year are the
        /// same for the given DateTime's
        /// </summary>
        /// <param name="left"></param>
        /// <param name="test"><see cref="DateTimeOffset"/> to compare</param>
        /// <returns>True if the day, month and year are the same. False otherwise</returns>
        public static bool EqualDate(this DateTimeOffset left, DateTimeOffset test)
        {
            return left.Year == test.Year &&
                   left.Month == test.Month &&
                   left.Day == test.Day;
        }

        /// <summary>
        /// Gets the first day of the month in the given <see cref="DateTime"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>First day of the month</returns>
        public static DateTime ToFirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// Gets the last day of the month in the given <see cref="DateTime"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Last day of the month</returns>
        public static DateTime ToLastDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        /// <summary>
        /// Gets the first day of the week for the <see cref="DateTime"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>First day of the week</returns>
        public static DateTime ToFirstDayOfWeek(this DateTime dt)
        {
            return dt.AddDays(-((int) dt.DayOfWeek)).Date;
        }

        /// <summary>
        /// Gets the last day of the week for the <see cref="DateTimeOffset"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>First day of the week</returns>
        public static DateTime ToFirstDayOfWeek(this DateTimeOffset dt)
        {
            return dt.AddDays(-((int)dt.DayOfWeek)).Date;
        }

        /// <summary>
        /// Gets the last day of the week for the <see cref="DateTime"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Last day of the week</returns>
        public static DateTime ToLastDayOfWeek(this DateTime dt)
        {
            return dt.AddDays(6 - ((int) dt.DayOfWeek)).Date;
        }

        /// <summary>
        /// Gets a user-friendly <see cref="DateTime"/> string.
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to display</param>
        /// <param name="showTime">Boolean value if the time should be shown (default: false)</param>
        /// <returns>A string representation of the <see cref="DateTime"/></returns>
        public static string ToFriendlyDateString(this DateTime date, bool showTime = false)
        {
            // TODO: extend to show granular hour/min?

            string FormattedDate = "";
            if (date.Date == DateTime.Today)
                FormattedDate = "Today";
            else if (date.Date == DateTime.Today.AddDays(-1))
                FormattedDate = "Yesterday";
            else if (date.Date > DateTime.Today.AddDays(-6))
                // *** Show the Day of the week
                FormattedDate = date.ToString("dddd").ToString();
            else if (date.Date.Year == DateTime.Today.Year)
                FormattedDate = date.ToString("MMMM dd");
            else
                FormattedDate = date.ToString("MMMM dd, yyyy");

            // append the time portion to the output
            if (showTime)
                FormattedDate += " at " + date.ToString("t").ToLower();
            return FormattedDate;
        }

        /// <summary>
        /// Gets a user-friendly <see cref="DateTimeOffset"/> string.
        /// </summary>
        /// <param name="date"><see cref="DateTimeOffset"/> to display</param>
        /// <param name="showTime">Boolean value if the time should be shown (default: false)</param>
        /// <returns>A string representation of the <see cref="DateTimeOffset"/></returns>
        public static string ToFriendlyDateString(this DateTimeOffset date, bool showTime = false)
        {
            // TODO: extend to show granular hour/min?

            string FormattedDate = "";
            if (date.Date == DateTime.Today)
                FormattedDate = "Today";
            else if (date.Date == DateTime.Today.AddDays(-1))
                FormattedDate = "Yesterday";
            else if (date.Date > DateTime.Today.AddDays(-6))
                // *** Show the Day of the week
                FormattedDate = date.ToString("dddd").ToString();
            else
                FormattedDate = date.ToString("MMMM dd, yyyy");

            //append the time portion to the output
            if (showTime)
                FormattedDate += " at " + date.ToString("t").ToLower();
            return FormattedDate;
        }
    }
}