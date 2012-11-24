#region license

//Copyright 2008 Ritesh Rao 

//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Data;
using ClubStarterKit.Core.DataAccess;
using Xunit;

namespace ClubStarterKit.Tests.Core.DataAccess
{
    public class UnitOfWorkScopeTransactionTests
    {
        [Fact]
        public void
            UnitOfWorkScopeTransaction_GetTransactionForScope_ThrowsInvalidOperationException_WhenUseCompatibleAndCreateNewOptionsSelected
            ()
        {
            Assert.Throws<ArgumentNullException>(() =>
                                                     {
                                                         UnitOfWorkScopeTransactionOptions options =
                                                             UnitOfWorkScopeTransactionOptions.UseCompatible |
                                                             UnitOfWorkScopeTransactionOptions.CreateNew;
                                                         UnitOfWorkScopeTransaction.GetTransaction(null,
                                                                                                   IsolationLevel.
                                                                                                       Serializable,
                                                                                                   options);
                                                     });
        }
    }
}