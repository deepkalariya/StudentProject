using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.Branch.Controllers
{
    [Area("Branch")]
    [Route("{controller}/{action}")]
    public class BranchController : Controller
    {
        // GET: /<controller>/
        public IActionResult BranchList()
        {
            return View();
        }
        public IActionResult BranchAddEdit()
        {
            return View();
        }
    }
}

