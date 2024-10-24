using HRSystem.Application.Dtos;
using HRSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Interfaces
{
    public interface IDeparments
    {
        /// <summary>
        /// Get main departments for index page (parentId is null)
        /// </summary>
        /// <returns>List of DepartmentDto</returns>
        Task<List<DepartmentDto>> GetParentDepartments();
        /// <summary>
        /// Get deparment all parent levels + child levels
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DepartmentHierarchyDto object or null if there is anything wrong</returns>
        Task<DepartmentHierarchyDto?> GetAllDepartmentHierarchy(int id);
        Task<List<DepartmentDto>?> GetAllDepartmentParents(int id);
        Task<ChildDepartmentDto?> GetAllDepartmentChildren(int id);
    }
}
