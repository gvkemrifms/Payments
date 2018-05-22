using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class ExpectedDailyCollections : Page
    {
        private readonly Helper _helper = new Helper();
        public string UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string)Session["UserId"];
            if (!IsPostBack)
            {
                BindStatesData();
                txtDate.Text = DateTime.Now.Date.ToShortDateString();
                BindGridDetails();
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

        private void BindGridDetails()
        {
            try
            {
                _helper.FillDropDownHelperMethodWithSp("userexpected_Collectiontest_grid", null, null, null, null, "@uid", UserId, null, gvDailyPayments);
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
            {
                _helper.InsertCollectionDetails(Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(UserId));
                Show("Successfully Inserted");
            }
            else
            {
                var cid = Session["IdCol"].ToString();
                _helper.UpdateCollectionDetails(Convert.ToInt32(ddlProject.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(UserId), Convert.ToInt32(cid));
                btnSave.Text = "Save";
                Show("Successfully Updated");
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
            btnSave.Text = "Save";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvDailyPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvDailyPayments_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            var row1 = (GridViewRow)((ImageButton)sender).NamingContainer;
            var cid = ((Label)gvDailyPayments.Rows[row1.RowIndex].FindControl("C_ID")).Text;
            Session["IdCol"] = cid;
            var ds = _helper.ReturnDs("user_expected_Collection_grid", "@uid", UserId);
            ClearControls();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var custid = row["c_id"].ToString();
                if (custid == cid)
                {
                    ClearControls();
                    ddlState.Items.FindByText(row["state_name"].ToString()).Selected = true;
                    _helper.FillDropDownHelperMethodWithSp("userbased_projects", "project_name", "project_id", ddlState, ddlProject, "@uid", UserId, "@stid");
                    ddlProject.Items.FindByText(row["project_name"].ToString()).Selected = true;
                    txtDate.Text = Convert.ToString(row["date"]);
                    txtAmount.Text = Convert.ToString(row["amount"]);
                    btnSave.Text = "Update";
                }
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void gvDailyPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = gvDailyPayments.Rows[e.RowIndex];
            var usernamelable = (Label)row.FindControl("C_ID");
            var id = usernamelable.Text;
            var query = "delete  from t_expected_collections where c_id='" + id + "'";
            var i = _helper.ExecuteInsertStatement(query);
            if (i > 0)
                BindGridDetails();
        }

        protected void gvDailyPayments_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }

        protected void gvDailyPayments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        public void Show(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + message + "');", true);
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}