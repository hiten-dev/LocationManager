using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LocationManager.Classes
{
    public class DataAccess
    {
        SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        public DataSet RunProc(string procedureName, List<SqlParameter> parameters, out string msg, out int code)
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(procedureName, connection);
                cmd.CommandTimeout = 600;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                msg = "";
                code = -1;
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataAdapter sqa = new SqlDataAdapter(cmd);
                sqa.Fill(ds);

            }
            catch (Exception e)
            {
                msg = "";
                code = -1;
            }
            return ds;
        }
    }
}