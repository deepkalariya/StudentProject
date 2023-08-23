using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.City.Controllers
{
    [Area("City")]
    [Route("{controller}/{action}")]
    public class LOC_CityController : Controller
    {
        // GET: /<controller>/
        public IActionResult LOC_CityList()
        {
            return View();
        }

        public IActionResult LOC_CityAddEdit()
        {
            return View();
        }
    }
}

