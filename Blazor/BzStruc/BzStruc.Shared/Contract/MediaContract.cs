using BzStruc.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Shared.Contract
{

    public partial class MediaContract
    {

        public int Id { get; set; }



        public string FilePath { get; set; }


        public string Format { get; set; }
        public bool Generic { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        public virtual MediaTypes MediaType { get; set; }




    }
}
