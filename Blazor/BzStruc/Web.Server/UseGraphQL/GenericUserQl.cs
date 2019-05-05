using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BzStruc.Repository.Models;
using GraphQL.Types;

namespace Web.Server.UseGraphQL
{
    public class GenericUserQl : ObjectGraphType<GenericUser>
    {
        public GenericUserQl()
        {
            Name = "GenericUser";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id desc");
            Field(x => x.FirstName).Description("FirstName desc");
            Field(x => x.Email).Description("Email desc");

            Field(
              type: typeof(ListGraphType<CharacterQl>),
              name: "Characters",
              arguments: new QueryArguments(
                  new QueryArgument<StringGraphType> { Name = "name" },
                  new QueryArgument<IntGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var name = context.GetArgument<string>("name");
                  var id = context.GetArgument<int>("id");
                  return context.Source.Character.Where(w => string.IsNullOrEmpty(name) || w.Name == name);
              });
           
        }
    }
     
    ///  
    public class PageInfoType : ObjectGraphType<PageInfo<GenericUser>>
    {
        public PageInfoType()
        {
            Field<ListGraphType<GenericUserQl>>(
                  "GenericUserPaging",
                  resolve: context => context.Source.DataList
              );
            Field(xx => xx.Chars);
            Field(xx => xx.PageCount);
            Field(xx => xx.Size);
            Field(xx => xx.TotalCount);
            Field(xx => xx.DataList);
        }
    }

    public class PageInfo<T>
    {
        public int TotalCount { get; private set; }
        public int Size { get; private set; }
        public int PageCount { get; private set; }
        public IEnumerable<T> DataList { get; private set; }
        public IEnumerable<string> Chars { get; set; }
        //private IEnumerable<T> OriginalList { get; set; }
        public PageInfo(IEnumerable<T> list, int page, int size)
        {
            //OriginalList = list;//for a-z filters
            DataList = list.Skip((page - 1) * size).Take(size);
            TotalCount = list.Count();
            PageCount = Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDouble(size)));
            Size = size;
        }
    }


}
