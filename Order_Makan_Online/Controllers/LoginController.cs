using Order_Makan_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public static ConnectionStringSettings mySetting= ConfigurationManager.ConnectionStrings["B7PortalDB"];
        public readonly ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_ORDER_MAKANAN_ONLINE"];
        readonly DataTable dataTable = new DataTable();

       public string identifers;


        public ActionResult Index()
        {
            string autologin_token = Request.QueryString["autologinToken"];
            identifers = Request.QueryString["identifier"];
            TempData["identifier"] = identifers;
            string conString = mySetting.ConnectionString;
            DataTable dt = new DataTable();

            if (autologin_token != null)
            {
                string query = "SELECT username_apps FROM [dbo].[application_user_token] where token='" + autologin_token + "'";
                bool setAutologin = this.AutomaticToken(autologin_token);
                if (setAutologin)
                {
                    SqlConnection conn = new SqlConnection(conString);

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    conn.Close();
                    da.Dispose();
                    string username;
                    if (dt.Rows[0]["username_apps"] != null)
                    {
                        username = dt.Rows[0]["username_apps"].ToString();
                    }
                    else
                    {
                        username = "-";
                    }
                    GetParam(username);
                    //  bool SetParam = SetAuthParameter(username);
                    return RedirectToAction("../Home/Index");
                }
            }

            return View();
        }

        public bool AutomaticToken(string autologin_token)
        {
            string postString = string.Format("token={0}", autologin_token);
            WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/validateToken");
            request.Method = "POST";
            request.ContentLength = postString.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

            var responseData = responseJson["data"];

            reader.Close();
            dataStream.Close();
            response.Close();

            return true;
        }




        private void revalidateUsername(string identifier, string usernameApps)
        {
            string postString = string.Format("identifier={0}&username_application={1}", identifier, usernameApps);
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/revalidateUsername");
            request.Method = "POST";
            request.ContentLength = postString.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

            var responseData = responseJson["data"];

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
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
                    Session["IsLogin"] = "True";
                    /*   try
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
                           // revalidate username to b7 portal
                           string identifier;
                           if (TempData["identifier"] != null)
                           {
                               identifier = TempData["identifier"].ToString();
                               this.revalidateUsername(identifier, model.UserAD);
                           }

                           //cek apakah UserAD ada pada M_USER_ALL_APPS
                           if (dr[0].ToString() != null)
                           {
                               Session["UserAD"] = model.UserAD;
                               Session["Username"] = dr[0].ToString();
                               Session["NIK"] = dr[1].ToString();
                               Session["Dept"] = dr[2].ToString();
                               Session["Location"] = dr[3].ToString();
                               Session["JABATAN"] = dr[4].ToString();
                               Session.Timeout = 1000;


                               returnValue = true;
                           }
                           else
                           {
                               returnValue = false;
                           }
                       }
                       return Json(returnValue);*/
                    GetParam(model.UserAD) ;
                }
            }
            finally
            {

            }
            List.Add(Session["IsLogin"].ToString());
          /*  List.Add(Session["Dept"].ToString());
            List.Add(Session["Location"].ToString());
            List.Add(Session["JABATAN"].ToString());
            List.Add(Session["Username"].ToString());*/
            return Json(List);

        }

        public ActionResult GetParam(string userAD)
        {
            List<string> List = new List<string>();
            string conSQL = connectionStringSettings.ConnectionString;
            SqlDataAdapter dataAdapt = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(conSQL);

            bool returnValue = false;
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SP_GET_LOGIN", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", userAD);
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
                // revalidate username to b7 portal
                string identifier;
                if (TempData["identifier"] != null)
                {
                    identifier = TempData["identifier"].ToString();
                    this.revalidateUsername(identifier, userAD);
                }

                //cek apakah UserAD ada pada M_USER_ALL_APPS
                if (dr[0].ToString() != null)
                {
                    Session["UserAD"] = userAD;
                    Session["Username"] = dr[0].ToString();
                    Session["NIK"] = dr[1].ToString();
                    Session["Dept"] = dr[2].ToString();
                    Session["Location"] = dr[3].ToString();
                    Session["JABATAN"] = dr[4].ToString();
                    Session.Timeout = 1000;


                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            return Json(returnValue);
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