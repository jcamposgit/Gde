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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Core.ExpressionHelpers;
using NHibernate;
using NHibernate.Linq;

namespace ClubStarterKit.Data.NHibernate
{
    public class NHRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : IDataModel
    {
        private readonly List<string> _expands = new List<string>();
        private readonly ISession _privateSession;
        private string _cachedQueryName;
        private bool _enableCached;
        public NHRepository()
        {
        }

        public NHRepository(ISession session)
        {
            _privateSession = session;
        }

        internal ISession Session
        {
            get { return _privateSession ?? GetUnitOfWork<NHUnitOfWork>().Session; }
        }

        protected override IQueryable<TEntity> RepositoryQuery
        {
            get
            {
                INHibernateQueryable<TEntity> query = Session.Linq<TEntity>();
                var nhQuery = query as INHibernateQueryable;
                if (_expands.Count > 0)
                    _expands.ForEach(x => query.Expand(x));
                
                
                if (_enableCached)
                {
                    nhQuery.QueryOptions.SetCachable(true);
                    nhQuery.QueryOptions.SetCacheMode(CacheMode.Normal);
                    nhQuery.QueryOptions.SetCacheRegion(_cachedQueryName);
                }

                _enableCached = false;
                _cachedQueryName = null;
                return query;
            }
        }

        public override void Save(TEntity entity)
        {
            if (entity.Id == 0)
                Session.Save(entity);
            else
                Session.Update(entity, entity.Id);
        }

        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public override void Detach(TEntity entity)
        {
            Session.Evict(entity);
        }

        public override void Attach(TEntity entity)
        {
            Session.Lock(entity, LockMode.None);
        }

        public override void Refresh(TEntity entity)
        {
            Session.Refresh(entity, LockMode.None);
        }

        /// <summary>
        /// Instructs the repository to eager load a child entities. 
        /// </summary>
        /// <param name="path">The path of the child entities to eager load.</param>
        /// <remarks>Implementors should throw a <see cref="NotSupportedException"/> if the underling provider
        /// does not support eager loading of entities</remarks>
        public override IRepository<TEntity> With(Expression<Func<TEntity, object>> path)
        {
            return With<TEntity>(path);
        }

        /// <summary>
        /// Instructs the repository to eager load entities that may be in the repository's association path.
        /// </summary>
        /// <param name="path">The path of the child entities to eager load.</param>
        /// <remarks>Implementors should throw a <see cref="NotSupportedException"/> if the underling provider
        /// does not support eager loading of entities</remarks>
        public override IRepository<TEntity> With<T>(Expression<Func<T, object>> path)
        {
            if(path == null)
                throw new ArgumentNullException("path");
            var visitor = new MemberAccessPathVisitor();
            visitor.Visit(path);
            if (typeof(T) == typeof(TEntity))
                _expands.Add(visitor.Path);
            else
            {
                //The path represents an collection association. Find the property on the target type that
                //matches a IEnumerable<T> property.
                var pathExpression = visitor.Path;
                var targetType = typeof(TEntity);
                var matchesType = typeof(IEnumerable<T>);
                var targetProperty = (from property in targetType.GetProperties()
                                      where matchesType.IsAssignableFrom(property.PropertyType)
                                      select property).FirstOrDefault();
                if (targetProperty != null)
                    pathExpression = string.Format("{0}.{1}", targetProperty.Name, pathExpression);
                _expands.Add(pathExpression);
            }
            return this;
        }

        public override void Delete<T>(IPropertyValuePair<T> where)
        {
            string hql_query = string.Format("delete {0} v where v.{1} = :val", typeof(TEntity).Name, where.Property);
            Session.CreateQuery(hql_query).SetParameter<T>("val", where.Value).ExecuteUpdate();


            //Session.Delete(hql_query, where.Value);

            //Session.CreateSQLQuery(string.Format("delete from {0} where {1} = :val", typeof(TEntity).Name, where.Property)).SetParameter<T>("val", where.Value).ExecuteUpdate();
            //Session.CreateSQLQuery(string.Format("DELETE FROM {0} WHERE ({1} = :val)", typeof(TEntity).Name, where.Property)).SetParameter<T>("val", where.Value).ExecuteUpdate();


        }

        public override void Update(IEnumerable<IPropertyValuePair<object>> set, IEnumerable<IPropertyValuePair<object>> where)
        {
            //update Customer set name = :newName where name = :oldName
            var hql_query = string.Format("update {0} set {1} where {2}", typeof(TEntity).Name, ParsePairs(set, 's'), ParsePairs(where, 'w'));

            IQuery query = Session.CreateQuery(hql_query);

            // set the parameter values
            int index = 0;
            set.Foreach(s =>
            {
                query = query.SetParameter('s'.ToString() + index, s.Value);
                index++;
            });

            index = 0;
            where.Foreach(w =>
            {
                query = query.SetParameter('w'.ToString() + index, w.Value);
                index++;
            });

            // execute the query
            query.ExecuteUpdate();
        }

        private string ParsePairs(IEnumerable<IPropertyValuePair<object>> pairs, char uniqueChar)
        {
            int count = pairs.Count();
            if (pairs == null || count == 0)
                throw new ArgumentException("pairs connot be empty");

            int index = 0;
            string returnString = "";
            pairs.Foreach(setValue =>
            {
                returnString += setValue.Property + " = :" + uniqueChar + index.ToString();

                // add a comma if the count isn't 1 and
                // we're not adding the last value
                if (index != count - 1 && count != 1)
                    returnString += ", ";

                index++;
            });

            return returnString;
        }
    }
}