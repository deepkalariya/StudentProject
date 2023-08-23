using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.State.Controllers
{
    [Area("State")]
    [Route("{controller}/{action}")]
    public class LOC_StateController : Controller
    {
        // GET: /<controller>/
        public IActionResult LOC_StateList()
        {
            return View();
        }

        public IActionResult LOC_StateAddEdit()
        {
            return View();
        }
    }
}

