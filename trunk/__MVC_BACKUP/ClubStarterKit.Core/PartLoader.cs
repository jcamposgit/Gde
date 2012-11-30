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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace ClubStarterKit.Core
{
    /// <summary>
    /// Abstraction over the Managed Extensibility Framework
    /// composition containers
    /// </summary>
    /// <typeparam name="TPartType"></typeparam>
    [Export]
    public class PartLoader<TPartType>
    {
        private readonly List<ComposablePartCatalog> _parts = new List<ComposablePartCatalog>();

        public PartLoader()
        {
            WithAssembly(typeof (TPartType).Assembly);
            WithAssembly(Assembly.GetExecutingAssembly());
        }

        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<TPartType> InternalItems { get; set; }

        public virtual CompositionContainer Container
        {
            get { return new CompositionContainer(new AggregateCatalog(_parts.ToArray())); }
        }

        public virtual IList<ComposablePartCatalog> Parts
        {
            get { return _parts; }
        }

        public virtual IEnumerable<TPartType> Items
        {
            get
            {
                // compose the part loader
                Container.ComposeParts(this);
                return InternalItems;
            }
        }

        public virtual void AddPart(ComposablePartCatalog catalog)
        {
            _parts.Add(catalog);
        }

        public virtual PartLoader<TPartType> WithAssemblyOf(object obj)
        {
            return WithAssembly(obj.GetType().Assembly);
        }

        public virtual PartLoader<TPartType> WithAssemblyOf<TAdditive>()
        {
            return WithAssembly(typeof (TAdditive).Assembly);
        }

        public virtual PartLoader<TPartType> WithAssembly(Assembly assembly)
        {
            // test if the assembly is already in the part collection
            if (
                _parts.Where(cat => cat is AssemblyCatalog && (cat as AssemblyCatalog).Assembly.Equals(assembly)).Count() >
                0)
                return this;

            // add a new assembly catelog
            _parts.Add(new AssemblyCatalog(assembly));

            return this;
        }
    }
}