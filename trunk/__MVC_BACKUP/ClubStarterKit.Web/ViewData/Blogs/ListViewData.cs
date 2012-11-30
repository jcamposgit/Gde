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
using System.Web.Mvc;

namespace ClubStarterKit.Web.ViewData.Blogs
{
    public class ListViewData
    {
        private const string ActionViewDataKey = "__PagerAction";
        private const string HeaderViewDataKey = "__Header";
        private const string RssActionViewData = "__RssAction";
        private ViewDataDictionary _viewData;
        private string _author;

        public ListViewData(ViewDataDictionary viewData, string author)
        {
            _viewData = viewData;
            _author = author;

            // set view data values
            Header = null;
            Action = null;
            RssAction = null;
        }

        public ListViewData(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        public string Header
        {
            get 
            {
                return _viewData[HeaderViewDataKey].ToString();
            }
            private set
            {
                if (!string.IsNullOrEmpty(_author))
                    _viewData[HeaderViewDataKey] = "Blog Post for " + _author;
                _viewData[HeaderViewDataKey] = "Blog Posts";
            }
        }

        public Func<int, ActionResult> Action
        {
            get
            {
                return _viewData[ActionViewDataKey] as Func<int, ActionResult>;
            }
            private set
            {
                Func<int, ActionResult> res;
                if (!string.IsNullOrEmpty(_author))
                    res = page => Website.Blog.Author(_author, page);
                res = page => Website.Blog.List(page);

                _viewData[ActionViewDataKey] = res;
            }
        }

        public ActionResult RssAction
        {
            get
            {
                return _viewData[RssActionViewData] as ActionResult;
            }
            private set
            {
                if (!string.IsNullOrEmpty(_author))
                    _viewData[RssActionViewData] = Website.Blog.Rss(_author);
                _viewData[RssActionViewData] = Website.Blog.Rss();
            }
        }
    }
}