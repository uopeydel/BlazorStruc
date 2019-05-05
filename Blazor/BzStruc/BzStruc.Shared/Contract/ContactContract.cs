 
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BzStruc.Shared.Contract
{

    [DataContract]
    public class ConversationContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsGroup { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime UpdateAt { get; set; }

        [DataMember]
        public MediaContract Media { get; set; }
        [DataMember]
        public List<ParticipantsContract> Participants { get; set; }
        [DataMember]
        public List<MessagesContract> Messages { get; set; }

    }
}
