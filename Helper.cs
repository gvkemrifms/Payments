using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DailyCollectionAndPayments
{
    public class Helper
    {
        public void FillDropDownHelperMethodWithSp1(string commandText, string textFieldValue = null, string valueField = null, DropDownList dropDownValue = null, DropDownList dropDownValue1 = null, string parameterValue = null, string uid = null, string parameterValue1 = null, GridView gvCollections = null)
        {
            var connString = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(connString);
            var ds = new DataSet();

            conn.Open();
            var cmd = new MySqlCommand {Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText};
            if (dropDownValue != null)
            {
                if (parameterValue != null)
                    cmd.Parameters.AddWithValue(parameterValue, Convert.ToInt32(dropDownValue.SelectedValue));
                if (parameterValue1 != null)
                    if (dropDownValue1 != null)
                        cmd.Parameters.AddWithValue(parameterValue1, Convert.ToInt32(dropDownValue1.SelectedValue));
                var da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                var dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns[0].ColumnName = "STATES";
                    if (gvCollections != null)
                    {
                        gvCollections.DataSource = dt;
                        gvCollections.DataBind();
                    }
                }
                else
                {
                    if (gvCollections != null)
                    {
                        gvCollections.DataSource = null;
                        gvCollections.DataBind();
                    }
                }
            }
        }

        private static void CommonMethod(string textFieldValue, string valueField, DropDownList dropDownValue, DataSet ds, MySqlCommand cmd)
        {
            var da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            dropDownValue.DataSource = ds.Tables[0];
            dropDownValue.DataTextField = textFieldValue;
            dropDownValue.DataValueField = valueField;
            dropDownValue.DataBind();
        }

        public void FillDropDownHelperMethodWithSp(string commandText, string textFieldValue = null, string valueField = null, DropDownList dropDownValue = null, DropDownList dropDownValue1 = null, string parameterValue = null, string uid = null, string parameterValue1 = null, GridView gvCollections = null)
        {
            var connString = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(connString);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand {Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText};
            if (dropDownValue != null)
            {
                if (parameterValue != null && uid != null)
                    cmd.Parameters.AddWithValue(parameterValue, Convert.ToInt32(uid));
                if (parameterValue1 != null)
                    cmd.Parameters.AddWithValue(parameterValue1, Convert.ToInt32(dropDownValue.SelectedValue));
                if (parameterValue != null && parameterValue1 == null && gvCollections == null)
                {
                    CommonMethod(textFieldValue, valueField, dropDownValue, ds, cmd);
                    dropDownValue.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    CommonMethod(textFieldValue, valueField, dropDownValue1, ds, cmd);
                    if (dropDownValue1 != null) dropDownValue1.Items.Insert(0, new ListItem("--Select--", "0"));
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

        public DataTable FillGrid(string commandText, DropDownList dropDownValue = null, DropDownList dropDownValue1 = null, string parameterValue = null, string parameterValue1 = null, GridView gvCollections = null)
        {
            var connString = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(connString);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand {Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText};
            if (dropDownValue != null) cmd.Parameters.AddWithValue(parameterValue, dropDownValue.SelectedValue);
            if (dropDownValue1 != null) cmd.Parameters.AddWithValue(parameterValue1, dropDownValue1.SelectedValue);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            var dt = ds.Tables[0];
            return dt;
        }

        internal int UpdatePaymentsDetailsDetails(DateTime dateTime, int v1, int v2, decimal v3, int v4, int v5)
        {
            var num = 0;
            var cmd = new MySqlCommand();
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
                var dataAdapter = new MySqlDataAdapter {SelectCommand = new MySqlCommand(query, connection)};
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
                var errorNo = frame.GetFileLineNumber();
                //Get  Error Source
                var errorSource = ex.Source;
                if (errorSource == null) throw new ArgumentNullException(nameof(errorSource));
                //Get Error Description
                var errorDescription = ex.Message;
                streamWriter.WriteLine("====================" + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString());
                streamWriter.WriteLine(errorDescription);
                streamWriter.WriteLine(errorSource);
                streamWriter.WriteLine(errorNo.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        public int InsertCollectionDetails(int stateid, int projectId, DateTime dcDate, decimal amount, int uid)
        {
            var num = 0;
            var cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@sid", stateid);
                cmd.Parameters.AddWithValue("@pid", projectId);
                cmd.Parameters.AddWithValue("@date", dcDate);
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

        public int UpdateCollectionDetails(int projectId, DateTime dcDate, decimal amount, int uid, int custid)
        {
            var num = 0;
            var cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@projid", projectId);
                cmd.Parameters.AddWithValue("@ddate", dcDate);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@cid", custid);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, "update_Collections");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }

            return num;
        }

        public int InsertexpectedCollectionDetails(int stateid, int projectId, decimal amount, int uid, DateTime startDate, DateTime endDate, int? cid = null)
        {
            var num = 0;
            var cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@sid", stateid);
                cmd.Parameters.AddWithValue("@pid", projectId);
                cmd.Parameters.AddWithValue("@amouont", amount);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@sDate", startDate);
                cmd.Parameters.AddWithValue("@eDate", endDate);
                if (cid != null)
                    cmd.Parameters.AddWithValue("@cid", cid);
                num = ExecuteNonQuery(cmd, CommandType.StoredProcedure, cid == null ? "insert_ExpectedCollection" : "update_ExpectedCollection");
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }

            return num;
        }

        public int InsertPaymentDetails(DateTime datetime, int stateid, int projectId, int projectType, decimal amount, int uid)
        {
            var num = 0;
            var cmd = new MySqlCommand();
            try
            {
                cmd.Parameters.AddWithValue("@sid", stateid);
                cmd.Parameters.AddWithValue("@pid", projectId);
                cmd.Parameters.AddWithValue("@payid", projectType);
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
            var num = 0;

            using (var conn = new MySqlConnection(ConfigurationManager.AppSettings["GvkEmriCon"]))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText);
                num = cmd.ExecuteNonQuery();
            }

            return num;
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
        }

        public DataSet ReturnDs(string commandText, string parameterValue = null, string uid = null)
        {
            var connString = ConfigurationManager.AppSettings["GvkEmriCon"];
            var conn = new MySqlConnection(connString);
            var ds = new DataSet();
            conn.Open();
            var cmd = new MySqlCommand {Connection = conn, CommandType = CommandType.StoredProcedure, CommandText = commandText};

            cmd.Parameters.AddWithValue(parameterValue, Convert.ToInt32(uid));
            var da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public void LoadExcelSpreadSheet(Page page, Panel panel = null, string fileName = null, GridView gridView = null)
        {
            page.Response.ClearContent();
            page.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            page.Response.ContentType = "application/excel";
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);

            if (gridView != null)
                gridView.RenderControl(htw);
            else
                panel.RenderControl(htw);
            page.Response.Write(sw.ToString());
            page.Response.End();
        }

        public void SendMailMessage(string fromEmailAddress, string recipients, string subject, string bodyMessage, string hostname, string password, decimal expectamount, decimal actualamount)
        {
            var mailText = "";
            try
            {
                var greeting = "";
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 11)
                    greeting = "Good Morning,";
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 15)
                    greeting = "Good Afternoon,";
                else
                    greeting = "Good Evening,";
                mailText = mailText + "<div style='color:darkblue;font-size:14px;font-family:Trebuchet MS;'><br />" + greeting + "<br /><br /><b> Please find below EstimatedCollection Vs ActualCollection details.";
                mailText = mailText + " </b> <br /> <br />  </div>";
                mailText = mailText + "<table border='1' cellpadding='2' cellspacing='0' style='color:darkblue; border-color:Highlight; font-size:14px; font-family:Trebuchet MS; '>";
                mailText = mailText + "<tr style='background-color: brown;'> "
                                    + "      <td style='width:200px;'> Estimated Collection </td> "
                                    + "      <td style='width:200px;'> Actual Collection </td>"
                                    + "      <td style='width:200px;'> Balance Amount </td> </tr>";
                mailText = mailText + "<tr>";
                mailText = mailText + "<td style='text-align:center'>" + expectamount + "</td>";
                mailText = mailText + "<td style='text-align:center'>" + actualamount + "</td>";
                mailText = mailText + "<td style='text-align:center'>" + (expectamount - actualamount) + "</td>";
                mailText = mailText + "</tr>";
                mailText = mailText + "</table>";
                mailText = mailText + "<br /><div style='color:black;font-size:10px;font-family:Trebuchet MS;'><br />* This is system generated E-mail.</div>";
                var email = new MailMessage {From = new MailAddress(fromEmailAddress)};
                email.To.Add(recipients);
                email.Subject = subject;
                email.Body = mailText;
                email.IsBodyHtml = true;
                var smtp = new SmtpClient
                {
                Host = hostname,
                Port = 25,
                Credentials = new NetworkCredential(fromEmailAddress, password)
                };
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                ErrorsEntry(ex);
            }
        }

        public List<decimal> ShowMailButton(GridViewRow row, LinkButton lnkSendEmail, string expectedCollections, string actualCollections)
        {
            decimal estimatedTotal;
            decimal actualTotal = 0;
            var nums = new List<decimal>();

            if (decimal.TryParse(((Label) row.FindControl(expectedCollections)).Text, out estimatedTotal))
            {
                if (decimal.TryParse(((Label) row.FindControl(actualCollections)).Text, out actualTotal))
                    lnkSendEmail.Visible = actualTotal < estimatedTotal;
                else
                    lnkSendEmail.Visible = false;
            }
            else
            {
                lnkSendEmail.Visible = false;
            }

            nums.Add(estimatedTotal);
            nums.Add(actualTotal);
            return nums;
        }

        private void SendEmail(IEnumerable<string> emailids, decimal estimatedAmount, decimal actualAmount, DataTable dataTable, string stateProject)
        {
            var subject = stateProject + "-" + "Expected  Collection Not Met";
            var ids = string.Join(",", emailids);
            var message = stateProject + "-" + "Expected Collection Not Met Actual Collection Estimated Collection =" + "  " + estimatedAmount + "and Actual Collection =" + " " + actualAmount;
            var userName = dataTable.AsEnumerable().Select(x => x.Field<string>("sendingMailId")).First();
            var password = dataTable.AsEnumerable().Select(x => x.Field<string>("PASSWORD")).First();
            SendMailMessage(userName, ids, subject, message, ConfigurationManager.AppSettings["hostname"], password, estimatedAmount, actualAmount);
        }

        public void SendEmailToSpecificStates(DataTable dt, decimal estimated, decimal actual, string stateProject)
        {
            IEnumerable<string> emailids = dt.AsEnumerable().Select(x => x.Field<string>("email_id"));
            var query = "SELECT sendingMailId,PASSWORD FROM rpt_finance_mail_sync";
            var dataTable = ExecuteSelectStmt(query);
            SendEmail(emailids, estimated, actual, dataTable, stateProject);
        }

    }
}