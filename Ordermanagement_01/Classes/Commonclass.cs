using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration;
using System.Collections;



/// <summary>
/// Summary description for Commonclass
/// </summary>
public class Commonclass
{
	public Commonclass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    public float age;
    public float mission;
    List<string> al = new List<string>();
    public int DoTransaction(string sql)
    {
        int count = 0;
        try
        {
            con.Open();
         
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            count = 1;
        }
        catch
        {
            count = 0;
            con.Close();
        }
        return count;
    }

    public DataTable DoNontransaction(string sql)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);
        return dt;
    }
    public object DoScalarTransaction(string sql)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        object value = cmd.ExecuteScalar();
        con.Close();
        return value;
    }

    public List<string> Crystal_report_Login()
    {

        string connectString = ConfigurationManager.ConnectionStrings["Con"].ToString();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);
        // Retrieve the DataSource property.    

        builder.ConnectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

        string server = builder["Data Source"] as string;
        string database = builder["Initial Catalog"] as string;
        string UserID = builder["User ID"] as string;
        string password = builder["Password"] as string;



        al.Add(server);
        al.Add(database);
        al.Add(UserID);
        al.Add(password);

        return al;




    }
}