using System;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseEmployeeDto
    {
        public string FullName { get; set; }
        public string Tin { get; set; }
        public DateTime Birthdate { get; set; }
        public long EmployeeTypeId { get; set; }
    }
}
