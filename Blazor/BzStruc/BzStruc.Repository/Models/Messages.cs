using BzStruc.Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BzStruc.Repository.Models
{
    public class Messages
    {
        public Messages()
        {
            MessageReadBy = new HashSet<MessageReadBy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }
        public string Payload { get; set; }

        public int FromUserId { get; set; }
        public int? MediaId { get; set; }

        public MessageTypes MessageType { get; set; }

        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
        [ForeignKey("FromUserId")]
        public virtual GenericUser User { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }


        public virtual ICollection<MessageReadBy> MessageReadBy { get; set; }

        [ForeignKey("MediaId")]
        public virtual Media Media { get; set; }

    }
}
