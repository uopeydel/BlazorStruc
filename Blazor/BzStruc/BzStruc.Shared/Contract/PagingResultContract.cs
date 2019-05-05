using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Shared.Contract
{

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
}
