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

namespace ClubStarterKit.Core
{
    public static class MaybeExt
    {
        public static Maybe<U> SelectMany<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> convert)
        {
            if (maybe.IsNothing)
                return Maybe<U>.Nothing;
            return convert(maybe.Return());
        }

        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> maybe, Func<T, Maybe<U>> converter, Func<T, U, V> selector)
        {
            return maybe.SelectMany(x => converter(x).SelectMany(y => selector(x, y).AsMaybe()));
        } 

        /// <summary>
        /// Binds a value to the Maybe abstract data type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Value to bind</param>
        /// <returns>
        ///     If value is null, a "Nothing" maybe will be returned. 
        ///     Otherwise a "Just" maybe will be returned.
        /// </returns>
        public static Maybe<T> AsMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }
    }
}
