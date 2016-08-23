using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Common.ViewModels
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
    }
}
