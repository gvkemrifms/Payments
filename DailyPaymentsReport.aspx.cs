using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class DailyPaymentsReport : System.Web.UI.Page
    {
        DailyReportHelper rep = new DailyReportHelper();
        readonly Helper _helper = new Helper();
        public string _userId;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
            {
                _userId = (string)Session["UserId"];
            }
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
                BindStatesData();
                BindPayment();
                BindGridDetails();
            }
        }

        private void BindPayment()
        {
            try
            {
                var sqlQuery = "SELECT payment_type_id, payment_name FROM m_payments WHERE isactive = 1;";
                _helper.FillDropDownHelperMethod(sqlQuery, "payment_name", "payment_type_id", ddlSelectPayment);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }

        }

        private void BindStatesData()
        {
            try
            {

                _helper.FillDropDownHelperMethodWithSp("userbased_state", "state_name", "state_id", ddlState, null, "@uid", _userId);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                _helper.FillDropDownHelperMethodWithSp("userbased_projects", "project_name", "project_id", ddlState, ddlProject, "@uid", _userId, "@stid");
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                try
                {
                    _helper.InsertPaymentDetails(Convert.ToDateTime(txtDate.Text), (Convert.ToInt32(ddlState.SelectedValue)), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToInt32(ddlSelectPayment.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(_userId));
                }
                catch (Exception ex)
                {
                    _helper.ErrorsEntry(ex);
                }
            }
            else
            {
                try
                {
                    string pid = Session["IdCol"].ToString();
                    int result = _helper.UpdatePaymentsDetailsDetails(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToInt32(ddlSelectPayment.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(_userId),Convert.ToInt32(pid));

                    Show("Successfully Updated");
                }
                catch(Exception ex)
                {
                    _helper.ErrorsEntry(ex);
                }
            }
            
            ClearControls();
            BindGridDetails();
        }
        private void ClearControls()
        {
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtAmount.Text = "";
            ddlState.ClearSelection();
            ddlProject.ClearSelection();
            ddlSelectPayment.ClearSelection();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            btnSave.Text = "Save";
        }
        private void BindGridDetails()
        {
            try
            {

                _helper.FillDropDownHelperMethodWithSp("userpaymentstest_grid", null, null, null, null, "@uid", _userId, null, gvPayments);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void gvPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
  
        }

        protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //var ds = _helper.ReturnDS("userpayments_grid", "@uid", _userId);
            //ddlState.SelectedItem.Text = ds.Tables[0].Rows[0]["state_name"].ToString();
            //// ddlProject.SelectedItem.Text = ds.Tables[0].Rows[0]["project_name"].ToString();
            //txtDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["createdon"]);
            //txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["amount"]);
            //btnSave.Text = "Update";
            GridViewRow row1 = (GridViewRow)((ImageButton)sender).NamingContainer;
            string pid = ((Label)gvPayments.Rows[row1.RowIndex].FindControl("P_ID")).Text;
            Session["IdCol"] = pid;
            var ds = _helper.ReturnDS("userpaymentstest_grid", "@uid", _userId);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var custid = row["p_id"].ToString();
                if (custid == pid)
                {


                    ddlState.SelectedItem.Text = row["state_name"].ToString();

                    if (ddlProject.SelectedIndex == -1)
                        ddlProject.Items.Insert(0, "--Select--");
                    txtDate.Text = Convert.ToString(row["createdon"]);
                    txtAmount.Text = Convert.ToString(row["amount"]);
                    btnSave.Text = "Update";
                }
            }
        }

        protected void gvPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvPayments.Rows[e.RowIndex];
            Label usernamelable = (Label)row.FindControl("P_ID");
            string id = usernamelable.Text;
            
            //var ID = gvPayments.Rows[e.RowIndex].FindControl("P_ID");
            //int index = (e.RowIndex);
            string query = "delete  from t_payments where p_id='" + id + "'";
            int i = _helper.ExecuteInsertStatement(query);
            if (i > 0)
                BindGridDetails();
        }

        protected void gvPayments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvPayments.Rows[e.NewEditIndex];
            Label usernamelable = (Label)row.FindControl("P_ID");
            string id = usernamelable.Text;

        }
        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }
    }
}