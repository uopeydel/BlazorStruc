using BzStruc.Repository.DAL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Web.Server.UseGraphQL
{
    //https://developer.okta.com/blog/2019/04/16/graphql-api-with-aspnetcore
    public class CharacterQlQuery : ObjectGraphType
    {
        public CharacterQlQuery(MsSql1DbContext db)
        {
            Field<CharacterQl>(
              "Character",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Character." }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  var character =
                  db
                  .Character
                  .Include(a => a.User)
                  .FirstOrDefault(i => i.Id == id);
                  return character;
              });

            Field<ListGraphType <CharacterQl>>(
              "Characters",
              resolve: context =>
              {
                  var character = db.Character.Include(a => a.User);
                  return character;
              });
        }
    }

}
