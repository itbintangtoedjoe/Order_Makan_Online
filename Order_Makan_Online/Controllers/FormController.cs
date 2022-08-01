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
using Dapper;
using static Order_Makan_Online.Models.DapperModel;
using ClaimOnline.Scripts.DataAccess;

namespace Order_Makan_Online.Controllers
{
    public class FormController : Controller
    {
        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        //readonly ConnectionStringSettings SQLConnection = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable DT = new DataTable();
        DataAccess DAL = new DataAccess();
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

        public JsonResult InsertHeaderOrder(Form model)
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
                    command.Parameters["@UserLastUpdate"].Value = model.UserLastUpdate;

                    /*  command.Parameters.Add("@TanggalClosing", System.Data.SqlDbType.DateTime);
                      command.Parameters["@TanggalClosing"].Value = model.EndPeriod;

                      command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                      command.Parameters["@Quantity"].Value = model.Quantity;

                      command.Parameters.Add("@Shift", System.Data.SqlDbType.NVarChar);
                      command.Parameters["@Shift"].Value = model.Shift;*/

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

        public JsonResult InsertDetailOrder(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_INSERT_ORDER_MAKAN_DETAIL", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@UserID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@UserID"].Value = model.Username;

                   /* command.Parameters.Add("@Department", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Department"].Value = model.Department;

                    command.Parameters.Add("@Lokasi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Lokasi"].Value = model.Lokasi;
*/
                    command.Parameters.Add("@UserLastUpdate", System.Data.SqlDbType.DateTime);
                    command.Parameters["@UserLastUpdate"].Value = model.UserLastUpdate;

                    command.Parameters.Add("@OMD_Tanggal", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OMD_Tanggal"].Value = model.StartPeriod;

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

        public JsonResult InsertCreateOrderForm(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_INSERT_CREATE_ORDER_FORM", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@OMH_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMH_NO"].Value = model.omh_no;

                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.Username;

                    command.Parameters.Add("@TanggalBuat", System.Data.SqlDbType.DateTime);
                    command.Parameters["@TanggalBuat"].Value = model.TanggalBuat;

                    command.Parameters.Add("@Department", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Department"].Value = model.Department;

                    command.Parameters.Add("@Lokasi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Lokasi"].Value = model.Lokasi;

                    command.Parameters.Add("@UserLastUpdateHeader", System.Data.SqlDbType.DateTime);
                    command.Parameters["@UserLastUpdateHeader"].Value = model.UserLastUpdateHeader;

                    command.Parameters.Add("@OMD_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMD_NO"].Value = model.omd_no;

                    command.Parameters.Add("@UserIDDetail", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@UserIDDetail"].Value = model.UserIdDetail;

                    command.Parameters.Add("@UserLastUpdateDetail", System.Data.SqlDbType.DateTime);
                    command.Parameters["@UserLastUpdateDetail"].Value = model.UserLastUpdateDetail;

                    command.Parameters.Add("@OMD_Tanggal", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OMD_Tanggal"].Value = model.StartPeriod;

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
        public JsonResult GetDetailOrder(Form model) 
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();

            try
            {
                using (SqlCommand command = new SqlCommand("GET_DETAIL", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.Username;
                    command.Parameters.Add("@OMD_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMD_NO"].Value = model.OrderNum;

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


        public JsonResult UpdateOrderDetail(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_UPDATE_ORDER_MAKAN_DETAIL", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                    command.Parameters["@Quantity"].Value = model.Quantity;

                    command.Parameters.Add("@OMD_Shift", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMD_Shift"].Value = model.Shift;

                    command.Parameters.Add("@OMD_Tanggal", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OMD_Tanggal"].Value = model.Omd_Tanggal;

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
        
        public JsonResult GetDepartment(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);

            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_DEPARTMENT", conn))
                {
                    conn.Open();
                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.UserAD;
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

        public JsonResult GetLocation(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);

            try
            {
                using (SqlCommand command = new SqlCommand("SP_GET_LOCATION", conn))
                {
                    conn.Open();
                    command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@Username"].Value = model.UserAD;
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

        public JsonResult PopulateOrder(string num)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);

            try
            {
                using (SqlCommand command = new SqlCommand("SP_POPULATE_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@OMH_NO", SqlDbType.NVarChar);
                    command.Parameters["@OMH_NO"].Value = num;
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn dc in DT.Columns)
                {
                    row.Add(dc.ColumnName, dr[dc]);
                }
                rows.Add(row);
            }

            return Json(rows);
        }


        public JsonResult DeleteOrderHeader(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_DELETE_HEADER_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@OMH_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMH_NO"].Value = model.OrderNum;
                    command.Parameters.Add("@OMD_NO", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMD_NO"].Value = model.OrderNumDetail;

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

        public JsonResult DeleteOrderDetail(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_DELETE_DETAIL_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@OMD_Shift", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMD_Shift"].Value = model.Shift;

                    command.Parameters.Add("@OMD_Tanggal", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OMD_Tanggal"].Value = model.Omd_Tanggal;



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

        public JsonResult SubmitOrder(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_SUBMIT_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

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

        public JsonResult ApproveOrder(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_APPROVE_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@NIK", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NIK"].Value = model.NIK;

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

        public JsonResult RejectOrder(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_REJECT_ORDER", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    command.Parameters.Add("@OMH_ALASAN_REJECT", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OMH_ALASAN_REJECT"].Value = model.AlasanReject;

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

        public JsonResult InsertRevisi(Form model)
        {
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            List<string> ModelData = new List<string>();
            string result;

            try
            {
                using (SqlCommand command = new SqlCommand("SP_INSERT_REVISI", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@NoOrder", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@NoOrder"].Value = model.OrderNum;

                    /*command.Parameters.Add("@RevOdr", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@RevOdr"].Value = model.RevOdr;*/

                    command.Parameters.Add("@OmrQty", System.Data.SqlDbType.Int);
                    command.Parameters["@OmrQty"].Value = model.Quantity;

                    command.Parameters.Add("@OmrTanggal", System.Data.SqlDbType.DateTime);
                    command.Parameters["@OmrTanggal"].Value = model.Omd_Tanggal;

                    command.Parameters.Add("@OmrShift", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OmrShift"].Value = model.Shift;

                    command.Parameters.Add("@OmrUserID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@OmrUserID"].Value = model.Username;

                    command.Parameters.Add("@UserLastUpdate", System.Data.SqlDbType.DateTime);
                    command.Parameters["@UserLastUpdate"].Value = model.UserLastUpdate;

                    command.Parameters.Add("@OmrQtySbm", System.Data.SqlDbType.Int);
                    command.Parameters["@OmrQtySbm"].Value = model.QuantitySbm;

                    command.Parameters.Add("@OmrDept", System.Data.SqlDbType.VarChar);
                    command.Parameters["@OmrDept"].Value = model.Department;

                    command.Parameters.Add("@OmrLokasi", System.Data.SqlDbType.VarChar);
                    command.Parameters["@OmrLokasi"].Value = model.Lokasi;

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

        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname), JsonRequestBehavior.AllowGet);

        }



    }



}