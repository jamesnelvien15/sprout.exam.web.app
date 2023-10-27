using Dapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Repositories.Employee;
using Sprout.Exam.DataAccess.Helper;
using Sprout.Exam.DataAccess.Repositories.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext dapper;

        public EmployeeRepository(DapperContext _dapper)
        {
            dapper = _dapper;
        }

        public async Task<List<EmployeeDto>> List()
        {
            try
            {
                string spName = "pr_GetEmployeeList";
                using var connection = dapper.CreateConnection();
                connection.Open();
                var list = await connection.ExecuteStoredProcedureAsync<EmployeeDto>(spName);
                if (list != null && list.Any())
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
            return new List<EmployeeDto>();
        }

        public async Task<List<EmployeeTypeDto>> EmployeeTypeList()
        {
            try
            {
                string spName = "pr_GetEmployeeTypeList";
                using var connection = dapper.CreateConnection();
                connection.Open();
                var list = await connection.ExecuteStoredProcedureAsync<EmployeeTypeDto>(spName);
                if (list != null && list.Any())
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
            return new List<EmployeeTypeDto>();
        }

        public async Task<long> Create(CreateEmployeeDto model, string user)
        {
            try
            {
                string spName = "pr_InsertEmployee";
                using var connection = dapper.CreateConnection();
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@fullName", model.FullName, dbType: DbType.String, direction: ParameterDirection.Input);
                parameter.Add("@birthDate", model.Birthdate.ToShortDateString(), dbType: DbType.Date, direction: ParameterDirection.Input);
                parameter.Add("@tin", model.Tin, dbType: DbType.String, direction: ParameterDirection.Input);
                parameter.Add("@employeeTypeId", model.EmployeeTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameter.Add("@user", user, dbType: DbType.String, direction: ParameterDirection.Input);
                return await connection.ExecuteStoredProcedureAsync(spName, parameter);
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return 0;
            }
        }

        public async Task<bool> Update(EditEmployeeDto model, string user)
        {
            try
            {
                string spName = "pr_UpdateEmployee";
                using var connection = dapper.CreateConnection();
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", model.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameter.Add("@fullName", model.FullName, dbType: DbType.String, direction: ParameterDirection.Input);
                parameter.Add("@birthDate", model.Birthdate.ToShortDateString(), dbType: DbType.Date, direction: ParameterDirection.Input);
                parameter.Add("@tin", model.Tin, dbType: DbType.String, direction: ParameterDirection.Input);
                parameter.Add("@employeeTypeId", model.EmployeeTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameter.Add("@user", user, dbType: DbType.String, direction: ParameterDirection.Input);
                var list = await connection.ExecuteStoredProcedureAsync(spName, parameter);
                return list > 0;
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return false;
            }
        }

        public async Task<bool> Delete(long id, string user)
        {
            try
            {
                string spName = "pr_DeleteEmployee";
                using var connection = dapper.CreateConnection();
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameter.Add("@user", id, dbType: DbType.String, direction: ParameterDirection.Input);
                var list = await connection.ExecuteStoredProcedureAsync(spName, parameter);
                return list > 0;
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return false;
            }
        }

        public async Task<decimal> ComputeSalary(SalaryComputationDto model)
        {
            try
            {
                string spName = "pr_ComputeSalary";
                using var connection = dapper.CreateConnection();
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@employeeType", model.EmployeeTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameter.Add("@basicSalary", model.BasicSalary, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                parameter.Add("@daysAbsent", model.DaysAbsent, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                parameter.Add("@daysPresent", model.DaysPresent, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                parameter.Add("@hoursPresent", model.HoursPresent, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                var result = await connection.ExecuteStoredProcedureAsync<decimal>(spName, parameter);
                return result[0];
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return 0;
            }
        }
    }
}