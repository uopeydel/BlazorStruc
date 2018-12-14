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
        private readonly IGenericEFRepository _genericEFRepo;
        private readonly IContactService _contactService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_msGenericDb">Dont use this in this facade ,use for send to InterfaceOfService</param>
        public ConversationFacade(IGenericEFRepository genericEFRepo, IContactService contactService)
        {
            _genericEFRepo = genericEFRepo;
            _contactService = contactService;
        }

        public async Task<Results<List<ContactContract>>> GetPaging(PagingParameters paging)
        {
            Expression<Func<Contact, bool>> predicate = p => true;
            Expression<Func<Contact, ContactContract>> selector = s =>
            new ContactContract
            {
                ActionTime = s.ActionTime,
                ContactId = s.ContactId,
                ContactReceiverId = s.ContactReceiverId,
                ContactSenderId = s.ContactSenderId,
                ReceiverStatus = s.ReceiverStatus,
                SenderStatus = s.SenderStatus
            };
            var response = await _genericEFRepo.GetPagingAsync<Contact, ContactContract>(predicate, selector, paging);
            return response;
        } 
    }

    public interface IConversationFacade
    {
        Task<Results<List<ContactContract>>> GetPaging(PagingParameters paging);
    }
}
