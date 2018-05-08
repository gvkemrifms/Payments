using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Login
/// </summary>
public class VerifyLoginDetails
{
    
    public  DataTable VerifyUser(string UserNAme, string Password)
    {
        DataTable dtLoginDetails = new DataTable();
        try
        {
            MySqlCommand cmd = new MySqlCommand();
            string query = " SELECT au.`user_id`,`user_name`, ar.`role_id`, ar.`role_name`   "
                    + "     FROM  `finacne`.`m_users`  au "
                    + "     JOIN `finacne`.`m_user_role` aur ON au.`user_id`= aur.`user_id`   "
                    + "     JOIN `finacne`.`m_role` ar ON aur.`role_id`= ar.`role_id`  "
                    + "     where user_name='" + UserNAme + "' and password='" + Password + "'";
            MySqlDataReader sdr = ExecuteReader(cmd, CommandType.Text, query);
            dtLoginDetails.Load(sdr);
        }
        catch (Exception ex)
        {
           
        }
        return dtLoginDetails;
    }
    public static MySqlDataReader ExecuteReader(MySqlCommand cmd, CommandType cmdType, string cmdText)
    {
        string CONN_STRING = ConfigurationManager.AppSettings["GvkEmriCon"];

        MySqlConnection conn = new MySqlConnection(CONN_STRING);
        try
        {
            PrepareCommand(cmd, conn, (MySqlTransaction)null, cmdType, cmdText);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch(Exception ex)
        {
            
            //Do Log
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
}