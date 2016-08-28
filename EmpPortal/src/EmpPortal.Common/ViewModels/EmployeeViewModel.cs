using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Common.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "First name is mandatory field.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date of joining")]
        public DateTime DateOfJoining { get; set; }
        [Required]
        [Display(Name = "Primary Email")]
        public string PrimaryEmail { get; set; }
        [Display(Name = "Secondary Email")]
        public string SecondaryEmail { get; set; }
    }
}
