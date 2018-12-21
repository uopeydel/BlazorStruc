using BzStruc.Repository.Models;
using System;
using System.Collections.Generic;

namespace BzStruc.Repository.Contract
{
    public class ConversationContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public Media Media { get; set; }
        public List<Participants> Participants { get; set; }
        public List<Messages> Messages { get; set; }

    }
}
