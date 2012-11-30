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
using ClubStarterKit.Core;
using Xunit;

namespace ClubStarterKit.Tests.Core
{
    public class MaybeTests
    {
        [Fact]
        public void Maybe_NothingContructor_SetsNothingFlag()
        {
            var maybe = Maybe<int>.Nothing;
            bool isNothing = maybe.IsNothing;
            Assert.True(isNothing);
        }

        [Fact]
        private void Maybe_JustContructor_DoesntSetNothingFlag()
        {
            var maybe = new Maybe<int>(5);
            bool isNothing = maybe.IsNothing;
            Assert.False(isNothing);
        }

        [Fact]
        public void Maybe_JustContructor_SetsNothingFlag_WhenTheValuePassedIsNull()
        {
            var maybe = Maybe<int>.Nothing;
            bool isNothing = maybe.IsNothing;
            Assert.True(isNothing);
        }

        [Fact]
        public void Maybe_MCast_ReturnsAValue_WhenThereIsAValue()
        {
            var maybe = new Maybe<string>("");
            string value = maybe.Return();
            Assert.NotNull(value);
        }

        [Fact]
        public void Maybe_MCast_ReturnsSameValue_WhenThereIsAValue()
        {
            string initialValue = "";
            var maybe = new Maybe<string>(initialValue);
            string value = maybe.Return();
            Assert.Same(initialValue, value);
        }

        [Fact]
        public void Maybe_MCast_ReturnsNull_WhenThereIsNotAValue()
        {
            var maybe = Maybe<string>.Nothing;
            string value = maybe.Return();
            Assert.Null(value);
        }

        [Fact]
        public void Maybe_MaybeCast_ReturnsNothingMaybe_WhenTheValueIsNull()
        {
            string val = null;
            Maybe<string> maybe = val;
            Assert.True(maybe.IsNothing);
        }

        [Fact]
        public void Maybe_MaybeCast_ReturnsJustMaybe_WhenTheValueIsNotNull()
        {
            string val = "";
            Maybe<string> maybe = val;
            Assert.False(maybe.IsNothing);
        }

        [Fact]
        public void Maybe_SelectMany_ReturnsNothingMonad_WhenTheInitialMaybeMonadIsNothingMaybeMonad()
        {
            var initialMaybe = Maybe<string>.Nothing;
            Maybe<string> appliedMaybe = from i in initialMaybe
                                         from some in "s".AsMaybe()
                                         select some;
            Assert.True(appliedMaybe.IsNothing);
        }

        [Fact]
        public void Maybe_SelectMany_ReturnsNewMaybeWithNewValue()
        {
            Func<Maybe<string>, Maybe<string>> addBchar = (maybe) =>
                                                          new Maybe<string>((maybe) + "b");

            string initial = "", final = "b";

            var initialMaybe = new Maybe<string>(initial);
            var afterApply = from s in initial.AsMaybe()
                                from s2 in final.AsMaybe()
                                select s2;
            Assert.Equal(final, afterApply.Return());
        }

        [Fact]
        public void Maybe_Execute_ExecutesHasValueAction_WhenNotNothingMaybe()
        {
            bool executed = false;

            Action hasValue = () => executed = true;
            Action noValue = () => executed = false;

            new Maybe<string>("").Execute(hasValue, noValue);

            Assert.True(executed);
        }

        [Fact]
        public void Maybe_Execute_ExecutesNoValueAction_WhenNothingMaybe()
        {
            bool executed = false;

            Action hasValue = () => executed = false;
            Action noValue = () => executed = true;

            Maybe<string>.Nothing.Execute(hasValue, noValue);

            Assert.True(executed);
        }

        [Fact]
        public void Maybe_ExecuteWithArg_ExecutesHasValueAction_WhenNotNothingMaybe()
        {
            bool executed = false;

            Action<string> hasValue = (s) => executed = true;
            Action noValue = () => executed = false;

            new Maybe<string>("").Execute(hasValue, noValue);

            Assert.True(executed);
        }

        [Fact]
        public void Maybe_ExecuteWithArg_ExecutesNoValueAction_WhenNothingMaybe()
        {
            bool executed = false;

            Action<string> hasValue = (s) => executed = false;
            Action noValue = () => executed = true;

            Maybe<string>.Nothing.Execute(hasValue, noValue);

            Assert.True(executed);
        }

        [Fact]
        public void Maybe_DefaultTo_ReturnsNewMaybeWithDefaultValue_WhenOriginalMaybeIsNothing()
        {
            string def = "default";
            var maybe = Maybe<string>.Nothing;

            string value = maybe.DefaultTo(def).Return();

            Assert.Same(def, value);
        }

        [Fact]
        public void Maybe_DefaultTo_ReturnsSameMaybe_WhenOriginalMaybeNotNothing()
        {
            string def = "default";
            var maybe = new Maybe<string>("");

            Maybe<string> newMaybe = maybe.DefaultTo(def);

            Assert.Same(maybe, newMaybe);
        }
    }
}