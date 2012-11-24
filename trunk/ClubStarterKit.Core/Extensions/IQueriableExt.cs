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

using ClubStarterKit.Core;

namespace System.Linq
{
    public static class QueriableExt
    {
        /// <summary>
        /// <see cref="IPagedList{T}"/> for an <see cref="IQueryable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index">0-based page index</param>
        /// <param name="pageSize">Size of the page (default: 10)</param>
        /// <exception cref="ArgumentNullException">When IQueriable is null</exception>
        /// <exception cref="InvalidOperationException">When index is less than 0</exception>
        /// <returns><see cref="IPagedList{T}"/> from a query</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize = 10)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (index < 0)
                throw new InvalidOperationException(string.Format("Index {0} cannot be less than 0", index));

            return new PagedList<T>(source, index, pageSize);
        }
    }
}