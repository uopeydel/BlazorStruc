using BzStruc.Repository.DAL;
using BzStruc.Repository.Models;
using BzStruc.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BzStruc.Repository.Query
{
    public static class GenericUserQuery
    {
        public static Task<GenericUserContract> GetById(IGenericEFRepository<MsSql1DbContext> _genericEFRepo, int IdentityUser)
        {
            Expression<Func<GenericUser, bool>> predicate = p => p.Id == IdentityUser;
            Expression<Func<GenericUser, GenericUserContract>> selector = s =>
            new GenericUserContract
            {
                Id = s.Id,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                OnlineStatus = s.OnlineStatus
            };
            var response = _genericEFRepo.GetQueryAble<GenericUser>()
                .Where(predicate)
                .Select(selector)
                .FirstOrDefaultAsync(); 
            return response;
        }
    }
}
