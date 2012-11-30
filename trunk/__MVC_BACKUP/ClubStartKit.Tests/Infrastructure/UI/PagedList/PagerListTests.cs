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

using System.Collections;
using System.Collections.Generic;
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure.UI.PagedList;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.UI.PagedList
{
    public class PagerListTests
    {
        private List<string> GenerateList(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
                list.Add("item_" + i);

            return list;
        }

        [Fact]
        public void PagerList_List_Empty_WhenPagedListEmpty()
        {
            List<string> items = GenerateList(0);
            var pager = new PagerList(new PagedList<string>(items, 0, 10), false);

            Assert.Empty(pager.Links);
        }

        [Fact]
        public void PagerList_List_DoesntContainFirst_WhenOnFirstPage()
        {
            List<string> items = GenerateList(20);
            var pager = new PagerList(new PagedList<string>(items, 0, 10), false);

            pager.Links.DoesntContainItemWithType<PreviousPageLink>();
        }

        [Fact]
        public void PagerList_List_ContainPrevious_WhenNotFirstPage()
        {
            List<string> items = GenerateList(20);
            var pager = new PagerList(new PagedList<string>(items, 1, 10), false);

            pager.Links.ContainItemWithType<PreviousPageLink>();
        }

        [Fact]
        public void PagerList_List_ContainNext_WhenNotOnLastPage()
        {
            List<string> items = GenerateList(20);
            var pager = new PagerList(new PagedList<string>(items, 0, 10), false);

            pager.Links.ContainItemWithType<NextPageLink>();
        }

        [Fact]
        public void PagerList_List_DoesntContainNext_WhenOnLastPage()
        {
            List<string> items = GenerateList(20);
            var pager = new PagerList(new PagedList<string>(items, 1, 10), false);

            pager.Links.DoesntContainItemWithType<NextPageLink>();
        }

        [Fact]
        public void PagerList_List_ContainsOnlyOneDeliminator_WhenOnFirstPage()
        {
            List<string> items = GenerateList(100);
            var pager = new PagerList(new PagedList<string>(items, 0, 10), false);

            Assert.Equal(1, pager.Links.NumberOfItemsWithType<ListDeliminator>());
        }

        [Fact]
        public void PagerList_List_ContainsOnlyTwoDeliminators_WhenOnMiddlePage()
        {
            List<string> items = GenerateList(100);
            var pager = new PagerList(new PagedList<string>(items, 10, 5), false);

            Assert.Equal(2, pager.Links.NumberOfItemsWithType<ListDeliminator>());
        }

        [Fact]
        public void PagerList_List_ContainsOnlyOneDeliminator_WhenOnLastPage()
        {
            List<string> items = GenerateList(100);
            var pager = new PagerList(new PagedList<string>(items, 10, 10), false);

            Assert.Equal(1, pager.Links.NumberOfItemsWithType<ListDeliminator>());
        }

        [Fact]
        public void PagerList_List_DoesntDeliminators_WhenPagesLessThanMax()
        {
            List<string> items = GenerateList(100);
            var pager = new PagerList(new PagedList<string>(items, 1, 10), false, 11);

            Assert.Equal(0, pager.Links.NumberOfItemsWithType<ListDeliminator>());
        }
    }
}