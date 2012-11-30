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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace ClubStarterKit.Web.Helpers
{
    public static class GravatarHelper
    {
        public enum GravatarRating
        { 
            G,
            PG, 
            R,
            X
        }

        private const string _url = "http://www.gravatar.com/avatar/";

        /// <summary>
        /// Generates a gravatar image Url for a given user
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="size">Gravatar image size</param>
        /// <param name="rating">Rating of the requested image</param>
        /// <returns>Image URL</returns>
        public static string GravatarImageUrl(string email, int size = 1, GravatarRating rating = GravatarRating.G)
        {
            email = email.ToLower();  
            email = GetMd5Hash(email);  
            if (size < 1 | size > 600)  
            {  
                throw new ArgumentOutOfRangeException("size",  
                   "The image size should be between 20 and 80");  
            }

            return _url + email + "&s=" + size.ToString() + "&r=" + rating.ToString().ToLower();
        }

        private static string GetMd5Hash(string email)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(email));
            return data.Select(x => x.ToString("x2")).ConcatAll();
        }

        /// <summary>
        /// Generate a gravatar image link
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="size">Size of the image</param>
        /// <param name="rating">Rating of the image</param>
        /// <returns>Image tag with gravatar url as source</returns>
        public static MvcHtmlString GravatarImage(string email, int size = 1, GravatarRating rating = GravatarRating.G)
        {
            TagBuilder imgtag = new TagBuilder("img");
            imgtag.Attributes.Add("src", GravatarImageUrl(email, size, rating));
            imgtag.Attributes.Add("alt", email.Replace("@", "").Replace(".", ""));
            return MvcHtmlString.Create(imgtag.ToString());
        }
    }
}