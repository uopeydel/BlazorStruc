using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Shared.Contract
{
    public class ParticipantsContract
    {
     
        public int ConvertationId { get; set; } 
        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        //public virtual Conversation Conversation { get; set; } 
        //public virtual GenericUser User { get; set; }

    }
}
