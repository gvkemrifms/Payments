using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyCollectionAndPayments
{
    public partial class MainForm : System.Web.UI.Page
    {
        private static readonly string CONN_STRING = ConfigurationManager.AppSettings["GvkEmriCon"];
        DataTable dtMenuItems = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                String strUserName = Session["RName"].ToString();
                String strRoleid = Session["RoleId"].ToString();
                LblUserName.Text = strUserName;
                MySqlCommand cmd = new MySqlCommand();
                string query = "SELECT mp.`previlage_id`, mp.`previlage_name`, `parent_previlage_id`, `path`  "
                        + "     FROM `finacne`.`m_previlage` mp  "
                        + "     JOIN `finacne`.`m_roleprevilage` ur ON mp.`previlage_id`= ur.`previlage_id`  "
                        + "     WHERE mp.`is_active`= 1 AND ur.role_id =  " + strRoleid + " ORDER BY order_by ASC; ";
                MySqlDataReader sdr = ExecuteReader(cmd, CommandType.Text, query);
                dtMenuItems.Load(sdr);
                BuildTreeForthisUser(dtMenuItems);
                this.uriIFrame.Attributes["src"] = "Default.aspx";
            }
        }
        public static MySqlDataReader ExecuteReader(MySqlCommand cmd, CommandType cmdType, string cmdText)
        {
            MySqlConnection conn = new MySqlConnection(CONN_STRING);
            try
            {
                PrepareCommand(cmd, conn, (MySqlTransaction)null, cmdType, cmdText);
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
            menuBar.MenuItemClick += new MenuEventHandler(menuBar_MenuItemClick);
            foreach (DataRow dr in dsmenuItems.Select("parent_previlage_id >" + 0))
            {               
                MenuItem mnu = new MenuItem(dr["previlage_name"].ToString(),
                              dr["previlage_id"].ToString()
                              );
                menuBar.FindItem(dr["parent_previlage_id"].ToString()).ChildItems.Add(mnu);
            }
        }
        protected void menuBar_MenuItemClick(object sender, MenuEventArgs e)
        {
            String strRoleid = Session["RoleId"].ToString();
            MySqlCommand cmd = new MySqlCommand(); 
            string query = " SELECT mp.previlage_id, previlage_name,parent_previlage_id, path  "
                    + "     FROM `finacne`.m_previlage mp "
                    + "     JOIN `finacne`.m_roleprevilage mr ON mp.previlage_id = mr.previlage_id "
                    + "     WHERE role_id = " + strRoleid + " ORDER BY order_by ASC; ";
            MySqlDataReader sdr = ExecuteReader(cmd, CommandType.Text, query);
            dtMenuItems.Load(sdr);
            foreach (DataRow dr in dtMenuItems.Rows)
            {
                if (menuBar.SelectedItem.Text.Trim().ToString() == dr["previlage_name"].ToString())
                {
                    this.uriIFrame.Attributes["src"] = dr["path"].ToString();
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}