using HRsystem.Infrastructure.Services;
using HRSystem.Application.Interfaces;
using HRSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRSystem.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDeparments _IDeparments;

        public DepartmentsController(IDeparments deparments)
        {
            _IDeparments = deparments;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _IDeparments.GetParentDepartments();
            return View(departments);
        }

        public async Task<IActionResult> DepartmentHirarchy(int id)
        {
            var department = await _IDeparments.GetAllDepartmentHierarchy(id);

            return View(department);
        }
    }
}
