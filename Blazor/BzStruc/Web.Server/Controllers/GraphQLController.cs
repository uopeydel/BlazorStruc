using BzStruc.Repository.DAL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Server.UseGraphQL;
using Web.Server.UseGraphQL.Contract;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly List<string> entityTypes;
        private readonly MsSql1DbContext _db;
        private readonly List<QlQueryName> QlQueries = new List<QlQueryName>();
        public GraphQLController(MsSql1DbContext db)
        {
            _db = db;

            entityTypes = _db.Model.GetEntityTypes().Select(s => s.Name.Split(".").LastOrDefault()).ToList();
            QlQueries = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => x.Name.EndsWith("QlQuery") && typeof(ObjectGraphType).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => new QlQueryName { FullName = x.FullName, Name = x.Name }).ToList();
        }

        [HttpPost]
        [Route("{QlQuery}")]
        public async Task<IActionResult> GenericUser([FromRoute]string QlQuery, [FromBody] GraphQLQuery query)
        {
            var QlQueryFullName = QlQueries.Where(w => w.Name.ToLower().Equals(($"{QlQuery}QlQuery").ToLower())).Select(s => s.FullName).FirstOrDefault();
            var dbTableName = entityTypes.Where(w => w.ToLower().Equals(QlQuery.ToLower())).Select(s => s).FirstOrDefault();
            if (string.IsNullOrEmpty(QlQueryFullName) || string.IsNullOrEmpty(dbTableName))
            {
                var useAbleClass = entityTypes.Where(s => QlQueries.Any(a => a.Name.Contains(s))).ToList();
                throw new Exception($"Invalid {QlQuery}. Please use route from [ {string.Join(" , ", useAbleClass)} ]");
            }
            var inputs = query.Variables.ToInputs();
            Type typ = Type.GetType(QlQueryFullName);
            object[] args = new object[] { _db };
            var schema = new Schema
            {
                Query = (IObjectGraphType)Activator.CreateInstance(typ, args)
            };
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
        #region Legacy

        //[HttpPost]
        //[Route("Character")]
        //public async Task<IActionResult> Character([FromBody] GraphQLQuery query)
        //{
        //    var inputs = query.Variables.ToInputs();

        //    var schema = new Schema
        //    {
        //        Query = new CharacterQlQuery(_db),

        //    };

        //    var result = await new DocumentExecuter().ExecuteAsync(_ =>
        //    {
        //        _.Schema = schema;
        //        _.Query = query.Query;
        //        _.OperationName = query.OperationName;
        //        _.Inputs = inputs;
        //    });

        //    if (result.Errors?.Count > 0)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(result);
        //}



        //[HttpGet]
        //[Route("test")]
        //public async Task<IActionResult> Test()
        //{
        //    var query = @"
        //    {
        //      genericUser(firstName: ""1"") {
        //        firstName
        //        email
        //        character {
        //            name
        //        }
        //      }
        //    }";
        //    //var query = @"
        //    //{
        //    //  genericUser(id: 1) {
        //    //    firstName
        //    //    email
        //    //    character {
        //    //        name
        //    //    }
        //    //  }
        //    //}";

        //    var postData = new { Query = query };
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
        //    var httpClient = new HttpClient();
        //    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var res = await httpClient.PostAsync("https://localhost:44381/graphql/GenericUser", stringContent);
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var content = await res.Content.ReadAsStringAsync();
        //        return Ok(content);

        //    }
        //    else
        //    {
        //        return Ok(res);

        //    }

        //}


        #endregion

    }
}
