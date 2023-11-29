using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Areas.State.Models;
using static StudentProject.Areas.Country.Models.LOC_CountryModel;
using StudentProject.DAL;

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
        public IActionResult LOC_StateList(LOC_StateModel stateModel)
        {
            FillCountryDDL();
            string conn = this.Configuration.GetConnectionString("conn");
            DataTable dt = new DataTable();
            LOC_STATE_DAL locStateDal = new LOC_STATE_DAL();
            if (stateModel.StateName == null && stateModel.StateCode == null && stateModel.CountryID == 0)
            {
                dt = locStateDal.getAllState(conn, "PR_State_SelectAll");
                
            }
            else
            {
                dt = locStateDal.getFillteredData(conn, "PR_State_Apply_Filtter",stateModel);
            }
            ViewData["Table"] = dt;
            return View("LOC_StateList");
        }

        public IActionResult LOC_StateAddEdit(int? stateId)
        {
            if (stateId != null)
            {
                FillCountryDDL();
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_State_SelectByPK";
                cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = stateId;
                LOC_StateModel stateModel = new LOC_StateModel();
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        stateModel.StateID = Convert.ToInt32(dataReader["StateID"]);
                        stateModel.StateName = dataReader["StateName"].ToString();
                        stateModel.StateCode = dataReader["StateCode"].ToString();
                        stateModel.CountryID = Convert.ToInt32(dataReader["CountryID"]);
                    }
                    return View("LOC_StateAddEdit", stateModel);
                }
                return View("LOC_StateAddEdit");
            }
            FillCountryDDL();
            return View();
        }

        public IActionResult Save_State(LOC_StateModel statemodel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                if (statemodel.StateID == null)
                {
                    cmd.CommandText = "PR_State_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_State_Update";
                    cmd.Parameters.Add("@StateID",SqlDbType.Int).Value=statemodel.StateID;
                }

                cmd.Parameters.AddWithValue("StateName",statemodel.StateName);
                cmd.Parameters.AddWithValue("StateCode", statemodel.StateCode);
                cmd.Parameters.AddWithValue("CountryID", statemodel.CountryID);
                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (statemodel.StateID == null)
                    {
                        TempData["message"] = "State Inserted Successfully";
                    }
                    else
                    {
                        TempData["message"] = "State Updated Successfully";
                    }
                }
            }
            FillCountryDDL();
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

