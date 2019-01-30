using BzStruc.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BzStruc.Repository.DAL
{

    public class GenericEFRepository<CustomDbContext> : IGenericEFRepository<CustomDbContext>
        where CustomDbContext : IdentityDbContext<GenericUser, GenericRole, int, GenericUserClaim, GenericUserRole, GenericUserLogin, GenericRoleClaim, GenericUserToken>
    {
        private readonly CustomDbContext _msGenericDb;
        public CustomDbContext Context() { return _msGenericDb; }
        public GenericEFRepository(CustomDbContext msGenericDb)
        {
            _msGenericDb = msGenericDb;
        }

        public async Task<Results<List<TbDest>>> GetPagingAsync<TbSource, TbDest>(
           Expression<Func<TbSource, bool>> predicate,
           Expression<Func<TbSource, TbDest>> selector,
           PagingParameters paging) where TbDest : class where TbSource : class
        {
            var query = _msGenericDb.Set<TbSource>().Where(predicate).Select(selector);
            var results = await LinqExtensions.PagingResultProjectionAsync<IQueryable<TbDest>, TbDest>(query, paging);
            var result = await results.Data.ToListAsync();
            var response = LinqExtensions.CreateSuccessResponse(result, results.PageInfo);
            return response;
        }

        public async Task<CustomTable> FirstOrDefaultAsync<CustomTable>(
            Expression<Func<CustomTable, bool>> predicate,
            bool AsNoTracking = false)
            where CustomTable : class
        {
            var query = _msGenericDb.Set<CustomTable>().Where(predicate);
            if (AsNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TSelector> FirstOrDefaultAsync<CustomTable, TSelector>(
            Expression<Func<CustomTable, bool>> predicate,
            Expression<Func<CustomTable, TSelector>> selector,
            bool AsNoTracking = false)
           where CustomTable : class where TSelector : class
        {
            var query = _msGenericDb.Set<CustomTable>().Where(predicate).Select(selector);
            if (AsNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<CustomTable> GetQueryAble<CustomTable>() where CustomTable : class
        {
            return _msGenericDb.Set<CustomTable>();
        }

        public async Task<CustomTable> AddAsync<CustomTable>(CustomTable entity)
            where CustomTable : class
        {
            await _msGenericDb.Set<CustomTable>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<CustomTable>> AddRangeAsync<CustomTable>(IEnumerable<CustomTable> entity)
            where CustomTable : class
        {
            var addAsync = entity as CustomTable[] ?? entity.ToArray();
            await _msGenericDb.Set<CustomTable>().AddRangeAsync(addAsync);
            return addAsync;
        }

        public void Update<CustomTable>(CustomTable entity)
            where CustomTable : class
        {
            _msGenericDb.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateSpecficProperty<CustomTable, TProperty>(CustomTable entity, params Expression<Func<CustomTable, TProperty>>[] properties)
            where CustomTable : class
        {
            if (properties != null && properties.Length > 0)
            {
                _msGenericDb.Entry(entity).State = EntityState.Detached;
                _msGenericDb.Attach(entity);
                foreach (var prop in properties)
                {
                    _msGenericDb.Entry(entity).Property(prop).IsModified = true;
                }
                return;
            }
            _msGenericDb.Entry(entity).State = EntityState.Modified;

        }

        public void UpdateSpecficPropertyMultiType<CustomTable>(CustomTable entity, params Expression<Func<CustomTable, object>>[] properties)
             where CustomTable : class
        {
            if (properties != null && properties.Length > 0)
            {
                _msGenericDb.Entry(entity).State = EntityState.Detached;
                _msGenericDb.Attach(entity);
                foreach (var prop in properties)
                {
                    _msGenericDb.Entry(entity).Property(prop).IsModified = true;
                }
                return;
            }
            _msGenericDb.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _msGenericDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var tran = _msGenericDb.Database.CurrentTransaction;
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw new Exception(GetAllMessages(ex));
            }
        }

        private string GetAllMessages(Exception ex, string separator = "\r\nInnerException: ")
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }

            return ex.Message + separator + GetAllMessages(ex.InnerException, separator);
        }

    }

    public interface IGenericEFRepository<CustomDbContext>
    {
        Task<Results<List<TbDest>>> GetPagingAsync<TbSource, TbDest>(
            Expression<Func<TbSource, bool>> predicate,
            Expression<Func<TbSource, TbDest>> selector,
            PagingParameters paging)
            where TbDest : class where TbSource : class;

        Task<CustomTable> FirstOrDefaultAsync<CustomTable>(
            Expression<Func<CustomTable, bool>> predicate, bool AsNoTracking = false)
            where CustomTable : class;

        Task<TSelector> FirstOrDefaultAsync<CustomTable, TSelector>(
            Expression<Func<CustomTable, bool>> predicate,
            Expression<Func<CustomTable, TSelector>> selector,
            bool AsNoTracking = false)
           where CustomTable : class where TSelector : class;

        IQueryable<CustomTable> GetQueryAble<CustomTable>() where CustomTable : class;

        Task<CustomTable> AddAsync<CustomTable>(CustomTable entity)
             where CustomTable : class;

        Task<IEnumerable<CustomTable>> AddRangeAsync<CustomTable>(IEnumerable<CustomTable> entity)
            where CustomTable : class;

        void Update<CustomTable>(CustomTable entity)
           where CustomTable : class;


        void UpdateSpecficProperty<CustomTable, TProperty>(CustomTable entity, params Expression<Func<CustomTable, TProperty>>[] properties)
           where CustomTable : class;

        void UpdateSpecficPropertyMultiType<CustomTable>(CustomTable entity, params Expression<Func<CustomTable, object>>[] properties)
             where CustomTable : class;

        Task<int> SaveAsync();
    }



    public class Results<DTO>
    {
        public DTO Data { get; set; }
        public List<string> Errors { get; set; }
        public PagingInfo PageInfo { get; set; }
    }

    public class PagingParameters
    {
        public string OrderBy { get; set; } = "Id";
        public bool Asc { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int Top { get; set; } = 0;
    }

    public class PagingInfo
    {
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source,
                     bool asc, params string[] orderByProperties) where T : class
        {
            var command = asc ? "OrderBy" : "OrderByDescending";
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");

            var parts = orderByProperties[0].Split('.');

            Expression parent = parameter;

            foreach (var part in parts)
            {
                parent = Expression.Property(parent, part);
            }
            var orderByExpression = Expression.Lambda(parent, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new[] { type, parent.Type },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static async Task<Results<IQueryable<O>>> PagingResultProjectionAsync<DTO, O>
            (IQueryable<O> query, PagingParameters paging, bool noLimit = false)
            where O : class
        {
            try
            {
                query = LinqExtensions.OrderBy(query, paging.Asc, paging.OrderBy);
                paging.Top = (paging.Top < 0 || paging.Top > 5) ? 5 : paging.Top;
                int total = 0;
                if (paging.Top != 0)
                {
                    paging.PageSize = paging.Top;
                    paging.Page = 1;
                    total = paging.Top;
                    query = query
                        .Take(paging.Top);
                }
                else if (noLimit == false)
                {
                    paging.PageSize = (paging.PageSize > 51 || paging.PageSize < 1) ? 20 : paging.PageSize;
                    paging.Page = (paging.Page < 1) ? 1 : paging.Page;
                    total = await query.CountAsync();
                    query = query
                        .Skip(paging.PageSize * (paging.Page - 1))
                        .Take(paging.PageSize);
                }
                else
                {
                    total = await query.CountAsync();
                    paging.PageSize = total;
                    paging.Page = 1;
                }

                var totalPages = paging.PageSize > 0 ? (int)Math.Ceiling((double)total / paging.PageSize) : 0;
                var pageInfo = new PagingInfo
                {
                    Total = total,
                    TotalPages = totalPages,
                    CurrentPage = paging.Page,
                    PageSize = paging.PageSize
                };

                return CreateSuccessResponse<IQueryable<O>>(query, pageInfo);
            }
            catch (Exception ex)
            {
                var result = new Results<List<DTO>>();
                result.Errors = new List<string> { ex.Message, ex.InnerException?.Message, ex.InnerException?.InnerException?.Message };
                return CreateErrorResponse<IQueryable<O>>("error while get paging", ex);
            }


        }

        public static Results<TResult> CreateSuccessResponse<TResult>(TResult tresult, PagingInfo pagingInfo)
        {
            var result = new Results<TResult>();
            result.Data = tresult;
            result.PageInfo = pagingInfo;
            return result;
        }

        public static Results<TResult> CreateErrorResponse<TResult>(string error, Exception exception = null)
        {
            var result = new Results<TResult>();
            result.Errors = GetInnerExceptionMessage(exception);
            result.Errors.Insert(0, error);
            return result;
        }

        private static List<string> GetInnerExceptionMessage(Exception exception)
        {
            var errorTextList = new List<string>();
            var error = GetSubInnerExceptionMessage(exception);
            while (string.IsNullOrEmpty(error.Trim()))
            {
                errorTextList.Add(error);
                error = GetSubInnerExceptionMessage(exception);
            }
            return errorTextList;
        }

        private static string GetSubInnerExceptionMessage(Exception exception)
        {
            return exception.InnerException?.Message;
        }

    }


}
