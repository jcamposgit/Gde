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
    public class PollQuestion : IDataModel, IOwnable
    {
        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public virtual string Question { get; set; }

        public virtual DateTimeOffset CreationDate { get; set; }

        public virtual bool Hidden { get; set; }

        #region IDataModel Members

        public virtual int Id { get; set; }

        #endregion

        #region IOwnable Members

        public virtual User Owner { get; set; }

        #endregion
    }
}