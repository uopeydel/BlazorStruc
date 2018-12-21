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
        public Guid id { get; set; }

        public Guid conversationId { get; set; }
        public string message { get; set; }

        public int fromUserId { get; set; }
        public int? mediaId { get; set; }

        public MessageTypes MessageType { get; set; }

        [ForeignKey("conversationId")]
        public virtual Conversation Conversation { get; set; }
        [ForeignKey("fromUserId")]
        public virtual GenericUser User { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }


        public virtual ICollection<MessageReadBy> MessageReadBy { get; set; }

        [ForeignKey("mediaId")]
        public virtual Media Media { get; set; }

    }
}
