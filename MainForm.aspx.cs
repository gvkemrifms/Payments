using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class MainForm : Page
    {
        private static readonly string ConnString = ConfigurationManager.AppSettings["GvkEmriCon"];
        readonly DataTable _dtMenuItems = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RName"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                var strUserName = Session["RName"].ToString();
                var strRoleid = Session["RoleId"].ToString();
                LblUserName.Text = strUserName;
                var cmd = new MySqlCommand();
                string query = "SELECT mp.`previlage_id`, mp.`previlage_name`, `parent_previlage_id`, `path`  "
                        + "     FROM `finance`.`m_previlage` mp  "
                        + "     JOIN `finance`.`m_roleprevilage` ur ON mp.`previlage_id`= ur.`previlage_id`  "
                        + "     WHERE mp.`is_active`= 1 AND ur.role_id =  " + strRoleid + " ORDER BY order_by ASC; ";
                var sdr = ExecuteReader(cmd, CommandType.Text, query);
                _dtMenuItems.Load(sdr);
                BuildTreeForthisUser(_dtMenuItems);
                uriIFrame.Attributes["src"] = "Default.aspx";
            }
        }
        public static MySqlDataReader ExecuteReader(MySqlCommand cmd, CommandType cmdType, string cmdText)
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                
                conn.Close();
                throw;
            }
        }
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
        }

        public void BuildTreeForthisUser(DataTable dsmenuItems)
        {
            DataRow[] drowpar = dsmenuItems.Select("parent_previlage_id=" + 0);
            foreach (DataRow dr in drowpar)
            {
                menuBar.Items.Add(new MenuItem(dr["previlage_name"].ToString(),
                        dr["previlage_id"].ToString()));
            }
            menuBar.MenuItemClick += menuBar_MenuItemClick;
            foreach (DataRow dr in dsmenuItems.Select("parent_previlage_id >" + 0))
            {               
                MenuItem mnu = new MenuItem(dr["previlage_name"].ToString(),
                              dr["previlage_id"].ToString());
                menuBar.FindItem(dr["parent_previlage_id"].ToString()).ChildItems.Add(mnu);
            }
        }
        protected void menuBar_MenuItemClick(object sender, MenuEventArgs e)
        {
            var strRoleid = Session["RoleId"].ToString();
            MySqlCommand cmd = new MySqlCommand(); 
            string query = " SELECT mp.previlage_id, previlage_name,parent_previlage_id, path  "
                    + "     FROM `finance`.m_previlage mp "
                    + "     JOIN `finance`.m_roleprevilage mr ON mp.previlage_id = mr.previlage_id "
                    + "     WHERE role_id = " + strRoleid + " ORDER BY order_by ASC; ";
            MySqlDataReader sdr = ExecuteReader(cmd, CommandType.Text, query);
            _dtMenuItems.Load(sdr);
            foreach (DataRow dr in _dtMenuItems.Rows)
            {
                if (menuBar.SelectedItem.Text.Trim() == dr["previlage_name"].ToString())
                    uriIFrame.Attributes["src"] = dr["path"].ToString();
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}