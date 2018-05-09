using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class CollectionReports : System.Web.UI.Page
    {
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
                BindMonthsDropdown();
                BindYearDropDown();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                _helper.FillDropDownHelperMethodWithSp1("report_statewise_daywise", null, null, ddlYear, ddlMonth, "@yr",null, "@mnt", gvCollectionReport);
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
          string month=  ddlMonth.Items.FindByText(DateTime.Today.ToString("MMMM")).ToString();
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
                _helper.LoadExcelSpreadSheet(this, null, "CollectionReport.xls",gvCollectionReport);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }
    }
}