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
using System.Web.Routing;

namespace ClubStarterKit.Infrastructure.Routing
{
    public static class Helper
    {
        /// <summary>
        /// Remove route values from the given collection that are in the list of values to remove
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="values">Values to remove from route value dicitonary</param>
        /// <remarks>"Current" is often added to a new RouteValueDictionary. It is removed in this method</remarks>
        /// <returns>New <see cref="RouteValueDictionary"/> without specified route values</returns>
        public static RouteValueDictionary ExtractValues(this RouteValueDictionary collection, string[] values)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (values == null)
                throw new ArgumentNullException("values");

            var lowercaseValues = values.Select(x=>x.ToLower());
            var dictionary = new RouteValueDictionary();

            foreach (var pair in collection)
                if (!lowercaseValues.Contains(pair.Key.ToLower()))
                    dictionary.Add(pair.Key, pair.Value);

            // sometimes the dictionary adds the "Current" value, that has no
            // purpose... so remove it.
            if (dictionary.ContainsKey("Current"))
                dictionary.Remove("Current");
            return dictionary;
        }
    }
}
