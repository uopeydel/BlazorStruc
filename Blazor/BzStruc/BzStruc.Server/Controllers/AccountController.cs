using BzStruc.Facade.Interface;
using BzStruc.Repository.Contract;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
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
        public async Task<IActionResult> SignIn([FromBody]GenericUserContract account)
        {
            GenericUser accountData = await _accountFacade.SignIn(account);
            var response = LinqExtensions.CreateErrorResponse<RefreshTokenContract>("Unauthorized");
            if (accountData != null)
            {
                var token = _jwtTokenService.GenerateToken(accountData.Email, accountData.Id.ToString());
                var refreshToken = _jwtTokenService.GenerateRefreshToken();
                var tokenData = new RefreshTokenContract { RefreshToken = refreshToken, Token = token };
                response = LinqExtensions.CreateSuccessResponse(tokenData);
            }
            return Ok(response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyAccount()
        {
            GenericUserContract user = await _accountFacade.GetMyAccount(IdentityUser);
            var response = LinqExtensions.CreateSuccessResponse(user);
            return Ok(response);
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

            var result = LinqExtensions.CreateSuccessResponse<RefreshTokenContract>(
                    new RefreshTokenContract
                    {
                        Token = newJwtToken,
                        RefreshToken = newRefreshToken
                    });
            return Ok(result);
        }

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2
        //[EnableThrottling(PerDay = 10, PerMinute = 1, PerHour = 5)]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]GenericUserContract newAccount)
        {
            return Ok(await _accountFacade.CreateAccount(newAccount));
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
