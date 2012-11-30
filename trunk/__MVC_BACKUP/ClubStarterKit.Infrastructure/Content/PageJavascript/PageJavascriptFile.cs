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

using System.IO;
using ClubStarterKit.Infrastructure.Content.Javascript;

namespace ClubStarterKit.Infrastructure.Content.PageJavascript
{
    public class PageJavascriptFile
    {
        public PageJavascriptFile(FileInfo file, ISiteConfigProvider provider)
        {
            Filename = Path.GetFileNameWithoutExtension(file.Name).ToLower();
            Javascript = FileExt.ReadAll(new FileInfo[] { file });

            if (provider.MinifyJavascript)
                Javascript = new JavaScriptMinifier().Minify(Javascript);
        }

        public string Filename { get; set; }
        public string Javascript { get; set; }
    }
}
