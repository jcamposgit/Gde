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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Content
{
    public class ContentSectionRetrievalAction : IDataAction<string, ContentSection>
    {
        public ContentSectionRetrievalAction(string sectionName)
        {
            Context = sectionName;
        }

        public string Context
        {
            get; private set;
        }

        public ContentSection Execute()
        {
            var cachedValue = new SingleItemDataCache<ContentSection>(s => s.Name == Context).CachedValue;

            if (cachedValue != null)
                return cachedValue;

            // the section doesn't exist.
            // create a blank section
            cachedValue = new ContentSection
            {
                Content = " ",
                Name = Context,
                LastUpdated = DateTimeOffset.Now
            };

            new ContentSectionUpdateAction(cachedValue).Execute();

            return cachedValue;
        }
    }
}