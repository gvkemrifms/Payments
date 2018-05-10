using System;
using System.Data;
using System.Web.UI;

namespace DailyCollectionAndPayments
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        public string UserName { get; set; }
        public string UserId;
        private readonly Helper _helper = new Helper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string)Session["UserId"];
            if (!IsPostBack)
            {

            }
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtnewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
            txtcurrentPassword.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string query = "select user_id,password from m_users where isactive=1 ";
            try
            {
               DataTable dtCredentials= _helper.ExecuteSelectStmt(query);
                foreach (DataRow row in dtCredentials.Rows)
                {
                    if (row["password"].ToString() == txtcurrentPassword.Text)
                    {
                        string queryMap= "update m_users set Password='" + txtnewPassword.Text + "' where user_id='" + Session["UserId"] + "'";
                        _helper.ExecuteInsertStatement(queryMap);
                        Show("Password Updated Successfully");
                    }

                }
               Show("Error updating the Password");
            
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }
    }
}