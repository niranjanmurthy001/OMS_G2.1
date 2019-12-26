using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class Employee_Order_Information : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, order_id, order_type_abs, workType, task; string state_id, user_Role;
        System.Data.DataTable dt_Select_Order_Details = new System.Data.DataTable();

        System.Data.DataTable dtSelect_Order_Details = new System.Data.DataTable();
        int start = 0;
        int indexOfSearchText = 0;

        public Employee_Order_Information(int userid, string State_ID, int Order_Id, string USER_ROLE, int workType, int task)
        {
            InitializeComponent();
            Userid = userid;
            state_id = State_ID;
            order_id = Order_Id;
            user_Role = USER_ROLE;
            this.workType = workType;
            this.task = task;
        }



        private void Bind_US_Tax()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            ht.Add("@State_Id", state_id);
            dt = dataaccess.ExecuteSP("Sp_State_Tax_Due_Date", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_Tax.DataSource = dt;

            }
            else
            {

                Grid_Tax.DataSource = null;

                Grid_Tax.Rows.Clear();
            }
        }
        private void Get_Order_Details()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            // DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT_EMPLOYEE_ENTRY_INFORMATION");
            ht_Select_Order_Details.Add("@Order_ID", order_id);
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);
            //if (dt_Select_Order_Details.Rows.Count > 0)
            //{
            //    txt_Order_Instructions.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
            //    order_type_abs = int.Parse(dt_Select_Order_Details.Rows[0]["OrderType_ABS_Id"].ToString());
            //}

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                rtb.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
                order_type_abs = int.Parse(dt_Select_Order_Details.Rows[0]["OrderType_ABS_Id"].ToString());
            }
        }



        private void Bind_Statute_limitation()
        {
            Hashtable ht_Select = new Hashtable();
            DataTable dt_Select = new DataTable();

            ht_Select.Add("@Trans", "SELECT_EMPLOYEE_STATUE_INFO");
            ht_Select.Add("@State", state_id);
            ht_Select.Add("@Order_Type_Id", order_type_abs);

            dt_Select = dataaccess.ExecuteSP("Sp_Order", ht_Select);
            if (dt_Select.Rows.Count > 0)
            {
                //grd_Statue_of_limitation.DataSource = dt_Select;
                txt_Statue_of_Info.Text = dt_Select.Rows[0]["Statute of limitation"].ToString();
            }
            else
            {
                txt_Statue_of_Info.Text = "";
            }

        }


        private void Employee_Order_Information_Load(object sender, EventArgs e)
        {
            Bind_US_Tax();
            Get_Order_Details();
            Bind_Statute_limitation();

            if (user_Role == "2")
            {
                this.ControlBox = false;
            }
            else
            {
                this.ControlBox = true;
            }
            if (workType > 0)
            {
                try
                {
                    checkBoxInstructions.Visible = true;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        Form form = Application.OpenForms["Employee_Order_Entry"];
                        form.Enabled = false;
                    }));
                    MessageBox.Show("Please read all the instruction and acknowledge !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong contact admin");
                }
            }

        }

        private void txt_Search_Text_TextChanged(object sender, EventArgs e)
        {
            ClearSelection(rtb);
        }

        private void Employee_Order_Information_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workType > 0)
            {
                if (!checkBoxInstructions.Checked)
                {
                    MessageBox.Show("Read all the instruction and acknowledge !");
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        var htInsert = new Hashtable()
                        {
                            { "@Trans","INSERT"       },
                            { "@Order_Id",order_id    },
                            { "@User_Id",Userid       },
                            { "@Task_Id",task         },
                            { "@Work_Type_Id",workType},
                            { "@Form_State",true      }
                        };
                        var id = dataaccess.ExecuteSPForScalar("Sp_Order_Info_Instruction_Log", htInsert);
                        this.Invoke(new MethodInvoker(delegate
                        {
                            Form form = Application.OpenForms["Employee_Order_Entry"];
                            form.Enabled = true;
                        }));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong contact admin");
                    }
                }
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = rtb.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (rtb.Text != string.Empty)
                {// if the ritchtextbox is not empty; highlight the search criteria
                    int index = 0;
                    String temp = rtb.Text;
                    rtb.Text = "";
                    rtb.Text = temp;
                    while (index < rtb.Text.ToLower().LastIndexOf(txt_Search_Text.Text.ToLower()))
                    {
                        rtb.Find(txt_Search_Text.Text.ToLower(), index, rtb.TextLength, RichTextBoxFinds.None);
                        rtb.SelectionBackColor = Color.Yellow;
                        index = rtb.Text.ToLower().IndexOf(txt_Search_Text.Text.ToLower(), index) + 1;
                        rtb.Select();
                    }
                }

            }

            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }


        }

        private void ClearSelection(RichTextBox rtb)
        {
            if (rtb.Text.Length > 0)
            {
                int currentIndex = rtb.SelectionStart;
                rtb.SelectAll();
                rtb.SelectionBackColor = Color.White;
                rtb.SelectionLength = 0;
                rtb.SelectionStart = currentIndex;



            }
        }

        private void txt_Search_Text_KeyDown(object sender, KeyEventArgs e)
        {

        }


        public int FindMy_Text(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            {
                rtb.Undo();
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = rtb.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                    else
                    {
                        start = 0;
                        indexOfSearchText = 0;
                        //return indexOfSearchText;
                    }
                }
            }
            return retVal;

        }


    }
}
