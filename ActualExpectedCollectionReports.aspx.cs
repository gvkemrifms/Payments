using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class ActualExpectedCollectionReports : Page
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
                BindYearDropDown();
                BindMonthsDropdown();
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
                            _helper.ShowMailButton(row, lnksendEmail21Toend, "lblExpectedMonthEndCollection", "lblActualMonthEndCollection");
                            _helper.ShowMailButton(row, lnksendEmail11To20, "lblExpected20DaysCollection", "lblActual20DaysCollection");
                            _helper.ShowMailButton(row, lnksendEmail1To10, "lblExpectedFirst10DaysCollection", "lblActual10DaysCollection");
                            _helper.ShowMailButton(row, lnksendEmail, "lblEstimatedTotal", "lblActualTotal");
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
            var query = "select * from m_users";
            var dt = _helper.ExecuteSelectStmt(query);
            try
            {
                switch (e.CommandName)
                {
                    case "1to10":
                    case "11to20":
                    case "21toend":
                    case "send":
                        _helper.SendEmailToSpecificStates(stateName, dt);
                        break;
                }
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void lnkSendEmail_OnClick(object sender, EventArgs e)
        {
        }
    }
}