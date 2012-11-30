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
    /// <summary>
    /// Expression visitor that converts field values to
    /// constant values
    /// </summary>
    public class FieldVisitor : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression methodExp)
        {
            if (methodExp != null && methodExp.Expression != null &&
                methodExp.Expression.NodeType == ExpressionType.Constant)
            {
                var val = GetExpressionValue(methodExp);
                return Expression.Constant(val, val.GetType());
            }

            return base.VisitMember(methodExp);
        }

        private static object GetExpressionValue(Expression expression)
        {
            var constExpr = expression as ConstantExpression;
            if (constExpr != null)
                return constExpr.Value;

            return Expression.Lambda(typeof(Func<>).MakeGenericType(expression.Type), expression)
                                                   .Compile()
                                                   .DynamicInvoke();
        }
    }
}
