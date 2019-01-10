using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BzStruc.Repository.Models
{
    public class Conversation
    {
        public Conversation()
        {
            Participants = new HashSet<Participants>();
            Messages = new HashSet<Messages>();
           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDelete { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime UpdateAt { get; set; }

        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public virtual Media Media { get; set; }

        public virtual ICollection<Participants> Participants { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
         
    }


}
