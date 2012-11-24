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

namespace ClubStarterKit.Core.ExpressionHelpers
{
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Converts an expression to a property name
        /// </summary>
        /// <remarks>The expression must be a property expression</remarks>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TOut">Output of the property</typeparam>
        /// <param name="propertyExpression">Expression that, given an entity, returns a property value</param>
        /// <returns>String property name</returns>
        public static string ToProperty<T, TOut>(this Expression<Func<T, TOut>> propertyExpression)
        {
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(propertyExpression);
            return visitor.Path;
        }
    }
}