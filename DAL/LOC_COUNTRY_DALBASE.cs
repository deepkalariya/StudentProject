using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace StudentProject.DAL
{
	public class LOC_COUNTRY_DALBASE
	{
		public DataTable getAllCountry(string conn, string procedurename)
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

		public DataTable getFillteredData(string conn,string procedurename, string? countryName, string? countryCode)
		{
			DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
			database.AddInParameter(command,"CountryName",SqlDbType.VarChar,countryName);
            database.AddInParameter(command, "CountryCode", SqlDbType.VarChar,countryCode);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
		}
	}
}

