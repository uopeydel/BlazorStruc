using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server.UseGraphQL.Contract
{
    //public class QlPagingQueryable
    //{
    //}
    //public class PagingParameters
    //{ 
    //    public string orderBy { get; set; } = "id";
    //    public bool asc { get; set; } = true;
    //    public int page { get; set; } = 1;
    //    public int pageSize { get; set; } = 20;
    //    public int top { get; set; } = 0;
    //}

    //public virtual async Task<Results<List<O>>> PagingQueryableAsync<O>(IQueryable<O> query, PagingParameters paging) where O : class
    //{
    //    try
    //    { 
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
    //        else
    //        {
    //            paging.pageSize = (paging.pageSize > 51 || paging.pageSize < 1) ? 20 : paging.pageSize;
    //            paging.page = (paging.page < 1) ? 1 : paging.page;
    //            total = await query.CountAsync();
    //            query = query
    //                .Skip(paging.pageSize * (paging.page - 1))
    //                .Take(paging.pageSize);
    //        }
    //        var results = await query;
    //        return getResults<O>(results, paging, total);
    //    }
    //    catch (Exception ex)
    //    {
    //        return createErrorResponse<List<O>>(GetAllMessages(ex));
    //    }
    //}

    //public virtual Results<List<DTO>> getResults<DTO>(List<DTO> results, PagingParameters paging, int total)
    //{
    //    var totalPages = paging.pageSize > 0 ? (int)Math.Ceiling((double)total / paging.pageSize) : 0;
    //    var pageInfo = new PagingInfo
    //    {
    //        total = total,
    //        totalPages = totalPages,
    //        currentPage = paging.page,
    //        pageSize = paging.pageSize
    //    };
    //    return createSuccessResponse(results, pageInfo);
    //}

    //public class Results<DTO>
    //{
    //    public DTO results { get; set; }
    //    public List<string> errors { get; set; }
    //    public PagingInfo pageInfo { get; set; }
    //}
    //public class PagingInfo
    //{
    //    public int total { get; set; }
    //    public int totalPages { get; set; }
    //    public int pageSize { get; set; }
    //    public int currentPage { get; set; }
    //}

    //public virtual Results<DTO> createSuccessResponse<DTO>(DTO results, PagingInfo pageInfo = null)
    //{
    //    Results<DTO> pagedResult = new Results<DTO>
    //    {
    //        results = results,
    //        pageInfo = pageInfo
    //    };
    //    return pagedResult;
    //}
}
