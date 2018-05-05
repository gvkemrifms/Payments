using System;

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
            _helper.InsertPaymentDetails(Convert.ToDateTime(txtDate.Text),(Convert.ToInt32(ddlState.SelectedValue)), Convert.ToInt32(ddlProject.SelectedValue), Convert.ToInt32(ddlSelectPayment.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(_userId));
            ClearControls();
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
        }
    }
}