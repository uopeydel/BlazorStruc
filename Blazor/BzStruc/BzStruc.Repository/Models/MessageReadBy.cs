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
        public Guid messageId { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userId { get; set; }



        [ForeignKey("messageId")]
        public virtual Messages Messages { get; set; }
        [ForeignKey("userId")]
        public virtual GenericUser User { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }


    }
}
