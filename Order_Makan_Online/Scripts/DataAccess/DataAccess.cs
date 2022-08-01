using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static Order_Makan_Online.Models.DapperModel;

namespace ClaimOnline.Scripts.DataAccess
{
    public class DataAccess
    {

        readonly ConnectionStringSettings DBConString = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
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