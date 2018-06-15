using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class ActualExpectedCollectionReports : Page
    {
        private readonly Helper _helper = new Helper();
        private List<decimal> _numbers;
        public string UserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string) Session["UserId"];
            if (!IsPostBack)
            {
                BindYearDropDown();
                BindMonthsDropdown();
                BindGridData();
            }
        }

        private void BindYearDropDown()
        {
            for (var i = 2018; i <= 2025; i++) ddlyear.Items.Add(i.ToString());
            ddlyear.SelectedItem.Text = DateTime.Now.Year.ToString();
        }

        private void BindMonthsDropdown()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (var i = 0; i < 13; i++) ddlmonth.Items.Add(new ListItem(months[i], (i + 1).ToString()));
            ddlmonth.Items.Insert(0, "--Select--");
            var month = ddlmonth.Items.FindByText(DateTime.Today.ToString("MMMM")).ToString();
            ddlmonth.Items.FindByText(month).Selected = true;
        }

        protected void btnShowReport_OnClick(object sender, EventArgs e)
        {
            BindGridData();
        }

        private void BindGridData()
        {
            try
            {
                var dt = _helper.FillGrid("report_projectwise_collection", ddlmonth, ddlyear, "@mnt", "@yr", gvActualCollectionReport);
                if (dt.Rows.Count > 0)
                {
                    if (gvActualCollectionReport != null)
                    {
                        gvActualCollectionReport.DataSource = dt;
                        gvActualCollectionReport.DataBind();

                        foreach (GridViewRow row in gvActualCollectionReport.Rows)
                        {
                            var lnksendEmail = (LinkButton) row.FindControl("lnkSendEmail");
                            var lnksendEmail1To10 = (LinkButton) row.FindControl("lnkSendEmail1to10");
                            var lnksendEmail11To20 = (LinkButton) row.FindControl("lnkSendEmail11to20");
                            var lnksendEmail21Toend = (LinkButton) row.FindControl("lnkSendEmail21toend");
                            _numbers = _helper.ShowMailButton(row, lnksendEmail21Toend, "lblExpectedMonthEndCollection", "lblActualMonthEndCollection");
                            _numbers = _helper.ShowMailButton(row, lnksendEmail11To20, "lblExpected20DaysCollection", "lblActual20DaysCollection");
                            _numbers = _helper.ShowMailButton(row, lnksendEmail1To10, "lblExpectedFirst10DaysCollection", "lblActual10DaysCollection");
                            _numbers = _helper.ShowMailButton(row, lnksendEmail, "lblEstimatedTotal", "lblActualTotal");
                        }
                    }
                }
                else
                {
                    gvActualCollectionReport.DataSource = null;
                    gvActualCollectionReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void gvActualCollectionReport_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var row = (GridViewRow) ((LinkButton) e.CommandSource).NamingContainer;
            var rowindex = row.RowIndex;
            var stateProject = (Label) gvActualCollectionReport.Rows[rowindex].FindControl("lblStateProject");
            var stateName = stateProject.Text.Substring(0, stateProject.Text.IndexOf('-'));
            var query = "SELECT s.State_name,u.state_name ,u.email_id FROM m_users u JOIN m_states s ON u.state_name=s.short_name WHERE s.State_name='" + stateName + "'";
            var dt = _helper.ExecuteSelectStmt(query);
            try
            {
                switch (e.CommandName)
                {
                    case "1to10":
                        CommonMethod(rowindex, dt, stateProject, "lblActual10DaysCollection", "lblExpectedFirst10DaysCollection");
                        break;
                    case "11to20":
                        CommonMethod(rowindex, dt, stateProject, "lblActual20DaysCollection", "lblExpected20DaysCollection");

                        break;
                    case "21toend":
                        CommonMethod(rowindex, dt, stateProject, "lblActualMonthEndCollection", "lblExpectedMonthEndCollection");
                        break;
                    case "send":
                        CommonMethod(rowindex, dt, stateProject, "lblActualTotal", "lblEstimatedTotal");
                        break;
                }
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        private void CommonMethod(int rowindex, DataTable dt, Label stateProject, string actual, string expected)
        {
            decimal estimatedTotal;
            var actualCollection = (Label) gvActualCollectionReport.Rows[rowindex].FindControl(actual);
            var expectedCollection = (Label) gvActualCollectionReport.Rows[rowindex].FindControl(expected);
            if (decimal.TryParse(expectedCollection.Text, out estimatedTotal))
            {
                decimal actualTotal;
                if (decimal.TryParse(actualCollection.Text, out actualTotal))
                    _helper.SendEmail(this, estimatedTotal, actualTotal, dt, stateProject.Text);
            }
        }

        protected void lnkSendEmail_OnClick(object sender, EventArgs e)
        {
        }
        protected void ExportToExcel_Click(object sender,EventArgs e)
        {
            try
            {
                _helper.LoadExcelSpreadSheet(this, pnlActualExpectedCollectionReport, "ActualExpectedCollectionReport.xls");
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}