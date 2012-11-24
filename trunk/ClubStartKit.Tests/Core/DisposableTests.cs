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

using ClubStarterKit.Core;
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Core
{
    public class DisposableTests
    {
        #region Helper

        public Disposable GenerateMock()
        {
            return new Mock<Disposable>().Object;
        }

        public Disposable GenerateMock(out Mock<Disposable> mock)
        {
            var mockDisposable = new Mock<Disposable>();
            mock = mockDisposable;
            return mockDisposable.Object;
        }

        #endregion

        [Fact]
        public void Disposable_DefaultState_IsNotDisposed()
        {
            Assert.False(GenerateMock().IsDisposed);
        }

        [Fact]
        public void Disposable_AfterDisposedState_IsDisposedTrue()
        {
            Disposable disp = GenerateMock();
            disp.Dispose();

            Assert.True(disp.IsDisposed);
        }

        [Fact]
        public void Disposable_Dispose_TriggersDisposedEvent()
        {
            bool eventCalled = false;
            Disposable disp = GenerateMock();
            disp.Disposed += (sender, args) => eventCalled = true;
            disp.Dispose();

            Assert.True(eventCalled);
        }
    }
}