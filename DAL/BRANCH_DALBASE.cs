using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace StudentProject.DAL
{
	public class BRANCH_DALBASE
	{
        public DataTable getAllBranch(string conn, string procedurename)
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

        public DataTable getFillteredData(string conn, string procedurename, string? branchName, string? branchCode)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
            database.AddInParameter(command, "BranchName", SqlDbType.VarChar, branchName);
            database.AddInParameter(command, "BranchCode", SqlDbType.VarChar, branchCode);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}

