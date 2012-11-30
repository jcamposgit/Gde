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
using System.ComponentModel.DataAnnotations;
using ClubStarterKit.Core.DataAccess;

namespace ClubStarterKit.Domain
{
    public class Message : IDataModel
    {
        [Required, StringLength(int.MaxValue, MinimumLength=1)]
        public virtual string Body { get; set; }

        public virtual bool Hidden { get; set; }
        
        public virtual DateTimeOffset SubmissionDate { get; set; }

        [Required]
        public virtual User User { get; set; }
        
        [Required]
        public virtual Thread Thread { get; set; }
        
        public virtual IList<SpamFlag> SpamFlags { get; set; }
        
        //public virtual IList<ThreadInteraction> Interactions { get; set; }

        #region IDataModel Members

        public virtual int Id { get; set; }

        #endregion
    }
}