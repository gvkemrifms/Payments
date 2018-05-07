using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class DailyCollectionReportDetails : System.Web.UI.Page
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
            if(!IsPostBack)
            BindGridDetails();
        }

        private void BindGridDetails()
        {
            try
            {
               
                _helper.FillDropDownHelperMethodWithSp("userCollection_grid", null,null,null,null,"@uid", _userId,null,GvCollections);
                _helper.FillDropDownHelperMethodWithSp("userpayments_grid", null, null, null, null, "@uid", _userId, null,GvPayments);
            }
            catch (Exception ex)
            {
                _helper.ErrorsEntry(ex);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DailyCollectionReport.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DailyPaymentsReport.aspx");
        }
    }
}