using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;


namespace Ordermanagement_01.Classes
{
    class Grid_Data
    {
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader DataReader;
        connection connect = new connection();
        public Grid_Data()
	{
        cmd = new SqlCommand("");
        cmd.Connection = connect.con;
	}
        public System.Data.DataTable objDataTable = new System.Data.DataTable();
        public System.Data.DataTable GetOrderTable
        {
            get
            {
                return this.objDataTable;
            }

            set
            {
                objDataTable = value;
            }
        }

    }
}
