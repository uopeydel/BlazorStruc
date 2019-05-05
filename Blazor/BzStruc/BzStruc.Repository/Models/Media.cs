 
using BzStruc.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BzStruc.Repository.Models
{
    public partial class Media
    {
        public Media()
        { 
            Messages = new HashSet<Messages>();
            Conversation = new HashSet<Conversation>(); 
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         

        [StringLength(1024)]
        public string FilePath { get; set; }
       
        [StringLength(100)]
        public string Format { get; set; }
        public bool Generic { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        //TODO : https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions
        public virtual MediaTypes MediaType { get; set; }
       
         
        public virtual ICollection<Messages> Messages { get; set; } 
        public virtual ICollection<Conversation> Conversation { get; set; }
        

    }
}
