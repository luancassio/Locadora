using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Application.Dto
{
    public class ClientDto 
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; } 
        public AddressDto Address { get; set; }  
    }
}
