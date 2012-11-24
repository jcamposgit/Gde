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


namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Concrete implementation of IPropertyValuePair that stores a simple
    /// string, representing the property, and a value of given generic type
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public class PropertyValuePair<TOutput> : IPropertyValuePair<TOutput>
    {
        public PropertyValuePair(string property, TOutput value)
        {
            Property = property;
            Value = value;
        }

        public string Property { get; private set; }

        public TOutput Value { get; private set; }
    }
}
