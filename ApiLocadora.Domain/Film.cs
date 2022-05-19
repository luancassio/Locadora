using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Domain
{
    public class Film : EntityModel
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Gender { get; set; } 
    }
}
