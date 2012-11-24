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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Core.ExpressionHelpers;
using ClubStarterKit.Infrastructure.Application;
using StructureMap;

namespace ClubStarterKit.Infrastructure.Cache
{
    /// <summary>
    /// Single item cache
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class SingleItemDataCache<T> : HttpCacheBase<T>
        where T : class, IDataModel
    {
        public SingleItemDataCache(Expression<Func<T, bool>> where, HttpContextBase context = null)
            : base(
                context ?? new HttpContextWrapper(System.Web.HttpContext.Current),
                ObjectFactory.GetInstance<IApplicationIdProvider>())
        {
            if (where == null)
                throw new ArgumentNullException("where");

            Where = where;
            UnitOfWork = null;
            Filter = x => x;
            CheckOtherCaches = true;
        }

        public Expression<Func<T, bool>> Where { get; protected set; }

        /// <summary>
        /// When flagged as true, an attempt is made to locate the singular item
        /// from another cache, removing the need to grab the value from the data
        /// persistance store
        /// </summary>
        public bool CheckOtherCaches { get; set; }

        public Expression<Func<IRepository<T>, IRepository<T>>> Filter { get; protected set; }

        /// <summary>
        /// Filter the repository
        /// </summary>
        /// <param name="filter">Repository filter</param>
        /// <remarks>Overrides a current filter if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public SingleItemDataCache<T> With(Expression<Func<IRepository<T>, IRepository<T>>> filter)
        {
            Filter = filter;
            return this;
        }

        public IUnitOfWork UnitOfWork { get; protected set; }

        /// <summary>
        /// Constrain loading by a given <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work to load entities</param>
        /// <remarks>Overrides a current <see cref="IUnitOfWork"/> if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public SingleItemDataCache<T> LoadedBy(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            return this;
        }

        public override string ContentType
        {
            get 
            {
                string str_where = string.Empty;

                if (Where != null)
                    str_where = new FieldVisitor().Visit(Where).ToString();

                return typeof(T).FullName + "_" + str_where + "_SingleItem"; 
            }
        }

        protected override T Grab()
        {
            T value;

            if (UnitOfWork == null)
            {
                using (var scope = new UnitOfWorkScope())
                    value = GetValue(scope.UnitOfWork);
            }
            else
                value = GetValue(UnitOfWork);

            return value;
        }

        protected virtual T GetValue(IUnitOfWork unitOfWork)
        {
            if (CheckOtherCaches)
            {
                // pull from cache
                var fromCaches = FindFromCaches();

                // if there is a returned value, return it 
                // (looks like we saved a db call)
                if (fromCaches != null && !fromCaches.IsNothing)
                    return fromCaches.Return();
            }

            var repository = unitOfWork.RepositoryFor<T>();

            if (Filter != null)
                repository = Filter.Compile()(repository);

            return repository.FirstOrDefault(Where);
        }

        /// <summary>
        /// Query the current cache for the singular item that 
        /// satisfies the condition
        /// </summary>
        /// <returns>Possible value from the current cache</returns>
        protected virtual Maybe<T> FindFromCaches()
        {
            var keys = CacheKeyStore.KeysFor<T>();

            if (keys == null || keys.Count == 0)
                return Maybe<T>.Nothing;

            var where = Where.Compile();

            foreach (var key in keys)
            {
                var value = HttpCache[key];

                if (value == null) // something expired
                    break;

                // attempt to cast as singular object
                var valueAsT = value as T;
                if (valueAsT != null && where(valueAsT))
                    return valueAsT;

                // attempt to cast as list of T
                var valueAsEnumerable = value as IEnumerable<T> ?? new List<T>();
                var possibility = valueAsEnumerable.FirstOrDefault(where);

                // item found
                if (possibility != null)
                    return possibility;
            }

            return Maybe<T>.Nothing;
        }
    }
}