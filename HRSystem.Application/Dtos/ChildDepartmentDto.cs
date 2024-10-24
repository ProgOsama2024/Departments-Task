using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Dtos
{
    //Class will be used in get department child 
    public class ChildDepartmentDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public List<ChildDepartmentDto> Children { get; set; }
    }
}
