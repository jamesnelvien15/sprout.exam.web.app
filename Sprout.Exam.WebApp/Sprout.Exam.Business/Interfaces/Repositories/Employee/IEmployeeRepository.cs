using Sprout.Exam.Business.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces.Repositories.Employee
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> List();
        Task<List<EmployeeTypeDto>> EmployeeTypeList();
        Task<long> Create(CreateEmployeeDto model, string user);
        Task<bool> Update(EditEmployeeDto model, string user);
        Task<bool> Delete(long id, string user);
        Task<decimal> ComputeSalary(SalaryComputationDto model);
    }
}
