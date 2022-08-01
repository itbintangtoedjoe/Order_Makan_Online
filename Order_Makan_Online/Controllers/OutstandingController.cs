using Order_Makan_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order_Makan_Online.Controllers
{
    public class OutstandingController : Controller
    {

        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        //readonly ConnectionStringSettings SQLConnection = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable DT = new DataTable();
        // GET: Outstanding
        public ActionResult OutstandingApv()
        {
            return View();
        }

        public JsonResult GetOutStandingTable(Outstanding model) 
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            //myObject obj = serializer.Deserialize<yourObject>(yourJsonString);

            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_PENDING_TASK", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@EMPname", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@EMPname"].Value = model.Username;

                    command.Parameters.Add("@Jabatan", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Jabatan"].Value = model.Jabatan;

                    /*command.Parameters.Add("@NIK", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NIK"].Value = model.NIK;*/

                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex.Message);
            }

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }


    }
}