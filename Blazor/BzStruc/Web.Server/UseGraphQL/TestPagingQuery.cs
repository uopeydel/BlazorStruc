using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server.UseGraphQL
{
    public class TestPagingQuery : ObjectGraphType
    {
        public TestPagingQuery(MsSql1DbContext db)
        {
            Name = "TestPaging";
            Field<PageInfoType>(
                 "paginatedCompany",
                 arguments: new QueryArguments(
                     new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page", Description = "page of the list" },
                     new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "size", Description = "size of the list returned" },
                     new QueryArgument<StringGraphType> { Name = "q", Description = "filter by name" }
                 ),


                 resolve: context =>
                 {
                     var result = GetGenericUsersAsync(db,
                     context.GetArgument<int>("page"),
                     context.GetArgument<int>("size"),
                     context.GetArgument<string>("q"));

                     return result;
                 }
             );
        }


        public Task<PageInfo<GenericUser>> GetGenericUsersAsync(MsSql1DbContext db, int page, int size, string q = "")
        {
            IEnumerable<GenericUser> filtered;
            //if (!string.IsNullOrEmpty(q))
            //{
            //    filtered = db.Users.Where(xx => xx..ToUpper().Contains(q.ToUpper()));
            //}
            //else
            //{
            //    filtered = _companies;
            //}
            PageInfo<GenericUser> comp = new PageInfo<GenericUser>(db.Users, page, size);
            //    comp.Chars = filtered.Select(xx => xx.user.ToUpper().Remove(1)).OrderBy(xx => xx).Distinct();
            return Task.FromResult(comp);
        }


    }



    //public class CompanyType : ObjectGraphType<GenericUser>
    //{
    //    public CompanyType()
    //    {
    //        Name = "companyType";
    //        Field(x => x.FirstName);
    //        Field(x => x.LastName);
    //        Field(x => x.Id);
    //    }
    //}

    public class PageInfoType : ObjectGraphType<PageInfo<GenericUser>>
    {
        public PageInfoType()
        {
            Name = "PageInfoType";
            Field<ListGraphType<GenericUserQl>>(
                "users",
                resolve: context => context.Source.List
            );
            Field(x => x.Chars);
            Field(x => x.PageCount);
            Field(x => x.Size);
            Field(x => x.TotalCount);
        }
    }


    public class PageInfo<T>
    {
        public int TotalCount { get; private set; }
        public int Size { get; private set; }
        public int PageCount { get; private set; }
        public IEnumerable<T> List { get; private set; }
        public IEnumerable<string> Chars { get; set; }
        //private IEnumerable<T> OriginalList { get; set; }
        public PageInfo(IEnumerable<T> list, int page, int size)
        {
            //OriginalList = list;//for a-z filters
            List = list.Skip((page - 1) * size).Take(size);
            TotalCount = list.Count();
            PageCount = Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDouble(size)));
            Size = size;
        }
    }

}
