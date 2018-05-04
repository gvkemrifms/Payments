using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public class Helper
    {
        private static void CommonMethod(string textFieldValue, string valueField, DropDownList dropDownValue, DataSet ds, MySqlCommand cmd)
        {
            var da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            dropDownValue.DataSource = ds.Tables[0];
            dropDownValue.DataTextField = textFieldValue;
            dropDownValue.DataValueField = valueField;
            dropDownValue.DataBind();
        }
        public void FillDropDownHelperMethodWithSp(string commandText, string textFieldValue = null, string valueField = null, DropDownList dropDownValue = null, string parameterValue = null,int? uid=null)
        {
            string CONN_STRING = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(CONN_STRING);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand { Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText };
            if (dropDownValue != null )
            {
                if (parameterValue != null)
                    cmd.Parameters.AddWithValue(parameterValue,uid);
                CommonMethod(textFieldValue, valueField, dropDownValue, ds, cmd);
                dropDownValue.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
           
        public void FillDropDownHelperMethod(string query, string textFieldValue, string valueField, DropDownList dropdownId)
        {
            using (var con = new SqlConnection(ConfigurationManager.AppSettings["Str"]))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);
                dropdownId.Items.Clear();
                dropdownId.DataSource = ds.Tables[0];
                dropdownId.DataTextField = textFieldValue;
                dropdownId.DataValueField = valueField;
                dropdownId.DataBind();
                dropdownId.Items.Insert(0, new ListItem("--Select--", "0"));
                con.Close();
            }
        }
        public DataTable ExecuteSelectStmt(string query)
        {
            var cs = ConfigurationManager.AppSettings["GvkEmriCon"];
            var dtSyncData = new DataTable();
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(cs);
                connection.Open();
                var dataAdapter = new MySqlDataAdapter { SelectCommand = new MySqlCommand(query, connection) };
                dataAdapter.Fill(dtSyncData);
                TraceService(query);
                return dtSyncData;
            }
            catch (Exception ex)
            {
                TraceService("executeSelectStmt() " + ex + query);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public void TraceService(string content)
        {
            var str = @"C:\smslog_1\Log.txt";
            var path1 = str.Substring(0, str.LastIndexOf("\\", StringComparison.Ordinal));
            var path2 = str.Substring(0, str.LastIndexOf(".txt", StringComparison.Ordinal)) + "-" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                if (!Directory.Exists(path1)) Directory.CreateDirectory(path1);
                if (path2.Length >= Convert.ToInt32(4000000)) path2 = str.Substring(0, str.LastIndexOf(".txt", StringComparison.Ordinal)) + "-" + "2" + ".txt";
                var streamWriter = File.AppendText(path2);
                streamWriter.WriteLine("====================" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString());
                streamWriter.WriteLine(content);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch
            {
                // traceService(ex.ToString());
            }
        }

        public int ExecuteInsertStatement(string insertStmt)
        {
            using (var conn = new SqlConnection(ConfigurationManager.AppSettings["Str"]))
            {
                using (var comm = new SqlCommand())
                {
                    var i = 0;
                    comm.Connection = conn;
                    comm.CommandText = insertStmt;
                    try
                    {
                        conn.Open();
                        i = comm.ExecuteNonQuery();
                        TraceService(insertStmt);
                        return i;
                    }
                    catch (SqlException ex)
                    {
                        TraceService(" executeInsertStatement " + ex + insertStmt);
                        return i;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void ErrorsEntry(Exception ex)
        {
            var appSetting = ConfigurationManager.AppSettings["LogLocation"];
            if (appSetting == null) throw new ArgumentNullException(nameof(appSetting));
            var path = appSetting.Substring(0, appSetting.LastIndexOf("\\", StringComparison.Ordinal));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            using (var streamWriter = File.AppendText(ConfigurationManager.AppSettings["LogLocation"]))
            {
                var trace = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = trace.GetFrame(0);
                if (frame == null) throw new ArgumentNullException(nameof(frame));
                // Get the line number from the stack frame
                int errorNo = frame.GetFileLineNumber();
                //Get  Error Source
                string errorSource = ex.Source;
                if (errorSource == null) throw new ArgumentNullException(nameof(errorSource));
                //Get Error Description
                string errorDescription = ex.Message;
                streamWriter.WriteLine("====================" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString());
                streamWriter.WriteLine(errorDescription);
                streamWriter.WriteLine(errorSource);
                streamWriter.WriteLine(errorNo.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
    }
}