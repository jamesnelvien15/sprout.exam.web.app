namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeDto : BaseEmployeeDto
    {
        public long Id { get; set; }
        public string EmployeeType { get; set; }
    }
}
