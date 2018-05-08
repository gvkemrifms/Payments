using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DailyCollectionAndPayments
{
    public partial class DailyPaymentsReport1 : System.Web.UI.Page
    {
        public IEnumerable<DailyReportHelper> reports;
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
                BindStatesData();
                txtDate.Text = DateTime.Now.Date.ToShortDateString();
                BindGridDetails();
            }


        }

        private void BindStatesData()
        {
            try
            {

                _helper.FillDropDownHelperMethodWithSp("userbased_state", "state_name", "state_id", ddlState, null, "@uid", _userId);
                if (ddlState.Items.Count == 2)
                    ddlState.SelectedIndex = 2;
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

                _helper.FillDropDownHelperMethodWithSp("userCollectiontest_grid", null, null, null, null, "@uid", _userId, null, gvDailyPayments);
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
                _helper.InsertCollectionDetails((Convert.ToInt32(ddlState.SelectedValue)), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(_userId));
                Show("Successfully Inserted");
            }
            else
            {
                string cid = Session["IdCol"].ToString();
                int result = _helper.UpdateCollectionDetails(Convert.ToInt32(ddlProject.SelectedValue), Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(_userId), Convert.ToInt32(cid));

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
            GridViewRow row = gvDailyPayments.Rows[e.NewEditIndex];
            Label usernamelable = (Label)row.FindControl("C_ID");
            string id = usernamelable.Text;
        }

        protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)sender).NamingContainer;
            string cid = ((Label)gvDailyPayments.Rows[row1.RowIndex].FindControl("C_ID")).Text;
            Session["IdCol"] = cid;
            var ds = _helper.ReturnDS("userCollectiontest_grid", "@uid", _userId);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var custid = row["c_id"].ToString();
                if (custid == cid)
                {


                    ddlState.SelectedItem.Text = row["state_name"].ToString();

                    if (ddlProject.SelectedIndex == -1)
                        ddlProject.Items.Insert(0, "--Select--");
                    txtDate.Text = Convert.ToString(row["date"]);
                    txtAmount.Text = Convert.ToString(row["amount"]);
                    btnSave.Text = "Update";
                }
            }

        }

        protected void btnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
        }

        protected void gvDailyPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDailyPayments.Rows[e.RowIndex];
            Label usernamelable = (Label)row.FindControl("C_ID");
            string id = usernamelable.Text;
            string query = "delete  from t_collections where c_id='" + id + "'";
            int i = _helper.ExecuteInsertStatement(query);
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
    }
}