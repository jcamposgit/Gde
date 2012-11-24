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
    /// <summary>
    /// Value container for storing 
    /// a) a value ("Just")
    /// b) lack of a value ("Nothing")
    /// </summary>
    /// <typeparam name="M">Type M of the Maybe Monad</typeparam>
    public sealed class Maybe<M>
    {
        public static Maybe<M> Nothing = new Maybe<M>();

        private readonly M _value;

        /// <summary>
        /// Value container for storing a nothing maybe
        /// </summary>
        private Maybe()
        {
            // assign the nothing flag
            IsNothing = true;
        }

        /// <summary>
        /// Value container for storing a just maybe
        /// </summary>
        /// <param name="value"></param>
        public Maybe(M value)
        {
            // assign the nothing flag when not null
            IsNothing = value == null;
            _value = value;
        }

        /// <summary>
        /// Determines the state of the 
        /// value in the container
        /// </summary>
        public bool IsNothing { get; private set; }

        #region Methods

        /// <summary>
        /// Executes an action based on whether this
        /// is nothing or whether it has a value
        /// </summary>
        /// <param name="hasValue">Action to perform when there is a value defined</param>
        /// <param name="noValue">Action to perform when there is not a value defined</param>
        public void Execute(Action hasValue, Action noValue)
        {
            if (IsNothing)
                noValue();
            else
                hasValue();
        }

        /// <summary>
        /// Executes an action based on whether this
        /// is nothing or whether it has a value
        /// </summary>
        /// <param name="hasValue">Action to perform when there is a value defined</param>
        /// <param name="noValue">Action to perform when there is not a value defined</param>
        public void Execute(Action<M> hasValue, Action noValue)
        {
            if (IsNothing)
                noValue();
            else
                hasValue(_value);
        }

        public Maybe<M> DefaultTo(M defaultValue)
        {
            if (IsNothing)
                return new Maybe<M>(defaultValue);

            return this;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Maybe<M>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var objMaybe = (Maybe<M>)obj;

            if (IsNothing && objMaybe.IsNothing)
                return true;

            if (IsNothing || objMaybe.IsNothing)
                return false;

            return _value.Equals(objMaybe.Return());
        }

        public override int GetHashCode()
        {
            return IsNothing ? typeof(M).GetHashCode() : _value.GetHashCode();
        }

        public M Return()
        {
            return _value;
        }

        #endregion

        #region Operators

        public static explicit operator M(Maybe<M> maybe)
        {
            if (maybe == null)
                throw new ArgumentNullException("maybe");

            return maybe._value;
        }

        public static implicit operator Maybe<M>(M value)
        {
            return new Maybe<M>(value);
        }

        public static explicit operator bool(Maybe<M> maybe)
        {
            return maybe.IsNothing;
        }

        public static bool operator !=(Maybe<M> left, Maybe<M> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Maybe<M> left, Maybe<M> right)
        {
            return left.Equals(right);
        }

        #endregion
    }
}