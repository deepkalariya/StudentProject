using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Areas.State.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.State.Controllers
{
    [Area("State")]
    [Route("{controller}/{action}")]
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;

        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult LOC_StateList()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                dt.Load(dataReader);
            }
            return View(dt);
        }

        public IActionResult LOC_StateAddEdit()
        {
            return View();
        }

        public IActionResult Save_State(LOC_StateModel statemodel)
        {
            if (ModelState.IsValid)
            {

            }
            return View("LOC_StateAddEdit");
        }

        public IActionResult StateDelete(int stateId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_DeletePK";
            cmd.Parameters.Add("@StateId",SqlDbType.Int).Value=stateId;
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_StateList");
        }
    }
}

