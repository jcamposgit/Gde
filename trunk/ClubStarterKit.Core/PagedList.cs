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
using System.Linq;

namespace ClubStarterKit.Core
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        private int _pageIndex;
        private int _pagesize;
        private int _count;
        private int _totalpages;

        public PagedList(IQueryable<T> source, int index, int pageSize)
        {
            _pagesize = pageSize;
            _pageIndex = index;
            _count = source.Count();
            AddRange(source.Skip(PageIndex * PageSize).Take(PageSize).ToList());
            
            // set total pages
            _totalpages = Convert.ToInt32(TotalCount/PageSize);
            if (TotalCount % PageSize != 0)
                _totalpages++;
        }

        public PagedList(IEnumerable<T> source, int index, int pageSize)
        {
            _pagesize = pageSize;
            _pageIndex = index;
            _count = source.Count();
            AddRange(source.AsQueryable().Skip(PageIndex * PageSize).Take(PageSize).ToList());

            // set total pages
            _totalpages = Convert.ToInt32(TotalCount / PageSize);
            if (TotalCount % PageSize != 0)
                _totalpages++;
        }

        #region IPagedList<T> Members

        public int TotalCount
        {
            get { return _count; }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public int PageSize
        {
            get { return _pagesize; }
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPages; }
        }

        public int PageNumber
        {
            get { return PageIndex + 1; }
        }

        public bool IsFirstPage
        {
            get { return PageIndex == 0; }
        }

        public bool IsLastPage
        {
            get
            {
                return PageNumber == TotalPages ||
                       TotalPages == 0;
            }
        }

        public int TotalPages
        {
            get
            {
                return _totalpages;
            }
        }

        #endregion
    }
}