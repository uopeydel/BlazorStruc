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

    public abstract class GenericEFRepository<CustomDbContext, CustomTable>
        where CustomTable : class
        where CustomDbContext : IdentityDbContext<GenericUser, GenericRole, int, GenericUserClaim, GenericUserRole, GenericUserLogin, GenericRoleClaim, GenericUserToken>
    {
        public readonly CustomDbContext _msGenericDb;
        public CustomDbContext context() { return _msGenericDb; }
        public GenericEFRepository(CustomDbContext msGenericDb)
        {
            _msGenericDb = msGenericDb;
        }

        public async Task<object> AddGeneric<IAdgeneric>(Expression<Func<IAdgeneric, bool>> predicate) where IAdgeneric : class
        {
            var iii = await _msGenericDb.Set<IAdgeneric>().Where(predicate).ToListAsync();
            return iii;
        }


        public async Task<object> TestGet(Expression<Func<CustomTable, bool>> predicate)
        {
            var iii = await _msGenericDb.Set<CustomTable>().Where(predicate).ToListAsync();
            return iii;
        }

        //public virtual async Task<Results<IQueryable<O>>> PagingResultProjectionAsync<DTO, O>(IQueryable<O> query, PagingParameters paging, bool noLimit = false) where O : class
        //{
        //    try
        //    {
        //        query = LinqExtensions.OrderBy(query, paging.asc, paging.orderBy);
        //        paging.top = (paging.top < 0 || paging.top > 5) ? 5 : paging.top;
        //        int total = 0;
        //        if (paging.top != 0)
        //        {
        //            paging.pageSize = paging.top;
        //            paging.page = 1;
        //            total = paging.top;
        //            query = query
        //                .Take(paging.top);
        //        }
        //        else if (noLimit == false)
        //        {
        //            paging.pageSize = (paging.pageSize > 51 || paging.pageSize < 1) ? 20 : paging.pageSize;
        //            paging.page = (paging.page < 1) ? 1 : paging.page;
        //            total = await query.CountAsync();
        //            query = query
        //                .Skip(paging.pageSize * (paging.page - 1))
        //                .Take(paging.pageSize);
        //        }
        //        else
        //        {
        //            total = await query.CountAsync();
        //            paging.pageSize = total;
        //            paging.page = 1;

        //        }

        //        var totalPages = paging.pageSize > 0 ? (int)Math.Ceiling((double)total / paging.pageSize) : 0;
        //        var pageInfo = new PagingInfo
        //        {
        //            total = total,
        //            totalPages = totalPages,
        //            currentPage = paging.page,
        //            pageSize = paging.pageSize
        //        };

        //        return CreateSuccessResponse<IQueryable<O>>(query, pageInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new Results<List<DTO>>();
        //        result.errors = new List<string> { ex.Message, ex.InnerException?.Message, ex.InnerException?.InnerException?.Message };
        //        return CreateErrorResponse<IQueryable<O>>("error while get paging", ex);
        //    }
        //}


        //public Results<TResult> CreateSuccessResponse<TResult>(TResult tresult, PagingInfo pagingInfo)
        //{
        //    var result = new Results<TResult>();
        //    result.results = tresult;
        //    result.pageInfo = pagingInfo;
        //    return result;
        //}

        //public Results<TResult> CreateErrorResponse<TResult>(string error, Exception exception = null)
        //{
        //    var result = new Results<TResult>();
        //    result.errors = GetInnerExceptionMessage(exception);
        //    result.errors.Insert(0, error);
        //    return result;
        //}

        //private List<string> GetInnerExceptionMessage(Exception exception)
        //{
        //    var errorTextList = new List<string>();
        //    var error = GetSubInnerExceptionMessage(exception);
        //    while (string.IsNullOrEmpty(error.Trim()))
        //    {
        //        errorTextList.Add(error);
        //        error = GetSubInnerExceptionMessage(exception);
        //    }
        //    return errorTextList;
        //}

        //private string GetSubInnerExceptionMessage(Exception exception)
        //{
        //    return exception.InnerException?.Message;
        //}

    }

    public interface IGenericEFRepository
    {
        //MsSqlGenericDb context();
        Task TestGet();
    }



    public class Results<DTO>
    {
        public DTO results { get; set; }
        public List<string> errors { get; set; }
        public PagingInfo pageInfo { get; set; }
    }

    public class PagingParameters
    {
        public string orderBy { get; set; } = "id";
        public bool asc { get; set; } = true;
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 20;
        public int top { get; set; } = 0;
    }

    public class PagingInfo
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
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
                query = LinqExtensions.OrderBy(query, paging.asc, paging.orderBy);
                paging.top = (paging.top < 0 || paging.top > 5) ? 5 : paging.top;
                int total = 0;
                if (paging.top != 0)
                {
                    paging.pageSize = paging.top;
                    paging.page = 1;
                    total = paging.top;
                    query = query
                        .Take(paging.top);
                }
                else if (noLimit == false)
                {
                    paging.pageSize = (paging.pageSize > 51 || paging.pageSize < 1) ? 20 : paging.pageSize;
                    paging.page = (paging.page < 1) ? 1 : paging.page;
                    total = await query.CountAsync();
                    query = query
                        .Skip(paging.pageSize * (paging.page - 1))
                        .Take(paging.pageSize);
                }
                else
                {
                    total = await query.CountAsync();
                    paging.pageSize = total;
                    paging.page = 1; 
                }

                var totalPages = paging.pageSize > 0 ? (int)Math.Ceiling((double)total / paging.pageSize) : 0;
                var pageInfo = new PagingInfo
                {
                    total = total,
                    totalPages = totalPages,
                    currentPage = paging.page,
                    pageSize = paging.pageSize
                };

                return CreateSuccessResponse<IQueryable<O>>(query, pageInfo);
            }
            catch (Exception ex)
            {
                var result = new Results<List<DTO>>();
                result.errors = new List<string> { ex.Message, ex.InnerException?.Message, ex.InnerException?.InnerException?.Message };
                return CreateErrorResponse<IQueryable<O>>("error while get paging", ex);
            }


        }

        public static Results<TResult> CreateSuccessResponse<TResult>(TResult tresult, PagingInfo pagingInfo)
        {
            var result = new Results<TResult>();
            result.results = tresult;
            result.pageInfo = pagingInfo;
            return result;
        }

        public static Results<TResult> CreateErrorResponse<TResult>(string error, Exception exception = null)
        {
            var result = new Results<TResult>();
            result.errors = GetInnerExceptionMessage(exception);
            result.errors.Insert(0, error);
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
