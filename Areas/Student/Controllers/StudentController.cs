using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Areas.Student.Models;
using StudentProject.DAL;
using static StudentProject.Areas.Branch.Models.BranchModel;
using static StudentProject.Areas.City.Models.LOC_CityModel;

namespace StudentProject.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("{controller}/{action}")]
    public class StudentController : Controller
	{
        private IConfiguration Configuration;

		public StudentController(IConfiguration _configuration)
		{
            Configuration = _configuration;
		}

        public IActionResult StudentList()
        {
            string conn = this.Configuration.GetConnectionString("conn");
            DataTable dt = new DataTable();
            STUDENT_DAL studentdal = new STUDENT_DAL();
            dt = studentdal.getAllStudent(conn, "PR_Student_SelectAll");
            
            return View(dt);
        }

        public IActionResult StudentAddEdit(int? studentId)
        {

            FillBranchDDL();
            FillCityDDL();
            if (studentId != null)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Student_SelectByPK";
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                StudentModel studentModel = new StudentModel();
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        studentModel.BranchID = Convert.ToInt32(dataReader["BranchID"]);
                        studentModel.CityID = Convert.ToInt32(dataReader["CityID"]);
                        studentModel.StudentName = dataReader["StudentName"].ToString();
                        studentModel.MobileNoStudent = dataReader["MobileNoStudent"].ToString();
                        studentModel.Email = dataReader["Email"].ToString();
                        studentModel.MobileNoFather = dataReader["MobileNoFather"].ToString();
                        studentModel.Address = dataReader["Address"].ToString();
                        studentModel.BitrhDate = Convert.ToDateTime(dataReader["BirthDate"]);
                        studentModel.Age = Convert.ToInt32(dataReader["Age"]);
                        studentModel.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                        studentModel.Gender = dataReader["Gender"].ToString();
                        studentModel.Password = dataReader["Password"].ToString();
                    }
                }
                conn.Close();
                return View(studentModel);
            }
            return View();
        }

        public IActionResult StudentDelete(int studentId)
        {
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Student_DeleteByPK";
            cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
            cmd.ExecuteNonQuery();
            return RedirectToAction("StudentList");
        }

        public IActionResult Save_Student(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                if (studentModel.StudentID == null)
                {
                    cmd.CommandText = "PR_Student_Insert";
                }
                else
                {
                    cmd.CommandText = "PR_Student_UpdateByPK";
                    cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentModel.StudentID;
                }
                cmd.Parameters.AddWithValue("@BranchID",studentModel.BranchID);
                cmd.Parameters.AddWithValue("@CityID", studentModel.CityID);
                cmd.Parameters.AddWithValue("@StudentName",studentModel.StudentName);
                cmd.Parameters.AddWithValue("@MobileNoStudent", studentModel.MobileNoStudent);
                cmd.Parameters.AddWithValue("@Email", studentModel.Email);
                cmd.Parameters.AddWithValue("@MobileNoFather", studentModel.MobileNoFather);
                cmd.Parameters.AddWithValue("@Address", studentModel.Address);
                cmd.Parameters.AddWithValue("@BirthDate", studentModel.BitrhDate);
                cmd.Parameters.AddWithValue("@Age", studentModel.Age);
                cmd.Parameters.AddWithValue("@IsActive", studentModel.IsActive);
                cmd.Parameters.AddWithValue("@Gender", studentModel.Gender);
                cmd.Parameters.AddWithValue("@Password", studentModel.Password);

                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (studentModel.StudentID == null)
                    {
                        TempData["message"] = "Student Insertd Successfully";
                    }
                    else
                    {
                        TempData["message"] = "Student Updated Successfully";
                    }
                }
            }
            FillBranchDDL();
            FillCityDDL();
            return View("StudentAddEdit");
        }

        public void FillCityDDL()
        {
            List<CityDropDownModel> list = new List<CityDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    CityDropDownModel cityDropDown = new CityDropDownModel()
                    {
                        CityID = Convert.ToInt32(dataReader["CityId"]),
                        CityName = dataReader["CityName"].ToString()
                    };
                    list.Add(cityDropDown);
                }
                dataReader.Close();
            }
            conn.Close();
            ViewBag.CityList = list;
        }

        public void FillBranchDDL()
        {
            List<BranchDropDownModel> list = new List<BranchDropDownModel>();
            SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("conn"));
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_SelectAll";
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    BranchDropDownModel BranchDropDown = new BranchDropDownModel()
                    {
                        BranchID = Convert.ToInt32(dataReader["BranchID"]),
                        BranchName = dataReader["BranchName"].ToString()
                    };
                    list.Add(BranchDropDown);
                }
                dataReader.Close();
                ViewBag.BranchList = list;
            }
        }
    }
}

