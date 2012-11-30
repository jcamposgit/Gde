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
    public class Topic : IDataModel
    {
        [Required, StringLength(int.MaxValue, MinimumLength=1)]
        public virtual string Title { get; set; }

        [Required, StringLength(25, MinimumLength = 1)]
        public virtual string Description { get; set; }

        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public virtual string TopicGroup { get; set; }
        
        public virtual int Position { get; set; }
        
        public virtual bool VisibleToAnonymous { get; set; }

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

        public virtual bool Locked { get; set; }

        [SqlFormula("(select count(*) from Thread where Thread.Hidden = 0 and Thread.Topic_Id = Id)")]
        public virtual int ThreadCount { get; set; }

        [SqlFormula(@"(select top(1) Message.SubmissionDate from Message inner join Thread on Message.Thread_id = Thread.Id group by Thread.Topic_id, Message.SubmissionDate having Thread.Topic_id = Id order by Message.SubmissionDate desc)")]
        public virtual DateTimeOffset? LastUpdated { get; set; }

        #region IDataModel Members

        public virtual int Id { get; set; }

        #endregion
    }
}