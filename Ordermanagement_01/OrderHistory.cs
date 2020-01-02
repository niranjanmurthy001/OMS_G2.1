using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class OrderHistory : DevExpress.XtraEditors.XtraForm
    {
        private readonly int userId, orderId,roleId;
        private readonly string orderNumber, client, subProcess, state, county;        
        private DataAccess dataaccess = new DataAccess();
        public OrderHistory(int userid,string roleId, int Orderid, string OrderNo, string Clientname, string Subprocessname, string State, string County)
        {
            InitializeComponent();
            this.userId = userid;
            this.orderId = Orderid;
            this.orderNumber = OrderNo;
            this.client = Clientname;
            this.subProcess = Subprocessname;
            this.state = State;
            this.county = County;
            this.roleId = Convert.ToInt32(roleId);
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            groupControlOrderHistory.Text = orderNumber + "'s History";
            lbl_Clientname.Text = client;
            lbl_Subprocess.Text = subProcess;
            lbl_State.Text = state;
            lbl_County.Text = county;
            BindGridHistory();
            BindStatusHistory();
            if (roleId == 2)
            {
                splitContainerControl1.Panel2.Visible = false;
            }
        }
        private void BindStatusHistory()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@Order_Id", orderId);
            dtselect = dataaccess.ExecuteSP("Sp_Order_Task_Status_Detail", htselect);
            if (dtselect.Rows.Count > 0)
            {
                gridControlOrderStatusHistory.DataSource = dtselect;
            }
        }
        private void BindGridHistory()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@Order_Id", orderId);
            dtselect = dataaccess.ExecuteSP("Sp_Order_History", htselect);
            if (dtselect.Rows.Count > 0)
            {
                gridControlOrderHistory.DataSource = dtselect;
            }
        }

        private void gridViewOrderHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewOrderStatusHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}