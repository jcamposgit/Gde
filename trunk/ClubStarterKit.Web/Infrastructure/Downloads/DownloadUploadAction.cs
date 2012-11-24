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
using System.Web;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Downloads
{
    public class DownloadUploadAction : IDataAction<HttpContextBase, bool>
    {
        public DownloadUploadAction(HttpContextBase httpContext)
        {
            Context = httpContext;
        }

        public HttpContextBase Context { get; private set; }
        private const string TitleKey = "Title";
        public bool Execute()
        {
            if (Context.Request.Files.Count == 0)
                throw new Exception("There must be files to upload");

            if(string.IsNullOrEmpty(Context.Request.Form[TitleKey]))
                throw new Exception("Title must be specified");

            
            var file = Context.Request.Files.Get(0);
            var fileUrl = Context.Server.MapPath(Constants.DownloadsFolder) + file.FileName;
            file.SaveAs(fileUrl);
            var download = new Download
            {
                ContentType = file.ContentType,
                Title = Context.Request.Form[TitleKey],
                Url = file.FileName
            };

            using (var scope = new UnitOfWorkScope())
            {
                scope.UnitOfWork.RepositoryFor<Download>().Save(download);
                scope.Commit();
            }
            CacheKeyStore.ExpireWithType<Download>();
            return true;
        }
    }
}