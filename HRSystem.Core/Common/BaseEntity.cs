using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Core.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? InsertDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
