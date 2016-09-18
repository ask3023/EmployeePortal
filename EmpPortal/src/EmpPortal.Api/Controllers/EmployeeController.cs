using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpPortal.Common.ViewModels;
using EmpPortal.Api.Infrastructure;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpPortal.Api.Controllers
{
    [Route("api/Employee")]
    public class EmployeeController : BaseController
    {
        // GET: returns all employees
        [HttpGet]
        public IActionResult Get()
        {
            //TODO: Call employee service to get all employee records

            var emp1 = new EmployeeViewModel()
            {
                FirstName = "Ramesh",
                LastName = "Babu",
                DateOfBirth = DateTime.Now.AddYears(-30),
                DateOfJoining = DateTime.Now.AddYears(-2),
                PrimaryEmail = "Hello@bolo.com",
                SecondaryEmail = "Welcome@mad.com"
            };

            var emp2 = new EmployeeViewModel()
            {
                FirstName = "Pavan",
                LastName = "Kalyan",
                DateOfBirth = DateTime.Now.AddYears(-32),
                DateOfJoining = DateTime.Now.AddYears(-3),
                PrimaryEmail = "hi@bolo.com",
                SecondaryEmail = "swagath@mad.com"
            };

            List<EmployeeViewModel> response = new List<EmployeeViewModel>()
            { emp1, emp2};

            Logger.LogInformation("Employees found: " + response.Count);

            return Ok(response);
        }

        // GET: returns specific employee
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Emp1");
        }

        // POST: creates a new employee
        [HttpPost]
        public void Post([FromBody]EmployeeViewModel newEmployee)
        {
            //TODO: call employee service to push the new employee into database
            Logger.LogInformation("Employee added to the system");
        }

        // PUT: updates employee
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: removes an employee
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
