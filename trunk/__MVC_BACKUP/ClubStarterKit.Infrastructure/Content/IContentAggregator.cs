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

using System.Web.Mvc;

namespace ClubStarterKit.Infrastructure.Content
{
    /// <summary>
    /// Aggrigator of content for the website
    /// </summary>
    public interface IContentAggregator
    {
        /// <summary>
        /// Result to render to users
        /// </summary>
        ActionResult ContentResult { get; }

        /// <summary>
        /// Type of content to aggrigate
        /// </summary>
        /// <remarks>By convention, the content will be under Content/{ContentType}</remarks>
        string ContentType { get; }
    }
}