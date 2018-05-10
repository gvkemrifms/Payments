using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class DailyPaymentsReport : Page
    {
        private readonly Helper _helper = new Helper();
        public string UserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string) Session["UserId"];
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
                _helper.FillDropDownHelperMethodWithSp("userbased_state", "state_name", "state_id", ddlState, null, "@uid", UserId);
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
                _helper.FillDropDownHelperMethodWithSp("userbased_projects", "project_name", "project_id", ddlState, ddlProject, "@uid", UserId, "@stid");
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
                try
                {
                    _helper.InsertPaymentDetails(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToInt32(ddlSelectPayment.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(UserId));
                }
                catch (Exception ex)
                {
                    _helper.ErrorsEntry(ex);
                }
            else
                try
                {
                    var pid = Session["IdCol"].ToString();
                    _helper.UpdatePaymentsDetailsDetails(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToInt32(ddlSelectPayment.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(UserId), Convert.ToInt32(pid));
                    btnSave.Text = "Save";
                    Show("Successfully Updated");
                }
                catch (Exception ex)
                {
                    _helper.ErrorsEntry(ex);
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
                _helper.FillDropDownHelperMethodWithSp("userpaymentstest_grid", null, null, null, null, "@uid", UserId, null, gvPayments);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void gvPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            var row1 = (GridViewRow) ((ImageButton) sender).NamingContainer;
            var pid = ((Label) gvPayments.Rows[row1.RowIndex].FindControl("P_ID")).Text;
            Session["IdCol"] = pid;
            var ds = _helper.ReturnDs("userpaymentstest_grid", "@uid", UserId);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var custid = row["p_id"].ToString();
                if (custid == pid)
                {
                    ClearControls();
                    if (ddlState != null)
                    {
                        ddlState.Items.FindByText(row["state_name"].ToString()).Selected = true;
                        _helper.FillDropDownHelperMethodWithSp("userbased_projects", "project_name", "project_id", ddlState, ddlProject, "@uid", UserId, "@stid");
                    }

                    ddlProject.Items.FindByText(row["project_name"].ToString()).Selected = true;
                    ddlSelectPayment.Items.FindByText(row["payment_name"].ToString()).Selected = true;
                    txtDate.Text = Convert.ToString(row["createdon"]);
                    txtAmount.Text = Convert.ToString(row["amount"]);
                    btnSave.Text = "Update";
                }
            }
        }

        protected void gvPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = gvPayments.Rows[e.RowIndex];
            var usernamelable = (Label) row.FindControl("P_ID");
            var id = usernamelable.Text;
            var query = "delete  from t_payments where p_id='" + id + "'";
            var i = _helper.ExecuteInsertStatement(query);
            if (i > 0)
                BindGridDetails();
        }

        protected void gvPayments_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }
    }
}