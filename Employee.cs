using System.ComponentModel.DataAnnotations;

namespace jalaproj.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Mobile { get; set; }

        public string Gender { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        [StringLength(200)]
        public string OtherCity { get; set; }

        public bool Aws { get; set; }

        public bool DevOps { get; set; }

        public bool FullstackDevelopment { get; set; }

        public bool QaAutomation { get; set; }

        public bool MiddleWare { get; set; }

        public bool WebServices { get; set; }

        public City City { get; set; }
        public Country Country { get; set; }

    }
}
