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

namespace ClubStarterKit.Web
{
    public static class Constants
    {
        public const string AdminRole = "Admin";
        public const string ReturnUrlRequestKey = "ReturnUrl";
        public const int DefaultPageSize = 10;
        public const int HomepageItemSize = 5;
        public const int TruncateLength = 50;
        public const string UserSessionKey = "__User__";
        public const string UserIdCookieKey = "__User_Id__";
        public const string UploadsFolder = "~/Content/Uploads/";
        public const string DownloadsFolder = UploadsFolder + "/Downloads/";
        public const string PhotoUploadFolder = UploadsFolder + "/Photos/";
        public const string RssLink_Html = @"<a class=""rss-link"" href=""{0}""><img src=""{1}"" alt=""rss"" /></a>";
        public const bool DisablePhotoCommentViewForUnauthorized = true;
        public const string EventDateFormat = "g";
        public const int ImageMaxWidth = 446;
    }
}