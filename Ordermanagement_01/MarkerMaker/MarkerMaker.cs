using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Bytescout.PDFViewer;
using Bytescout.PDFRenderer;
using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;

using System.Diagnostics;
using System.IO;

namespace Ordermanagement_01.MarkerMaker

{
    public partial class MarkerMaker : Form
    {
        
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DateTimePicker dtp_Value = new DateTimePicker();
        TextBox txt_Value = new TextBox();
        TextBox str_Value = new TextBox();
        ComboBox cbo_Value = new ComboBox();
        TreeNode parentnode;
        TreeNode childnode1;
        int deedValue;
        int Orderid;
        string Path;
        Double Right, Top, Left, Bottom;
        string Select_Node;
        string Select_Node_Value;
        int Order_Status;
        string Pdf_Path;
        string Markermaker_id;
        private Point tbx;
        string Value_Txt;
        int pdf_foc=0;
      
        public MarkerMaker(int Order_Id,int OrderStatus)
        {
            InitializeComponent();
            lbl_Value.Text = "";
            Value_Txt = "";
            Orderid = Order_Id;
            Order_Status =OrderStatus;
          
        }

        private void MarkerMaker_Load(object sender, EventArgs e)
        {
            lbl_Value.Text = "";
            if (ddl_Marker.Text == "Deed")
            {
               // AddParent_Deed();
                //tvwRightSide.ExpandAll();
            }

            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "PACKAGE_VALIDATE");
            ht.Add("@Order_Id", Orderid);
            dt = dataaccess.ExecuteSP("Sp_Document_Upload", ht);
            if (dt.Rows.Count > 0)
            {
                Pdf_Path = dt.Rows[0]["Document_Path"].ToString();
                File.Copy(Pdf_Path, Environment.CurrentDirectory + "\\MarkerMaker1.pdf", true);
                pdfViewerControl1.InputFile = dt.Rows[0]["Document_Path"].ToString();
               
            }
            if (Order_Status == 4)
            {
               //groupBox1.Enabled = false;
                txt_Value.Enabled = false;
                dtp_Value.Enabled = false;
                str_Value.Enabled = false;
            }
            rb_Selectionmode.Checked = true;
            rb_View_Mode.Checked = false;
            AddParent_Deed();
        }
        private void AddParent_Deed()
        {
            tvwRightSide.Nodes.Clear();
            string sKeyTemp = "";
            tvwRightSide.Nodes.Clear();         
           
                sKeyTemp = "Deed";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Mortgage";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Assessment";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Total Tax";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Judgment";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);


                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Legal Description";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Tax";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Order Information";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);
                parentnode.BackColor = System.Drawing.Color.Pink;
                sKeyTemp = "Additional Information";
                // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                parentnode = tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds_Deed(parentnode);

                parentnode.BackColor = System.Drawing.Color.Pink;
        }
        private void AddSubChild(TreeNode ChildNode2,string Id)
        {
            // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
            //if (ChildNode2.Name == Id)
            //{ ht.Add("@Trans", "BIND");
           // Hashtable ht = new Hashtable();
           // DataTable dt = new System.Data.DataTable();
           // ht.Add("@Order_Id", Orderid);
           // dt = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", ht);
           // for (int i = 0; i < dt.Rows.Count; i++)
           //         {
           //             if (ChildNode2.Text == dt.Rows[i]["Path"].ToString())
           //             {
           //                 parentnode = ChildNode2.Parent.Nodes.Add("Judgment Sub Document", "Judgment Sub Document");
           //                 AddChilds_Deed(parentnode);
           //             }
           // }
           //// }
        }
        private void AddChilds_Deed(TreeNode Parent)
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode childnode = tvwRightSide.SelectedNode; 
          //  deedValue = int.Parse(Value.ToString())+1;
            if (Parent.Text == "Deed")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Deed", ht);
                string Marker_Deed_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Deed_Id = dt.Rows[i]["Marker_Deed_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Deed_Id"].ToString(), "Deed " + j);
                        Markermaker_id = dt.Rows[i]["Marker_Deed_Id"].ToString();
                        childnode.BackColor = System.Drawing.Color.Yellow;
                        AddChilds1_Deed(childnode, Marker_Deed_Id);
                    }

                }
            }

            else if (Parent.Text == "Mortgage")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Mortgage", ht);
                string Marker_Mortgage_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Mortgage_Id = dt.Rows[i]["Marker_Mortgage_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Mortgage_Id"].ToString(), "Mortgage " + j);
                        AddChilds1_Deed(childnode, Marker_Mortgage_Id);
                    }

                }
            }

            else if (Parent.Text == "Total Tax")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", ht);
                string Marker_Total_Tax_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Total_Tax_Id = dt.Rows[i]["Marker_Total_Tax_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Total_Tax_Id"].ToString(), "Total_Tax " + j);
                        AddChilds1_Deed(childnode, Marker_Total_Tax_Id);
                    }

                }
            }


            else if (Parent.Text == "Assessment")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Assessment", ht);
                string Marker_Assessment_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Assessment_Id = dt.Rows[i]["Marker_Assessment_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Assessment_Id"].ToString(), "Assessment " + j);
                        AddChilds1_Deed(childnode, Marker_Assessment_Id);
                    }

                }
            }

            else if (Parent.Text == "Judgment")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Judgment", ht);
                string Marker_Judgment_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Judgment_Id = dt.Rows[i]["Marker_Judgment_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Judgment_Id"].ToString(), "Judgment " + j);
                      
                        AddChilds1_Deed(childnode, Marker_Judgment_Id);
                        parentnode = childnode.Nodes.Add("Judgment Sub Document", "Judgment Sub Document");
                      //  AddSubChild(childnode1, childnode1.Parent.Name);
                    }
                   
                }
            }
            else if (Parent.Text == "Judgment Sub Document")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", ht);
                string Marker_Sub_Document_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Sub_Document_Id = dt.Rows[i]["Marker_Sub_Document_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Sub_Document_Id"].ToString(), "Judgment Sub Document " + j);
                        AddChilds1_Deed(childnode, Marker_Sub_Document_Id);
                    }

                }
            }


            else if (Parent.Text == "Tax")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Tax", ht);
                string Marker_Tax_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Tax_Id = dt.Rows[i]["Marker_Tax_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Tax_Id"].ToString(), "Tax " + j);
                        AddChilds1_Deed(childnode, Marker_Tax_Id);
                    }

                }
            }



            else if (Parent.Text == "Legal Description")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", ht);
                string Marker_Legal_Description_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Legal_Description_Id = dt.Rows[i]["Marker_Legal_Description_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Legal_Description_Id"].ToString(), "Legal_Description " + j);
                        AddChilds1_Deed(childnode, Marker_Legal_Description_Id);
                    }

                }
            }

            else if (Parent.Text == "Order Information")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Order_Information", ht);
                string Marker_Order_Information_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Order_Information_Id = dt.Rows[i]["Marker_Order_Information_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Order_Information_Id"].ToString(), "Order_Information " + j);
                        AddChilds1_Deed(childnode, Marker_Order_Information_Id);
                    }

                }
            }

            else if (Parent.Text == "Mortgage Sub Document")
            {
                ht.Add("@Trans", "BIND");
                ht.Add("@Order_Id", Orderid);
                dt = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", ht);
                string Marker_Additional_Information_Id = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int j = i + 1;
                        Marker_Additional_Information_Id = dt.Rows[i]["Marker_Additional_Information_Id"].ToString();
                        childnode = parentnode.Nodes.Add(dt.Rows[i]["Marker_Additional_Information_Id"].ToString(), "Additional_Information " + j);
                        AddChilds1_Deed(childnode, Marker_Additional_Information_Id);
                    }

                }
            }

           
        }
        private void AddChilds1_Deed(TreeNode childnode, string Marker_Deed_Id)
        {
            if (childnode.Parent.Text == "Deed")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Deed", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Deed_Information"].ToString(), dt.Rows[i]["Deed_Information"].ToString());
                   
                }
               
            }

            else if (childnode.Parent.Text == "Mortgage")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Mortgage", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Mortgage_Information"].ToString(), dt.Rows[i]["Mortgage_Information"].ToString());
                 
                }
            }

            else if (childnode.Parent.Text == "Total Tax")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Total_Tax", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Total_Tax_Information"].ToString(), dt.Rows[i]["Total_Tax_Information"].ToString());

                }
            }



            else if (childnode.Parent.Text == "Assessment")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Assessment", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Assessment", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Assessment_Information"].ToString(), dt.Rows[i]["Assessment_Information"].ToString());

                }
            }




            else if (childnode.Parent.Text == "Judgment")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
               
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Judgment", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Judgment", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Judgment_Information"].ToString(), dt.Rows[i]["Judgment_Information"].ToString());
                   
                }
             
            }

            else if (childnode.Parent.Text == "Judgment Sub Document")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Sub_Document", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Sub_Document_Information"].ToString(), dt.Rows[i]["Sub_Document_Information"].ToString());

                }
            }


            else if (childnode.Parent.Text == "Tax")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Tax", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Tax_Information"].ToString(), dt.Rows[i]["Tax_Information"].ToString());
                    
                }
            }


            else if (childnode.Parent.Text == "Legal Description")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Legal_Description_Information"].ToString(), dt.Rows[i]["Legal_Description_Information"].ToString());
                  
                }
            }


            else if (childnode.Parent.Text == "Order Information")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Order_Information", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Order_Information", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Order_Information"].ToString(), dt.Rows[i]["Order_Information"].ToString());
                    
                }
            }



            else if (childnode.Parent.Text == "Mortgage Sub Document")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                TreeNode childnode1;
                Hashtable ht1 = new Hashtable();
                DataTable dt1 = new System.Data.DataTable();
                ht1.Add("@Trans", "BIND");
                ht1.Add("@Order_Id", Orderid);
                dt1 = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", ht1);
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    childnode1 = childnode.Nodes.Add(dt.Rows[i]["Additional_Information"].ToString(), dt.Rows[i]["Additional_Information"].ToString());
                  
                }
            }
        }

        //private void AddChilds2_Deed(TreeNode childnode1, string Marker_Deed_Id)
        //{
        //    Hashtable htComments1 = new Hashtable();
        //    DataTable dtComments1 = new System.Data.DataTable();
        //    bool isNum;
        //    TreeNode childnode2;
        //  //  Select_Node = tvwRightSide.SelectedNode.Text;
        //    int nodevalue = 0;
        //    Value_Txt = "";
        //    if (tvwRightSide.SelectedNode != null)
        //    {
        //        if (Select_Node != "")
        //        {
        //            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
        //        }
        //    }
        //    if (ddl_Marker.Text == "Deed")
        //    {
        //        htComments1.Add("@Trans", "SELECT");
        //        htComments1.Add("@Marker_Deed_Id", Markermaker_id);
        //        htComments1.Add("@Deed_Information", Marker_Deed_Id);
        //        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments1);
        //        for (int i = 0; i < dtComments1.Rows.Count; i++)
        //        {
        //            int j = i + 1;
        //            childnode2 = childnode1.Nodes.Add(dtComments1.Rows[i]["Marker_Deed_Id"].ToString(),j.ToString());
        //        }
        //    }
        //}
           private void Controls_Enable()
           {
               if (tvwRightSide.SelectedNode.Text == "Dated Date" || tvwRightSide.SelectedNode.Text == "Recorded Date" || tvwRightSide.SelectedNode.Text == "Paid Date" || tvwRightSide.SelectedNode.Text == "Due Date" || tvwRightSide.SelectedNode.Text=="Delinquent Date" || tvwRightSide.SelectedNode.Text == "Maturity Date")
               {
                   dtp_Value.Enabled = true;
                   dtp_Value.Visible = true;
                    tbx.X = 380; tbx.Y = 518;
                    dtp_Value.Location = tbx;
                    dtp_Value.Width = 165;
                    dtp_Value.Height = 26;
                    dtp_Value.Font = new System.Drawing.Font("Times New Roman", 12);
                    dtp_Value.CustomFormat = "MM/dd/yyyy";
                    dtp_Value.Format = DateTimePickerFormat.Custom;
                    this.Controls.Add(dtp_Value);
                    dtp_Value.Text = Value_Txt;
                    dtp_Value.KeyDown += new KeyEventHandler(dtp_Value_KeyDown);

                }
               else if (tvwRightSide.SelectedNode.Text == "Consideration Amount" || tvwRightSide.SelectedNode.Text == "Page" || tvwRightSide.SelectedNode.Text == "Judgment Amount" || tvwRightSide.SelectedNode.Text == "Total Amount" || tvwRightSide.SelectedNode.Text == "Delinquent Amount" || tvwRightSide.SelectedNode.Text == "Account" || tvwRightSide.SelectedNode.Text == "Mortgage Amount"
                   || tvwRightSide.SelectedNode.Text=="Land" ||tvwRightSide.SelectedNode.Text=="Building" || tvwRightSide.SelectedNode.Text=="Total"||tvwRightSide.SelectedNode.Text=="Assessment No." || tvwRightSide.SelectedNode.Text=="Tax Year"||tvwRightSide.SelectedNode.Text=="Zip")
               {
                   txt_Value.Enabled = true;
                   txt_Value.Visible = true;
                  
                   tbx.X = 380; tbx.Y = 518;
                   txt_Value.Location = tbx;
                   txt_Value.Width = 165;
                   txt_Value.Height = 26;
                   txt_Value.Font = new System.Drawing.Font("Times New Roman", 12);
                   this.Controls.Add(txt_Value);
                   txt_Value.Text = Value_Txt;
                   txt_Value.KeyPress += new KeyPressEventHandler(txt_Value_KeyPress);
                   txt_Value.KeyDown += new KeyEventHandler(txt_Value_KeyDown);
               }
               else if (tvwRightSide.SelectedNode.Text == "Book" || tvwRightSide.SelectedNode.Text == "Instrument No" || tvwRightSide.SelectedNode.Text == "Grantor" || tvwRightSide.SelectedNode.Text == "Grantee" || tvwRightSide.SelectedNode.Text == "Additional Information" ||
                   tvwRightSide.SelectedNode.Text == "Mortgagor/Borrower" || tvwRightSide.SelectedNode.Text == "Mortgagee/Beneficiary" ||tvwRightSide.SelectedNode.Text=="Trustee"||tvwRightSide.SelectedNode.Text=="Open Ended Language"
                   || tvwRightSide.SelectedNode.Text == "Case No" || tvwRightSide.SelectedNode.Text == "Plaintiff" || tvwRightSide.SelectedNode.Text == "Defendant" || tvwRightSide.SelectedNode.Text == "Comments" 
                   || tvwRightSide.SelectedNode.Text == "HOA" || tvwRightSide.SelectedNode.Text == "Municipality Name" || tvwRightSide.SelectedNode.Text== "Description"
                   || tvwRightSide.SelectedNode.Text=="Borrower Name" ||tvwRightSide.SelectedNode.Text== "Property Address" ||tvwRightSide.SelectedNode.Text=="Deed Type"
                   || tvwRightSide.SelectedNode.Text == "Open Ended (Yes/No)" || tvwRightSide.SelectedNode.Text == "Tax Status" || tvwRightSide.SelectedNode.Text == "Mortgage Type" || tvwRightSide.SelectedNode.Text == "Judgment Type"
                   ||tvwRightSide.SelectedNode.Text=="Tax Type" || tvwRightSide.SelectedNode.Text=="Status"||tvwRightSide.SelectedNode.Text=="Municipality Type")
               {
                   str_Value.Enabled = true;
                   str_Value.Visible = true;

                   tbx.X = 380; tbx.Y = 518;
                   str_Value.Location = tbx;
                   str_Value.Multiline = true;
                   str_Value.Width = 765;
                   str_Value.Height = 100;
                   str_Value.ScrollBars = ScrollBars.Both;
                   str_Value.Font = new System.Drawing.Font("Times New Roman", 12);
                   this.Controls.Add(str_Value);
                   str_Value.Text = Value_Txt;
                   
                   str_Value.KeyDown += new KeyEventHandler(str_Value_KeyDown);
               }
               else if (tvwRightSide.SelectedNode.Text == "Type")
               {
                   cbo_Value.Enabled = true;
                   cbo_Value.Visible = true;
                   tbx.X = 380; tbx.Y = 518;
                   cbo_Value.Location = tbx;
                   cbo_Value.Width = 165;
                   cbo_Value.Height = 26;
                   this.Controls.Add(cbo_Value);
                   if (tvwRightSide.SelectedNode.Parent.Parent.Text == "Additional Information")
                   {
                       dbc.BindMortgage(cbo_Value, Orderid);
                   }
                   else if (tvwRightSide.SelectedNode.Parent.Parent.Text == "Judgment Sub Document")
                   {
                       dbc.BindJudgment(cbo_Value, Orderid);
                   }
                   if (Value_Txt != null && Value_Txt != "")
                   {
                       cbo_Value.SelectedValue= Value_Txt;
                   }
                   cbo_Value.KeyDown += new KeyEventHandler(cbo_Value_KeyDown);
               }
               
            }

        private void tvwRightSide_AfterSelect(object sender, TreeViewEventArgs e)
        {
          
            txt_Value.Enabled=false;
            txt_Value.Visible=false;
            dtp_Value.Visible=false;
            dtp_Value.Enabled=false;
            str_Value.Enabled = false;
            str_Value.Visible= false;
            cbo_Value.Enabled = false;
            cbo_Value.Visible = false;
           
            bool isNum;
            Select_Node = tvwRightSide.SelectedNode.Text;
            int nodevalue = 0;
            Value_Txt = "";
            if (tvwRightSide.SelectedNode.Parent != null)
            {
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                }
            }
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();

            //DateTime date = new DateTime();
            //date = DateTime.Now;
            //string dateeval = date.ToString("dd/MM/yyyy");
            //string time = date.ToString("hh:mm tt");
            if (ddl_Marker.Text == "Deed")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Deed_Id", nodevalue);
                htComments1.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments1);
            }
           else if (ddl_Marker.Text == "Mortgage")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Mortgage_Id", nodevalue);
                htComments1.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments1);
            }


            else if (ddl_Marker.Text == "Total Tax")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Total_Tax_Id", nodevalue);
                htComments1.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments1);
            }


            else if (ddl_Marker.Text == "Judgment")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Judgment_Id", nodevalue);
                htComments1.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Judgment Sub Document")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Sub_Document_Id", nodevalue);
                htComments1.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Tax")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Tax_Id", nodevalue);
                htComments1.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Legal Description")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Legal_Description_Id", nodevalue);
                htComments1.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Order Information")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Order_Information_Id", nodevalue);
                htComments1.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments1);
            }
            else if (ddl_Marker.Text == "Assessment Information")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Assessment_Information_Id", nodevalue);
                htComments1.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Assessment_Information_Child", htComments1);
            }
            else if (ddl_Marker.Text == "Mortgage Sub Document")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Additional_Information_Id", nodevalue);
                htComments1.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments1);
            }
            Controls_Enable();
            int PageNo1 = 0;
            string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
            iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
          
            
            pdfViewerControl1.InputFile = null;
            if (dtComments1 != null)
            {
                File.Copy(Pdf_Path, Environment.CurrentDirectory + "\\MarkerMaker1.pdf", true);
                using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    using (PdfStamper stamper = new PdfStamper(reader1, fs))
                    {
                        for (int i = 0; i < dtComments1.Rows.Count; i++)
                        {
                            if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                            {
                                pdfViewerControl1.Clear();
                                axAcroPDF1.LoadFile(null);
                               
                                float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                Value_Txt = dtComments1.Rows[i]["Value"].ToString();
                                Controls_Enable();
                                iTextSharp.text.Rectangle pagesize;
                                pagesize = reader1.GetPageSize(PageNo1);
                                //  int current_page = pdfViewerControl1.CurrentPageIndex;
                                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                highlight.Color = BaseColor.YELLOW;
                                stamper.AddAnnotation(highlight, PageNo1);
                            }
                        }
                    }
                    fs.Close();
                }
                if (rb_Selectionmode.Checked == true)
                {
                    pdfViewerControl1.InputFile = highLightFile;
                    if (PageNo1 != 0)
                    {
                        pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;
                    }
                }
                else
                {
                  
                    axAcroPDF1.LoadFile(highLightFile);
                 
                    if (PageNo1 != 0)
                    {
                       axAcroPDF1.setCurrentPage(PageNo1);
                    }
                }
               // pdfViewerControl1.Visible = false;
              
               
                
              

            }
            lbl_Value.Text = tvwRightSide.SelectedNode.Text +" :";
        }
        private void tvwRightSide_KeyDown(object sender, KeyEventArgs e)
        
        {

             Right = pdfViewerControl1.SelectionRectangleInPoints.Right;
        Left = pdfViewerControl1.SelectionRectangleInPoints.Left;
        Bottom = pdfViewerControl1.SelectionRectangleInPoints.Bottom;
        Top = pdfViewerControl1.SelectionRectangleInPoints.Top;
        int PageNo = pdfViewerControl1.CurrentPageIndex;
        if (ddl_Marker.Text == "Deed")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Deed_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                MessageBox.Show("Insert success");
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                }
               
            }
            AddParent_Deed();
        }




        else  if (ddl_Marker.Text == "Assessment")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Assessment_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                MessageBox.Show("Insert success");
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                }
              
            }

            AddParent_Deed();
        }



        else  if (ddl_Marker.Text == "Mortgage")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htCount = new Hashtable();
                DataTable dtCount = new System.Data.DataTable();
                htCount.Add("@Trans", "BIND");
                htCount.Add("@Order_Id", Orderid);
                dtCount = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htCount);

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");
                int Mortgage_Count = dtCount.Rows.Count + 1;
                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Mortgage_Number", "Mortgage" + Mortgage_Count);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);
              
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                MessageBox.Show("Insert success");
               
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Name);

                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);
                   
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                }
            }
            AddParent_Deed();
        }


        else if (ddl_Marker.Text == "Total Tax")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Total_Tax_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", htComments);

            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                MessageBox.Show("Insert success");

            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Name);

                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", htComments);

                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                }
            }
            AddParent_Deed();
        }




        else if (ddl_Marker.Text == "Judgment")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htCount = new Hashtable();
                DataTable dtCount = new System.Data.DataTable();
                htCount.Add("@Trans", "BIND");
                htCount.Add("@Order_Id", Orderid);
                dtCount = dataaccess.ExecuteSP("Sp_Marker_Judgment", htCount);
                int Judgment_Count = dtCount.Rows.Count+1;
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Judgment_Number", "Judgment" + Judgment_Count);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                MessageBox.Show("Insert success");
               
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                }
            }
            AddParent_Deed();
        }

        else if (ddl_Marker.Text == "Judgment Sub Document")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Sub_Document_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", htComments);

            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                MessageBox.Show("Insert success");

            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                }
            }
            AddParent_Deed();
        }


        else if (ddl_Marker.Text == "Tax")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Tax_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
             
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                MessageBox.Show("Insert success");
                
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                }
            }
            AddParent_Deed();
        }


        else if (ddl_Marker.Text == "Legal Description")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Legal_Description_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htComments);
               
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);
                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                MessageBox.Show("Insert success");
               
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                }
            }
            AddParent_Deed();
        }



        else if (ddl_Marker.Text == "Order Information")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Order_Information_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information", htComments);
            
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);
                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                MessageBox.Show("Insert success");
               
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                }
            }
            AddParent_Deed();
        }


        else if (ddl_Marker.Text == "Mortgage Sub Document")
        {
            if (e.KeyCode == Keys.Insert)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Additional_Information_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
               
            }
            if (e.KeyCode == (Keys.ShiftKey))
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");

                htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Name);
                htComments.Add("@Value", Value_Txt);
                htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                htComments.Add("@Right", Right);
                htComments.Add("@Top", Top);
                htComments.Add("@Left", Left);

                htComments.Add("@Bottom", Bottom);
                htComments.Add("@PageNo", PageNo);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                MessageBox.Show("Insert success");
               
            }
            if (e.KeyCode == (Keys.Delete))
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                }
            }
            AddParent_Deed();
        }
        
        if (e.KeyCode == (Keys.Enter))
        {
         
            bool isNum;
            if (tvwRightSide.SelectedNode != null)
            {
                Select_Node = tvwRightSide.SelectedNode.Text;
            }
            int nodevalue = 0;
            Value_Txt = "";
            if (tvwRightSide.SelectedNode.Parent != null)
            {
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                }
            }
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();

            //DateTime date = new DateTime();
            //date = DateTime.Now;
            //string dateeval = date.ToString("dd/MM/yyyy");
            //string time = date.ToString("hh:mm tt");
            if (ddl_Marker.Text == "Deed")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Deed_Id", nodevalue);
                htComments1.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Assessment")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Assessment_Id", nodevalue);
                htComments1.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Mortgage")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Mortgage_Id", nodevalue);
                htComments1.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Total Tax")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Total_Tax_Id", nodevalue);
                htComments1.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Judgment")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Judgment_Id", nodevalue);
                htComments1.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments1);
            }
            else if (ddl_Marker.Text == "Judgment Sub Document")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Sub_Document_Id", nodevalue);
                htComments1.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Tax")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Tax_Id", nodevalue);
                htComments1.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Legal Description")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Legal_Description_Id", nodevalue);
                htComments1.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Order Information")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Order_Information_Id", nodevalue);
                htComments1.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments1);
            }

            else if (ddl_Marker.Text == "Mortgage Sub Document")
            {
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Marker_Additional_Information_Id", nodevalue);
                htComments1.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments1);
            }
            
            int PageNo1 = 0; 
            string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
            iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
            pdfViewerControl1.InputFile = null;
            if (dtComments1 != null)
            {
                if (dtComments1.Rows.Count > pdf_foc)
                {
                using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                {

                    using (PdfStamper stamper = new PdfStamper(reader1, fs))
                    {
                       
                            if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                            {
                                pdfViewerControl1.Clear();
                                float Left1 = float.Parse(dtComments1.Rows[pdf_foc]["Left"].ToString());
                                float Right1 = float.Parse(dtComments1.Rows[pdf_foc]["Right"].ToString());
                                float Top1 = float.Parse(dtComments1.Rows[pdf_foc]["Top"].ToString());
                                float Bottom1 = float.Parse(dtComments1.Rows[pdf_foc]["Bottom"].ToString());
                                PageNo1 = int.Parse(dtComments1.Rows[pdf_foc]["PageNo"].ToString()) + 1;
                                Value_Txt = dtComments1.Rows[pdf_foc]["Value"].ToString();

                                iTextSharp.text.Rectangle pagesize;
                                pagesize = reader1.GetPageSize(PageNo1);
                                //  int current_page = pdfViewerControl1.CurrentPageIndex;
                                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                highlight.Color = BaseColor.GREEN;
                                stamper.AddAnnotation(highlight, PageNo1);
                              
                            }
                            pdf_foc = pdf_foc + 1;
                        }
                       
                    }
                }
                 else
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;
                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                pdfViewerControl1.InputFile = null;
                               // reader1.Close();
                                pdf_foc = 0;
                               // tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                               
                            }
                            
                        }
                pdfViewerControl1.InputFile = null;
                pdfViewerControl1.InputFile = highLightFile;
                if (PageNo1 != 0)
                {
                    pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;
                }


                lbl_Value.Text = tvwRightSide.SelectedNode.Text + " :";
            }
              
        }
       
        }
        private void MarkerMaker_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void pdfViewerControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.Enter))
            {
                TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;
                if (tvwRightSide.SelectedNode.NextNode != null)
                {
                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                    tvwRightSide.SelectedNode = Selecttreenode;
                }
            }
        }
        private void btn_tr_Deed_Click(object sender, EventArgs e)
        {
        }
        private void pdfViewerControl1_Load(object sender, EventArgs e)
        {
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        
        private void str_Value_KeyDown(object sender, KeyEventArgs e)
        {
            Value_Txt = str_Value.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (str_Value.Text != "")
                // Ctrl-S Save
                {
                    tvwRightSide.SelectedNode.BackColor = Color.White;

                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "UPDATE");
                    htComments.Add("@Value", Value_Txt);
                    if (ddl_Marker.Text == "Deed")
                    {
                        htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                        
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Deed_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Assessment")
                    {
                        htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Assessment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Mortgage")
                    {
                        htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Mortgage_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Total Tax")
                    {
                        htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Total_Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Judgment")
                    {
                        htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Judgment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Judgment Sub Document")
                    {
                        htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Sub_Document_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }


                    else if (ddl_Marker.Text == "Tax")
                    {
                        htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Legal Description")
                    {
                        htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Legal_Description_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Order Information")
                    {
                        htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Order_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Mortgage Sub Document")
                    {
                        htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Additional_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }

                }


                else
                {
                    MessageBox.Show("Enter Proper Data");
                    str_Value.Select(0, 0);
                    str_Value.Focus();
                    str_Value.BackColor = System.Drawing.Color.AliceBlue;
                }
            }
            
        }

        private void cbo_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbo_Value.Text != "")
            {
                Value_Txt = cbo_Value.SelectedValue.ToString();
            }
           
            if (e.KeyCode == Keys.Enter)
            {
                if (cbo_Value.Text != "")
                // Ctrl-S Save
                {
                    tvwRightSide.SelectedNode.BackColor = Color.White;

                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "UPDATE");
                    htComments.Add("@Value", Value_Txt);
                    if (ddl_Marker.Text == "Deed")
                    {
                        htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);

                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Deed_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Assessment")
                    {
                        htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Assessment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Mortgage")
                    {
                        htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Mortgage_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Total Tax")
                    {
                        htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Total_Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Judgment")
                    {
                        htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Judgment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }


                    else if (ddl_Marker.Text == "Judgment Sub Document")
                    {
                        htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Sub_Document_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }


                    else if (ddl_Marker.Text == "Tax")
                    {
                        htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Legal Description")
                    {
                        htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Legal_Description_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Order Information")
                    {
                        htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Order_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Mortgage Sub Document")
                    {
                        htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Additional_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }

                }


                else
                {
                    MessageBox.Show("Enter Proper Data");
                    str_Value.Select(0, 0);
                    str_Value.Focus();
                    str_Value.BackColor = System.Drawing.Color.AliceBlue;
                }
            }

        }
     
        private void txt_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txt_Value_KeyDown(object sender, KeyEventArgs e)
        {

            Value_Txt = txt_Value.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_Value.Text != "")// Ctrl-S Save
                {
                    tvwRightSide.SelectedNode.BackColor = Color.White;

                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "UPDATE");
                    htComments.Add("@Value", Value_Txt);
                    if (ddl_Marker.Text == "Deed")
                    {
                        htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Deed_Information"].ToString() != tvwRightSide.SelectedNode.Text)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Assessment")
                    {
                        htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Assessment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Mortgage")
                    {
                        htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Mortgage_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }



                    else if (ddl_Marker.Text == "Total Tax")
                    {
                        htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Total_Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }




                    else if (ddl_Marker.Text == "Judgment")
                    {
                        htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Judgment_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }


                    else if (ddl_Marker.Text == "Judgment Sub Document")
                    {
                        htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Sub_Document_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }


                    else if (ddl_Marker.Text == "Tax")
                    {
                        htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Tax_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Legal Description")
                    {
                        htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Legal_Description_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Order Information")
                    {
                        htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Order_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }
                    else if (ddl_Marker.Text == "Mortgage Sub Document")
                    {
                        htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                        htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                        for (int i = 0; i < dtComments.Rows.Count; i++)
                        {
                            TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                            if (tvwRightSide.SelectedNode.NextNode != null)
                            {
                                if (dtComments.Rows[i]["Additional_Information_Information"] != tvwRightSide.SelectedNode)
                                {
                                    tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                    tvwRightSide.SelectedNode = Selecttreenode;
                                }
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Enter Proper Data");
                    txt_Value.Select(0, 0);
                    txt_Value.Focus();
                    txt_Value.BackColor = System.Drawing.Color.AliceBlue;
                }
            }
           
        }
        private void dtp_Value_KeyDown(object sender, KeyEventArgs e)
        {
            Value_Txt = dtp_Value.Text;
            if (e.KeyCode == Keys.Enter)       // Ctrl-S Save
            {
                tvwRightSide.SelectedNode.BackColor = Color.White;

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();
                htComments.Add("@Trans", "UPDATE");
                htComments.Add("@Value", Value_Txt);
                if (ddl_Marker.Text == "Deed")
                {
                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Deed_Information"]!= tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }



                else if (ddl_Marker.Text == "Assessment")
                {
                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Assessment_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }




                else if (ddl_Marker.Text == "Mortgage")
                {
                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Mortgage_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }



                else if (ddl_Marker.Text == "Total Tax")
                {
                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Total_Tax_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }




                else if (ddl_Marker.Text == "Judgment")
                {
                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Judgment_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }

                else if (ddl_Marker.Text == "Judgment Sub Document")
                {
                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Sub_Document_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }


                else if (ddl_Marker.Text == "Tax")
                {
                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Tax_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }
                else if (ddl_Marker.Text == "Legal Description")
                {
                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Legal_Description_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }
                else if (ddl_Marker.Text == "Order Information")
                {
                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Order_Information_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }
                else if (ddl_Marker.Text == "Mortgage Sub Document")
                {
                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                    for (int i = 0; i < dtComments.Rows.Count; i++)
                    {
                        TreeNode Selecttreenode = tvwRightSide.SelectedNode.NextNode;


                        if (tvwRightSide.SelectedNode.NextNode != null)
                        {
                            if (dtComments.Rows[i]["Additional_Information_Information"] != tvwRightSide.SelectedNode)
                            {
                                tvwRightSide.SelectedNode.NextNode.BackColor = Color.LightBlue;
                                tvwRightSide.SelectedNode = Selecttreenode;
                            }
                        }
                    }
                }

            }

        }
        private void pdfViewerControl1_SelectionRectangleChanged(object sender, EventArgs e)
        {
            Right = pdfViewerControl1.SelectionRectangleInPoints.Right;
            Left = pdfViewerControl1.SelectionRectangleInPoints.Left;
            Bottom = pdfViewerControl1.SelectionRectangleInPoints.Bottom;
            Top = pdfViewerControl1.SelectionRectangleInPoints.Top;
            int PageNo = pdfViewerControl1.CurrentPageIndex;
            if (ddl_Marker.Text == "Deed")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);
                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Deed_Id", nodevalue);
                        htComments1.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = Pdf_Path;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();

                                            iTextSharp.text.Rectangle pagesize;
                                           pagesize= reader1.GetPageSize(PageNo1);
                                           
                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                         
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);
                                            
                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1-1;


                        }
                    }
                }
            }

          else  if (ddl_Marker.Text == "Mortgage")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Mortgage_Id", nodevalue);
                        htComments1.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }


            else if (ddl_Marker.Text == "Total Tax")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Total_Tax_Id", nodevalue);
                        htComments1.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory + "\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }




            else if (ddl_Marker.Text == "Assessment")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Assessment_Id", nodevalue);
                        htComments1.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory + "\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }


            else if (ddl_Marker.Text == "Judgment")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Judgment_Id", nodevalue);
                        htComments1.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }

            else if (ddl_Marker.Text == "Judgment Sub Document")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Sub_Document_Id", nodevalue);
                        htComments1.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory + "\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }


            else if (ddl_Marker.Text == "Tax")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Tax_Id", nodevalue);
                        htComments1.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();



                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);
                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }


            else if (ddl_Marker.Text == "Legal Description")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);
                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");
                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Legal_Description_Id", nodevalue);
                        htComments1.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }


            else if (ddl_Marker.Text == "Order Information")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Order_Information_Id", nodevalue);
                        htComments1.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();


                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);

                                            //  int current_page = pdfViewerControl1.CurrentPageIndex;


                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);

                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);




                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;


                        }
                    }
                }
            }



            else if (ddl_Marker.Text == "Mortgage Sub Document")
            {
                if (Right != 0.0 && Left != 0.0 && Bottom != 0.0 && Top != 0.0)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    htComments.Add("@Trans", "INSERT");

                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Value", Value_Txt);
                    htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                    htComments.Add("@Right", Right);
                    htComments.Add("@Top", Top);
                    htComments.Add("@Left", Left);

                    htComments.Add("@Bottom", Bottom);
                    htComments.Add("@PageNo", PageNo);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                    //    MessageBox.Show("Insert success");

                    if (tvwRightSide.SelectedNode != null)
                    {
                        bool isNum;
                        Select_Node = tvwRightSide.SelectedNode.Text;
                        int nodevalue = 0;
                        if (Select_Node != "")
                        {
                            isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                        }
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new System.Data.DataTable();

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");
                        //string time = date.ToString("hh:mm tt");

                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Additional_Information_Id", nodevalue);
                        htComments1.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments1);
                        int PageNo1 = 0;
                        string highLightFile = Environment.CurrentDirectory +"\\MarkerMaker1.pdf";
                        iTextSharp.text.pdf.PdfReader reader1 = new PdfReader(Pdf_Path);
                        pdfViewerControl1.InputFile = null;
                        if (dtComments1 != null)
                        {
                            using (FileStream fs = new FileStream(highLightFile, FileMode.Open, FileAccess.Write, FileShare.None))
                            {

                                using (PdfStamper stamper = new PdfStamper(reader1, fs))
                                {
                                    for (int i = 0; i < dtComments1.Rows.Count; i++)
                                    {
                                        if (dtComments1.Rows.Count > 0 && Pdf_Path != null)
                                        {
                                            pdfViewerControl1.Clear();
                                            float Left1 = float.Parse(dtComments1.Rows[i]["Left"].ToString());
                                            float Right1 = float.Parse(dtComments1.Rows[i]["Right"].ToString());
                                            float Top1 = float.Parse(dtComments1.Rows[i]["Top"].ToString());
                                            float Bottom1 = float.Parse(dtComments1.Rows[i]["Bottom"].ToString());
                                            PageNo1 = int.Parse(dtComments1.Rows[i]["PageNo"].ToString()) + 1;
                                            Value_Txt = dtComments1.Rows[i]["Value"].ToString();
                                            iTextSharp.text.Rectangle pagesize;
                                            pagesize = reader1.GetPageSize(PageNo1);
                                            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, 0, 0, 0);
                                            float[] quad = { Left1, pagesize.Height - Top1, Right1, pagesize.Height - Top1, Left1, pagesize.Height - Bottom1, Right1, pagesize.Height - Bottom1 };
                                            PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, rect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);
                                            iTextSharp.text.pdf.PdfAnnotation annotNote = iTextSharp.text.pdf.PdfAnnotation.CreateStamp(stamper.Writer, rect, "ffsdg", "dgdhgd");
                                            highlight.Color = BaseColor.GREEN;
                                            stamper.AddAnnotation(highlight, PageNo1);
                                           
                                        }
                                    }
                                }
                            }
                            pdfViewerControl1.InputFile = null;
                            pdfViewerControl1.InputFile = highLightFile;
                            pdfViewerControl1.CurrentPageIndex = PageNo1 - 1;
                        }
                    }
                }
            }
        }
        private void pdfViewerControl1_DragLeave(object sender, EventArgs e)
        {
        }
        private void pdfViewerControl1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void pdfViewerControl1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pdfViewerControl1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pdfViewerControl1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void ddl_Marker_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void txt_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void rb_Selectionmode_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Selectionmode.Checked == true)
            {
                pdfViewerControl1.Visible = true;
                axAcroPDF1.Visible = false;
            }
            else
            {
                pdfViewerControl1.Visible = false;
                axAcroPDF1.Visible = true;
            }
        }

        private void rb_View_Mode_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_View_Mode.Checked == true)
            {
                pdfViewerControl1.Visible = false;
                axAcroPDF1.Visible = true;
                axAcroPDF1.LoadFile(Environment.CurrentDirectory + "\\MarkerMaker1.pdf");
            }
            else
            {
                pdfViewerControl1.Visible = true;
                axAcroPDF1.Visible = false;
                pdfViewerControl1.InputFile = Environment.CurrentDirectory + "\\MarkerMaker1.pdf";
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            //Ordermanagement_01.Templete.Templete_View_Package Temp_Viewer = new Ordermanagement_01.Templete.Templete_View_Package(Orderid);
            //Temp_Viewer.Show();
        }

        private void btn_Add_Node_Click(object sender, EventArgs e)
        {
             if (tvwRightSide.SelectedNode.Text == "Deed")
        {
            
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Deed_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
          }

             else if (tvwRightSide.SelectedNode.Text == "Assessment")
            {           
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Assessment_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
            }
             else if (tvwRightSide.SelectedNode.Text == "Mortgage")
        {
            
                Hashtable htCount = new Hashtable();
                DataTable dtCount = new System.Data.DataTable();
                htCount.Add("@Trans", "BIND");
                htCount.Add("@Order_Id", Orderid);
                dtCount = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htCount);

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");
                int Mortgage_Count = dtCount.Rows.Count + 1;
                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Mortgage_Number", "Mortgage" + Mortgage_Count);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);
              
            }
             else if (tvwRightSide.SelectedNode.Text == "Total Tax")
           {
           
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Total_Tax_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", htComments);

            }

             else if (tvwRightSide.SelectedNode.Text == "Judgment")
        {
                Hashtable htCount = new Hashtable();
                DataTable dtCount = new System.Data.DataTable();
                htCount.Add("@Trans", "BIND");
                htCount.Add("@Order_Id", Orderid);
                dtCount = dataaccess.ExecuteSP("Sp_Marker_Judgment", htCount);
                int Judgment_Count = dtCount.Rows.Count+1;
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Judgment_Number", "Judgment" + Judgment_Count);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);
                dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
            }
             else if (tvwRightSide.SelectedNode.Text == "Judgment Sub Document")
        {
            
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Sub_Document_Number", tvwRightSide.SelectedNode.Parent.Name);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", htComments);

            }


             else if (tvwRightSide.SelectedNode.Text == "Tax")
        {
            
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Tax_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
             
            }
             else if (tvwRightSide.SelectedNode.Text == "Legal Description")
        {
           
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Legal_Description_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htComments);
               
            }

             else if (tvwRightSide.SelectedNode.Text == "Order Information")
        {
           
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Order_Information_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information", htComments);
            
            }
             else if (tvwRightSide.SelectedNode.Text == "Mortgage Sub Document")
            {
           
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Additional_Information_Number", deedValue);
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", Select_Node);

                dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
               
            }
             AddParent_Deed();

        }

        private void btn_Delete_Node_Click(object sender, EventArgs e)
        {
            if (ddl_Marker.Text == "Deed")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Deed_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Deed_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                }
             
            }

            else if (ddl_Marker.Text == "Assessment")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Assessment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Assessment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                }
              
            }
            else if (ddl_Marker.Text == "Mortgage")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Name);

                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);

                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Mortgage_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                }
            }
            else if (ddl_Marker.Text == "Total Tax")
            {

                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Name);

                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax", htComments);

                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Total_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", htComments);
                }

            }

            else if (ddl_Marker.Text == "Judgment")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Judgment_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Judgment_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                }
            }
            else if (ddl_Marker.Text == "Judgment Sub Document")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Sub_Document_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", htComments);
                }

            }


            else if (ddl_Marker.Text == "Tax")
            {

                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Tax_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Tax_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                }

            }
            else if (ddl_Marker.Text == "Legal Description")
            {

                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Legal_Description_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                }

            }

            else if (ddl_Marker.Text == "Order Information")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Order_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", htComments);
                }
              

            }
            else if (ddl_Marker.Text == "Mortgage Sub Document")
            {
                bool isNum = false;
                Select_Node = tvwRightSide.SelectedNode.Text;
                int nodevalue = 0;
                if (Select_Node != "")
                {
                    isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out nodevalue);
                }
                if (isNum == true)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
                }
                else
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    htComments.Add("@Trans", "DELETE");
                    htComments.Add("@Marker_Additional_Information_Id", tvwRightSide.SelectedNode.Parent.Name);
                    htComments.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Name);
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                }
             
            }
            AddParent_Deed();

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int nodevalue = 0;
            if (tvwRightSide.SelectedNode.Parent != null)
            {
                if (Select_Node != "")
                {
                  bool  isNum = Int32.TryParse(tvwRightSide.SelectedNode.Parent.Name, out nodevalue);
                }
            }
            Hashtable ht_Delete = new Hashtable();
            DataTable dt_Delete = new DataTable();
            if (ddl_Marker.Text == "Deed")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Deed_Id", nodevalue);
                ht_Delete.Add("@Deed_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", ht_Delete);
            }
            else if (ddl_Marker.Text == "Mortgage")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Mortgage_Id", nodevalue);
                ht_Delete.Add("@Mortgage_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", ht_Delete);
            }


            else if (ddl_Marker.Text == "Total Tax")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Total_Tax_Id", nodevalue);
                ht_Delete.Add("@Total_Tax_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Total_Tax_Child", ht_Delete);
            }


            else if (ddl_Marker.Text == "Judgment")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Judgment_Id", nodevalue);
                ht_Delete.Add("@Judgment_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", ht_Delete);
            }

            else if (ddl_Marker.Text == "Judgment Sub Document")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Sub_Document_Id", nodevalue);
                ht_Delete.Add("@Sub_Document_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Sub_Document_Child", ht_Delete);
            }

            else if (ddl_Marker.Text == "Tax")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Tax_Id", nodevalue);
                ht_Delete.Add("@Tax_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", ht_Delete);
            }

            else if (ddl_Marker.Text == "Legal Description")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Legal_Description_Id", nodevalue);
                ht_Delete.Add("@Legal_Description_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", ht_Delete);
            }

            else if (ddl_Marker.Text == "Order Information")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Order_Information_Id", nodevalue);
                ht_Delete.Add("@Order_Information_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Order_Information_Child", ht_Delete);
            }
            else if (ddl_Marker.Text == "Assessment Information")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Assessment_Information_Id", nodevalue);
                ht_Delete.Add("@Assessment_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Assessment_Information_Child", ht_Delete);
            }
            else if (ddl_Marker.Text == "Mortgage Sub Document")
            {
                ht_Delete.Add("@Trans", "DELETE");
                ht_Delete.Add("@Marker_Additional_Information_Id", nodevalue);
                ht_Delete.Add("@Additional_Information_Information", tvwRightSide.SelectedNode.Text);
                dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", ht_Delete);
            }
        }

    
    }
}
