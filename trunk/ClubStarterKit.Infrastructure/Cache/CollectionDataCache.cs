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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Core.ExpressionHelpers;
using ClubStarterKit.Infrastructure.Application;
using StructureMap;

namespace ClubStarterKit.Infrastructure.Cache
{
    /// <summary>
    /// Collection data cache for an entire collection of entities in the data store
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionDataCache<T> : CollectionDataCache<T, T>
        where T : IDataModel
    {
        public CollectionDataCache(HttpContextBase context = null)
            : base(x => x, context)
        { }
    }

    /// <summary>
    /// Collection data cache for an entire collection of entities in the data store
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TOutput">Output type (projection)</typeparam>
    public class CollectionDataCache<T, TOutput> : HttpCacheBase<IEnumerable<TOutput>>
        where T: IDataModel
    {
        public CollectionDataCache(Expression<Func<T, TOutput>> select, HttpContextBase context = null)
            : base(context ?? new HttpContextWrapper(System.Web.HttpContext.Current), ObjectFactory.GetInstance<IApplicationIdProvider>())
        {
            if (select == null)
                throw new ArgumentNullException("select");

            Select = select;
            Where = x => true;
        }

        public Expression<Func<T, TOutput>> Select { get; protected set; }

        public Expression<Func<T, bool>> Where { get; protected set; }

        /// <summary>
        /// Constrain the output to only include values that satisfy a predicated
        /// </summary>
        /// <param name="predicate"></param>
        /// <remarks>Overrides a current constraint if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public CollectionDataCache<T, TOutput> Only(Expression<Func<T, bool>> predicate)
        {
            Where = predicate;
            return this;
        }

        public Expression<Func<IRepository<T>, IRepository<T>>> Filter { get; protected set; }

        /// <summary>
        /// Filter the repository
        /// </summary>
        /// <param name="filter">Repository filter</param>
        /// <remarks>Overrides a current filter if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public CollectionDataCache<T, TOutput> With(Expression<Func<IRepository<T>, IRepository<T>>> filter)
        {
            Filter = filter;
            return this;
        }

        public ISortation<T, object> SortBy { get; protected set; }

        /// <summary>
        /// Adds a sortation constraint for the query
        /// </summary>
        /// <param name="sortation">Property sortation</param>
        /// <param name="ascending">Whether the sortation is ascending</param>
        /// <remarks>Overrides a current sortation if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public CollectionDataCache<T, TOutput> Sorted(Expression<Func<T, object>> sortation, bool ascending = true)
        {
            return Sorted(new Sortation<T>(sortation, ascending));
        }

        /// <summary>
        ///  Adds a sortation constraint for the query
        /// </summary>
        /// <param name="sortation">Sortation value</param>
        /// <remarks>Overrides a current sortation if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public CollectionDataCache<T, TOutput> Sorted(ISortation<T, object> sortation)
        {
            SortBy = sortation;
            return this;
        }

        public IUnitOfWork UnitOfWork { get; protected set; }

        /// <summary>
        /// Constrain loading by a given <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work to load entities</param>
        /// <remarks>Overrides a current <see cref="IUnitOfWork"/> if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public CollectionDataCache<T, TOutput> LoadedBy(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            return this;
        }

        public override string ContentType
        {
            get 
            {
                string str_where = new FieldVisitor().Visit(Where).ToString();
                return typeof(T).FullName + "_" + str_where + "_Collection"; 
            }
        }

        protected override IEnumerable<TOutput> Grab()
        {
            IEnumerable<TOutput> value;

            if (UnitOfWork == null)
            {
                using (var scope = new UnitOfWorkScope())
                    value = GetValue(scope.UnitOfWork);
            }
            else
                value = GetValue(UnitOfWork);

            return value;
        }

        protected virtual IEnumerable<TOutput> GetValue(IUnitOfWork unitOfWork)
        {
            var repo = unitOfWork.RepositoryFor<T>();

            // filter repository
            if (Filter != null)
                repo = Filter.Compile()(repo);
            
            // constrain
            IQueryable<T> query = Where != null ? repo.Where(Where) : repo;

            if (SortBy != null)
                query = SortBy.Ascending ? query.OrderBy(SortBy.SortBy) :
                                           query.OrderByDescending(SortBy.SortBy);
            
            // NOTE: we need to call ToList and note AsEnumerable to avoid
            //       lazy loading
            return query.ToList().Select(Select.Compile());
        }
    }
}