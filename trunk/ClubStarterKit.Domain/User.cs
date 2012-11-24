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
using System.ComponentModel.DataAnnotations;
using ClubStarterKit.Core.DataAccess;

namespace ClubStarterKit.Domain
{
    public class User : IDataModel
    {
        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public virtual string Username { get; set; }

        [Required]
        public virtual string PasswordSalt { get; set; }

        [Required]
        public virtual string SaltedPassword { get; set; }
        
        public virtual string Address { get; set; }
        
        public virtual string Phone { get; set; }
        
        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        [Required, StringLength(int.MaxValue, MinimumLength = 1), 
        RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|edu|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum)\b")]
        public virtual string Email { get; set; }
        
        public virtual bool SendNewsletter { get; set; }
        
        public virtual string Signature { get; set; }
        
        public virtual string Bio { get; set; }
        
        public virtual bool ShowGravatar { get; set; }
        
        public virtual bool ShowEmail { get; set; }
        
        public virtual int MessagesPerPage { get; set; }

        #region IDataModel Members

        public virtual int Id { get; set; }

        #endregion

        public virtual string FullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        private string slug;
        [Required]
        public virtual string Slug
        {
            get
            {
                if (string.IsNullOrEmpty(slug))
                    slug = FullName().AsSlug();
                return slug;
            }
            set
            {
                slug = value;
            }
        }
    }
}