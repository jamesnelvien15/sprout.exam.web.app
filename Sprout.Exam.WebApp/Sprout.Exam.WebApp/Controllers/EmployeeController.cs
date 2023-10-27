using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Repositories.Employee;
using Sprout.Exam.WebApp.Models.Employee;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employee;
        public EmployeeController(IEmployeeRepository _employee)
        {
            employee = _employee;
        }

        public async Task<IActionResult> Index()
        {
            EmployeeListViewModel model = new()
            {
                List = await employee.List(),
                EmployeeTypeList = await employee.EmployeeTypeList()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Employee/Add")]
        public async Task<long> Create(CreateEmployeeDto model)
        {
            try
            {
                return await employee.Create(model, User.Identity.Name);
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return 0;
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Employee/Update")]
        public async Task<bool> Edit(EditEmployeeDto model)
        {
            try
            {
                return await employee.Update(model, User.Identity.Name);
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return false;
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Employee/Remove")]
        public async Task<bool> Delete(Int64 id)
        {
            try
            {
                return await employee.Delete(id, User.Identity.Name);
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return false;
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Employee/Compute")]
        public async Task<decimal> Compute(SalaryComputationDto model)
        {
            try
            {
                return await employee.ComputeSalary(model);
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return 0;
            }
        }
    }
}
