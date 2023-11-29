using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using StudentProject.Areas.State.Models;
using StudentProject.Areas.Student.Models;

namespace StudentProject.DAL
{
	public class STUDENT_DALBASE
	{
        public DataTable getAllStudent(string conn, string procedurename)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }

        public DataTable getFillteredData(string conn, string procedurename, StudentModel? studentModel)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
            database.AddInParameter(command, "@CityID", DbType.String, studentModel.CityID);
            database.AddInParameter(command, "@BranchID", DbType.String, studentModel.BranchID);
            database.AddInParameter(command, "@StudentName", DbType.String, studentModel.StudentName);

            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}

