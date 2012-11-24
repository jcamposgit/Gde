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
using Xunit;

namespace ClubStarterKit.Tests.Core
{
    public class ISpecificationTests
    {
        [Fact]
        public void ISpecification_Or_ReturnsFalseIfBothFalse()
        {
            ISpecification<string> spec = new StringEmpty().Or(new StringEmpty());

            Assert.False(spec.IsSatisfiedBy("non-empty-string"));
        }

        [Fact]
        public void ISpecification_Or_ReturnsTrueIfBothTrue()
        {
            ISpecification<string> spec = new StringEmpty().Or(new StringEmpty());

            Assert.True(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_Or_ReturnsTrueIfRightFalse()
        {
            ISpecification<string> spec = new StringEmpty().Or(new StringEmpty().Not());

            Assert.True(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_Or_ReturnsTrueIfLeftFalse()
        {
            ISpecification<string> spec = new StringEmpty().Not().Or(new StringEmpty());

            Assert.True(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_Not_ReturnsOppositeSatisfaction()
        {
            ISpecification<string> spec = new StringEmpty().Not();

            Assert.False(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_And_ReturnsFalseIfLeftFalse()
        {
            ISpecification<string> spec = new StringEmpty().And(new StringEmpty().Not());

            Assert.False(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_And_ReturnsFalseIfRightFalse()
        {
            ISpecification<string> spec = new StringEmpty().Not().And(new StringEmpty());

            Assert.False(spec.IsSatisfiedBy(string.Empty));
        }

        [Fact]
        public void ISpecification_And_ReturnsTrueIfBothTrue()
        {
            ISpecification<string> spec = new StringEmpty().And(new StringEmpty());

            Assert.True(spec.IsSatisfiedBy(string.Empty));
        }

        #region Nested type: StringEmpty

        private class StringEmpty : ISpecification<string>
        {
            #region ISpecification<string> Members

            public bool IsSatisfiedBy(string entity)
            {
                return string.IsNullOrEmpty(entity);
            }

            #endregion
        }

        #endregion
    }
}