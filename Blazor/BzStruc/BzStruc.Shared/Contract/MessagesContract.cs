using BzStruc.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Shared.Contract
{ 

    public class MessagesContract
    {
       
        public int Id { get; set; }

        public int ConversationId { get; set; }
        public string Payload { get; set; }

        public int FromUserId { get; set; }
        public int? MediaId { get; set; }

        public MessageTypes MessageType { get; set; }

       
        public DateTime CreatedAt { get; set; }
 

    }
}
