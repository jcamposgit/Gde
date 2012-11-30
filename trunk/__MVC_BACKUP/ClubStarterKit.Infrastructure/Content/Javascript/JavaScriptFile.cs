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

namespace ClubStarterKit.Infrastructure.Content.Javascript
{
    internal sealed class JavaScriptFile : IComparable<JavaScriptFile>
    {
        internal const int UnsortedOrderValue = -1;
        internal const int ExcludeOrderValue = -2;

        private readonly string _value;

        public JavaScriptFile(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the specified order of the javascript file 
        /// </summary>
        public int Order
        {
            get
            {
                if (!_value.StartsWith("// "))
                    return UnsortedOrderValue;

                if (_value.StartsWith("// exclude"))
                    return ExcludeOrderValue;

                string num = string.Empty;
                int index = 3; // start at index 3 (4th character)

                // parse the numeric value of the sortation
                while (Char.IsNumber(_value[index]))
                {
                    num += _value[index];
                    index++;
                }

                if (string.IsNullOrEmpty(num))
                    return UnsortedOrderValue;

                return int.Parse(num);
            }
        }

        public override string ToString()
        {
            return _value;
        }

        public int CompareTo(JavaScriptFile obj)
        {
            return Order.CompareTo(obj.Order);
        }
    }
}
