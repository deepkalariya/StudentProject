﻿using System;
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
    }
}
