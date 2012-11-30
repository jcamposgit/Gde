﻿#region license

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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Poll
{
    public class PollQuestionUpdateAction : IDataAction<PollQuestion, bool>
    {
        public PollQuestionUpdateAction(PollQuestion question)
        {
            Context = question;
        }

        public PollQuestion Context
        {
            get;
            private set;
        }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    scope.UnitOfWork.RepositoryFor<PollQuestion>().Save(Context);
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<PollQuestion>(Context.Id.ToString());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}