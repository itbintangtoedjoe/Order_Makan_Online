using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Order_Makan_Online.Models;
using System.Web.Script.Serialization;

namespace Order_Makan_Online.Controllers
{
    public class FormController : Controller
    {
        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        //readonly ConnectionStringSettings SQLConnection = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable DT = new DataTable();
        // GET: Form
        public ActionResult FormOrder()
        {
            return View();
        }

        public ActionResult GenerateNoOrder(Form Model)
        {
            string result; ;
            List<string> ModelData = new List<string>();
            string ConString = connectionStringSettings.ConnectionString;
            SqlConnection Conn = new SqlConnection(ConString);
            try
            {
                Conn.Open();
                using (SqlCommand command = new SqlCommand("GENERATE_ORDER_NO", Conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //command.Parameters.Add("@OrderCat", SqlDbType.VarChar);
                    //command.Parameters["@OrderCat"].Value = Model.Category;

                    result = (string)command.ExecuteScalar();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {

                //result = ex.ToString();
                throw ex;
            }

            ModelData.Add(result);
            return Json(ModelData);
        }

        public JsonResult InsertOrder(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_INSERT_ORDER_MAKAN", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.Username;

                    command.Parameters.Add("@TanggalBuat", System.Data.SqlDbType.DateTime);
                    command.Parameters["@TanggalBuat"].Value = model.TanggalBuat;

                    command.Parameters.Add("@Department", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Department"].Value = model.Department;

                    command.Parameters.Add("@Lokasi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Lokasi"].Value = model.Lokasi;

                    command.Parameters.Add("@UserLastUpdate", System.Data.SqlDbType.DateTime);
                    command.Parameters["@UserLastUpdate"].Value = model.StartPeriod;

                    command.Parameters.Add("@TanggalClosing", System.Data.SqlDbType.DateTime);
                    command.Parameters["@TanggalClosing"].Value = model.EndPeriod;

                    command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                    command.Parameters["@Quantity"].Value = model.Quantity;

                    command.Parameters.Add("@Shift", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Shift"].Value = model.Shift;

                    result = (string)command.ExecuteScalar();
                    conn.Close();
                    ModelData.Add(result);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex.Message);
            }

            return Json(ModelData);
        }


        public JsonResult GetDetailOrder() //Untuk mengambil Header Detail
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            //myObject obj = serializer.Deserialize<yourObject>(yourJsonString);

            try
            {
                using (SqlCommand command = new SqlCommand("GET_DETAIL", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
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