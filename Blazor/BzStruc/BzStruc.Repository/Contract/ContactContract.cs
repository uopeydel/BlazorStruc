using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Repository.Contract
{ 
    public partial class ContactContract
    { 
        public int ContactId { get; set; }
        public int SenderStatus { get; set; }
        public int ReceiverStatus { get; set; }
        public DateTimeOffset ActionTime { get; set; } 
        public int ContactReceiverId { get; set; }  
        public int ContactSenderId { get; set; } 
    }
}
