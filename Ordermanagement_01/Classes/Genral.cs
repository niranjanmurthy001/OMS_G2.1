using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Hostel
/// </summary>
public class Genral
{

    private SqlCommand cmd = new SqlCommand();
    private SqlDataReader DataReader;
    connection connect = new connection();
    public Genral()
	{
        cmd = new SqlCommand("");
        cmd.Connection = connect.con;
	}
    public int InsertHostel(string  HostelNo, string HostelName,string Username,string Date)
    {
        string QueryString = "Insert into Mst_Hostel(Hostel_No,HostelName,EnteredBy,EnteredDate) values('" + HostelNo + "','" + HostelName + "','" +Username+ "',convert(datetime,'"+Date+"',103))";
        cmd.CommandText = QueryString;
        connect.OpenConnection();
        int check = cmd.ExecuteNonQuery();
        connect.CloseConnection();
        return check;

    }
   

    public DataTable Getmax()
    {
        string qry = "select case when  max(Tbl_Auto.Id) is null then 0 else  max(Tbl_Auto.Id) end Id from dbo.Tbl_Auto ";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }

    public DataTable GetData()
    {
        string qry = "SELECT [Auto_Id],[Id],[File_Path],File_Name FROM [Tbl_Auto] order by Id";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }



    public DataTable FillChildTable(int orderid)
    {
        string qry = " select * from dbo.Tbl_Treeview_Child_Name where Order_Id='" + orderid + "' and status='True' order by Order_Index ";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }
    public DataTable Get_File(int id)
    {
        string qry = "select Tbl_Attachments.Name,Tbl_Attachments.contents,Tbl_Attachments.contentSize from Tbl_Attachments where Tbl_Attachments.pindex='"+id+"'";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }
  
    public int Truncate_MailAndAttachemnt()
    {
        string QueryString = "truncate table Tbl_Mail truncate table dbo.Tbl_Attachments";
        cmd.CommandText = QueryString;
        connect.OpenConnection();
        int check = cmd.ExecuteNonQuery();
        connect.CloseConnection();
        return check;

    }
    //public DataTable Get_User_Order_Status_Data()
    //{
    //    string qry = " select * from dbo.Tbl_Treeview_Child_Name where Order_Id='" + orderid + "' and status='True' order by Order_Index ";
    //    connect.OpenConnection();
    //    SqlDataAdapter ada = new SqlDataAdapter();
    //    ada = new SqlDataAdapter(qry, connect.con);
    //    DataTable dt = new DataTable();
    //    ada.Fill(dt);
    //    connect.CloseConnection();
    //    return dt;
    //}     
    public DataTable FillParentTable()
    {
        string qry = "  select * from dbo.Tbl_Treeview_Main_Name";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }

    public DataTable FillChildTable()
    {
        string qry = " SELECT Tbl_Treeview_Child_Name.User_id,Tbl_Treeview_Child_Name.User_Name + ' --> ' +CAST(Tbl_Treeview_Child_Name.No_OF_Orders AS VARCHAR(10)) as User_Name,Tbl_Treeview_Child_Name.Main_id  from Tbl_Treeview_Child_Name ";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }
    public DataTable FillChildTable_New()
    {
        string qry = "SELECT dbo.Tbl_Treeview_Child_Name.User_id, dbo.Tbl_Treeview_Child_Name.User_Name, dbo.Tbl_Treeview_Child_Name.No_OF_Orders,dbo.Tbl_Treeview_Child_Name.Main_id, dbo.View_Active_Users.Salary, dbo.View_Active_Users.Job_Role_Id " +
                      "FROM  dbo.Tbl_Treeview_Child_Name INNER JOIN dbo.View_Active_Users ON dbo.Tbl_Treeview_Child_Name.User_id = dbo.View_Active_Users.User_id ";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }
    public DataTable FillChildTable_For_Abstractor_COST_TAT()
    {
        string qry = " SELECT Tbl_Treeview_Child_Name.User_id,Tbl_Treeview_Child_Name.User_Name + ' --> ' +CAST(Tbl_Treeview_Child_Name.No_OF_Orders AS VARCHAR(10)) as User_Name,Tbl_Treeview_Child_Name.Main_id  from Tbl_Treeview_Child_Name ";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }

    public DataTable FillChildTaxTable()
    {
        string qry = "SELECT Tbl_Treeview_Child_Name.User_id,UPPER(Tbl_Treeview_Child_Name.User_Name + ' ('+CAST(Tbl_Treeview_Child_Name.No_OF_Orders AS VARCHAR(10))+')') as User_Name,Tbl_Treeview_Child_Name.Main_id  from Tbl_Treeview_Child_Name";
        connect.OpenConnection();
        SqlDataAdapter ada = new SqlDataAdapter();
        ada = new SqlDataAdapter(qry, connect.con);
        DataTable dt = new DataTable();
        ada.Fill(dt);
        connect.CloseConnection();
        return dt;
    }


    
}