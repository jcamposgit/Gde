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

using ClubStarterKit.Infrastructure;
using StructureMap;

namespace System.Web.Mvc
{
    public static class TitleHelper
    {
        private const string TitleViewDataKey = "Page__Title";
        private static string ApplicationNameCache;

        private static string ApplicationName
        {
            get
            {
                if (ApplicationNameCache == null)
                    ApplicationNameCache = ObjectFactory.GetInstance<ISiteConfigProvider>().ApplicationName;

                return ApplicationNameCache;
            }
        }

        /// <summary>
        /// Gets the title view data
        /// </summary>
        /// <param name="viewData"></param>
        /// <returns></returns>
        public static string Title(this ViewDataDictionary viewData)
        {
            try
            {
                return viewData[TitleViewDataKey].ToString();
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the page title view data
        /// </summary>
        /// <param name="viewData"></param>
        /// <param name="title">Title to set</param>
        public static void Title(this ViewDataDictionary viewData, string title)
        {
            viewData[TitleViewDataKey] = ApplicationName + " - " + title;
        }
    }
}