using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Domain
{
    public class EntityModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? UpdatedAt { get; set; }
    }
}
