using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JaminY_SMS.Repositories;

namespace JaminY_SMS.Models
{
    public class Student :BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Gender { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public string Class { get; set; }
        public string Section { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }


        public virtual Course Course { get; set; }

        public bool IsActive { get; set; }
    }
}
