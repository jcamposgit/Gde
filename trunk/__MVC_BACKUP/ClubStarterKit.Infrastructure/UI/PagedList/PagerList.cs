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
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using ClubStarterKit.Core;

namespace ClubStarterKit.Infrastructure.UI.PagedList
{
    public class PagerList
    {
        public PagerList(IPagedList pagedList, bool showHidden, int maxNumberedPages = 5)
        {
            Links = new List<IPagedLink>();
            HiddenLinks = showHidden ? new List<IPagedLink>() : null;
            PagedList = pagedList;
            MaxNumberedPages = maxNumberedPages;

            Setup();
        }

        public int MaxNumberedPages { get; private set; }
        public IList<IPagedLink> Links { get; private set; }
        public IList<IPagedLink> HiddenLinks { get; private set; }
        public IPagedList PagedList { get; private set; }

        private void Setup()
        {
            AddPreviousPageLink();

            if (MaxNumberedPages > 0)
                AddNumberedPageLinks();
            AddHiddenNumbers();
            AddNextPageLink();
        }

        #region Link Additions

        private void AddPreviousPageLink()
        {
            if (PagedList.HasPreviousPage)
                Links.Add(new PreviousPageLink(PagedList));
        }

        private void AddNumberedPageLinks()
        {
            if (PagedList.TotalPages == 0)
                return;

            int start = 0;
            int end = PagedList.TotalPages - 1;

            if (PagedList.TotalPages > MaxNumberedPages)
            {
                int median = Convert.ToInt32(Math.Ceiling(MaxNumberedPages/2.0)) - 1;
                int min = PagedList.PageIndex - median;
                int max = PagedList.PageIndex + median;

                if (min < 0)
                {
                    min = 0;
                    max = MaxNumberedPages - 1;
                }
                if (max >= PagedList.TotalPages)
                {
                    min = PagedList.TotalPages - 1 - MaxNumberedPages;
                    max = PagedList.TotalPages - 1;
                }

                start = min;
                end = max;
            }

            if (start > 0)
            {
                Links.Add(new NumberedPageLink(PagedList, 0));
                Links.Add(new ListDeliminator());
            }

            for (int i = start; i <= end; i++)
                Links.Add(new NumberedPageLink(PagedList, i));

            if (end < PagedList.TotalPages - 1)
            {
                Links.Add(new ListDeliminator());
                Links.Add(new NumberedPageLink(PagedList, PagedList.TotalPages - 1));
            }
        }

        private void AddHiddenNumbers()
        {
            if (HiddenLinks == null)
                return;

            var numberedPages = Enumerable.Range(0, PagedList.TotalPages)
                                          .Except(Links.Select(x => x.PageIndex));

            numberedPages.Select(i => new HiddenNumeredPageLink(PagedList, i))
                         .Foreach(HiddenLinks.Add);
        }

        private void AddNextPageLink()
        {
            if (PagedList.HasNextPage)
                Links.Add(new NextPageLink(PagedList));
        }

        #endregion

        #region Render

        public string RenderHtml(UrlHelper url, string action, string controller, string pageRouteValue = "page", RouteValueDictionary values = null, string prefix = "Page")
        {
            var builder = new StringBuilder();

            Action<IPagedLink> appendLink = link => builder.Append(FormatLink(link, url, controller, action, pageRouteValue, values, prefix));

            Links.Foreach(appendLink);

            if(HiddenLinks != null)
                HiddenLinks.Foreach(appendLink);

            return @"<div class=""pager""><strong>" + prefix + "  </strong>" + builder + "</div>";
        }

        protected virtual string FormatLink(IPagedLink link, UrlHelper url, string controller, string action,
                                            string pageRouteValue, RouteValueDictionary values, string prefix)
        {
            if (link is ListDeliminator)
                return FormatDeliminator(link as ListDeliminator);

            var tag = new TagBuilder("a");
            tag.AddCssClass(link.CssClass);
            tag.InnerHtml = link.LinkText;
            tag.Attributes.Add("title", prefix.ToLower() + "-" + (link.PageIndex + 1));

            var valueDictionary = new RouteValueDictionary(values);
            valueDictionary.Add("controller", controller);
            valueDictionary.Add(pageRouteValue, (link.PageIndex + 1).ToString());
            tag.Attributes.Add("href", url.Action(action, valueDictionary));

            return tag.ToString();
        }

        protected virtual string FormatDeliminator(ListDeliminator delim)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass(delim.CssClass);
            tag.InnerHtml = delim.LinkText;
            return tag.ToString();
        }

        #endregion
    }
}