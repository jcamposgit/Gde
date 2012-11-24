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
using ClubStarterKit.Core;
using Xunit;

namespace ClubStarterKit.Tests.Core
{
    public class PartLoaderTests
    {
        #region Helpers

        #region Nested type: ISamplePart

        internal interface ISamplePart
        {
            string GetName();
        }

        #endregion

        #region Nested type: SamplePart1

        [Export(typeof (ISamplePart))]
        internal class SamplePart1 : ISamplePart
        {
            #region ISamplePart Members

            public string GetName()
            {
                return "Sample 1";
            }

            #endregion
        }

        #endregion

        #region Nested type: SamplePart2

        [Export(typeof (ISamplePart))]
        internal class SamplePart2 : ISamplePart
        {
            #region ISamplePart Members

            public string GetName()
            {
                return "Sample 2";
            }

            #endregion
        }

        #endregion

        #endregion

        [Fact]
        public void PartLoader_ItemsProperty_HasTwoItems_WhenISamplePartImported()
        {
            IEnumerable<ISamplePart> parts = new PartLoader<ISamplePart>().Items;

            Assert.Equal(2, parts.Count());
        }

        [Fact]
        public void PartLoader_ContainerProperty_HasOneAggregateCatelog()
        {
            ComposablePartCatalog catelog = new PartLoader<ISamplePart>().Container.Catalog;
            Assert.IsType<AggregateCatalog>(catelog);
        }

        [Fact]
        public void
            PartLoader_ContainerProperty_HasAggregateCatelogWithTwoAssemblyCatelogs_WhenTypeIsOutsidePartLoaderAssembly()
        {
            var catelog = new PartLoader<ISamplePart>().Container.Catalog as AggregateCatalog;
            Assert.Equal(2, catelog.Catalogs.Count);
        }

        [Fact]
        public void PartLoader_ItemsProperty_HasTwoItems_WhenISamplePartImportedTwoTimes()
        {
            var loader = new PartLoader<ISamplePart>();

            IEnumerable<ISamplePart> load1 = loader.Items;
            IEnumerable<ISamplePart> load2 = loader.Items;

            Assert.Equal(2, load2.Count());
        }

        [Fact]
        public void PartLoader_WithAssemblyOf_NonGeneric_AddsTheAssemblyToThePartCatalog()
        {
            var loader = new PartLoader<ISamplePart>();

            // reset the part catalog list
            while (loader.Parts.Count > 0)
                loader.Parts.RemoveAt(0);

            Assembly assemblyToAdd = GetType().Assembly;

            loader.WithAssemblyOf(this);

            Assert.Equal(assemblyToAdd, (loader.Parts[0] as AssemblyCatalog).Assembly);
        }

        [Fact]
        public void PartLoader_WithAssemblyOf_Generic_AddsTheAssemblyToThePartCatalog()
        {
            var loader = new PartLoader<ISamplePart>();

            // reset the part catalog list
            while (loader.Parts.Count > 0)
                loader.Parts.RemoveAt(0);

            Assembly assemblyToAdd = GetType().Assembly;

            loader.WithAssemblyOf<PartLoaderTests>();

            Assert.Equal(assemblyToAdd, (loader.Parts[0] as AssemblyCatalog).Assembly);
        }
    }
}