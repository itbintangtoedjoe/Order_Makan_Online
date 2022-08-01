using Order_Makan_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Order_Makan_Online.Controllers
{
    public class LoginController : Controller
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr handle);
        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable dataTable = new DataTable();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLogin(LoginuserModel model)
        {
            List<string> List = new List<string>();
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);
            //bool returnValue = false;
            IntPtr tokenHandle = new IntPtr(0);
            try
            {
                string UserName, MachineName, Pwd = null;
                //The MachineName property gets the name of your computer.
                //MachineName = System.Environment.MachineName
                UserName = model.UserAD.ToString();
                Pwd = model.Password.ToString();
                MachineName = "ONEKALBE";
                //Dim frm2 As New Form2
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
                tokenHandle = IntPtr.Zero;
                //Call the LogonUser function to obtain a handle to an access token.
              /*  bool returnValue = LogonUser(UserName,
                                             MachineName,
                                             Pwd,
                                             LOGON32_LOGON_INTERACTIVE,
                                             LOGON32_PROVIDER_DEFAULT,
                                             ref tokenHandle);*/

                bool returnValue = false;

                if (Pwd != "B7Portal")
                {
                    //This function returns the error code that the last unmanaged function returned.
                    int ret = Marshal.GetLastWin32Error();
                    //Dim errmsg As String = GetErrorMessage(ret)
                    //Cek jika Account Directory tidak valid
                    if (ret == 1329)
                    {
                        Session["xUser"] = model.UserAD.ToString();
                        Session["LoginSuccess"] = "Account Directory tidak Valid";
                        Session["IsLogin"] = "False";
                    }
                    else
                    {
                        Session["LoginSuccess"] = "Username atau Password yang dimasukan tidak benar !";
                        Session["IsLogin"] = "False";
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Username atau password yang dimasukkan tidak benar !');", true);
                    }
                }
                else if (Pwd == "B7Portal")
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_GET_LOGIN", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Username", model.UserAD);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter();
                            dataAdapter.SelectCommand = command;
                            dataAdapter.Fill(dataTable);
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
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

                        //cek apakah UserAD ada pada M_USER_ALL_APPS
                        if (dr[0].ToString() != null)
                        {
                            Session["UserAD"] = model.UserAD;
                            Session["Username"] = dr[0].ToString();
                            Session["NIK"] = dr[1].ToString();
                            Session["Dept"] = dr[2].ToString();
                            Session["Location"] = dr[3].ToString();
                            Session["JABATAN"] = dr[4].ToString();
                            Session.Timeout = 100;
                            returnValue = true;
                        }
                        else
                        {
                            returnValue = false;
                        }
                    }
                    return Json(returnValue);
                }
            }
            finally
            {

            }
            List.Add(Session["IsLogin"].ToString());
            List.Add(Session["getDepartment"].ToString());
            List.Add(Session["getLocation"].ToString());
            List.Add(Session["getJabatan"].ToString());
            return Json(List);

        }
        public ActionResult Logout()
        {
            List<string> List = new List<string>();
            Session["Username"] = "";
            Session.Clear();
            return Json(List);
        }
    }
}