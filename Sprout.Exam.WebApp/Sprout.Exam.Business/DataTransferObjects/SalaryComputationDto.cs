namespace Sprout.Exam.Business.DataTransferObjects
{
    public class SalaryComputationDto
    {
        public long EmployeeTypeId { get; set; }
        public decimal BasicSalary { get; set; } = 0;
        public decimal DaysAbsent { get; set; } = 0;
        public decimal DaysPresent { get; set; } = 0;
        public decimal HoursPresent { get; set; } = 0;
    }
}