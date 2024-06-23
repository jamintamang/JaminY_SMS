using JaminY_SMS.Repositories;

namespace JaminY_SMS.Models
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
