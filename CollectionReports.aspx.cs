using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class CollectionReports : Page
    {
        private readonly Helper _helper = new Helper();
        public string UserId;
        protected void Page_Load(object sender,EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Login.aspx");
            else
                UserId = (string) Session["UserId"];
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
                _helper.FillDropDownHelperMethodWithSp1("report_statewise_daywise",null,null,ddlYear,ddlMonth,"@yr",null,"@mnt",gvCollectionReport);
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
                _helper.LoadExcelSpreadSheet(this,pnlCollectionReport,"CollectionReport.xls");
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