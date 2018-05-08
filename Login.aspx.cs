using System;
using System.Data;
using System.IO;
using System.Net;

namespace DailyCollectionAndPayments
{
    public partial class Login : System.Web.UI.Page
    {
        private WebProxy objProxy1 = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtloginemail.Focus();

            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtloginemail.Text == "")
            {
                lblError.Text = "Please Enter Valid Credentials";
                return;
            }
          VerifyLoginDetails clslogin = new VerifyLoginDetails();
            DataTable dtLogin = new DataTable();
            dtLogin = clslogin.VerifyUser(txtloginemail.Text, txtPassword.Text);
          
            if (dtLogin.Rows.Count > 0)
            {
                Session["UserId"] = dtLogin.Rows[0][0].ToString();
                Response.Redirect("~/Default.aspx");
                return;
            }
            //else if (dtLogin.Rows[0]["role_id"].ToString() == "6" || dtLogin.Rows[0]["role_id"].ToString() == "9")
            //{
            //    string strUserId, strRoleId, strUserName;
            //    strUserId = dtLogin.Rows[0]["user_id"].ToString();
            //    strRoleId = dtLogin.Rows[0]["role_id"].ToString();
            //    strUserName = dtLogin.Rows[0]["user_name"].ToString();
            //    Session["user_name"] = dtLogin.Rows[0]["user_id"].ToString();
            //    Session["UserId"] = strUserId;
            //    Session["RoleId"] = strRoleId;
            //    Session["RName"] = strUserName;
            //    Response.Redirect("~/MainForm.aspx");
            //}
            else
            {

                lblError.Text = "Please Enter Valid Credentials";
                return;
            }
        }

        public string UrlResponse(string str2)
        {
            //  System.Object stringpost = "&phone_number=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=" + Mtype + "&DR=" + DR + "&SID=" + SID;
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create(str2);
                objWebRequest.Method = "POST";

                if ((objProxy1 != null))
                {
                    objWebRequest.Proxy = objProxy1;
                }
                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                //    objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            finally
            {

                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }
    }
}