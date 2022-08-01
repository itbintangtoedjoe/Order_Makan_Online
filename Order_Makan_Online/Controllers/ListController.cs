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
    public class ListController : Controller
    {
        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable dataTable = new DataTable();
        // GET: List
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListRevisi()
        {
            return View();
        }

        public ActionResult ListMakanan()
        {
            return View();
        }


        public JsonResult GetDDLRevisi()
        {   
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);

            try
            {
                using (SqlCommand command = new SqlCommand("SP_DDL_NOREVISI", conn))
                {
                    conn.Open();
                  
                    command.CommandType = CommandType.StoredProcedure;
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(dataTable);
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
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return Json(rows);
        }

        public JsonResult GettblListRevisi(ListModel model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);


            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_REVISI", conn))
                {
                    conn.Open();
                    command.Parameters.Add("@OMR_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMR_NO"].Value = model.OMR_NO;
                    /*command.Parameters.Add("@OMR_TANGGAL", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OMR_TANGGAL"].Value = model.OMR_TANGGAL;*/
                    command.CommandType = CommandType.StoredProcedure;
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(dataTable);
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    row.Add(dc.ColumnName, dr[dc]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

        public JsonResult GetListMakanan(Form model) 
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
          
            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_LIST_MAKANAN", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.Username;

                    command.Parameters.Add("@OMH_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMH_NO"].Value = model.omh_no;

                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(dataTable);
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
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }

     


    }
}