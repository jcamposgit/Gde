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

namespace ClubStarterKit.Core
{
    public abstract class Disposable : IDisposable
    {
        public bool IsDisposed { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Event triggered by the Dispose of an object
        /// </summary>
        public event EventHandler Disposed;

        protected void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                    DisposeManagedResources();

                DisposeUnmanagedResources();

                if (Disposed != null)
                    Disposed(this, EventArgs.Empty);

                IsDisposed = true;
            }
        }

        protected void ThrowExceptionIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}