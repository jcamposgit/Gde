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
using System.IO;
using System.Linq;
using System.Web;

namespace ClubStarterKit.Infrastructure.Content
{
    internal class SiteContentUtils
    {
        internal static FileInfo[] GetFiles(string contentType, string ext = "*")
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");

            string root = HttpContext.Current.Server.MapPath("~/" + Constants.SiteContentFolder + contentType + "/");
            return FileExt.GetFiles(root, ext);
        }

        internal static string[] GetSiteUrls(string baseurl, string contentType, string ext)
        {
            if (string.IsNullOrEmpty(baseurl))
                throw new ArgumentNullException("contentType");

            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");

            string format = baseurl + Constants.SiteContentFolder + contentType + "/{0}." + ext;

            /*
            var files = GetFiles(contentType);
            var urls = new string[files.Length];
            int index = 0;

            foreach (FileInfo file in files)
            {
                urls[index] = string.Format(format, file.Name);
                index++;
            }*/

            var urls = from file in GetFiles(contentType)
                       select string.Format(format, file.Name);

            return urls.ToArray();
        }

        internal static string ReadAllContent(string contentType, string ext = "*")
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");

            return FileExt.ReadAll(GetFiles(contentType, ext));
        }
    }
}