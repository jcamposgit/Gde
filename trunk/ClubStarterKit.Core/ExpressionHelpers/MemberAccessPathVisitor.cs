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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ClubStarterKit.Core.ExpressionHelpers
{
    /// <summary>
    /// Inherits from the <see cref="ExpressionVisitor"/> base class and implements a expression visitor
    /// that builds up a path string that represents meber access in a Expression.
    /// </summary>
    public class MemberAccessPathVisitor : ExpressionVisitor
    {
        #region fields

        //StringBuilder instance that will store the path.
        private readonly Stack<string> _path = new Stack<string>();

        #endregion

        #region properties

        /// <summary>
        /// Gets the path analyzed by the visitor.
        /// </summary>
        public string Path
        {
            get
            {
                var pathString = new StringBuilder();
                foreach (string path in _path)
                {
                    if (pathString.Length == 0)
                        pathString.Append(path);
                    else
                        pathString.AppendFormat(".{0}", path);
                }
                return pathString.ToString();
            }
        }

        #endregion

        #region overriden methods

        /// <summary>
        /// Overriden. Overrides all MemberAccess to build a path string.
        /// </summary>
        /// <param name="methodExp"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression methodExp)
        {
            if (methodExp.Member.MemberType != MemberTypes.Field && methodExp.Member.MemberType != MemberTypes.Property)
                throw new NotSupportedException("MemberAccessPathVisitor does not support a member access of type " +
                                                methodExp.Member.MemberType);
            _path.Push(methodExp.Member.Name);
            return base.VisitMember(methodExp);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}