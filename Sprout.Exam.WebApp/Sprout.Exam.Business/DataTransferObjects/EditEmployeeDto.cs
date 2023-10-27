namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EditEmployeeDto : BaseEmployeeDto
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
