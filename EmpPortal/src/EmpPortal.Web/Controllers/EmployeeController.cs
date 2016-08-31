using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpPortal.Common.ViewModels;
using MVCApp.Infrastructure;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApp.Controllers
{
    public class EmployeeController : BaseController
    {
        public EmployeeController()
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Data"] = "Add employee here...";
            // throw new InvalidOperationException("Invalid operation is being performed...");
            Logger.LogError("Employee controller called...");

            return View("AddEmployee");
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            throw new InvalidOperationException("Invalid operation is being performed...");
            
            return View("AddEmployee");
        }
    }
}
