using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class ExpectedDailyCollections : Page
    {
        private readonly ExpectedColectionsModel _collection = new ExpectedColectionsModel();
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
                BindStatesData();
                FillDate();
                txtYear.Text = DateTime.Now.Year.ToString();
                txtMonth.Text = DateTime.Now.Month.ToString();
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
                try
                {
                    InsertExpectedCollectionDetails();
                    BindGridDetails();
                }
                catch (Exception ex)
                {
                    _helper.ErrorsEntry(ex);
                }

                Show("Successfully Inserted");
            }
            else
            {
                try
                {
                    btnSave.Text = "Save";
                    UpdateExpectedCollectionDetails();
                    BindGridDetails();
                }
                catch (Exception ex)
                {
                    _helper.ErrorsEntry(ex);
                }

                Show("Successfully Updated");
            }

            ClearControls();
            BindGridDetails();
        }

        private void InsertExpectedCollectionDetails()
        {
            var insertCollection = ExpectedCollectionDetails();
            _helper.InsertexpectedCollectionDetails(Convert.ToInt32(insertCollection.State), Convert.ToInt32(insertCollection.Project), insertCollection.Amount, Convert.ToInt32(UserId), insertCollection.ExpectedStartDate, insertCollection.ExpectedEndDate);
        }

        private void UpdateExpectedCollectionDetails()
        {
            var updateCollection = ExpectedCollectionDetails();
            _helper.InsertexpectedCollectionDetails(Convert.ToInt32(updateCollection.State), Convert.ToInt32(updateCollection.Project), updateCollection.Amount, Convert.ToInt32(UserId), updateCollection.ExpectedStartDate, updateCollection.ExpectedEndDate, Convert.ToInt32(Session["IdCol"]));
        }

        private ExpectedColectionsModel ExpectedCollectionDetails()
        {
            try
            {
                DateTime dtt;
                _collection.State = ddlState.SelectedValue;
                _collection.Project = ddlProject.SelectedValue;
                _collection.Amount = decimal.Parse(txtAmount.Text);
                _collection.Day = ddldate.SelectedItem.Text;
                var charRange = '-';
                var startIndex = 0;
                var endIndex = _collection.Day.LastIndexOf(charRange);
                var length = endIndex - startIndex;
                var startday = _collection.Day.Substring(startIndex, length);
                var firstDayconcatinated = txtMonth.Text + "/" + startday + "/" + txtYear.Text;
                _collection.ExpectedStartDate = startday.Length == 1 ? DateTime.Parse(firstDayconcatinated) : DateTime.ParseExact(firstDayconcatinated, "M/dd/yyyy", CultureInfo.InvariantCulture);
                var lastday = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));
                var lastDayOfMonth = lastday.ToString();
                var enddDay = _collection.Day.Substring(_collection.Day.IndexOf('-') + 1);
                var check = 0;
                var lastDayconcatinated = "";
                if (int.TryParse(enddDay, out check))
                    lastDayconcatinated = txtMonth.Text + "/" + enddDay + "/" + txtYear.Text;
                else
                    lastDayconcatinated = txtMonth.Text + "/" + lastDayOfMonth + "/" + txtYear.Text;

                _collection.ExpectedEndDate = DateTime.ParseExact(lastDayconcatinated, "M/dd/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }

            return _collection;
        }

        private void ClearControls()
        {
            txtAmount.Text = "";
            ddlState.ClearSelection();
            ddlProject.ClearSelection();
            ddldate.ClearSelection();
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
            var row1 = (GridViewRow) ((ImageButton) sender).NamingContainer;
            var cid = ((Label) gvDailyPayments.Rows[row1.RowIndex].FindControl("C_ID")).Text;
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
            var usernamelable = (Label) row.FindControl("C_ID");
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

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void FillDate()
        {
            ddldate.ClearSelection();
            ddldate.Items.Add("1-10");
            ddldate.Items.Add("11-20");
            ddldate.Items.Add("21-MonthEnd");
            ddldate.Items.Insert(0, "--Select--");
        }

        protected void txtYear_TextChanged(object sender, EventArgs e)
        {
        }
    }
}