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

using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Core.DataStorage;
using Moq;
using StructureMap;
using Xunit;

namespace ClubStarterKit.Tests.Core.DataAccess
{
    public class UnitOfWorkTests
    {
        public UnitOfWorkTests()
        {
            DataStore.Local.Remove("UnitOfWorkCurrent");
        }

        [Fact]
        public void UnitOfWork_Default_HasNotStarted()
        {
            Assert.False(UnitOfWork.InSession);
        }

        [Fact]
        public void UnitOfWork_Default_UnitOfWorkNull()
        {
            Assert.Null(UnitOfWork.Current);
        }

        [Fact]
        public void UnitOfWork_Start_UnitOfWorkHasStarted()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOWInstance = new Mock<IUnitOfWork>();
            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOWInstance.Object);
            ObjectFactory.Inject(mockUOWFactory.Object);

            IUnitOfWork uowInstance = UnitOfWork.Start();

            Assert.True(UnitOfWork.InSession);
        }

        [Fact]
        public void UnitOfWork_Start_UnitOfWorkSingletonSetToStartedUnitOfWork()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOWInstance = new Mock<IUnitOfWork>();
            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOWInstance.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);

            IUnitOfWork uowInstance = UnitOfWork.Start();

            Assert.Same(UnitOfWork.Current, uowInstance);
        }

        [Fact]
        public void UnitOfWork_Start_AlreadyStartedUnitOfWork_ReturnsSameUnitOfWork()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOWInstance = new Mock<IUnitOfWork>();
            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOWInstance.Object);
            ObjectFactory.Inject(mockUOWFactory.Object);

            IUnitOfWork uowInstance = UnitOfWork.Start();
            Assert.Same(UnitOfWork.Start(), uowInstance);
            UnitOfWork.Finish(false);
        }
    }
}