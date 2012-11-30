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

using System.ComponentModel.Composition;
using System.Web;
using ClubStarterKit.Infrastructure.Application;

namespace ClubStarterKit.Infrastructure.Content.Css
{
    /// <summary>
    /// Transforms a given CSS stream by replacing values
    /// </summary>
    [InheritedExport]
    public interface ICssTransformer
    {
        /// <summary>
        /// Transform CSS
        /// </summary>
        /// <param name="httpContext">HTTP Conext</param>
        /// <param name="css">Current CSS value</param>
        /// <param name="applicationId">Application id provider</param>
        /// <returns>New CSS value after transformation</returns>
        string Transform(HttpContextBase httpContext, string css, IApplicationIdProvider applicationId);
    }
}