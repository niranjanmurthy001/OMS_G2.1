using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Ordermanagement_01.Classes
{  
    class Notification_Data
    {
        private static string m_ConnectionString = "Server=192.168.12.33;Database=Chatter;User ID=sa;pwd=password1$";
        //public SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        private SqlConnection m_sqlConn = null;

        public delegate void NewMessage();

        public event NewMessage ONNewMessage;

        /// <summary>
        /// Constructor
        /// </summary>

        public Notification_Data()
        {
            // Stop an existing service on this connection string

            // Just be sure

            SqlDependency.Stop(m_ConnectionString);

            // Start the dependency
            
            // User must have SUBSCRIBE QUERY NOTIFICATIONS permission
            // Database must also have SSB enabled
            // ALTER DATABASE Chatter SET ENABLE_BROKER


            SqlDependency.Start(m_ConnectionString);

            // Create the Connection

             m_sqlConn = new SqlConnection(m_ConnectionString);

         
        }

        /// <summary>
        /// Destructor
        /// </summary>
        /// 

        ~Notification_Data()
        {
            // Stop the dependency before exiting
            SqlDependency.Stop(m_ConnectionString);
        }

        /// <summary>
        /// Retreive messages from database
        /// </summary>
        /// <returns></returns>
        /// 

        public DataTable Get_User_Order_Count(int User_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                // Create command
                // Command must use two part names for tables
                // SELECT <field> FROM dbo.Table rather than 
                // SELECT <field> FROM Table
                // Query also can not use *, fields must be designated
                SqlCommand cmd = new SqlCommand("Sp_Order_User_Wise_Notifications", m_sqlConn);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Trans", "GET_USER_ORDER_COUNT");
                cmd.CommandType = CommandType.StoredProcedure;

                // Clear any existing notifications
                cmd.Notification = null;

                // Create the dependency for this command
                SqlDependency dependency = new SqlDependency(cmd);

                // Add the event handler
                dependency.OnChange += new OnChangeEventHandler(OnChange);

                // Open the connection if necessary
                if (m_sqlConn.State == ConnectionState.Closed)
                    m_sqlConn.Open();

                // Get the messages
                dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;



        }

        /// <summary>
        /// Handler for the SqlDependency OnChange event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;

            // Notices are only a one shot deal
            // so remove the existing one so a new 
            // one can be added
            dependency.OnChange -= OnChange;

            // Fire the event
            if (ONNewMessage != null)
            {
                ONNewMessage();
            }
        }

        /// <summary>
        /// Insert a message into the database
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Person_ID"></param>
        public static void AddMessage_For_New_Order_Allocate(string Message, int Person_ID)
        {
            SqlConnection Conn = new SqlConnection(m_ConnectionString);
            SqlCommand cmd = new SqlCommand("usp_InsertMessage", Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Person_ID", Person_ID);

            Conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        /// <summary>
        /// Get users from databse
        /// </summary>
        /// <returns></returns>


    }
}
