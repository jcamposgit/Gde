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

using System.Security.Principal;
using ClubStarterKit.Core;
using StructureMap;

namespace ClubStarterKit.Infrastructure.View
{
    public class BaseControl : System.Web.Mvc.ViewUserControl
    {
        public ISiteConfigProvider SiteConfig
        {
            get { return ObjectFactory.GetInstance<ISiteConfigProvider>(); }
        }

        private ISpecification<IPrincipal> _elevatedPrincipalSpecification;
        public ISpecification<IPrincipal> ElevatedPrincipalSpecification
        {
            get
            {
                if (_elevatedPrincipalSpecification == null)
                    _elevatedPrincipalSpecification = (ViewData[Constants.ElevatedPermissionViewDataKey] as ISpecification<IPrincipal>) ??
                                                        ObjectFactory.GetInstance<ISpecification<IPrincipal>>();
                return _elevatedPrincipalSpecification;
            }
        }

        public bool UserHasElevatedPermission
        {
            get
            {
                return ElevatedPrincipalSpecification.IsSatisfiedBy(this.Context.User);
            }
        }
    }
}
