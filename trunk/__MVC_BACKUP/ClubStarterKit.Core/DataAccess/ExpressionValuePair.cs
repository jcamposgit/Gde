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
using ClubStarterKit.Core.ExpressionHelpers;

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Transforms the given expression to a <see cref="IPropertyValuePair{TOutput}"/>
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TOutput">Property value type</typeparam>
    public class ExpressionValuePair<T, TOutput> : IPropertyValuePair<TOutput>
    {
        public ExpressionValuePair(Expression<Func<T, TOutput>> propExpr, TOutput value)
        {
            Property = propExpr.ToProperty();
            Value = value;
        }

        public string Property { get; private set; }

        public TOutput Value { get; private set; }
    }
}
