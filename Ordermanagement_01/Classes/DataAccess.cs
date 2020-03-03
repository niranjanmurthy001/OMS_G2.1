using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Xml;

using System.IO;
using System.Windows;
/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{

   // static string con = "Data Source=192.168.12.30;Initial Catalog=OMS;User ID=sa;Password=password1$";
    SqlConnection connectionstring = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    
    int connection_sql = 0;
	public DataAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    

   

    #region ExecuteSP

    public DataTable ExecuteSP(string Procedure_Name, System.Collections.Hashtable htforSP)
    {

        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring))
            {
                cmd.Connection = connectionstring;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                connectionstring.Open();
                foreach (DictionaryEntry parameterEntry in htforSP)
                {
                    cmd.Parameters.Add((string)parameterEntry.Key, parameterEntry.Value);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);                 
                }

               
                connectionstring.Close();
            }



        }
        catch (Exception ex)
        {
            connection_sql=1;
            connectionstring.Close();
           // MessageBox.Show("Cannot Make Establishment of Connection"+Environment.NewLine + ex.Message + ex.StackTrace);
            //MessageBox.Show(ex.Message, ex.StackTrace);
        }
        finally
        {
            connectionstring.Close();

            //if (connection_sql == 1)
            //{
            //    using (SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring))
            //    {
            //        cmd.Connection = connectionstring;
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.CommandTimeout = 0;
            //        connectionstring.Open();
            //        foreach (DictionaryEntry parameterEntry in htforSP)
            //        {
            //            cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
            //        }
            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            da.Fill(dt);
            //        }
            //        connectionstring.Close();
            //    }
            //    connection_sql = 0;
            //}
            
        }
        return dt;

    }
    #endregion


    #region ExecuteSpNew

    public DataTable ExecuteSPNew(string Procedure_Name, IDictionary<string,object> Idict)
    {

        DataTable dt = new DataTable();

        try
        {

            using (SqlCommand cmd=new SqlCommand (Procedure_Name,connectionstring))
            {

                cmd.Connection = connectionstring;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                connectionstring.Open();

                foreach (KeyValuePair<string,object> kvp in Idict)
                {

                    cmd.Parameters.Add(kvp.Key.ToString(),kvp.Value);
                }

                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {

                    adp.Fill(dt);

                }

                connectionstring.Close();
            }
        }
        catch (Exception ex)
        {
            connection_sql = 1;
            connectionstring.Close();

        }
        finally
        {
            connectionstring.Close();

        }

        return dt;
    }

    #endregion



    #region ExecuteSPForCRUD

    public int ExecuteSPForCRUD(string Procedure_Name, System.Collections.Hashtable htforSP)
    {
        int count = 0;

        try
        {
            connectionstring.Open();

            SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring);
         
            cmd.Connection = connectionstring;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameterEntry in htforSP)
            {
                cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
            }
            cmd.ExecuteNonQuery();
            connectionstring.Close();
            count = 1;
        }
        catch
        {
            count = 0;
            connectionstring.Close();
        }
        finally
        {

            connectionstring.Close();
        }
        return count;
    }

    #endregion


    #region ExecuteSPForScalar

    public object ExecuteSPForScalar(string Procedure_Name, System.Collections.Hashtable htforSP)
    {
        object value=0;
        try
        {
            connectionstring.Open();
            SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring);
            cmd.Connection = connectionstring;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameterEntry in htforSP)
            {
                cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
            }
             value = cmd.ExecuteScalar();
            connectionstring.Close();
        }
        catch (Exception ex)
        {

            connectionstring.Close();
            MessageBox.Show(ex.Message.ToString());
        }
        finally
        {
            connectionstring.Close();

        }
        return value;
    }

    #endregion


    public bool Execute_SP_Output_Return(string Procedure_Name, System.Collections.Hashtable htforSP)
    {
        bool result = false;

        try
        {
            connectionstring.Open();
            SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring);
            cmd.Connection = connectionstring;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameterEntry in htforSP)
            {
                cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
            }
            SqlParameter OutputParam = new SqlParameter("@Flag", SqlDbType.Binary);
            OutputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Flag", true);

            cmd.ExecuteNonQuery();
            connectionstring.Close();

            result = Convert.ToBoolean(cmd.Parameters["@Flag"].Value.ToString());

        }
        catch
        {
           
            connectionstring.Close();
            result = false;
        }
        finally

        {

            connectionstring.Close();
        }


        return result;
    }


    #region base_Url
    public string Base_Url()
    {
        string url = "http://localhost:28537/Api/Login/Validate_User";
        return url;
    }
    #endregion



}