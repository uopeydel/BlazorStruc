using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Server.UseGraphQL
{
    //https://developer.okta.com/blog/2019/04/16/graphql-api-with-aspnetcore
    public class GenericUserQlQuery : ObjectGraphType
    {
        public GenericUserQlQuery(MsSql1DbContext db)
        {
            Field<GenericUserQl>(
              "GenericUser",
              arguments:
              new QueryArguments(
                  new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the User." },
                  new QueryArgument<IdGraphType> { Name = "firstName", Description = "The FirstName of the User." }
              ),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  var firstName = context.GetArgument<string>("firstName");
                  var user =
                  db.User.Include(a => a.Character).FirstOrDefault();
                  return user;
              });

            Field<ListGraphType<GenericUserQl>>(
              "GenericUsers",
              arguments:
              new QueryArguments(
                  new QueryArgument<IdGraphType> { Name = "firstName", Description = "The FirstName of the User." }
              ),
              resolve: context =>
              {
                  var firstName = context.GetArgument<string>("firstName");
                  var users = db.User.Where(w => w.FirstName == firstName).Include(a => a.Character);
                  return users;
              });
        }

      
    }

}
