
using BzStruc.Repository.DAL;
using BzStruc.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BzStruc.Facade.Interface
{
    public interface IConversationFacade
    {
        Task<Results<List<ConversationContract>>> GetPaging(int IdentityUser,PagingParameters paging);
    }
}
