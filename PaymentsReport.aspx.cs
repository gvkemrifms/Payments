using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class PaymentsReport : System.Web.UI.Page
    {
        public string _userId;
        readonly Helper _helper = new Helper();
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
                BindMonthsDropdown();
                BindYearDropDown();
               BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                string query = "SELECT pm.project_id,pm.state_id,CONCAT(s.short_name, ' - ' ,p.project_name) ProjectName FROM t_projectmapping pm JOIN `m_projects` p ON p.project_id = pm.project_id JOIN `m_states`  s ON s.state_id = pm.state_id WHERE pm.isactive = 1 ORDER BY ProjectName";
                DataTable dtPaymentsReport = _helper.ExecuteSelectStmt(query);
                if (dtPaymentsReport.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sbclm = new StringBuilder();
                    foreach (DataRow row in dtPaymentsReport.Rows)
                    {
                        sb.Append("IF(MONTH(`pay_date`)=" + ddlMonth.SelectedValue + " AND YEAR(`pay_date`)=" + ddlYear.SelectedValue + " AND project_id =" + row["project_id"] + " AND state_id =" + row["state_id"] + ", SUM(amount),NULL) '" + row["ProjectName"] + "' ,");
                        sbclm.Append("t.");
                        sbclm.Append("`" + row["ProjectName"] + "`,");
                    }
                    string sbResult = sb.ToString().TrimEnd(',');
                    string sblist = sbclm.ToString().TrimEnd(',');
                    string query1 = "SELECT `payment_name`," + sblist + "  FROM `m_payments`  mt LEFT JOIN (SELECT `payment_type_id`, " + sbResult + " from t_payments  GROUP BY payment_type_id  ) T ON mt.`payment_type_id`= T.`payment_type_id`";
                    DataTable dtPaymentsReports = _helper.ExecuteSelectStmt(query1);
                    dtPaymentsReports.Columns["payment_name"].ColumnName = "Payment";
                    gvPaymentsReport.DataSource = dtPaymentsReports;
                    gvPaymentsReport.DataBind();
                }
            }
            catch
            {

            }
        }

        private void BindYearDropDown()
        {
            for (int i = 2010; i <= 2025; i++)
            {
                ddlYear.Items.Add(i.ToString());


            }
            ddlYear.SelectedItem.Text = DateTime.Now.Year.ToString();
        }

        private void BindMonthsDropdown()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < 13; i++)
            {
                ddlMonth.Items.Add(new ListItem(months[i], i.ToString()));

            }
            ddlMonth.Items.Insert(0, "--Select--");
            string month = ddlMonth.Items.FindByText(DateTime.Today.ToString("MMMM")).ToString();
            ddlMonth.Items.FindByText(month).Selected = true;


        }
        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                _helper.LoadExcelSpreadSheet(this, null, "PaymentsReport.xls", gvPaymentsReport);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }
    }
}