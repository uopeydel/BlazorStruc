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
    public class ConversationFacade : IConversationFacade
    {
        private readonly IGenericEFRepository<MsSql1DbContext> _genericEFRepo;
        private readonly IConversationService _conversationService;

        public ConversationFacade(IGenericEFRepository<MsSql1DbContext> genericEFRepo, IConversationService contactService)
        {
            _genericEFRepo = genericEFRepo;
            _conversationService = contactService;
        }

        public async Task<Results<List<ConversationContract>>> GetPaging(int IdentityUser, PagingParameters paging)
        {
            Expression<Func<Conversation, bool>> predicate = p => true;
            Expression<Func<Conversation, ConversationContract>> selector = s =>
            new ConversationContract
            {
                Id = s.Id,
                Name = s.Name,
                //Media = s.Media
            };
            var response = await _genericEFRepo.GetPagingAsync<Conversation, ConversationContract>(predicate, selector, paging);
            return response;
        }


    }


}
