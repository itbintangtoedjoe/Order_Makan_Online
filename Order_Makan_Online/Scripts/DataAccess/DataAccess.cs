using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClaimOnline.Scripts.DataAccess
{
    public class DataAccess
    {

        readonly ConnectionStringSettings DBConString = ConfigurationManager.ConnectionStrings["NewB7"];
        public string StoredProcedure(DynamicParameters parameters, String Spname)
        {
            string result;

            using (IDbConnection db = new SqlConnection(DBConString.ConnectionString))
            {
                var StoredProcedure = db.Query<dynamic>(Spname, parameters,
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(StoredProcedure, Formatting.Indented);
                result = json;
            }

            return result;
        }
    }
}