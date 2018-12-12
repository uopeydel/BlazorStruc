using BzStruc.Repository.DAL;
using BzStruc.Repository.Service.Interface;

namespace BzStruc.Facade.Implement
{
    public class ConversationFacade : IConversationFacade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_msGenericDb">Dont use this in this facade ,use for send to InterfaceOfService</param>
        public ConversationFacade(MsSql1DbContext _msGenericDb, IContactService contactService)
        {

        }
    }

    public interface IConversationFacade
    {

    }
}
