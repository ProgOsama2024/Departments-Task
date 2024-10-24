using AutoMapper;
using HRSystem.Application.Dtos;
using HRSystem.Application.Interfaces;
using HRSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRsystem.Infrastructure.Services
{
    public class DepartmentsService : IDeparments
    {
        HrsystemContext _hrsystemContext;
        private readonly IMapper _mapper;
        public DepartmentsService(HrsystemContext hrsystemContext, IMapper mapper) 
        {
            _hrsystemContext = hrsystemContext;
            _mapper = mapper;
        }
        
        public async Task<List<DepartmentDto>> GetParentDepartments()
        {
            var result = await _hrsystemContext.Departments.Where(d => d.IsDeleted == false || d.IsDeleted == null && d.ParentId == null).ToListAsync();
            return _mapper.Map<List<DepartmentDto>>(result);
        }

        public async Task<DepartmentHierarchyDto?> GetAllDepartmentHierarchy(int id)
        {
            var department = await _hrsystemContext.Departments
               .Include(d => d.Parent)
               .FirstOrDefaultAsync(d => d.Id == id);
            if (department == null) 
            {
                return null;
            }
            DepartmentHierarchyDto departmentHierarchyDto = new DepartmentHierarchyDto();
            departmentHierarchyDto.Id = id;
            departmentHierarchyDto.Title = department.Title;

            departmentHierarchyDto.lstDepartmentParents = await GetAllDepartmentParents(id);
            departmentHierarchyDto.DepartmentChildren = await GetAllDepartmentChildren(id);

            return departmentHierarchyDto;
        }

        public async Task<List<DepartmentDto>?> GetAllDepartmentParents(int id)
        {
            var parents = new List<DepartmentDto>();

            var department = await _hrsystemContext.Departments
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department != null && department.Parent != null)
            {
                while (department?.Parent != null)
                {
                    parents.Add(_mapper.Map<DepartmentDto>(department.Parent));

                    if (department.Parent.Parent != null)
                    {
                        department = department.Parent;
                    }
                    else
                    {
                        department = await _hrsystemContext.Departments
                            .Include(d => d.Parent)
                            .FirstOrDefaultAsync(d => d.Id == department.ParentId && (d.IsDeleted == false || d.IsDeleted == null));
                    }

                }
                return parents.OrderBy(d=>d.Id).ToList();
            }
            return null;
        }

        public async Task<ChildDepartmentDto?> GetAllDepartmentChildren(int id)
        {
            var department = await _hrsystemContext.Departments
                 .Include(d => d.InverseParent)
                 .FirstOrDefaultAsync(d => d.Id == id && (d.IsDeleted == false || d.IsDeleted == null));

            if (department == null || department.InverseParent == null || department.InverseParent.Count == 0)
            {
                return null;
            }
            var departments = _hrsystemContext.Departments.ToList();
            return GetChildrenRecursively(department, departments);
        }

        private ChildDepartmentDto GetChildrenRecursively(Department department, List<Department> allDepartments)
        {
            var dto = new ChildDepartmentDto
            {
                Id = department.Id,
                Title = department.Title,
                Children = allDepartments
                    .Where(d => d.ParentId == department.Id)
                    .Select(child => GetChildrenRecursively(child, allDepartments))
                    .ToList()
            };

            return dto;
        }
    }
}
