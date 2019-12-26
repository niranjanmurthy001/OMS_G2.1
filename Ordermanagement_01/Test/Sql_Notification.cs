using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ordermanagement_01.Test
{
    public partial class Sql_Notification : Form
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd;
        public Sql_Notification()
        {
            InitializeComponent();
        }

        private void Sql_Notification_Load(object sender, EventArgs e)
        {
           
            Con.Open();
            SqlDependency.Start(Con.ToString());
             cmd = new SqlCommand("select MID from [Message]", Con);

            SqlDependency sqlDependency = new SqlDependency(cmd);

            sqlDependency.OnChange += SqlDependecy_OnChange;

            label1.Text = cmd.ExecuteScalar().ToString();




        }

        private void Sql_Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlDependency.Stop(Con.ToString());
            Con.Close();

        }

        private void SqlDependecy_OnChange(object sender,SqlNotificationEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() => SqlDependecy_OnChange(sender, e)));
            else
            {

                SqlDependency SqlDepenedncy = (SqlDependency)sender;
                SqlDepenedncy.OnChange -= SqlDependecy_OnChange;
                cmd.Notification = null;
                SqlDepenedncy = new SqlDependency(cmd);
                SqlDepenedncy.OnChange += SqlDependecy_OnChange;


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test.Message_Add msg_add = new Test.Message_Add();

            msg_add.Show();
        }
    }
}
