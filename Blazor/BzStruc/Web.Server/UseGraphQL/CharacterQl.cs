using BzStruc.Repository.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server.UseGraphQL
{
    public class CharacterQl : ObjectGraphType<Character>
    {
        public CharacterQl()
        {
            Name = "Character";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id desc");
            Field(x => x.Name).Description("Name desc");
            Field(x => x.CreatedAt).Description("Created At date");
            Field(x => x.UpdateAt).Description("Update At date");
            //Field(x => x.User  ,type: typeof(ListGraphType<GenericUserQl>)).Description("Update At date");
          
        }
    }


    
}
