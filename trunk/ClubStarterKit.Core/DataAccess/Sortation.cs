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
using System.Linq.Expressions;

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Sortation expression placeholder for a given T to property TKey
    /// </summary>
    /// <typeparam name="T">Sortation object value type</typeparam>
    /// <typeparam name="TKey">Property type</typeparam>
    public class Sortation<T, TKey> : ISortation<T, TKey>
    {
        public Sortation(Expression<Func<T, TKey>> sortation, bool ascending = true)
        {
            SortBy = sortation;
            Ascending = ascending;
        }

        public Expression<Func<T, TKey>> SortBy { get; private set; }

        public bool Ascending { get; private set; }
    }

    /// <summary>
    /// Sortation expression for given T to a property of any object
    /// </summary>
    /// <typeparam name="T">Sortation object value type</typeparam>
    public class Sortation<T> : Sortation<T, object>
    {
        public Sortation(Expression<Func<T, object>> sortation, bool ascending = true)
            : base(sortation, ascending)
        { }
    }
}
