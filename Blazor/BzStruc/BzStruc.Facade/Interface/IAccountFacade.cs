using BzStruc.Repository.Contract;
using BzStruc.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BzStruc.Facade.Interface
{
    public interface IAccountFacade
    {
        Task<Results<List<GenericUserContract>>> GetPaging(int IdentityUser,PagingParameters paging);
        Task<string> GetRefreshToken(string email);
        Task UpdateRefreshToken(string username, string newRefreshToken);
        Task CreateAccount(GenericUserContract newAccount);
    }
}
