using BzStruc.Facade.Interface;
using BzStruc.Repository.Contract;
using BzStruc.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Filters;

namespace BzStruc.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAccountFacade _accountFacade;
        public AccountController(IJwtTokenService jwtTokenService, IAccountFacade accountFacade)
        {
            _accountFacade = accountFacade;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public string token([FromBody]string email)
        {
            return _jwtTokenService.GenerateToken(email, "1");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAccount()
        {

            return Ok(IdentityUser);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string token, string refreshToken)
        {
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = await _accountFacade.GetRefreshToken(username); //retrieve the refresh token from a data store
            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
            var newJwtToken = _jwtTokenService.GenerateToken(principal.Claims.ToList());
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            await _accountFacade.UpdateRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2
        //[EnableThrottling(PerDay = 10, PerMinute = 1, PerHour = 5)]
        [HttpPost]
        public async Task CreateAccount([FromBody]GenericUserContract newAccount)
        {
            await _accountFacade.CreateAccount(newAccount);
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
