using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using StudentProject.Areas.State.Models;

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

        public DataTable getFillteredData(string conn, string procedurename, LOC_StateModel? model_LOC_State)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
                database.AddInParameter(command, "@CountryID", DbType.String, model_LOC_State.CountryID);
                database.AddInParameter(command, "@StateName", DbType.String, model_LOC_State.StateName);
                database.AddInParameter(command, "@StateCode", DbType.String, model_LOC_State.StateCode);

            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}

