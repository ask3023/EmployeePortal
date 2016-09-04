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

        public async Task<IActionResult> Search()
        {
            var result = await HttpHelper.GetAsync<List<EmployeeViewModel>>(@"api/employee");

            ViewData.Model = result;

            return View("Search");
        }

        public async Task<IActionResult> Add()
        {
            // Logger.LogError("Employee controller called...");

            return View("AddEmployee");
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel employeeViewModel)
        {
            bool isAdded = await HttpHelper.PostAsync<EmployeeViewModel>(@"api/employee", employeeViewModel);

            return View("AddEmployee");
        }
    }
}
