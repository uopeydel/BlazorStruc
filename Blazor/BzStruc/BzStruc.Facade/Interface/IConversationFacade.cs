using BzStruc.Repository.Contract;
using BzStruc.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BzStruc.Facade.Interface
{
    public interface IConversationFacade
    {
        Task<Results<List<ConversationContract>>> GetPaging(PagingParameters paging);
    }
}
