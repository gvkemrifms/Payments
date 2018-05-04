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
            string query = "select user_id, user_name,password from m_users where user_name='" + UserNAme+"' and password='"+ Password+"'";
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