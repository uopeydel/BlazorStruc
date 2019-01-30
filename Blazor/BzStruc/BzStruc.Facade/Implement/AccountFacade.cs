using BzStruc.Facade.Interface;
using BzStruc.Repository.Contract;
using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Repository.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BzStruc.Facade.Implement
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IGenericEFRepository<MsSql1DbContext> _genericEFRepo;
        private readonly IAccountService _accountService;

        public AccountFacade(IGenericEFRepository<MsSql1DbContext> genericEFRepo, IAccountService accountService)
        {
            _genericEFRepo = genericEFRepo;
            _accountService = accountService;
        }

        public Task CreateAccount(GenericUserContract newAccount)
        {
            _genericEFRepo.AddAsync<>
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

        public async Task UpdateRefreshToken(string email, string newRefreshToken)
        {
            Expression<Func<GenericUser, bool>> predicate = f => f.Email.ToUpper().Equals(email.ToUpper());
            var result = await _genericEFRepo.FirstOrDefaultAsync<GenericUser>(predicate);
            result.RefreshToken = newRefreshToken;
            await _genericEFRepo.SaveAsync();
        }
    }


}
