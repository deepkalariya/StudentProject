using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace StudentProject.DAL
{
	public class LOC_STATE_DALBASE
	{
        public DataTable getAllState(string conn, string procedurename)
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

