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

namespace ClubStarterKit.Infrastructure.Content.Javascript
{
    /// <summary>
    /// Class used to insert Javascript files in order 
    /// </summary>
    internal sealed class JavaScriptSorter
    {
        private readonly string[] files;
        internal JavaScriptSorter(string[] fileContent)
        {
            files = fileContent;
        }

        private string Sort()
        {
            var unsorted = new List<JavaScriptFile>();
            var sorted = new List<JavaScriptFile>();

            // delegate each file in either the sorted
            // or the unsorted list
            foreach (string file in files)
            {
                var f = new JavaScriptFile(file);

                // test if unsorted
                if (f.Order == JavaScriptFile.UnsortedOrderValue)
                    unsorted.Add(f);
                // add the rest of the files that are not excluded
                else if (f.Order != JavaScriptFile.ExcludeOrderValue)
                    sorted.Add(f);
            }

            // sort the sorted list by IComparable
            sorted.Sort(new Comparison<JavaScriptFile>((x, y) => x.CompareTo(y)));

            // ----------------------------------
            // build up the final list starting with the sorted files
            // ----------------------------------
            var finalList = new List<string>();

            // add the sorted values to string list
            sorted.Select(f => f.ToString()).Foreach(finalList.Add);

            // add the unsorted values to string list
            unsorted.Select(f => f.ToString()).Foreach(finalList.Add);

            return finalList.ConcatAll();
        }

        public override string ToString()
        {
            return Sort();
        }
    }
}
