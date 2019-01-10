using BzStruc.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace BzStruc.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public string token([FromBody]string email)
        {
            return _jwtTokenService.GenerateToken(email , "1");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAccount()
        {

            return Ok(IdentityUser);
        }



        [HttpPost]
        public void CreateAccount([FromBody]string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
