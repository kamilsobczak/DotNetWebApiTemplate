using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class AuditableEntity
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        public int StatusID { get; set; }

        public string InactivatedBy { get; set; }

        public DateTime? Inactivated { get; set; }
    }
}
