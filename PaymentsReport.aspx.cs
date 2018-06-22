using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class PaymentsReport : Page
    {
        private readonly Helper _helper = new Helper();
        public string UserId;
        public string HiddenVal{ get;set; }
        protected void Page_Load(object sender,EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string) Session["UserId"];
            string data = myHiddenField.Value;
            HiddenVal = Helper.RemoveWhitespace(data);
            if (HiddenVal != "")
            {
                DataSet dsInfo= _helper.FillDropDownHelperMethodWithSp3("report_daywise_payments", null, null, ddlYear, ddlMonth, "@yr", HiddenVal, "@mnt", null, "@hiddenValue");
                if (dsInfo.Tables[0].Rows.Count > 0)
                    BindDataToDiv(dsInfo);
            }

            if (!IsPostBack)
            {
                BindMonthsDropdown();
                BindYearDropDown();
                BindGrid();
            }
        }

        private void BindDataToDiv(DataSet dsInfo)
        {
            StringBuilder sbInfo = new StringBuilder();
            sbInfo.Append("<table border = '1'>");
            sbInfo.Append("<tr>");
            foreach (DataColumn column in dsInfo.Tables[0].Columns)
            {
                sbInfo.Append("<th>");
                sbInfo.Append(column.ColumnName);
                sbInfo.Append("</th>");
            }
            sbInfo.Append("</tr>");
            foreach (DataRow row in dsInfo.Tables[0].Rows)
            {
                sbInfo.Append("<tr>");
                foreach (DataColumn column in dsInfo.Tables[0].Columns)
                {
                    sbInfo.Append("<td>");
                    sbInfo.Append(row[column.ColumnName]);
                    sbInfo.Append("</td>");
                }
                sbInfo.Append("</tr>");
            }
            sbInfo.Append("</table>");
            PlaceHolder1.Controls.Add(new Literal { Text = sbInfo.ToString() });
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Dialog", "popup()", true);
        }

        private void BindGrid()
        {
            try
            {
                string query = null;
                string userQuery = "select user_id from m_user_role where user_id=" + UserId + " and role_id=1";
                DataTable dt = _helper.ExecuteSelectStmt(userQuery);
                if (dt.Rows.Count > 0)
                {
                    int value = dt.Rows[0].Field<int>("user_id");
                    if (UserId == value.ToString())
                        query = "SELECT pm.project_id,pm.state_id,CONCAT(s.short_name, ' - ' ,p.project_name) ProjectName FROM t_projectmapping pm left JOIN `m_projects` p ON p.project_id = pm.project_id left JOIN `m_states`  s ON s.state_id = pm.state_id  ORDER BY ProjectName";
                }
                else
                    query = "SELECT pm.project_id,pm.state_id,CONCAT(s.short_name, ' - ' ,p.project_name) ProjectName FROM t_projectmapping pm left JOIN `m_projects` p ON p.project_id = pm.project_id left JOIN `m_states`  s ON s.state_id = pm.state_id  WHERE pm.user_id = " + UserId + " ORDER BY ProjectName";

                var dtPaymentsReport = _helper.ExecuteSelectStmt(query);
                if (dtPaymentsReport.Rows.Count > 0)
                {
                    var sb = new StringBuilder();
                    var sbclm = new StringBuilder();
                    foreach (DataRow row in dtPaymentsReport.Rows)
                    {
                        sb.Append("SUM(IF(MONTH(`pay_date`)=" + ddlMonth.SelectedValue + " AND YEAR(`pay_date`)=" + ddlYear.SelectedValue + " AND project_id =" + row["project_id"] + " AND state_id =" + row["state_id"] + ", amount,NULL)) '" + row["ProjectName"] + "' ,");
                        sbclm.Append("t.");
                        sbclm.Append("`" + row["ProjectName"] + "`,");
                    }

                    sb.Append("SUM(IF(MONTH(`pay_date`)=" + ddlMonth.SelectedValue + " AND YEAR(`pay_date`)=" + ddlYear.SelectedValue + ",amount,NULL))'Total'");
                    sbclm.Append("t.");
                    sbclm.Append("`Total`");
                    var sbResult = sb.ToString().TrimEnd(',');
                    var sblist = sbclm.ToString().TrimEnd(',');
                    var query1 = "SELECT `payment_name`," + sblist + "  FROM `m_payments`  mt LEFT JOIN (SELECT `payment_type_id`, " + sbResult + " from t_payments  GROUP BY payment_type_id  ) T ON mt.`payment_type_id`= T.`payment_type_id` union SELECT 'Total', " + sbResult + " from t_payments   ";
                    var dtPaymentsReports = _helper.ExecuteSelectStmt(query1);
                    dtPaymentsReports.Columns["payment_name"].ColumnName = "Payment";
                    gvPaymentsReport.DataSource = dtPaymentsReports;
                    gvPaymentsReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }
        private void BindYearDropDown()
        {
            for (var i = 2018; i <= 2025; i++) ddlYear.Items.Add(i.ToString());
            ddlYear.SelectedItem.Text = DateTime.Now.Year.ToString();
        }
        private void BindMonthsDropdown()
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (var i = 0; i < 13; i++) ddlMonth.Items.Add(new ListItem(months[i],(i + 1).ToString()));
            ddlMonth.Items.Insert(0,"--Select--");
            var month = ddlMonth.Items.FindByText(DateTime.Today.ToString("MMMM")).ToString();
            ddlMonth.Items.FindByText(month).Selected = true;
        }
        protected void btnShowReport_Click(object sender,EventArgs e)
        {
            BindGrid();
        }
        protected void ExportToExcel_Click(object sender,EventArgs e)
        {
            try
            {
                _helper.LoadExcelSpreadSheet(this,lblPaymentReport,"PaymentsReport.xls",null);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void gvPaymentsReport_OnRowDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                foreach (TableCell c in e.Row.Cells)
                {
                    if (c.Text != "Payment" && c.Text != "Total")
                    {
                        myHiddenField.Value = Helper.RemoveWhitespace(HiddenVal);
                        HiddenVal = c.Text;
                        e.Row.Attributes.Add("onmouseover","mouseIn(this);");
                        e.Row.Attributes.Add("onmouseout","mouseOut();");
                        c.Attributes.Add("onclick", "DisplayToolTip('" + HiddenVal + "')");
                        //c.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, myHiddenField.Value));
                        //string jsPostBackCall=  ClientScript.GetPostBackEventReference(this, Helper.RemoveWhitespace(HiddenVal));

                    }
                }
        }  
    }
}