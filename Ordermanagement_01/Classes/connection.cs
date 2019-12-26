using System;
using System.Data;
using System.Configuration;


using System.Data.SqlClient;

/// <summary>
/// Summary description for connection
/// </summary>
public class connection
{
    public SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    SqlDataReader DataReader;
  
	public connection()
	{
      //  con.ConnectionString = "Data Source=192.168.12.30;Initial Catalog=OMS;User ID=sa;Password=password1$"; 
    }

    public void CloseConnection()
    {
        if (DataReader != null && !DataReader.IsClosed) DataReader.Close();
        if (con.State != ConnectionState.Closed)
        {
            try
            { con.Close(); }
            catch { }
        }
    }

    public void OpenConnection()
    {
        if (con.State != ConnectionState.Open) con.Open();
    }

   
}
