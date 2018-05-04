using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;


namespace DailyCollectionAndPayments
{
    public partial class DailyPaymentsReport1 : System.Web.UI.Page
    {
        public IEnumerable<DailyPaymentsReport> reports;
        DailyPaymentsReport rep = new DailyPaymentsReport();
        readonly Helper _helper = new Helper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) Response.Redirect("Login.aspx");
            if (!IsPostBack)
                BindStatesData();
        }

        private void BindStatesData()
        {
            try
            {
                
                _helper.FillDropDownHelperMethodWithSp("userbased_state", "state_name", "state_id", ddlState, "@uid", Convert.ToInt32(Session["UserId"]));
            }
            catch(Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        private void Data()
        {
            var totals = DailyPaymentsReport.dailyReport.Where(x => x.PaymentDate == new DateTime(2017, 7, 21)).Select(x => new { Salary = x.Salary, Fuel = x.Fuel, VendorRegular = x.VendorsRegular, VendorOverDue = x.VendorsOverDue });
            rep.TotalSalary = totals.Sum(x => x.Salary);
            reports = DailyPaymentsReport.dailyReport.Where(x => x.PaymentDate == new DateTime(2017, 7, 21));
            gvDailyPayments.DataSource = reports;
            gvDailyPayments.DataBind();
            GetTotalAmount();
            GetTotalSumForStates();
            //decimal total = reports.AsEnumerable().Sum(row =>row.Salary /*row.Field<int>("Total")*/);
            gvDailyPayments.FooterRow.Cells[1].Text = "Total";
            gvDailyPayments.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvDailyPayments.FooterRow.Cells[2].Text = rep.TotalSalary.ToString("N2");        
        }

        private void GetTotalAmount()
        {
            var totals = DailyPaymentsReport.dailyReport.Where(x => x.PaymentDate == new DateTime(2017, 7, 21)).Select(x => new { Salary = x.Salary, Fuel = x.Fuel, VendorRegular = x.VendorsRegular, VendorOverDue = x.VendorsOverDue });
            rep.TotalSalary = totals.Sum(x => x.Salary);
            rep.TotalFuelAmount = totals.Sum(x => x.Fuel);
            rep.TotalVendorRegular = totals.Sum(x => x.VendorRegular);
           rep.VendorsOverDue = totals.Sum(x => x.VendorOverDue);
            //Response.Write("Total Sum Of  Salary =" + rep.TotalSalary.ToString() + "<br/>");
            //Response.Write("  FuelAmount =" + rep.TotalFuelAmount.ToString() + "<br/>");
            //Response.Write("  Vender Regular =" + rep.TotalVendorRegular.ToString() + "<br/>");
            //Response.Write("  Vender Vender OverDue =" + rep.VendorsOverDue.ToString() + "<br/>");


        }
        private void GetTotalSumForStates()
        {
            var priceQuery =
      from prod in reports
      group prod by prod.StateName into grouping
      select new
      {
          grouping.Key,
          TotalPrice = grouping.Sum(p => p.Salary) + grouping.Sum(p => p.VendorsOverDue) + grouping.Sum(p => p.Fuel) + grouping.Sum(p => p.VendorsRegular),
          salary = grouping.Select(x => x.Salary),
          stateName = grouping.Select(x => x.StateName)
      };
            rep.Total = 0;
            foreach (var grp in priceQuery)
            {
                rep.Total = rep.Total + grp.TotalPrice;
                //Response.Write("State =" + " " + grp.stateName.SingleOrDefault() + " " + " " + "Total Amount=" + rep.Total + "<br/>");
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
           
        }
    }
}