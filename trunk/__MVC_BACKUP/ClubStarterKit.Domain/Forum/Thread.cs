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
using ClubStarterKit.Core.DataAnnotations;

namespace ClubStarterKit.Domain
{
    public class Thread : IDataModel
    {
        public virtual bool Locked { get; set; }

        public virtual Topic Topic { get; set; }
        
        public virtual int Views { get; set; }

        [Required]
        public virtual string Title { get; set; }

        private string slug = "";
        [Required]
        public virtual string Slug
        {
            get
            {
                if (string.IsNullOrEmpty(slug))
                    slug = Title.AsSlug();
                return slug;
            }
            set
            {
                slug = value;
            }
        }
        
        public virtual DateTimeOffset DateCreated { get; set; }

        [SqlFormula(@"(select top(1) Message.SubmissionDate from Message where Message.Thread_Id = Id order by Message.SubmissionDate desc)")]
        public virtual DateTimeOffset? LastUpdate { get; set; }

        //[SqlFormula("(select count(*) from Message where Message.Thread_Id = Id)")]
        [SqlFormula("(select count(*) from Message where Message.Thread_Id = Id)")]
        public virtual int MessageCount { get; set; }

        [Required]
        public virtual bool Hidden { get; set; }

        #region IDataModel Members

        public virtual int Id { get; set; }

        #endregion
    }
}