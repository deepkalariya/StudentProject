using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Areas.Branch.Models;
using StudentProject.DAL;

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
        public IActionResult BranchList(BranchModel branchModel)
        {
            string conn = this.Configuration.GetConnectionString("conn");
            DataTable dt = new DataTable();
            BRANCH_DAL branchdal = new BRANCH_DAL();
            if (branchModel.BranchName == null && branchModel.BranchCode == null)
            {
                dt = branchdal.getAllBranch(conn, "PR_Branch_SelectAll");
            }
            else
            {
                dt = branchdal.getFillteredData(conn, "PR_Branch_Apply_Filter", branchModel.BranchName, branchModel.BranchCode);
            }
            return View(dt);
        }
        public IActionResult BranchAddEdit()
        {
            return View();
        }

        public IActionResult Save_Branch(BranchModel branchModel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                if (branchModel.BranchID == null)
                {
                    cmd.CommandText = "PR_Branch_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Branch_Update";
                    cmd.Parameters.Add("@BranchID",SqlDbType.Int).Value = branchModel.BranchID;
                }
                cmd.Parameters.AddWithValue("BranchName",branchModel.BranchName);
                cmd.Parameters.AddWithValue("BranchCode", branchModel.BranchCode);
                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (branchModel.BranchID == null)
                    {
                        ViewData["message"] = "Branch Inserted Successfully";
                    }
                    else
                    {
                        ViewData["message"] = "Branch Updated Successfully";
                    }
                }
            }
            return View("BranchaddEdit");
        }

        public IActionResult editBranch(int branchId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_SelectByPK";
            cmd.Parameters.Add("@BranchID",SqlDbType.Int).Value=branchId;
            BranchModel branchModel = new BranchModel();
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    branchModel.BranchID = Convert.ToInt32(dataReader["BranchID"]);
                    branchModel.BranchName = dataReader["BranchName"].ToString();
                    branchModel.BranchCode = dataReader["BranchCode"].ToString();
                }
                return View("BranchAddEdit",branchModel);
            }
            return View("BranchAddEdit");
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

