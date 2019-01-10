using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BzStruc.Repository.Models
{
    public class MessageReadBy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MessageId { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }



        [ForeignKey("MessageId")]
        public virtual Messages Messages { get; set; }
        [ForeignKey("UserId")]
        public virtual GenericUser User { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }


    }
}
