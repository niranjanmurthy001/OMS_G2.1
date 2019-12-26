using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ordermanagement_01.Classes;
using System.Data.SqlClient;
using System.Collections;

namespace Ordermanagement_01.Test
{
    public partial class Real_Time_Nofication : Form
    {

        private Notification_Data N_Data = null;

        DataAccess dataaccess = new DataAccess();
        

        public Real_Time_Nofication()
        {
            InitializeComponent();

            try
            {
                SqlClientPermission perm = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
                perm.Demand();
            }
            catch
            {
                throw new ApplicationException("No permission");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable htinsertrec = new Hashtable();
            System.Data.DataTable dtinsertrec = new System.Data.DataTable();

            Hashtable htchk_Assign = new Hashtable();
            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            htchk_Assign.Add("@Trans", "CHECK");
            htchk_Assign.Add("@Order_Id", 1);
            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            if (dtchk_Assign.Rows.Count > 0)
            {
                Hashtable htupassin = new Hashtable();
                System.Data.DataTable dtupassign = new System.Data.DataTable();

                htupassin.Add("@Trans", "DELET_BY_ORDER");
                htupassin.Add("@Order_Id", 1);
                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            }
            htinsertrec.Add("@Trans", "INSERT");
            htinsertrec.Add("@Order_Id", 1);
            htinsertrec.Add("@User_Id", 1);
            htinsertrec.Add("@Order_Status_Id", 2);
            htinsertrec.Add("@Order_Progress_Id", 6);
            htinsertrec.Add("@Assigned_Date",null);
            htinsertrec.Add("@Assigned_By", 1);
            htinsertrec.Add("@Inserted_By", 1);
            htinsertrec.Add("@Inserted_date", null);
            htinsertrec.Add("@status", "True");
            htinsertrec.Add("@Order_Percentage", 2);
            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

        }

        /// <summary>
        /// Handler for NewMessage
        /// </summary>
        void OnNewMessage()
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;

            // Check if the event was generated from another
            // thread and needs invoke instead
            if (i.InvokeRequired)
            {

                Notification_Data.NewMessage tempDelegate = new Notification_Data.NewMessage(OnNewMessage);
                i.BeginInvoke(tempDelegate, null);
                return;
            }

            // If not coming from a seperate thread
            // we can access the Windows form controls
            LoadMessages();
        }

        /// <summary>
        /// Handler for OnLoad event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Real_Time_Nofication_Load(object sender, EventArgs e)
        {
            // Hook up event

           // N_Data.ONNewMessage += new Notification_Data.NewMessage(OnNewMessage);

        }

        private void LoadMessages()
        {

            DataTable dt = N_Data.Get_User_Order_Count(1);

            // Iterate through the records and add them
            // to the listbox

            if (dt.Rows.Count > 0)
            {

                linkLabel1.Text = dt.Rows[0]["Count"].ToString();
            }
            
        }


    }
}
