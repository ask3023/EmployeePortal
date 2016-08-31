using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Infrastructure;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApp.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController()
        {

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
