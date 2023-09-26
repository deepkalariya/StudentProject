using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Areas.Country.Models;
using StudentProject.DAL;
using static StudentProject.Areas.Country.Models.LOC_CountryModel;

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

        public IActionResult LOC_CountryList(LOC_CountryModel? countryModel)
        {
            string conn = this.Configuration.GetConnectionString("conn");
            LOC_COUNTRY_DAL locCountryDal = new LOC_COUNTRY_DAL();
            DataTable dt = new DataTable();
            if (countryModel.CountryName == null && countryModel.CountryCode == null)
            {
                dt = locCountryDal.getAllCountry(conn, "PR_Country_SelectAll");
            }
            else
            {
                dt = locCountryDal.getFillteredData(conn, "PR_Country_Apply_Filter",countryModel.CountryName,countryModel.CountryCode);
            }
            return View(dt);
        }

        public IActionResult LOC_CountryAddEdit()
        {
            return View();
        }

        public IActionResult Save_Country(LOC_CountryModel countryModel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                if (countryModel.CountryID == null)
                {
                    cmd.CommandText = "PR_Counrty_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Country_Update";
                    cmd.Parameters.AddWithValue("@CountryID",countryModel.CountryID);
                }

                cmd.Parameters.AddWithValue("@CountryName",countryModel.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", countryModel.CountryCode);
                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (countryModel.CountryID==null)
                    {
                        TempData["message"] = "Country Inserted Successfully";
                    }
                    else
                    {
                        TempData["message"] = "Country Updated Successfully";
                    }
                }
            }
            return View("LOC_CountryAddEdit");
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

        public IActionResult editCountry(int? countryId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_SelectByPK";
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;
            LOC_CountryModel countryModel = new LOC_CountryModel();
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    countryModel.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                    countryModel.CountryName = dataReader["CountryName"].ToString();
                    countryModel.CountryCode = dataReader["CountryCode"].ToString();
                }
                return View("LOC_CountryAddEdit",countryModel);
            }
            return View("LOC_CountryAddEdit");
        }

        public void FillCountryDDL()
        {
            List<CountryDropDownModel> list = new List<CountryDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    CountryDropDownModel countryDropDown = new CountryDropDownModel()
                    {
                        CountryID = Convert.ToInt32(dataReader["CountryID"]),
                        CountryName = dataReader["CountryName"].ToString()
                    };
                    list.Add(countryDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.CountryList = list;
        }
    }
}

