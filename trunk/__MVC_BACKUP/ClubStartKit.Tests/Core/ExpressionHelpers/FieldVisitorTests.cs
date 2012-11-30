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
using ClubStarterKit.Domain;
using Xunit;

namespace ClubStarterKit.Tests.Core.ExpressionHelpers
{
    public class FieldVisitorTests
    {
        [Fact]
        public void FieldVisitor_Visit_ReplacesFieldAccessor_WithConstantFExpression()
        {
            var myfield = "ans";

            Expression<Func<User, bool>> expr = u => u.Email == myfield;
            var visited = new FieldVisitor().Visit(expr);

            Assert.True(visited.ToString().Contains(myfield));
        }

        public string TestProp { get; set; }

        [Fact]
        public void FieldVisitor_Visit_ReplacesPropertyAccessor_WithConstantExpression()
        {
            TestProp = "ans";

            Expression<Func<User, bool>> expr = u => u.Email == TestProp;
            var visited = new FieldVisitor().Visit(expr);

            Assert.True(visited.ToString().Contains(TestProp));
        }
    }
}
