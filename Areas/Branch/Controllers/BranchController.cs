using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private IConfiguration Configuration;

        public BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult BranchList()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                dt.Load(dataReader);
            }
            return View(dt);
        }
        public IActionResult BranchAddEdit()
        {
            return View();
        }
        public IActionResult BranchDelete(int branchId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_DeletePK";
            cmd.Parameters.Add("@BranchID",SqlDbType.Int).Value=branchId;
            cmd.ExecuteNonQuery();
            return RedirectToAction("BranchList");
        }
    }
}

