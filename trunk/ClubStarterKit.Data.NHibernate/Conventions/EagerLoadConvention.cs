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
using ClubStarterKit.Core.DataAnnotations;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace ClubStarterKit.Data.NHibernate.Conventions
{
    public class EagerLoadConvention : AttributePropertyConvention<EagerLoad>, 
        IReferenceConvention, IReferenceConventionAcceptance, IHasManyConvention, IHasManyConventionAcceptance, ICollectionConvention, ICollectionConventionAcceptance
    {
        protected override void Apply(EagerLoad attribute, IPropertyInstance instance)
        {
            instance.Not.LazyLoad();
        }

        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            criteria.Expect(property => Attribute.GetCustomAttribute(property.Property, typeof(EagerLoad)) as EagerLoad != null);
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Not.LazyLoad();
        }

        public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
        {
            criteria.Expect(property => Attribute.GetCustomAttribute(property.Member, typeof(EagerLoad)) as EagerLoad != null);
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Not.LazyLoad();
        }

        public void Apply(ICollectionInstance instance)
        {
            instance.Not.LazyLoad();
        }

        public void Accept(IAcceptanceCriteria<ICollectionInspector> criteria)
        {
            criteria.Expect(property => Attribute.GetCustomAttribute(property.Member, typeof(EagerLoad)) as EagerLoad != null);
        }
    }
}
