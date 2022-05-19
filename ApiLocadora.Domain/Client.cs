using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Domain
{
    public class Client : EntityModel
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; } 
        public Address Address { get; set; }  
    }
}
