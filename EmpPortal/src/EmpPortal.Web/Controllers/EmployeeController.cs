﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpPortal.Common.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Data"] = "Add employee here...";
            return View("AddEmployee");
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            
            return View("AddEmployee");
        }
    }
}
