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

namespace ClubStarterKit.Infrastructure.UI.PagedList
{
    public class HiddenNumeredPageLink : NumberedPageLink
    {
        public HiddenNumeredPageLink(IPagedList pagedList, int index)
            : base(pagedList, index)
        {
        }
        public override string CssClass
        {
            get
            {
                return base.CssClass + " hidden";
            }
        }
    }
}
