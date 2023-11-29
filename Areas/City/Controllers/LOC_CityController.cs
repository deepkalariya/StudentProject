using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using static StudentProject.Areas.State.Models.LOC_StateModel;
using static StudentProject.Areas.Country.Models.LOC_CountryModel;
using StudentProject.Areas.City.Models;
using StudentProject.DAL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentProject.Areas.City.Controllers
{
    [Area("City")]
    [Route("{controller}/{action}")]
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;

        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        
        public IActionResult LOC_CityList(LOC_CityModel cityModel)
        {
            FillCountryDDL();
            FillStateDDL();
            string conn = this.Configuration.GetConnectionString("conn");
            DataTable dt = new DataTable();
            LOC_CITY_DAL loccitydal = new LOC_CITY_DAL();
            if (cityModel.CountryID == 0 && cityModel.StateID == 0 && cityModel.CityName == null && cityModel.CityCode == null)
            {
                dt = loccitydal.getAllCity(conn, "PR_City_SelectAll");
            }
            else
            {
                dt = loccitydal.getFillteredData(conn, "PR_City_Apply_Filter", cityModel);
            }
            ViewData["table"] = dt;
            return View();
        }

        public IActionResult LOC_CityAddEdit()
        {
            FillCountryDDL();
            FillStateDDL();
            return View();
        }

        public IActionResult CityDelete(int cityId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_DeleteByPK";
            cmd.Parameters.Add("@CityID",SqlDbType.Int).Value = cityId;
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CityList");
        }

        public IActionResult Save_City(LOC_CityModel citymodel)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (citymodel.CityID == null)
                    {
                        cmd.CommandText = "PR_City_Insert";
                    }
                    else
                    {
                        cmd.CommandText = "PR_City_Update";
                        cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = citymodel.CityID;
                    }

                    cmd.Parameters.AddWithValue("CityName", citymodel.CityName);
                    cmd.Parameters.AddWithValue("StateID", citymodel.StateID);
                    cmd.Parameters.AddWithValue("CountryID", citymodel.CountryID);
                    cmd.Parameters.AddWithValue("CityCode",citymodel.CityCode);
                    if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                    {
                        if (citymodel.CityID == null)
                        {
                            ViewData["message"] = "City Inserted Successfully";
                        }
                        else
                        {
                            ViewData["message"] = "City Updated Successfully";
                        }
                    }
                }
            }
            FillCountryDDL();
            FillStateDDL();
            return View("LOC_CityAddEdit");
        }

        public IActionResult editCity(int cityId)
        {
            FillCountryDDL();
            FillStateDDL();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_SelectByPK";
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = cityId;
            LOC_CityModel cityModel = new LOC_CityModel();
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    cityModel.CityID = Convert.ToInt32(dataReader["CityID"]);
                    cityModel.CityName = dataReader["CityName"].ToString();
                    cityModel.StateID = Convert.ToInt32(dataReader["StateID"]);
                    cityModel.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                    cityModel.CityCode = dataReader["CityCode"].ToString();
                }
                return View("LOC_CityAddEdit",cityModel);
            }
            return View("LOC_CityAddEdit");
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

        public void FillStateDDL()
        {
            List<StateDropDownModel> list = new List<StateDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    StateDropDownModel stateDropDown = new StateDropDownModel()
                    {
                        StateID = Convert.ToInt32(dataReader["StateID"]),
                        StateName = dataReader["StateName"].ToString()
                    };
                    list.Add(stateDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.StateList = list;
        }
    }
}
