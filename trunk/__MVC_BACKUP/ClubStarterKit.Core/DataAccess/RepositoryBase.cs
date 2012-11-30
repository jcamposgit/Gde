#region license

//Copyright 2008 Ritesh Rao 

//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ClubStarterKit.Core.DataAccess
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : IDataModel
    {
        #region UnitOfWork

        protected virtual T GetUnitOfWork<T>()
            where T : IUnitOfWork
        {
            if (!UnitOfWork.InSession) throw new InvalidOperationException("There is no current unit of work.");

            if (!(UnitOfWork.Current is T))
                throw new InvalidOperationException("The current unit of work is not of the specified type");
            return ((T) UnitOfWork.Current);
        }

        #endregion

        #region IQueryable

        public virtual Expression Expression
        {
            get { return RepositoryQuery.Expression; }
        }

        public virtual Type ElementType
        {
            get { return RepositoryQuery.ElementType; }
        }

        public virtual IQueryProvider Provider
        {
            get { return RepositoryQuery.Provider; }
        }

        #endregion

        #region IEnumerable

        public virtual IEnumerator<TEntity> GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        #endregion

        #region Abstract 

        protected abstract IQueryable<TEntity> RepositoryQuery { get; }

        public abstract void Save(TEntity entity);

        public abstract void Delete(TEntity entity);

        public abstract void Detach(TEntity entity);

        public abstract void Attach(TEntity entity);

        public abstract void Refresh(TEntity entity);

        public abstract IRepository<TEntity> With(Expression<Func<TEntity, object>> path);

        public abstract IRepository<TEntity> With<T>(Expression<Func<T, object>> path);

        public abstract void Delete<T>(IPropertyValuePair<T> where);

        public abstract void Update(IEnumerable<IPropertyValuePair<object>> set, IEnumerable<IPropertyValuePair<object>> where);
        #endregion

        #region IRepository<TEntity> Members

        public virtual void Delete<T>(Expression<Func<TEntity, T>> prop, T value)
        {
            IPropertyValuePair<T> vp = new ExpressionValuePair<TEntity, T>(prop, value);
            Delete(vp);
        }

        public virtual void Delete(int id)
        {
            Delete(x => x.Id, id);
        }

        public virtual void Update<TSet, TWhere>(IPropertyValuePair<TSet> set, IPropertyValuePair<TWhere> where)
        {

            var setList = new List<IPropertyValuePair<object>>
            {
                ToObjectPropertyPair(set)
            };

            var whereList = new List<IPropertyValuePair<object>>
            {
                ToObjectPropertyPair(where)
            };

            Update(setList, whereList);
        }

        protected virtual IPropertyValuePair<object> ToObjectPropertyPair<T>(IPropertyValuePair<T> pair)
        {
            return new PropertyValuePair<object>(pair.Property, pair.Value);
        }

        #endregion

    }
}