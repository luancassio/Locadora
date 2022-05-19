using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Domain
{
    public class RentalCompany : EntityModel
    {
        public Guid FilmId { get; set; }    
        public string ClientName { get; set; }
        public Guid ClientId { get; set; }
        public string FilmName { get; set; }
        public DateTime DateLocation { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DateReturn { get; set; }
        public string Observation { get; set; }
    }
}
