using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Threading;
using System.Diagnostics;

namespace Ordermanagement_01.Employee
{
    public partial class Break_Details : Form

    {
    
        
        DateTime Start_Time, Current_Time,Total_Break_Time;

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Closing_value = 0;
        int Order_Id,User_Id;
        DialogResult dialogResult = new DialogResult();
        int Last_Inserted_Record_Id;
        string First_date, Secod_Date;
        int datetimediff;
        bool IsOpen = false;
        string Production_Date;
        int Break_Status;
        public Break_Details(int USER_ID, string FIRST_DATE, string SECOND_DATE,string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_Id = USER_ID;
            First_date = FIRST_DATE;
            Secod_Date = SECOND_DATE;
            Production_Date = PRODUCTION_DATE;
            
            
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

          
          //  var datetimediff = new DateTime((DateTime.Now - Start_Time).Ticks);

            Hashtable htget_Start_End_Time = new Hashtable();
            DataTable dtget_Start_End_Time = new System.Data.DataTable();

            htget_Start_End_Time.Add("@Trans", "DATE_DIFF");
            htget_Start_End_Time.Add("@Order_Break_Id",Last_Inserted_Record_Id);
            dtget_Start_End_Time = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htget_Start_End_Time);

            if (dtget_Start_End_Time.Rows.Count > 0)
            {

                 datetimediff = int.Parse(dtget_Start_End_Time.Rows[0]["Diff_Seconds"].ToString());

            }

            if (datetimediff >= 60)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();         

                htComments.Add("@Trans", "UPDATE_BREAK_END_TIME");
                htComments.Add("@Order_Break_Id", Last_Inserted_Record_Id);
                htComments.Add("@User_Id", User_Id);
                dtComments = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htComments);

            }

            TimeSpan tb;


            if (ddl_Break_Mode.SelectedValue.ToString() == "1" && datetimediff < 900)
            {
                lbl_Total_Time.ForeColor = System.Drawing.Color.Green;


            }
           

            else  if (ddl_Break_Mode.SelectedValue.ToString() == "2" && datetimediff < 1800)
            {

                lbl_Total_Time.ForeColor = System.Drawing.Color.Green;
            }
          
            else if (ddl_Break_Mode.SelectedValue.ToString() == "3" && datetimediff < 900)
            {
                lbl_Total_Time.ForeColor = System.Drawing.Color.Green;


            }
            else
            {
                lbl_Total_Time.ForeColor = System.Drawing.Color.Red;
            }

            tb = TimeSpan.FromSeconds(datetimediff);




            string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   tb.Hours,
                   tb.Minutes,
                   tb.Seconds);


            lbl_Total_Time.Text = breakformatedTime.ToString();
                
         
        }

        private void btn_Start_Time_Click(object sender, EventArgs e)
        {
            if (ddl_Break_Mode.SelectedIndex > 0)
            {

                Break_Status = 1;

                timer3.Enabled = false;
                
                timer1.Enabled = true;
                timer1_Tick( sender,  e);
           
                lbl_Start.Visible = true;
                lbl_Start_Time.Visible = true;
                lbl_Total.Visible = true;
                lbl_Total_Time.Visible = true;
                btn_Start_Time.Enabled = false;

                btn_Stop.Enabled = true;
                btn_Exit.Enabled = false;


                if (ddl_Break_Mode.SelectedIndex == 1 || ddl_Break_Mode.SelectedIndex == 2 || ddl_Break_Mode.SelectedIndex == 3 || ddl_Break_Mode.SelectedIndex == 4 || ddl_Break_Mode.SelectedIndex==5 || ddl_Break_Mode.SelectedIndex==6 || ddl_Break_Mode.SelectedIndex==7)
                {
                    if (InvokeRequired == false)
                    {

                        this.Invoke(new MethodInvoker(delegate
                        {
                            FormCollection fc = Application.OpenForms;

                            foreach (Form frm in fc)
                            {
                                frm.Enabled = false;
                            }

                            foreach (Form f in Application.OpenForms)
                            {

                                if (f.Name == "Break_Details")
                                {

                                    f.Enabled = true;

                                }

                            }

                        })) ;
                    }
                    else
                    {
                        FormCollection fc = Application.OpenForms;

                        foreach (Form frm in fc)
                        {
                            frm.Enabled = false;
                        }

                        foreach (Form f in Application.OpenForms)
                        {

                            if (f.Name == "Break_Details")
                            {

                                f.Enabled = true;

                            }

                        }

                    }
                    


                    //Ordermanagement_01.Employee.Ideal_Timings il = new Ideal_Timings(User_Id, Production_Date);
                                

                    //Ordermanagement_01.Employee.Ideal_Timings il = new Ideal_Timings(User_Id,Production_Date);
                    //il.Close();


                   
                  


                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");
                  
                    htComments.Add("@Break_Mode_Id",int.Parse(ddl_Break_Mode.SelectedValue.ToString()));
                    htComments.Add("@Start_Time", date);
                    htComments.Add("@End_Time", date);
                    htComments.Add("@User_Id", User_Id);
                    htComments.Add("@Date", date);
                    htComments.Add("@Production_Date", Production_Date);
                    object Max_Id = dataaccess.ExecuteSPForScalar("Sp_Order_User_Break_Details", htComments);


                    Hashtable htget_Start_End_Time = new Hashtable();
                    DataTable dtget_Start_End_Time = new System.Data.DataTable();

                    htget_Start_End_Time.Add("@Trans", "GET_START_END_TIME");
                    htget_Start_End_Time.Add("@Order_Break_Id", Max_Id);
                    dtget_Start_End_Time = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htget_Start_End_Time);

                    if (dtget_Start_End_Time.Rows.Count > 0)
                    {

                        lbl_Start_Time.Text = dtget_Start_End_Time.Rows[0]["Start_Time"].ToString();
                       
                    }

                    Last_Inserted_Record_Id = int.Parse(Max_Id.ToString());



                    
                }
                else {


                    if (InvokeRequired == false)
                    {

                        this.Invoke(new MethodInvoker(delegate
                        {
                            FormCollection fc = Application.OpenForms;

                            foreach (Form frm in fc)
                            {
                                frm.Enabled = true;
                            }
                        }));
                    }
                    else
                    {
                        FormCollection fc = Application.OpenForms;

                        foreach (Form frm in fc)
                        {
                            frm.Enabled = true;
                        }

                    }
                }

              
            }
            else
            {

                MessageBox.Show("Select Break Mode.");
                ddl_Break_Mode.Focus();
               
            }


        }

        private void Break_Details_Load(object sender, EventArgs e)
        {
            lbl_Stop.Visible = false;
            lbl_Start_Time.Visible = false;
            lbl_Stop_Time.Visible = false;
            lbl_Total.Visible = false;
            lbl_Total_Time.Visible = false;
            btn_Stop.Enabled = false;
            lbl_Start.Visible = false;
            btn_Stop.Enabled = false;

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "GET_BREAK_DETAILS");
            htComments.Add("@Firstdate", First_date);
            htComments.Add("@Second_Date", Secod_Date);
            htComments.Add("@User_Id", User_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htComments);


            dbc.Bind_BreakMode_Type(ddl_Break_Mode,User_Id);





         // these timers are called in IDeal Timngs forms and access from there
                timer1.Enabled = false;
            
            


        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            string reason="";
            if (ddl_Break_Mode.SelectedValue.ToString() == "5")
            {
                if (String.IsNullOrWhiteSpace(txtReason.Text) || txtReason.Text.Length < 5)
                {
                    MessageBox.Show("Enter a valid reason with min 5 characters");
                    txtReason.Focus();
                    return;
                }else{
                    reason = txtReason.Text;
                }
            }
            Break_Status = 2;
            lbl_Stop.Visible = true;
            lbl_Stop_Time.Visible = true;
           
            timer1.Enabled = false;
           
            btn_Stop.Enabled = false;
            btn_Exit.Enabled = true;

            if (InvokeRequired == false)
            {

                this.Invoke(new MethodInvoker(delegate
                {
                    FormCollection fc = Application.OpenForms;

                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }

                }));
            }
            else
            {

                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    frm.Enabled = true;
                }
            }

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");

            htComments.Add("@Trans", "UPDATE_BREAK_END_TIME");
            htComments.Add("@Order_Break_Id", Last_Inserted_Record_Id);
            htComments.Add("@Start_Time", date);
            htComments.Add("@End_Time", date);
            htComments.Add("@Reason", reason);
            htComments.Add("@User_Id", User_Id);
            
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htComments);

            Hashtable htget_Start_End_Time = new Hashtable();
            DataTable dtget_Start_End_Time = new System.Data.DataTable();

            htget_Start_End_Time.Add("@Trans", "GET_START_END_TIME");
            htget_Start_End_Time.Add("@Order_Break_Id", Last_Inserted_Record_Id);
            dtget_Start_End_Time = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htget_Start_End_Time);

            if (dtget_Start_End_Time.Rows.Count > 0)
            {

                lbl_Stop_Time.Text = dtget_Start_End_Time.Rows[0]["End_Time"].ToString();
                if (InvokeRequired == false)
                {

                    this.Invoke(new MethodInvoker(delegate
                    {

                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Text == "Employee_Order_Entry")
                            {
                                IsOpen = true;
                                f.Focus();
                                f.Enabled = true;
                                f.Show();
                                break;
                            }
                        }
                    }));
                }
                else
                {

                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Text == "Employee_Order_Entry")
                        {
                            IsOpen = true;
                            f.Focus();
                            f.Enabled = true;
                            f.Show();
                            break;
                        }
                    }

                }



            }
            timer3.Enabled = true;


            txtReason.Text = "";

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Closing_value = 1;

             Break_Status = 3;
                dialogResult = MessageBox.Show("Do you Want to Exit?", "Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                if (InvokeRequired==false)
                {

                    this.Invoke(new MethodInvoker(delegate
                    {

                        FormCollection fc = Application.OpenForms;
                        foreach (Form frm in fc)
                        {
                            frm.Enabled = true;
                        }
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Text == "Employee_Order_Entry")
                            {
                                IsOpen = true;
                                f.Focus();
                                f.Enabled = true;
                                f.Show();
                                break;
                            }
                        }
                    }));
                    }
                else
                {
                    FormCollection fc = Application.OpenForms;
                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Text == "Employee_Order_Entry")
                        {
                            IsOpen = true;
                            f.Focus();
                            f.Enabled = true;
                            f.Show();
                            break;
                        }
                    }

                }

                    //Thread t = new Thread((ThreadStart)delegate { System.Windows.Forms.Application.Run(new Ordermanagement_01.Employee.Ideal_Timings(User_Id, Production_Date)); });
                    //t.SetApartmentState(ApartmentState.STA);
                    //t.Start(); 

                    this.Close();

                  
                }
                else
                {

                    
                    timer3_Tick( sender,  e);
                }

          
        }

        private void CLose_Form()
        {

            Closing_value = 1;

            Break_Status = 3;

            if (InvokeRequired == false)
            {

                this.Invoke(new MethodInvoker(delegate
                {

                    FormCollection fc = Application.OpenForms;
                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Text == "Employee_Order_Entry")
                        {
                            IsOpen = true;
                            f.Focus();
                            f.Enabled = true;
                            f.Show();
                            break;
                        }
                    }
                }));
            }
            else
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    frm.Enabled = true;
                }
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "Employee_Order_Entry")
                    {
                        IsOpen = true;
                        f.Focus();
                        f.Enabled = true;
                        f.Show();
                        break;
                    }
                }

            }
                //Thread t = new Thread((ThreadStart)delegate { System.Windows.Forms.Application.Run(new Ordermanagement_01.Employee.Ideal_Timings(User_Id, Production_Date)); });
                //t.SetApartmentState(ApartmentState.STA);
                //t.Start(); 
                Timer_Count = 0;
                this.Close();


            
          
        }

        private void Break_Details_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (Closing_value != 1)
            //{
            //    dialogResult = MessageBox.Show("Do you Want to Exit?", "Some Title", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        FormCollection fc = Application.OpenForms;
            //        foreach (Form frm in fc)
            //        {
            //            frm.Enabled = true;
            //        }

            //        this.Close();
            //    }
            //    else
            //    {



            //    }
            //}
            if (Closing_value != 1)
            {

                e.Cancel = true;

                this.WindowState = FormWindowState.Minimized;
            }

        }

        private void link_Break_Details_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, User_Id,"Break");

            foreach (Form f in Application.OpenForms)
            {

                if (f.Name == "Employee_View_Break_Details")
                {

                    f.Hide();


                }

            }
            emb.Show();

        }

      
        private void Ideal_Timer_Tick(object sender, EventArgs e)
        {

        }

        int Timer_Count = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            Timer_Count++;

            if (Timer_Count >= 10)
            {
                if (Break_Status == 0 || Break_Status == 2 || Break_Status==3)
                {
                  

                    timer3.Enabled = false;
                    CLose_Form();
                     
                    
                   
                }
             
            }

            
        }

        private void ddl_Break_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Break_Mode.SelectedValue.ToString() == "5")
            {
                lblReason.Visible = true;
                txtReason.Visible = true;
            }
            else {
                lblReason.Visible = false;
                txtReason.Visible = false;
            }
        }

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);

            if (e.Handled == true)
            {
                //e.Handled = true;
                MessageBox.Show("Invalid!,Special Charcters Not allowed");
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txtReason.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }


            //if (!(char.IsLetter(e.KeyChar)) && !(char.IsWhiteSpace(e.KeyChar)))  // && !(char.IsSymbol(e.KeyChar)) && e.KeyChar == '-' && e.KeyChar == '(' && e.KeyChar == ')' && e.KeyChar == '!' && e.KeyChar == '@') //&& e.KeyChar == '#' && e.KeyChar == '$' && e.KeyChar == '%' && e.KeyChar == '^' && e.KeyChar == '&' && e.KeyChar == '*' && e.KeyChar == '<' && e.KeyChar == '>' && e.KeyChar == '?' && e.KeyChar == '+' && e.KeyChar == '~' && e.KeyChar == '|')
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Invalid!,Special Charcters Not allowed");
            //}

           




        }

       
    }
}
