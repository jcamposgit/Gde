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
    public static class SpecificationExt
    {
        /// <summary>
        /// Composite specification that will satisfy an entity if all of the specifications
        /// are satisfied by an entity
        /// </summary>
        /// <typeparam name="T">Specification type</typeparam>
        /// <param name="left">Source specification</param>
        /// <param name="others">Specifications to compose</param>
        /// <returns>Composite and specification</returns>
        public static ISpecification<T> And<T>(this ISpecification<T> left, params ISpecification<T>[] others)
        {
            if (others.Length == 0)
                return left;

            return new AndSpecification<T>(left, others);
        }

        /// <summary>
        /// Composite specification that will satisfy an entity if any of the specifications 
        /// are satisfied by an entity
        /// </summary>
        /// <typeparam name="T">Specification type</typeparam>
        /// <param name="left">Source specification</param>
        /// <param name="others">Specifications to compose</param>
        /// <returns>Composite or specification</returns>
        public static ISpecification<T> Or<T>(this ISpecification<T> left, params ISpecification<T>[] others)
        {
            if (others.Length == 0)
                return left;

            return new OrSpecification<T>(left, others);
        }

        /// <summary>
        /// Inverts specification to be the opposite
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="left">Specification to invert</param>
        /// <exception cref="ArgumentNullException">When specification is null</exception>
        /// <returns>Inverted specification</returns>
        public static ISpecification<T> Not<T>(this ISpecification<T> left)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            return new NotSpecification<T>(left);
        }
    }

    internal class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T>[] _right;

        public AndSpecification(ISpecification<T> left, params ISpecification<T>[] right)
        {
            _left = left;
            _right = right;
        }

        #region ISpecification<T> Members

        public bool IsSatisfiedBy(T entity)
        {
            if (!_left.IsSatisfiedBy(entity))
                return false;

            for (int i = 0; i < _right.Length; i++)
                if (!_right[i].IsSatisfiedBy(entity))
                    return false;

            return true;
        }

        #endregion
    }

    internal class OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T>[] _others;

        public OrSpecification(ISpecification<T> left, ISpecification<T>[] others)
        {
            _left = left;
            _others = others;
        }

        #region ISpecification<T> Members

        public bool IsSatisfiedBy(T entity)
        {
            if (_left.IsSatisfiedBy(entity))
                return true;

            for (int i = 0; i < _others.Length; i++)
                if (_others[i].IsSatisfiedBy(entity))
                    return true;

            return false;
        }

        #endregion
    }

    internal class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _spec;

        public NotSpecification(ISpecification<T> spec)
        {
            _spec = spec;
        }

        #region ISpecification<T> Members

        public bool IsSatisfiedBy(T entity)
        {
            return !_spec.IsSatisfiedBy(entity);
        }

        #endregion
    }
}