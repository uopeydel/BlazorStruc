 
using BzStruc.Shared.Enums;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace BzStruc.Shared.Contract
{

    [DataContract]
    public class GenericUserContract : GenericUserSignInContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        //[DataMember]
        //public string Password { get; set; }
        [DataMember]
        public UserOnlineStatus OnlineStatus { get; set; }
        //[DataMember]
        //public string Email { get; set; }

    }

    [DataContract]
    public class GenericUserSignInContract
    { 
        [DataMember]
        public string Email { get; set; } 
        [DataMember]
        public string Password { get; set; } 
    }

    
}
