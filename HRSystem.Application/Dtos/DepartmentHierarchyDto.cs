using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Dtos
{
    public class DepartmentHierarchyDto : DepartmentDto
    {
        public List<DepartmentDto>? lstDepartmentParents { get; set; }
        public ChildDepartmentDto? DepartmentChildren { get; set; }
    }
}
