using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.Country.Countrollers
{
    [Area("Country")]
    [Route("{controller}/{action}")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult LOC_CountryList()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                dt.Load(dataReader);
            }
            return View(dt);
        }

        public IActionResult LOC_CountryAddEdit()
        {
            return View();
        }

        public IActionResult CountryDetete(int countryId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_DeletePK";
            cmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = countryId;
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CountryList");
        }
    }
}

