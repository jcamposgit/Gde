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

using System.Text.RegularExpressions;

namespace System
{
    public static class StringExt
    {
        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            string truncatedString = text;

            if (maxLength <= 0) 
                return truncatedString;

            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) 
                return truncatedString;

            if (text == null || text.Length <= maxLength) 
                return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;

            // finally, remove HTML tags
            return truncatedString.RemoveHtml();
        }

        /// <summary>
        /// Remove HTML tags from string
        /// </summary>
        public static string RemoveHtml(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        private static Regex Slug_InvalidChars = new Regex(@"[^a-z0-9\s-]", RegexOptions.Compiled);
        private static Regex Slug_MultipleSpaces = new Regex(@"\s+", RegexOptions.Compiled);
        private static Regex Slug_Spaces = new Regex(@"\s", RegexOptions.Compiled);
        
        /// <summary>
        /// Converts a string to a datastore-friendly slug
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string AsSlug(this string s)
        {
            string str = s.ToLower();
            str = Slug_InvalidChars.Replace(str, ""); // invalid chars
            str = Slug_MultipleSpaces.Replace(str, " ").Trim(); // convert multiple spaces into one space
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it
            str = Slug_MultipleSpaces.Replace(str, "-"); // hyphens
            return str;
        }
    }
}
