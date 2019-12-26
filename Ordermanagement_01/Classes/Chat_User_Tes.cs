using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ordermanagement_01
{
    public partial class Chat_User_Tes : Form
    {
        private int m_UserID = 0;
        private string m_UserName;
        private ChatterData.ChatData m_Data = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public Chat_User_Tes()
        {
            InitializeComponent();

            // Make sure client has permissions 
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

        /// <summary>
        /// Handler for OnLoad event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chat_Load(object sender, EventArgs e)
        {
            // Display the dialog to select users
            Ordermanagement_01.User dlg = new User();

            

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Set the variables and text
                m_UserID = dlg.UserID;
                this.Text = m_UserName = dlg.UserName;

                // Create a new ChatData object
                m_Data = new ChatterData.ChatData();

                // Hook up event
                m_Data.OnNewMessage += new ChatterData.ChatData.NewMessage(OnNewMessage);

                // Load existing message
                LoadMessages();

                Form2 f21 = new Form2();

                f21.Show();
            }

            else
                Close();
        }

        /// <summary>
        /// Handler for send button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSend(object sender, EventArgs e)
        {
            // Add new message to the database
            ChatterData.ChatData.AddMessage(txtMsg.Text, m_UserID);

         
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
                    ChatterData.ChatData.NewMessage tempDelegate = new ChatterData.ChatData.NewMessage(OnNewMessage);
                    i.BeginInvoke(tempDelegate, null);
                    return;
                }

                // If not coming from a seperate thread
                // we can access the Windows form controls
                LoadMessages();
            
        }

        /// <summary>
        /// Helper method to populate list box
        /// </summary>
        private void LoadMessages()
        {
            // Clear the listbox
            lstHistory.Items.Clear();

            // Get the messages
            DataTable dt = m_Data.GetMessages();

            // Iterate through the records and add them
            // to the listbox
            foreach (DataRow row in dt.Rows)
            {
                string Msg = string.Format("{0}" , row["Name"]);
                lstHistory.Items.Add(Msg);
            }

         
        }
    }
}