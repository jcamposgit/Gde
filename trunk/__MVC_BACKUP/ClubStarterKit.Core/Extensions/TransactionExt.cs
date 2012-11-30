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

namespace ClubStarterKit.Core.Extensions
{
    internal static class TransactionExt
    {
        internal static System.Data.IsolationLevel MapToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return System.Data.IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return System.Data.IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return System.Data.IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return System.Data.IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return System.Data.IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return System.Data.IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return System.Data.IsolationLevel.Unspecified;
                default:
                    return System.Data.IsolationLevel.ReadCommitted; //default;
            }
        }
    }
}
