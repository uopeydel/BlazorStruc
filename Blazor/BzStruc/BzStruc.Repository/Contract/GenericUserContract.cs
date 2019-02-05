using BzStruc.Repository.Enums;
using System.Runtime.Serialization;

namespace BzStruc.Repository.Contract
{
    [DataContract]
    public class GenericUserContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }

        public string Password { get; set; }
        [DataMember]
        public UserOnlineStatus OnlineStatus { get; set; }
        [DataMember]
        public string Email { get; set; }

    }
}
