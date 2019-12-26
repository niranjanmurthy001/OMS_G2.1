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

namespace Ordermanagement_01.Classes
{
    class Olddb_Datacess
    {
        SqlConnection connectionstring = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["olddbCon"].ConnectionString);
        int connection_sql = 0;
        public Olddb_Datacess()
        { 
        

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
                        cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
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
                connection_sql = 1;
                connectionstring.Close();
                // MessageBox.Show("Cannot Make Establishment of Connection"+Environment.NewLine + ex.Message + ex.StackTrace);
                //MessageBox.Show(ex.Message, ex.StackTrace);
            }
            finally
            {

                if (connection_sql == 1)
                {
                    using (SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring))
                    {
                        cmd.Connection = connectionstring;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        connectionstring.Open();
                        foreach (DictionaryEntry parameterEntry in htforSP)
                        {
                            cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        connectionstring.Close();
                    }
                    connection_sql = 0;
                }

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
            connectionstring.Open();
            SqlCommand cmd = new SqlCommand(Procedure_Name, connectionstring);
            cmd.Connection = connectionstring;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameterEntry in htforSP)
            {
                cmd.Parameters.AddWithValue((string)parameterEntry.Key, parameterEntry.Value);
            }
            object value = cmd.ExecuteScalar();
            connectionstring.Close();
            return value;
        }

        #endregion

    }
}
