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

using System.Text;

namespace System.Collections.Generic
{
    public static class IEnumerableExt
    {
        /// <summary>
        /// Iterates over a collection of items and performs an action
        /// on each item in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentNullException">Enumerable or action null</exception>
        /// <param name="action">Action to perform on each entity</param>
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T value in enumerable)
                action(value);
        }

        /// <summary>
        /// Iterates over a collection of items and performs an action
        /// on each item in a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException">Enumerable or action null</exception>
        /// <param name="items"></param>
        /// <param name="action">Action to perform on each entity</param>
        public static void Foreach<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (action == null) throw new ArgumentNullException("action");

            var index = 0;
            foreach (var item in items)
            {
                action(index, item);
                index++;
            }
        }

        /// <summary>
        /// Concatenates a string list into a single string value
        /// </summary>
        /// <param name="strings">Enumerable of Strings</param>
        /// <param name="deliminator">Value between each string</param>
        /// <returns>Concatenated result</returns>
        public static string ConcatAll(this IEnumerable<string> strings, string deliminator = "")
        {
            if (strings == null)
                return string.Empty;

            // build the new string from the existing strings in the array
            var builder = new StringBuilder();

            var strings_list = strings.ToNewList();
            // store the position and count to determine
            // if the delininator should be added
            int count = strings_list.Count, pos = 1;
            strings_list.Foreach(str =>
                {
                    builder.Append(str);
                    if (pos != count)
                        builder.Append(deliminator);

                    pos++;
                });

            return builder.ToString();
        }

        /// <summary>
        /// Splits a given string into an array from a deliminator
        /// </summary>
        /// <param name="s">string to split</param>
        /// <param name="deliminatorCharacter">Character to signal new string</param>
        /// <returns>Split string</returns>
        public static IEnumerable<string> SplitWith(this string s, char deliminatorCharacter)
        {
            List<string> strings = new List<string>();

            if (string.IsNullOrEmpty(s))
                return strings;

            string current = string.Empty;

            s.Foreach(ch =>
              {
                  if (ch == deliminatorCharacter)
                  {
                      // if the character is a deliminator, 
                      // add the current string to the list
                      // of strings, ignoring the deliminator,
                      // and resetting the current value
                      if (!string.IsNullOrEmpty(current))
                          strings.Add(current);

                      current = string.Empty;
                  }
                  else // otherwise add the character to the current string
                      current += ch;
              });

            // ensure we don't have anyting left to append in
            if (!current.Equals(string.Empty, StringComparison.OrdinalIgnoreCase))
                strings.Add(current);

            return strings;
        }

        /// <summary>
        /// Creates a new list based on a given IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">Current IEnumerable</param>
        /// <returns>List with value from enumberable</returns>
        public static IList<T> ToNewList<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable != null && enumerable is IList<T>)
                return enumerable as IList<T>;

            if (enumerable != null)
                return new List<T>(enumerable);

            return new List<T>();
        }

        /// <summary>
        /// Adds two IEnumerable isntances to a single List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Left <see cref="IEnumerable{T}"/></param>
        /// <param name="right"><see cref="IEnumerable{T}"/> to add</param>
        /// <returns>Composite List</returns>
        public static IList<T> AddRange<T>(this IEnumerable<T> left, IEnumerable<T> right)
        {
            if (left == null)
                return right.ToNewList();

            if (right == null)
                return left.ToNewList();

            var collection = left.ToNewList();

            right.Foreach(collection.Add);

            return collection;
        }
    }
}