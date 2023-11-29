using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using StudentProject.Areas.City.Models;

namespace StudentProject.DAL
{
	public class LOC_CITY_DALBASE
	{
        public DataTable getAllCity(string conn, string procedurename)
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

        public DataTable getFillteredData(string conn, string procedurename, LOC_CityModel? model_LOC_City)
        {
            DataTable dt = new DataTable();
            SqlDatabase database = new SqlDatabase(conn);
            DbCommand command = database.GetStoredProcCommand(procedurename);
                database.AddInParameter(command, "@CountryID", DbType.String, model_LOC_City.CountryID);
                database.AddInParameter(command, "@StateID", DbType.String, model_LOC_City.StateID);
                database.AddInParameter(command, "@CityName", DbType.String, model_LOC_City.CityName);
                database.AddInParameter(command, "@CityCode", DbType.String, model_LOC_City.CityCode);


            using (IDataReader reader = database.ExecuteReader(command))
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}

