using BzStruc.Facade.Interface;

using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Repository.Query;
using BzStruc.Repository.Service.Interface;
using BzStruc.Shared.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BzStruc.Facade.Implement
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IGenericEFRepository<MsSql1DbContext> _genericEFRepo;
        private readonly IAccountService _accountService;
        private readonly UserManager<GenericUser> _userManager;
        private readonly SignInManager<GenericUser> _signInManager;
        public AccountFacade(
            IGenericEFRepository<MsSql1DbContext> genericEFRepo,
            IAccountService accountService,
            SignInManager<GenericUser> signInManager,
            UserManager<GenericUser> userManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _genericEFRepo = genericEFRepo;
            _accountService = accountService;
        }

        public async Task<Results<bool>> CreateAccount(GenericUserContract account)
        {
            var newAccount = new GenericUser
            {
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                UserName = account.Email,
            };
            var result = await _userManager.CreateAsync(newAccount, account.Password);
            return LinqExtensions.CreateSuccessResponse<bool>(result.Succeeded);
        }

        public async Task<GenericUserContract> GetMyAccount(int IdentityUser)
        {
            return await GenericUserQuery.GetById(_genericEFRepo, IdentityUser);
            //Expression<Func<GenericUser, bool>> predicate = p => p.Id == IdentityUser;
            //Expression<Func<GenericUser, GenericUserContract>> selector = s =>
            //new GenericUserContract
            //{
            //    Id = s.Id,
            //    Email = s.Email,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    OnlineStatus = s.OnlineStatus
            //};
            //var response = await _genericEFRepo.GetQueryAble<GenericUser>()
            //    .Where(predicate)
            //    .Select(selector)
            //    .FirstOrDefaultAsync();
            //return response;
        }

        public async Task<Results<List<GenericUserContract>>> GetPaging(int IdentityUser, PagingParameters paging)
        {
            Expression<Func<GenericUser, bool>> predicate = p => true;
            Expression<Func<GenericUser, GenericUserContract>> selector = s =>
            new GenericUserContract
            {
                Id = s.Id,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                OnlineStatus = s.OnlineStatus
            };
            var response = await _genericEFRepo.GetPagingAsync<GenericUser, GenericUserContract>(predicate, selector, paging);
            return response;
        }

        public async Task<string> GetRefreshToken(string email)
        {
            Expression<Func<GenericUser, bool>> predicate = f => f.Email.ToUpper().Equals(email.ToUpper());
            Expression<Func<GenericUser, string>> selector = s => s.RefreshToken;
            var result = await _genericEFRepo.FirstOrDefaultAsync<GenericUser, string>(predicate, selector);
            return result;
        }

        public async Task<GenericUser> SignIn(GenericUserSignInContract account)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(account.Email, account.Password, false, false);
            if (signInResult.Succeeded)
            {
                return await _userManager.FindByEmailAsync(account.Email);
            }
            return null;
        }

        public async Task UpdateRefreshToken(string email, string newRefreshToken)
        {
            Expression<Func<GenericUser, bool>> predicate = f => f.Email.ToUpper().Equals(email.ToUpper());
            var result = await _genericEFRepo.FirstOrDefaultAsync<GenericUser>(predicate);
            result.RefreshToken = newRefreshToken;
            await _genericEFRepo.SaveAsync();
        }
    }


}
