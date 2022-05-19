using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Domain
{
    public class Address : EntityModel
    {
        public string City { get; set; }
        public string State { get; set; }      
        public string PostalCode { get; set; }      
        public string Street { get; set; }
        public Guid ClientId { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNumber { get; set; }
    }
}
