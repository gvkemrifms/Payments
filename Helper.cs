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
        public void FillDropDownHelperMethodWithSp(string commandText, string textFieldValue = null, string valueField = null, DropDownList dropDownValue = null,DropDownList dropDownValue1=null, string parameterValue = null,string uid=null,string parameterValue1=null,GridView gvCollections=null)
        {
            string CONN_STRING = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(CONN_STRING);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand { Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText };
            if (dropDownValue != null )
            {
                if (parameterValue != null)
                    cmd.Parameters.AddWithValue(parameterValue,Convert.ToInt32(uid));
                if(parameterValue1!=null)
                    cmd.Parameters.AddWithValue(parameterValue1,Convert.ToInt32(dropDownValue.SelectedValue));
                if (parameterValue != null && parameterValue1 == null && gvCollections==null)
                {
                    CommonMethod(textFieldValue, valueField, dropDownValue, ds, cmd);
                    dropDownValue.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    CommonMethod(textFieldValue, valueField, dropDownValue1, ds, cmd);
                    dropDownValue1.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                
                }
            if (gvCollections != null)
            {
                cmd.Parameters.AddWithValue(parameterValue, Convert.ToInt32(uid));
                var da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                var dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvCollections.DataSource = dt;
                    gvCollections.DataBind();
                }
                else
                {
                    gvCollections.DataSource = null;
                    gvCollections.DataBind();
                }
            }
        }

        internal int UpdatePaymentsDetailsDetails(DateTime dateTime, int v1, int v2, decimal v3, int v4, int v5)
        {
            int num = 0;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@pdate", dateTime);
                cmd.Parameters.AddWithValue("@projid", v1);
                cmd.Parameters.AddWithValue("@paytid", v2);
                cmd.Parameters.AddWithValue("@amt", v3);
                cmd.Parameters.AddWithValue("@uid", v4);
                cmd.Parameters.AddWithValue("@pid", v5);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, "update_payments");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }
            return num;
        }
        public void FillDropDownHelperMethod(string query, string textFieldValue, string valueField, DropDownList dropdownId)
        {
            using (var con = new MySqlConnection(ConfigurationManager.AppSettings["GvkEmriCon"]))
            {
                con.Open();
                var cmd = new MySqlCommand(query, con);
                var da = new MySqlDataAdapter(cmd);
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
            using (var conn = new MySqlConnection(ConfigurationManager.AppSettings["GvkEmriCon"]))
            {
                using (var comm = new MySqlCommand())
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
        public int InsertCollectionDetails(int stateid, int projectId, DateTime DCDate, decimal amount, int uid)
        {
            int num = 0;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@sid", stateid);
                cmd.Parameters.AddWithValue("@pid", projectId);
                cmd.Parameters.AddWithValue("@date", DCDate);
                cmd.Parameters.AddWithValue("@amouont", amount);
                cmd.Parameters.AddWithValue("@uid", uid);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, "insert_collection");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }
            return num;
        }
        public int UpdateCollectionDetails( int projectId, DateTime DCDate, decimal amount, int uid,int custid)
        {
            int num = 0;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@projid", projectId);
                cmd.Parameters.AddWithValue("@ddate", DCDate);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@cid", custid);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, "update_collections");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }
            return num;
        }
        public int InsertPaymentDetails(DateTime datetime,int stateid, int projectId,int projectType, decimal amount, int uid)
        {
            int num = 0;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@sid", stateid);
                cmd.Parameters.AddWithValue("@pid", projectId);
                cmd.Parameters.AddWithValue("@payid", projectId);
                cmd.Parameters.AddWithValue("@paydate", datetime);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@userid", uid);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, "insert_payments");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }
            return num;
        }
        public static int ExecuteNonQuery(MySqlCommand cmd, CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["GvkEmriCon"]))
                {
                    PrepareCommand(cmd, conn, (MySqlTransaction)null, cmdType, cmdText);
                    num = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }
            return num;
        }
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                if (trans != null)
                    cmd.Transaction = trans;
                cmd.CommandType = cmdType;
            }
            catch (Exception ex)
            {
               
            }
        }
        public DataSet ReturnDS(string commandText, string parameterValue = null, string uid = null)
        {
            string CONN_STRING = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(CONN_STRING);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand { Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText };

                cmd.Parameters.AddWithValue(parameterValue, Convert.ToInt32(uid));
                var da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            return ds;
        }

    }
}