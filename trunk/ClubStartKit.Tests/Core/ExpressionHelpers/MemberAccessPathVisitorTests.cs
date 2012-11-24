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
using ClubStarterKit.Core.ExpressionHelpers;
using Xunit;

namespace ClubStarterKit.Tests.Core.ExpressionHelpers
{
    public class MemberAccessPathVisitorTests
    {
        #region Test Classes

        #region Nested type: Customer

        public class Customer
        {
            public IList<Order> Orders;
        }

        #endregion

        #region Nested type: Order

        public class Order
        {
        }

        #endregion

        #region Nested type: SalesPerson

        public class SalesPerson
        {
            public Customer PrimaryCustomer { get; set; }

            public object MethodAccess()
            {
                return null;
            }
        }

        #endregion

        #endregion

        [Fact]
        public void MemberAccessPathVisitor_Visit_CustomerOrdersReturnOrdersAsPath()
        {
            Expression<Func<Customer, object>> expression = x => x.Orders;
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(expression);
            Assert.Equal("Orders", visitor.Path);
        }

        [Fact]
        public void MemberAccessPathVisitor_Visit_SalesPersonCustomerOrdersReturnCustomerOrdersAsPath()
        {
            Expression<Func<SalesPerson, object>> expression = x => x.PrimaryCustomer.Orders;
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(expression);
            Assert.Equal("PrimaryCustomer.Orders", visitor.Path);
        }

        [Fact]
        public void MemberAccessPathVisitor_Visit_ThrowsNotSupportedException_WhenExpressionContainsMethodCall()
        {
            Expression<Func<SalesPerson, object>> expression = x => x.MethodAccess();
            var visitor = new MemberAccessPathVisitor();

            Assert.Throws<NotSupportedException>(() => visitor.Visit(expression));
        }
    }
}