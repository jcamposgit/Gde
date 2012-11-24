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
    /// Paged list cache for a given entity type
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class PagedDataCache<T> : HttpCacheBase<IPagedList<T>>
        where T : IDataModel
    {
        public PagedDataCache(HttpContextBase context = null)
            : base(context ?? new HttpContextWrapper(System.Web.HttpContext.Current), 
                   ObjectFactory.GetInstance<IApplicationIdProvider>())
        {
            Where = x => true;
            PageSize = 25;
            Index = 0;
            Filter = x => x;
        }

        public Expression<Func<IRepository<T>, IRepository<T>>> Filter { get; private set; }

        /// <summary>
        /// Filter the repository
        /// </summary>
        /// <param name="filter">Repository filter</param>
        /// <remarks>Overrides a current filter if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> With(Expression<Func<IRepository<T>, IRepository<T>>> filter)
        {
            Filter = filter;
            return this;
        }

        public Expression<Func<T, bool>> Where { get; private set; }

        /// <summary>
        /// Constrain the output to only include values that satisfy a predicated
        /// </summary>
        /// <param name="predicate"></param>
        /// <remarks>Overrides a current constraint if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> Only(Expression<Func<T, bool>> where)
        {
            Where = where;
            return this;
        }

        public ISortation<T, object> SortBy { get; private set; }

        /// <summary>
        /// Adds a sortation constraint for the query
        /// </summary>
        /// <param name="sortation">Property sortation</param>
        /// <param name="ascending">Whether the sortation is ascending</param>
        /// <remarks>Overrides a current sortation if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> Sorted(Expression<Func<T, object>> sortation, bool ascending = true)
        {
            return Sorted(new Sortation<T>(sortation, ascending));
        }

        /// <summary>
        ///  Adds a sortation constraint for the query
        /// </summary>
        /// <param name="sortation">Sortation value</param>
        /// <remarks>Overrides a current sortation if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> Sorted(ISortation<T, object> sortation)
        {
            SortBy = sortation;
            return this;
        }

        public int Index { get; private set; }

        /// <summary>
        /// Sets the page index
        /// </summary>
        /// <param name="index"></param>
        /// <remarks>
        /// Overrides a current page index if it exists.
        /// 
        /// If index is invalid, a fallback of 0 will be used
        /// as the index
        /// </remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> OnPage(int index)
        {
            Index = index < 0 ? 0 : index;
            return this;
        }

        public int PageSize { get; private set; }

        /// <summary>
        /// Sets the page size
        /// </summary>
        /// <param name="size">Size of the page</param>
        /// <remarks>Overrides previous page size.
        /// If the page size is invalid, a page size of 1 is the fallback.
        /// </remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> WithPageSize(int size)
        {
            PageSize = size < 1 ? 1 : size;
            return this;
        }

        public IUnitOfWork UnitOfWork { get; protected set; }

        /// <summary>
        /// Constrain loading by a given <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work to load entities</param>
        /// <remarks>Overrides a current <see cref="IUnitOfWork"/> if it exists</remarks>
        /// <returns>Instance of the data cache (for ease of chainability)</returns>
        public PagedDataCache<T> LoadedBy(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            return this;
        }

        public override string ContentType
        {
            get 
            {
                string str_where = string.Empty;
                if(Where != null)
                    str_where = new FieldVisitor().Visit(Where).ToString();

                return typeof(T).FullName + "_" + str_where + "_Paged_" + Index + "_" + PageSize; 
            }
        }

        protected override IPagedList<T> Grab()
        {
            IPagedList<T> value;

            if (UnitOfWork == null)
            {
                using (var scope = new UnitOfWorkScope())
                    value = GetValue(scope.UnitOfWork);
            }
            else
                value = GetValue(UnitOfWork);

            return value;
        }

        protected virtual IPagedList<T> GetValue(IUnitOfWork unitOfWork)
        {
            if (Filter == null)
                Filter = x => x;

            if (Where == null)
                Where = x => true;

            var repo = Filter.Compile().Invoke(unitOfWork.RepositoryFor<T>());

            IQueryable<T> query = repo.Where(Where);

            if(SortBy != null)
                query = SortBy.Ascending ? query.OrderBy(SortBy.SortBy) : 
                                           query.OrderByDescending(SortBy.SortBy);
            
            return query.ToPagedList(Index, PageSize);
        }
    }
}
