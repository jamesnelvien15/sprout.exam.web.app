using Sprout.Exam.Business.DataTransferObjects;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp.Models.Employee
{
    public class EmployeeModel : EmployeeDto
    {
    }

    public class EmployeeListViewModel
    {
        public List<EmployeeDto> List { get; set; }
        public List<EmployeeTypeDto> EmployeeTypeList { get; set; }
    }
}
