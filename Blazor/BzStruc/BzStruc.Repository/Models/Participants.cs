using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BzStruc.Repository.Models
{
    public class Participants
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid convertationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userId { get; set; }
 
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("convertationId")]
        public virtual Conversation Conversation { get; set; }

        [ForeignKey("userId")]
        public virtual GenericUser User { get; set; }
 
    }
}
