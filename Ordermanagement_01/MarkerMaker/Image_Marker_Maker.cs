using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Globalization;


namespace Ordermanagement_01.MarkerMaker
{
    
    public partial class Image_Marker_Maker : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        InfiniteProgressBar.clsProgress probar = new InfiniteProgressBar.clsProgress();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        Hashtable ht_Select = new Hashtable();
        DataTable dt_Select = new System.Data.DataTable();
        Image Zoom_Image;
        object MarkerId;
        Timer t = new Timer();
        DateTimePicker oDateTimePicker = new DateTimePicker();
        DataGridViewComboBoxColumn ddl_DeedType = new DataGridViewComboBoxColumn();
        Control cnt;
        private RectangleF _SelectItem;
        private RectangleF _dragItem;
        int Tab_Change = 0,Colr=0,Cell_Chang=0,Last_PageNo;
        int Cur_Row ;
        int Cur_Col;
        string Order_NO,Marker_Deed_Id, Information, Tab_Page, Pdf_Path, Pdf_Name, Marker_Mortgage_Id, Marker_Tax_Id, Marker_Judgement_Id, Marker_Assessment_Id, Marker_Additional_Information_Id, Marker_Legal_Description_Id;
       pdftoimg.PDFConvertor pdf;
       byte[] bimage;
       Graphics sample;
       int Order_Status, PageNo, Page_Count, Cell_Chang_Eve = 0, Pre_Row_Index, Pre_Col_Index, Col_Index, Row_Index,mul_Select=0;
       int Orderid, Cell_Date_Check;
       int X, Y, Width, Height,Status=0,Selection_Id,Esc,Next_node;
       string image,Value_Data;



       string Client, Subprocess;
       int Client_id;
       



       public Image_Marker_Maker(int Order_Id, int OrderStatus, string OrderNO,string Mar_Mak,string Client_name,string Subprocess_name,int client_id)
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in Gv_Deed.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in Gv_mortgage.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in Gv_Judgement.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in Gv_Tax.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in Gv_Assessment.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in Gv_Additional_Information.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }






            Mortgage_Dec();
            Orderid = Order_Id;
            Order_Status = OrderStatus;
            Order_NO = OrderNO;
            Client = Client_name;
            Subprocess = Subprocess_name;
            Client_id = client_id;
            if (Mar_Mak == "Typing")
            {
                Status = 0;
                Gv_Deed.ReadOnly = true;
                Gv_mortgage.ReadOnly = true;
                Gv_Tax.ReadOnly = true;
                Gv_Assessment.ReadOnly = true;
                Gv_Additional_Information.ReadOnly = true;
                Gv_Judgement.ReadOnly = true;
                Txt_Legal_Description.ReadOnly = true;
            }
            else
            {
                Status = 1;
                Gv_Deed.ReadOnly = false;
                Gv_mortgage.ReadOnly = false;
                Gv_Tax.ReadOnly = false;
                Gv_Assessment.ReadOnly = false;
                Gv_Additional_Information.ReadOnly = false;
                Gv_Judgement.ReadOnly = false;
                Txt_Legal_Description.ReadOnly = false;
               
            }
           // Status = 1;
            //int m_i2 = Gv_mortgage.Columns[2].Width, m_i3 = Gv_mortgage.Columns[3].Width, m_i4 = Gv_mortgage.Columns[4].Width;
            //int m_i5 = Gv_mortgage.Columns[5].Width, m_i6 = Gv_mortgage.Columns[6].Width, m_i7 = Gv_mortgage.Columns[7].Width;
            //int m_i8 = Gv_mortgage.Columns[8].Width, m_i9 = Gv_mortgage.Columns[9].Width, m_i10 = Gv_mortgage.Columns[10].Width;
            //int m_i11 = Gv_mortgage.Columns[11].Width, m_i12 = Gv_mortgage.Columns[12].Width, m_i13 = Gv_mortgage.Columns[13].Width;
            //int m_i14 = Gv_mortgage.Columns[14].Width, m_i15 = Gv_mortgage.Columns[15].Width, m_i16 = Gv_mortgage.Columns[16].Width;
            //int m_i17 = Gv_mortgage.Columns[17].Width, m_i18 = Gv_mortgage.Columns[18].Width, m_i19 = Gv_mortgage.Columns[19].Width;
        }
        private void Mortgage_Dec()
        {
           
        }
        private void Conver_to_Image(string Path_Pdf)
        {
            Array.ForEach(Directory.GetFiles(Environment.CurrentDirectory + @"\Pdf_Image\"), File.Delete);
            ImageFormat imageFormat = new ImageFormat(Guid.Empty);
            switch ("Jpeg")
            {
                case "Jpeg": imageFormat = ImageFormat.Jpeg; break;
                case "Bmp": imageFormat = ImageFormat.Bmp; break;
                case "Png": imageFormat = ImageFormat.Png; break;
                case "Gif": imageFormat = ImageFormat.Gif; break;
            }
           
            pdf = new pdftoimg.PDFConvertor();
           // pdf.ExportProgressChanging += new ProgressChangingEventHandler(p_ExportProgressChanging);

           // progressBar1.Visible = true;
            int filescount = pdf.Convert(Path_Pdf, Environment.CurrentDirectory + @"\Pdf_Image", imageFormat);
           // progressBar1.Visible = false;
           // progressBar1.Value = 0;
         //   this.Text = filescount + " Items Exported!";
           // lblCurrentFileName.Text = "";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Environment.CurrentDirectory + @"\Pdf_Image\");
            Page_Count = dir.GetFiles().Length;
           // System.Diagnostics.Process.Start(txtOutPut.Text);
        }

        private void Image_Marker_Maker_Load(object sender, EventArgs e)
        {
            //Progress bar
            probar.startProgress();


           // imageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Stretch;
            PageNo = 1;
            imageBox.Zoom =200;
          
            Pdf_View();
            
            Image_View();
            Deed_Grid_Bind();
            Mortgage_Grid_Bind();
            Tax_Grid_Bind();
            Judgement_Grid_Bind();
            Additional_Information_Grid_Bind();
            Assessment_Grid_Bind();
            toolStripTextBox1.Text = (PageNo + 1).ToString(); 
            Cell_Chang_Eve = 1;
            Clear_Rect();
            if (Status == 1)
            {
                //Btn_Deed.Visible = false;
                //btn_Mortgage.Visible = false;
                //btn_Tax.Visible = false;
                //btn_Judgement.Visible = false;
                //btn_Assessment.Visible = false;
                //btn_Additional_Information.Visible = false;
               // ddl_ClientSelect.Visible = true;

                //Client Subprocess label and dropdowns
                //lbl_Client.Visible = true;
               // lbl_Subprocess.Visible = true;
               // ddl_ClientSelect.Text = "Client_Number";
               // dbc.BindClientNo(ddl_ClientSelect);
                //ddl_ClientSelect.SelectedText = Client;
               // ddl_Subprocess.Visible = true;
                
               // ddl_Subprocess.SelectedText = Subprocess;
             //   dbc.BindSubProcessNumber(ddl_Subprocess, int.Parse(ddl_ClientSelect.SelectedValue.ToString()));
            }

           
           this.Gv_Deed.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_Deed_EditingControlShowing);
            this.Gv_Deed.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_Deed_CellClick);

            this.Gv_mortgage.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_mortgage_EditingControlShowing);
            this.Gv_mortgage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_mortgage_CellClick);

            this.Gv_Tax.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_Tax_EditingControlShowing);
            this.Gv_Tax.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_Tax_CellClick);

            this.Gv_Judgement.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_Judgement_EditingControlShowing);
            this.Gv_Judgement.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_Judgement_CellClick);

            this.Gv_Assessment.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_Assessment_EditingControlShowing);
            this.Gv_Assessment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_Assessment_CellClick);

            this.Gv_Additional_Information.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Gv_Additional_Information_EditingControlShowing);
            this.Gv_Additional_Information.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gv_Additional_Information_CellClick);

            probar.stopProgress();
        }
      
        private void Gv_Deed_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;
            cnt.TextChanged += tb_TextChanged;
        }

        private void Gv_mortgage_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;

            cnt.TextChanged += tb_TextChanged;
        }

        private void Gv_Tax_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;

            cnt.TextChanged += tb_TextChanged;
        }

        private void Gv_Assessment_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            //if (Gv_Assessment.CurrentCell.ColumnIndex == 2 || Gv_Assessment.CurrentCell.ColumnIndex == 3 || Gv_Assessment.CurrentCell.ColumnIndex == 4) //Desired Column
            //{
            //    TextBox tb = e.Control as TextBox;
            //    if (tb != null)
            //    {
            //        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            //    }
            // }
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;

            cnt.TextChanged += tb_TextChanged;
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }
        private void Gv_Additional_Information_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;

            cnt.TextChanged += tb_TextChanged;
        }

        private void Gv_Judgement_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(tb_TextChanged);
            cnt = e.Control;

            cnt.TextChanged += tb_TextChanged;
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if (Tab_Page == "Deed")
            {
                if (cnt.Text != string.Empty)
                {
                    int chars = cnt.Text.Length;
                    Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].Width = 128 + (6 * chars);
                    int cellwidth = 128 + (6 * chars);

                    Gv_Deed.FirstDisplayedScrollingColumnIndex = Gv_Deed.CurrentCell.ColumnIndex;
                    //  txt_Deed.Text = cnt.Text;


                    //Hashtable htComments = new Hashtable();
                    //DataTable dtComments = new DataTable();

                    //htComments.Add("@Trans", "UPDATE");
                    //htComments.Add("@Marker_Deed_Id", Gv_Deed.CurrentRow.Cells[0].Value.ToString());
                    //htComments.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                    //htComments.Add("@Value", cnt.Text);
                    //dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                }
                
            }
            else if (Tab_Page == "Mortgage")
            {
                if (cnt.Text != string.Empty)
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new System.Data.DataTable();
                    int chars = cnt.Text.Length;
                    Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].Width = 128 + (6 * chars);
   
                    int cellwidth = 128 + (6 * chars);
                    Gv_mortgage.FirstDisplayedScrollingColumnIndex = Gv_mortgage.CurrentCell.ColumnIndex;

                }
            }

            else if (Tab_Page == "Tax")
            {
                if (cnt.Text != string.Empty)
                {
                    int chars = cnt.Text.Length;

                    Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].Width = 128 + (6 * chars);
                    int cellwidth = 128 + (6 * chars);
                    Gv_Tax.FirstDisplayedScrollingColumnIndex = Gv_Tax.CurrentCell.ColumnIndex;
                    //Hashtable htComments = new Hashtable();
                    //DataTable dtComments = new DataTable();

                    //htComments.Add("@Trans", "UPDATE");
                    //htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                    //htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                    //htComments.Add("@Value", cnt.Text);
                    //dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                }
            }
            else if (Tab_Page == "Assessment")
            {

                if (cnt.Text != string.Empty)
                {
    //                if (Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[2].Value != "" && Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[3].Value != "")
    //                {
    //                    string z = Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[2].Value.ToString();
    //                    int value_z;
    //                    string m = Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[3].Value.ToString();
    //                    int value_m;
    //                    if (int.TryParse(z, out value_z) && int.TryParse(m, out value_m))
    //                    {

    ////                        Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[4].Value = int.Parse(Gv_Assessment.Rows

    ////[Gv_Assessment.CurrentRow.Index].Cells[2].Value.ToString()) + int.Parse(Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[3].Value.ToString());
    //                    }
    //                }
                    int chars = cnt.Text.Length;

                    Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].Width = 128 + (6 * chars);
                    int cellwidth = 128 + (6 * chars);
                    Gv_Assessment.FirstDisplayedScrollingColumnIndex = Gv_Assessment.CurrentCell.ColumnIndex;


                }
            }
            else if (Tab_Page == "Additional Information")
            {
                if (cnt.Text != string.Empty)
                {
                    int chars = cnt.Text.Length;

                    Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].Width = 128 + (6* chars);
                    int cellwidth = 128 + (6 * chars);
                    Gv_Additional_Information.FirstDisplayedScrollingColumnIndex = Gv_Additional_Information.CurrentCell.ColumnIndex;

                }
            }
            else if (Tab_Page == "Judgement")
            {
                if (cnt.Text != string.Empty)
                {
                    int chars = cnt.Text.Length;

                    Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].Width = 128 + (6 * chars);
                    int cellwidth = 128 + (6 * chars);
                    Gv_Judgement.FirstDisplayedScrollingColumnIndex = Gv_Judgement.CurrentCell.ColumnIndex;
                    
                }
            }
            Value_Data = cnt.Text;

        }

        private void Clear_Rect()
        {

            imageBox.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
            imageBox.SelectionColor = Color.Yellow;
        }

        private void Image_View()
        {
            if (PageNo < Page_Count)
            {
                 image = Environment.CurrentDirectory + @"\Pdf_Image\" + Pdf_Name + "_" + PageNo + ".jpeg";
                 Bitmap bmp = new Bitmap(image);
                 FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                 Tab_Page = Tab_Typing.SelectedTab.Text.ToString();
                 bimage = new byte[fs.Length];
                 fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                 fs.Close();
                imageBox.Image = GetDataToImage((byte[])bimage);
                
                //  imageBox.Im
                // propertyGrid.SelectedGridItem = propertyGrid.FindItem("SelectionMode");
                this.UpdateStatusBar();
               
            }
        }
        private void Pdf_View()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "PACKAGE_VALIDATE");
            ht.Add("@Order_Id", Orderid);
            dt = dataaccess.ExecuteSP("Sp_Document_Upload", ht);
            if (dt.Rows.Count > 0)
            {
                Pdf_Path = dt.Rows[0]["Document_Path"].ToString();
                Pdf_Name = dt.Rows[0]["Document_Name"].ToString();
                Conver_to_Image(Pdf_Path);
                System.Diagnostics.Process.Start(dt.Rows[0]["Document_Path"].ToString());
                //File.Copy(Pdf_Path, Environment.CurrentDirectory + "\\MarkerMaker1.pdf", true);
                //pdfViewerControl1.CurrentPageIndex = 0;
                //pdfViewerControl1.InputFile = Environment.CurrentDirectory + "\\MarkerMaker1.pdf";
                //pdfViewerControl1.ScrollBarsAlwaysVisible = true;
            }
        }


      





        public Image GetDataToImage(byte[] bimage)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(bimage) as Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image not uploaded");
                return null;
            }
        }
        private void UpdateStatusBar()
        {
            //if (Esc == 1 && Status == 1)
            //{
            //    imageBox.HorizontalScroll.Value = X;
            //    imageBox.VerticalScroll.Value = (Y * 2) - Height;

            //    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
            //}
        }

        private void showImageRegionToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.Invalidate();
        }

        private void showSourceImageRegionToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.Invalidate();
        }

        private void actualSizeToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.ActualSize();
        }

        private void zoomInToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.ZoomIn();
        }

        private void zoomOutToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.ZoomOut();
        }

        private void selectAllToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.SelectAll();

            //this.UpdatePreviewImage();
        }

        private void selectNoneToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.SelectNone();
        }
        private void DrawBox(Graphics graphics, Color color, RectangleF rectangle, double scale)
        {
            float penWidth;

            penWidth = 2 * (float)scale;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, color)))
                graphics.FillRectangle(brush, rectangle);

            using (Pen pen = new Pen(color, penWidth) { DashStyle = DashStyle.Dot, DashCap = DashCap.Round })
                graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        private string FormatPoint(Point point)
        {
            return string.Format("X:{0}, Y:{1}", point.X, point.Y);
        }

        private string FormatRectangle(RectangleF rect)
        {
            return string.Format("X:{0}, Y:{1}, W:{2}, H:{3}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

     
        private void imageBox_MouseLeave(object sender, EventArgs e)
        {
            //cursorToolStripStatusLabel.Text = string.Empty;
        }

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            //if (Status == 0)
            //{
                if (dt_Select.Rows.Count > 0)
                {
                    // this.Refresh();

                    Image_View();
                    if (Status == 0)
                    {
                        for (int i = 0; i < dt_Select.Rows.Count; i++)
                        {
                            if (PageNo == int.Parse(dt_Select.Rows[i]["PageNo"].ToString()))
                            {
                                X = int.Parse(dt_Select.Rows[i]["X"].ToString());
                                Y = int.Parse(dt_Select.Rows[i]["Y"].ToString());
                                Width = int.Parse(dt_Select.Rows[i]["Width"].ToString());
                                Height = int.Parse(dt_Select.Rows[i]["Height"].ToString());
                                PageNo = int.Parse(dt_Select.Rows[i]["PageNo"].ToString());

                                if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                                {
                                    
                                    Graphics g = Graphics.FromImage(imageBox.Image);
                                    Rectangle Rect = new Rectangle(X , Y, Width, Height);
                                    if (Status == 0 && Gv_Deed.CurrentCell != null)
                                    {
                                        Gv_Deed.CurrentCell.ReadOnly = true;
                                    }
                                    //using (Pen pen = new Pen(Color.Red, 2))
                                    //{
                                    //    g.DrawRectangle(pen, Rect);
                                    //}

                                    using (Brush b = new SolidBrush(Color.FromArgb(100, Color.Yellow)))
                                    {
                                        g.FillRectangle(b, Rect);
                                    }
                                }
                            }
                        }
                    }
                    else if(Status==1)
                    {
                        for (int i = 0; i < dt_Select.Rows.Count; i++)
                        {
                            if (PageNo == int.Parse(dt_Select.Rows[i]["PageNo"].ToString()))
                            {
                                X = int.Parse(dt_Select.Rows[i]["X"].ToString());
                                Y = int.Parse(dt_Select.Rows[i]["Y"].ToString());
                                Width = int.Parse(dt_Select.Rows[i]["Width"].ToString());
                                Height = int.Parse(dt_Select.Rows[i]["Height"].ToString());
                                PageNo = int.Parse(dt_Select.Rows[i]["PageNo"].ToString());

                                if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                                {
                                    Graphics g = Graphics.FromImage(imageBox.Image);
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                   
                                    //using (Pen pen = new Pen(Color.Red, 2))
                                    //{
                                    //    g.DrawRectangle(pen, Rect);
                                    //}
                                    if (Colr == 1)
                                    {
                                        using (Brush b = new SolidBrush(Color.FromArgb(100, Color.White)))
                                        {
                                            g.FillRectangle(b, Rect);
                                        }
                                    }
                                    else if (Colr == 0)
                                    {
                                        using (Brush b = new SolidBrush(Color.FromArgb(100, Color.YellowGreen)))
                                        {
                                            g.FillRectangle(b, Rect);
                                        }
                                    }

                                }
                            }
                        }
                    }

               // }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageBox.SelectionMode =Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            imageBox.SelectionRegion = new Rectangle(228, 463, 147, 18);
       
            imageBox.SelectionColor = Color.Yellow;
            imageBox.AutoScrollPosition = new Point(228,0);
            if (imageBox.Height <imageBox.Image.Height)
            {
                int total = (imageBox.Height / 2);
                imageBox.AutoScrollPosition = new Point(X, Y + total);
            }
            //else
            //{
            //    int total = (imageBox.Image.Height-imageBox.Height);
            //    imageBox.AutoScrollPosition = new Point(X, Y - total);
            //}
        }
       

        private void Btn_Deed_Click(object sender, EventArgs e)
        {
            int Count_Value = 0;
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "BIND");
            ht.Add("@Order_Id", Orderid);
            dt = dataaccess.ExecuteSP("Sp_Marker_Deed", ht);
            Count_Value = dt.Rows.Count + 1;
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Deed_Number", Count_Value);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path", Tab_Typing.SelectedTab.Text.ToString());
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
            Deed_Grid_Bind();
        }
        private void Deed_Grid_Bind()
        {
            //Gv_Deed.DataSource = null;
            //Gv_Deed.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Deed", htComments1);

            if (dtComments1.Rows.Count > 0)
            {
                if (Status == 0)
                {
                    Gv_Deed.Columns.Clear();
                  //  Gv_Deed.Rows.Clear();
                    Gv_Deed.DataSource = dtComments1;
                    Gv_Deed.Columns[0].Visible = false;
                    lbl_Deed.Visible = true;
                    Lbl_Deed_Count.Text = dtComments1.Rows.Count.ToString();
                    if (Status == 0)
                    {
                        DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                        Gv_Deed.Columns.Add(btnDelete);
                        btnDelete.HeaderText = "Delete";
                        btnDelete.Text = "Delete";
                        btnDelete.Name = "btnDelete";

                        for (int i = 0; i < Gv_Deed.Rows.Count; i++)
                        {
                            Gv_Deed.Rows[i].Cells[Gv_Deed.Columns.Count - 1].Value = "Delete";
                            for (int j = 0; j < Gv_Deed.Columns.Count; j++)
                            {
                                if (Gv_Deed.Rows[i].Cells[j].Value == DBNull.Value)
                                {
                                    Gv_Deed.Rows[i].Cells[j].ReadOnly = true;
                                }
                            }
                        }
                    }
                }
                if (Status == 1)
                {
                   
                    
                    Gv_Deed.DataSource = null;
                    Gv_Deed.Columns.Clear();
                    Gv_Deed.Rows.Clear();
                    //ex2.Visible = true;
                    Gv_Deed.Visible = true;
                   // Gv_Deed.AutoGenerateColumns = false;
                   // Gv_Deed.ColumnCount = 11;

                   // Gv_Deed.Columns[0].Name = "Marker_Deed_ID";
                   // Gv_Deed.Columns[0].HeaderText = "Marker_Deed_ID";
                   // Gv_Deed.Columns[0].DataPropertyName = "Marker_Deed_Id";
                   

                   // Gv_Deed.Columns[1].Name = "Deed_Count";
                   // Gv_Deed.Columns[1].HeaderText = "Deed Count";
                   // Gv_Deed.Columns[1].DataPropertyName = "Deed Count";
                 
                   // Gv_Deed.Columns[2].Name = "Dated_Date";
                   // Gv_Deed.Columns[2].HeaderText = "Dated Date";
                   // Gv_Deed.Columns[2].DataPropertyName = "Dated Date";

                   // Gv_Deed.Columns[3].Name = "Recorded_Date";
                   // Gv_Deed.Columns[3].HeaderText = "Recorded Date";
                   // Gv_Deed.Columns[3].DataPropertyName = "Recorded Date";


                   // Gv_Deed.Columns[4].Name = "Instrument_No";
                   // Gv_Deed.Columns[4].HeaderText = "Instrument No";
                   // Gv_Deed.Columns[4].DataPropertyName = "Instrument No";

                   // Gv_Deed.Columns[5].Name = "Book";
                   // Gv_Deed.Columns[5].HeaderText = "Book";
                   // Gv_Deed.Columns[5].DataPropertyName = "Book";

                   // Gv_Deed.Columns[6].Name = "Page";
                   // Gv_Deed.Columns[6].HeaderText = "Page";
                   // Gv_Deed.Columns[6].DataPropertyName = "Page";

                   // Gv_Deed.Columns[7].Name = "Grantor";
                   // Gv_Deed.Columns[7].HeaderText = "Grantor";
                   // Gv_Deed.Columns[7].DataPropertyName = "Grantor";

                   // Gv_Deed.Columns[8].Name = "Grantee";
                   // Gv_Deed.Columns[8].HeaderText = "Grantee";
                   // Gv_Deed.Columns[8].DataPropertyName = "Grantee";

                   // Gv_Deed.Columns[9].Name = "Consideration_Amount";
                   // Gv_Deed.Columns[9].HeaderText = "Consideration Amount";
                   // Gv_Deed.Columns[9].DataPropertyName = "Consideration Amount";

                   // Gv_Deed.Columns[10].Name = "Additional_Information";
                   // Gv_Deed.Columns[10].HeaderText = "Additional Information";
                   // Gv_Deed.Columns[10].DataPropertyName = "Additional Information";

                   
                   // Gv_Deed.Columns.Add(ddl_DeedType);
                   // ddl_DeedType.HeaderText = "Deed Type";
                   // // Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                   // Hashtable htParam = new Hashtable();
                   // DataTable dt_Deedtype = new DataTable();
                   // htParam.Add("@Trans", "BIND");

                   // dt_Deedtype = dataaccess.ExecuteSP("Sp_Deed_Type", htParam);
                   // ddl_DeedType.DataSource = dt_Deedtype;

                   // ddl_DeedType.DisplayMember = "Value";
                   // ddl_DeedType.ValueMember = "Value";
                    
                   //// Gv_Deed.Columns[2].HeaderText = "Deed Type";
                   //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                   // ddl_DeedType.DisplayIndex = 2;
                    Gv_Deed.DataSource = dtComments1;
                    Gv_Deed.Columns[0].Visible = false;

                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                        
                    //    if (dtComments1.Rows[i]["Deed Type"].ToString() != "" &&  dtComments1.Rows[i]["Deed Type"].ToString().Substring(0, 2) !="X:")
                    //    {
                    //        Gv_Deed.Rows[i].Cells[11].Value = dtComments1.Rows[i]["Deed Type"].ToString();
                    //    }
                    //}
                    Lbl_Deed_Count.Text = dtComments1.Rows.Count.ToString();
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Deed.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";

                    for (int i = 0; i < Gv_Deed.Rows.Count; i++)
                    {
                        Gv_Deed.Rows[i].Cells[Gv_Deed.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Deed.Columns.Count; j++)
                        {
                            if (Gv_Deed.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                //Gv_Deed.Rows[i].Cells[j].ReadOnly = true;
                            }
                        }
                    }
                    for (int i = 0; i < Gv_Deed.Rows.Count; i++)
                    {
                        for (int j = 0; j < Gv_Deed.Columns.Count; j++)
                        {
                            if (Status == 1)
                            {
                                if (j != 0 && Gv_Deed.Rows[i].Cells[j].Value != null)
                                {
                                    if (Gv_Deed.Rows[i].Cells[j].Value.ToString().Length >= 2)
                                    {
                                        string HibemarkValue = Gv_Deed.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                        if (HibemarkValue == "X:")
                                        {
                                            Gv_Deed.Rows[i].Cells[j].Value = "";
                                        }
                                    }
                                }
                            }


                        }
                    }
                 
                   
                }
               
            }
            else
            {
                Gv_Deed.DataSource = null;
            }
            
        }


        private void Judgement_Grid_Bind()
        {
            Gv_Judgement.DataSource = null;
            Gv_Judgement.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Judgment", htComments1);

            if (dtComments1.Rows.Count > 0)
            {
                Gv_Judgement.DataSource = dtComments1;
              //  
                lbl_Judgment.Visible = true;
                lbl_judgement_Count.Text = dtComments1.Rows.Count.ToString();
                if (Status == 0)
                {
                    Gv_Judgement.Columns[0].Visible = false;
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Judgement.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Judgement.Rows.Count; i++)
                    {
                        Gv_Judgement.Rows[i].Cells[Gv_Judgement.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Judgement.Columns.Count; j++)
                        {


                            if (Gv_Judgement.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                Gv_Judgement.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                }
             if (Status == 1)
                  {
                               
                    Gv_Judgement.DataSource = null;
                    Gv_Judgement.Columns.Clear();
                    Gv_Judgement.Rows.Clear();
                    //ex2.Visible = true;t
                    Gv_Judgement.Visible = true;
                    //Gv_Judgement.AutoGenerateColumns = false;
                    //Gv_Judgement.ColumnCount = 14;

                    //Gv_Judgement.Columns[0].Name = "Marker_Judgment_Id";
                    //Gv_Judgement.Columns[0].HeaderText = "Marker_Judgment_Id";
                    //Gv_Judgement.Columns[0].DataPropertyName = "Marker_Judgment_Id";
                    //Gv_Judgement.Columns[0].Visible = false;

                    //Gv_Judgement.Columns[1].Name = "Judgment_Count";
                    //Gv_Judgement.Columns[1].HeaderText = "Judgment Count";
                    //Gv_Judgement.Columns[1].DataPropertyName = "Judgment Count";
                    

                    //Gv_Judgement.Columns[2].Name = "Dated_Date";
                    //Gv_Judgement.Columns[2].HeaderText = "Dated Date";
                    //Gv_Judgement.Columns[2].DataPropertyName = "Dated Date";

                    //Gv_Judgement.Columns[3].Name = "Recorded_Date";
                    //Gv_Judgement.Columns[3].HeaderText = "Recorded Date";
                    //Gv_Judgement.Columns[3].DataPropertyName = "Recorded Date";

                    //Gv_Judgement.Columns[4].Name = "Instrument_No";
                    //Gv_Judgement.Columns[4].HeaderText = "Instrument No";
                    //Gv_Judgement.Columns[4].DataPropertyName = "Instrument No";

                    //Gv_Judgement.Columns[5].Name = "Book";
                    //Gv_Judgement.Columns[5].HeaderText = "Book";
                    //Gv_Judgement.Columns[5].DataPropertyName = "Book";

                    //Gv_Judgement.Columns[6].Name = "Page";
                    //Gv_Judgement.Columns[6].HeaderText = "Page";
                    //Gv_Judgement.Columns[6].DataPropertyName = "Page";

                    //Gv_Judgement.Columns[7].Name = "Case_No";
                    //Gv_Judgement.Columns[7].HeaderText = "Case No";
                    //Gv_Judgement.Columns[7].DataPropertyName = "Case No";

                    //Gv_Judgement.Columns[8].Name = "Plaintiff";
                    //Gv_Judgement.Columns[8].HeaderText = "Plaintiff";
                    //Gv_Judgement.Columns[8].DataPropertyName = "Plaintiff";

                    //Gv_Judgement.Columns[9].Name = "Trustee";
                    //Gv_Judgement.Columns[9].HeaderText = "Trustee";
                    //Gv_Judgement.Columns[9].DataPropertyName = "Trustee";

                    //Gv_Judgement.Columns[10].Name = "Defendant";
                    //Gv_Judgement.Columns[10].HeaderText = "Defendant";
                    //Gv_Judgement.Columns[10].DataPropertyName = "Defendant";

                    //Gv_Judgement.Columns[11].Name = "Judgement_Amount";
                    //Gv_Judgement.Columns[11].HeaderText = "Judgement Amount";
                    //Gv_Judgement.Columns[11].DataPropertyName = "Judgement Amount";

                    //Gv_Judgement.Columns[12].Name = "Additional_Information";
                    //Gv_Judgement.Columns[12].HeaderText = "Additional Information";
                    //Gv_Judgement.Columns[12].DataPropertyName = "Additional Information";

                    //Gv_Judgement.Columns[13].Name = "Borrower";
                    //Gv_Judgement.Columns[13].HeaderText = "Borrower";
                    //Gv_Judgement.Columns[13].DataPropertyName = "Borrower";


                    //DataGridViewComboBoxColumn ddl_JudgementType = new DataGridViewComboBoxColumn();
                    //Gv_Judgement.Columns.Add(ddl_JudgementType);
                    //ddl_JudgementType.HeaderText = "Judgement Type";
                    //// Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                    //Hashtable htParam = new Hashtable();
                    //DataTable dt_Deedtype = new DataTable();
                    //htParam.Add("@Trans", "BIND");

                    //dt_Deedtype = dataaccess.ExecuteSP("Sp_Judgement_Type", htParam);
                    //ddl_JudgementType.DataSource = dt_Deedtype;

                    //ddl_JudgementType.DisplayMember = "Value";
                    //ddl_JudgementType.ValueMember = "Value";
                    
                    // //Gv_Judgement.Columns[2].HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                    //ddl_JudgementType.DisplayIndex = 2;
                    Gv_Judgement.DataSource = dtComments1;
                    Gv_Judgement.Columns[0].Visible = false;

                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                    //    Gv_Judgement.Rows[i].Cells[14].Value = dtComments1.Rows[i]["Judgement Type"].ToString();
                    //}
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Judgement.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Judgement.Rows.Count; i++)
                    {
                        Gv_Judgement.Rows[i].Cells[Gv_Judgement.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Judgement.Columns.Count; j++)
                        {


                            if (Gv_Judgement.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                             //   Gv_Judgement.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                    for (int i = 0; i < Gv_Judgement.Rows.Count; i++)
                    {
                        for (int j = 0; j < Gv_Judgement.Columns.Count; j++)
                        {
                            if (Status == 1)
                            {
                                if (j != 0 && Gv_Judgement.Rows[i].Cells[j].Value != null)
                                {
                                    if (Gv_Judgement.Rows[i].Cells[j].Value.ToString().Length >= 2)
                                    {
                                        string HibemarkValue = Gv_Judgement.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                        if (HibemarkValue == "X:")
                                        {
                                            Gv_Judgement.Rows[i].Cells[j].Value = "";
                                        }
                                    }
                                }
                            }


                        }
                    }
                }
            }
            else
            {
                Gv_Judgement.DataSource = null;
                //X = 0;
                //Y = 0;
                //Height = 0;
                //Width = 0;
                //PageNo = 0;
            }
           



        }


        private void Mortgage_Grid_Bind()
        {
            Gv_mortgage.DataSource = null;
            Gv_mortgage.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Mortgage", htComments1);

            if (dtComments1.Rows.Count > 0)
            {
                Gv_mortgage.DataSource = dtComments1;
               // Gv_mortgage.Columns[0].Visible = false;
                lbl_Mortgage.Visible = true;
                Lbl_Mortgage_Count.Text = dtComments1.Rows.Count.ToString();

             
                if (Status == 0)
                {
                    Gv_mortgage.Columns[0].Visible = false;
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_mortgage.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_mortgage.Rows.Count; i++)
                    {
                        Gv_mortgage.Rows[i].Cells[Gv_mortgage.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_mortgage.Columns.Count; j++)
                        {


                            if (Gv_mortgage.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                Gv_mortgage.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                }


                if (Status == 1)
                {
                    //if (j != 0)
                    //{
                    //    if (Gv_mortgage.Rows[i].Cells[j].Value.ToString().Length >= 2)
                    //    {
                    //        string HibemarkValue = Gv_mortgage.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                    //        if (HibemarkValue == "X:")
                    //        {
                    //            Gv_mortgage.Rows[i].Cells[j].Value = "";
                    //        }
                    //    }
                    //}
                    Gv_mortgage.DataSource = null;
                    Gv_mortgage.Columns.Clear();
                    Gv_mortgage.Rows.Clear();
                    //ex2.Visible = true;t
                    Gv_mortgage.Visible = true;
                    //Gv_mortgage.AutoGenerateColumns = false;
                    //Gv_mortgage.ColumnCount = 22;

                    //Gv_mortgage.Columns[0].Name = "Marker_Mortgage_Id";
                    //Gv_mortgage.Columns[0].HeaderText = "Marker_Mortgage_ID";
                    //Gv_mortgage.Columns[0].DataPropertyName = "Marker_Mortgage_Id";
                    //Gv_mortgage.Columns[0].Visible = false;

                    //Gv_mortgage.Columns[1].Name = "Mortgage_Count";
                    //Gv_mortgage.Columns[1].HeaderText = "Mortgage Count";
                    //Gv_mortgage.Columns[1].DataPropertyName = "Mortgage Count";
                    

                    //Gv_mortgage.Columns[2].Name = "Dated_Date";
                    //Gv_mortgage.Columns[2].HeaderText = "Dated Date";
                    //Gv_mortgage.Columns[2].DataPropertyName = "Dated Date";

                    //Gv_mortgage.Columns[3].Name = "Recorded_Date";
                    //Gv_mortgage.Columns[3].HeaderText = "Recorded Date";
                    //Gv_mortgage.Columns[3].DataPropertyName = "Recorded Date";

                    //Gv_mortgage.Columns[4].Name = "Instrument_No";
                    //Gv_mortgage.Columns[4].HeaderText = "Instrument No";
                    //Gv_mortgage.Columns[4].DataPropertyName = "Instrument No";

                    //Gv_mortgage.Columns[5].Name = "Book";
                    //Gv_mortgage.Columns[5].HeaderText = "Book";
                    //Gv_mortgage.Columns[5].DataPropertyName = "Book";

                    //Gv_mortgage.Columns[6].Name = "Page";
                    //Gv_mortgage.Columns[6].HeaderText = "Page";
                    //Gv_mortgage.Columns[6].DataPropertyName = "Page";

                    //Gv_mortgage.Columns[7].Name = "Mortgagor/Borrower";
                    //Gv_mortgage.Columns[7].HeaderText = "Mortgagor/Borrower";
                    //Gv_mortgage.Columns[7].DataPropertyName = "Mortgagor/Borrower";

                    //Gv_mortgage.Columns[8].Name = "Mortgagee/Beneficiary";
                    //Gv_mortgage.Columns[8].HeaderText = "Mortgagee/Beneficiary";
                    //Gv_mortgage.Columns[8].DataPropertyName = "Mortgagee/Beneficiary";

                    //Gv_mortgage.Columns[9].Name = "Trustee";
                    //Gv_mortgage.Columns[9].HeaderText = "Trustee";
                    //Gv_mortgage.Columns[9].DataPropertyName = "Trustee";

                    //Gv_mortgage.Columns[10].Name = "Mortgage_Amount";
                    //Gv_mortgage.Columns[10].HeaderText = "Mortgage Amount";
                    //Gv_mortgage.Columns[10].DataPropertyName = "Mortgage Amount";

                    //Gv_mortgage.Columns[11].Name = "Maturity_Date";
                    //Gv_mortgage.Columns[11].HeaderText = "Maturity Date";
                    //Gv_mortgage.Columns[11].DataPropertyName = "Maturity Date";

                    //Gv_mortgage.Columns[12].Name = "Open End";
                    //Gv_mortgage.Columns[12].HeaderText = "Open End";
                    //Gv_mortgage.Columns[12].DataPropertyName = "Open End";

                    //Gv_mortgage.Columns[13].Name = "Open End Amount";
                    //Gv_mortgage.Columns[13].HeaderText = "Open End Amount";
                    //Gv_mortgage.Columns[13].DataPropertyName = "Open End Amount";


                    //Gv_mortgage.Columns[14].Name = "Latest_Assignment_Assignor";
                    //Gv_mortgage.Columns[14].HeaderText = "Latest Assignment Assignor";
                    //Gv_mortgage.Columns[14].DataPropertyName = "Latest Assignment Assignor";


                    //Gv_mortgage.Columns[15].Name = "Latest_Assignment_Assignee";
                    //Gv_mortgage.Columns[15].HeaderText = "Latest Assignment Assignee";
                    //Gv_mortgage.Columns[15].DataPropertyName = "Latest Assignment Assignee";

                    //Gv_mortgage.Columns[16].Name = "Latest_Assignment_Book";
                    //Gv_mortgage.Columns[16].HeaderText = "Latest Assignment Book";
                    //Gv_mortgage.Columns[16].DataPropertyName = "Latest Assignment Book";


                    //Gv_mortgage.Columns[17].Name = "Latest_Assignment_Page";
                    //Gv_mortgage.Columns[17].HeaderText = "Latest Assignment Page";
                    //Gv_mortgage.Columns[17].DataPropertyName = "Latest Assignment Page";


                    //Gv_mortgage.Columns[18].Name = "Latest_Assignment_Instrument_No";
                    //Gv_mortgage.Columns[18].HeaderText = "Latest Assignment Instrument No";
                    //Gv_mortgage.Columns[18].DataPropertyName = "Latest Assignment Instrument No";


                    //Gv_mortgage.Columns[19].Name = "Latest_Assignment_Recorded_Date";
                    //Gv_mortgage.Columns[19].HeaderText = "Latest Assignment Recorded Date";
                    //Gv_mortgage.Columns[19].DataPropertyName = "Latest Assignment Recorded Date";

                    //Gv_mortgage.Columns[20].Name = "Latest_Assignment_Dated_Date";
                    //Gv_mortgage.Columns[20].HeaderText = "Latest Assignment Dated Date";
                    //Gv_mortgage.Columns[20].DataPropertyName = "Latest Assignment Dated Date";

                    //Gv_mortgage.Columns[21].Name = "Additional_Information";
                    //Gv_mortgage.Columns[21].HeaderText = "Additional Information";
                    //Gv_mortgage.Columns[21].DataPropertyName = "Additional Information";





                    //DataGridViewComboBoxColumn ddl_MortgageType = new DataGridViewComboBoxColumn();
                    //Gv_mortgage.Columns.Add(ddl_MortgageType);
                    //ddl_MortgageType.HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                    //Hashtable htParam = new Hashtable();
                    //DataTable dt_Deedtype = new DataTable();
                    //htParam.Add("@Trans", "BIND");

                    //dt_Deedtype = dataaccess.ExecuteSP("Sp_Mortgage_Type", htParam);
                    //ddl_MortgageType.DataSource = dt_Deedtype;

                    //ddl_MortgageType.DisplayMember = "Value";
                    //ddl_MortgageType.ValueMember = "Value";
                    
                    // //Gv_mortgage.Columns[2].HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                    //ddl_MortgageType.DisplayIndex = 2;
                    Gv_mortgage.DataSource = dtComments1;
                    Gv_mortgage.Columns[0].Visible = false;

                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                    //    if (dtComments1.Rows[i]["Mortgage Type"].ToString() != "" && dtComments1.Rows[i]["Mortgage Type"].ToString().Substring(0,2) !="X:")
                    //    Gv_mortgage.Rows[i].Cells[22].Value = dtComments1.Rows[i]["Mortgage Type"].ToString();
                    //}
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_mortgage.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_mortgage.Rows.Count; i++)
                    {
                        Gv_mortgage.Rows[i].Cells[Gv_mortgage.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_mortgage.Columns.Count; j++)
                        {


                            if (Gv_mortgage.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                              //  Gv_mortgage.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                
                }

                for (int i = 0; i < Gv_mortgage.Rows.Count; i++)
                {
                    for (int j = 0; j < Gv_mortgage.Columns.Count; j++)
                    {
                        if (Status == 1)
                        {
                            if (j != 0 && Gv_mortgage.Rows[i].Cells[j].Value !=null)
                            {
                                
                                if (Gv_mortgage.Rows[i].Cells[j].Value.ToString().Length >= 2)
                                {
                                    string HibemarkValue = Gv_mortgage.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                    if (HibemarkValue == "X:")
                                    {
                                        Gv_mortgage.Rows[i].Cells[j].Value = "";
                                    }
                                }
                            }
                        }


                    }
                }

            }
            else
            {
                Gv_mortgage.DataSource = null;
            }
         
        }
        
        private void Tax_Grid_Bind()
        {
            Gv_Tax.DataSource = null;
            Gv_Tax.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Tax", htComments1);

            if (dtComments1.Rows.Count > 0)
            {
                Gv_Tax.DataSource = dtComments1;
              //  Gv_Tax.Columns[0].Visible = false;
                lbl_Tax.Visible = true;
                lbl_Tax_count.Text = dtComments1.Rows.Count.ToString();

             
                if (Status == 0)
                {
                    Gv_Tax.Columns[0].Visible = false;
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Tax.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Tax.Rows.Count; i++)
                    {
                        Gv_Tax.Rows[i].Cells[Gv_Tax.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Tax.Columns.Count; j++)
                        {


                            if (Gv_Tax.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                Gv_Tax.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                }
                if (Status == 1)
                {
                    Gv_Tax.DataSource = null;
                    Gv_Tax.Columns.Clear();
                   
                    //ex2.Visible = true;t
                    Gv_Tax.Visible = true;
                    //Gv_Tax.AutoGenerateColumns = false;
                    //Gv_Tax.ColumnCount = 6;

                    //Gv_Tax.Columns[0].Name = "Marker_Tax_Id";
                    //Gv_Tax.Columns[0].HeaderText = "Marker_Tax_ID";
                    //Gv_Tax.Columns[0].DataPropertyName = "Marker_Tax_Id";
                    //Gv_Tax.Columns[0].Visible = false;

                    //Gv_Tax.Columns[1].Name = "Tax_Count";
                    //Gv_Tax.Columns[1].HeaderText = "Tax Count";
                    //Gv_Tax.Columns[1].DataPropertyName = "Tax Count";


                    //Gv_Tax.Columns[2].Name = "Tax Id/Parcel No";
                    //Gv_Tax.Columns[2].HeaderText = "Tax Id/Parcel No";
                    //Gv_Tax.Columns[2].DataPropertyName = "Tax Id/Parcel No";

                    ////Gv_Tax.Columns[3].Name = "Tax_Status";
                    ////Gv_Tax.Columns[3].HeaderText = "Tax Status";
                    ////Gv_Tax.Columns[3].DataPropertyName = "Tax Status";

                    ////Gv_Tax.Columns[4].Name = "Tax_Period";
                    ////Gv_Tax.Columns[4].HeaderText = "Tax Period";
                    ////Gv_Tax.Columns[4].DataPropertyName = "Tax Period";

                    //Gv_Tax.Columns[3].Name = "Tax_Year";
                    //Gv_Tax.Columns[3].HeaderText = "Tax Year";
                    //Gv_Tax.Columns[3].DataPropertyName = "Tax Year";

                    //Gv_Tax.Columns[4].Name = "Amount";
                    //Gv_Tax.Columns[4].HeaderText = "Amount";
                    //Gv_Tax.Columns[4].DataPropertyName = "Amount";

                    //Gv_Tax.Columns[5].Name = "Tax_Date";
                    //Gv_Tax.Columns[5].HeaderText = "Tax Date";
                    //Gv_Tax.Columns[5].DataPropertyName = "Tax Date";


                    //DataGridViewComboBoxColumn ddl_TaxType = new DataGridViewComboBoxColumn();
                    //Gv_Tax.Columns.Add(ddl_TaxType);
                    //ddl_TaxType.HeaderText = "Tax Type";
                    //// Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                    //Hashtable htParam = new Hashtable();
                    //DataTable dt_Deedtype = new DataTable();
                    //htParam.Add("@Trans", "BIND");

                    //dt_Deedtype = dataaccess.ExecuteSP("Sp_Tax_Type", htParam);
                    //ddl_TaxType.DataSource = dt_Deedtype;

                    //ddl_TaxType.DisplayMember = "Value";
                    //ddl_TaxType.ValueMember = "Value";

                    ////Gv_Tax.Columns[2].HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                    //ddl_TaxType.DisplayIndex = 3;

                    //Gv_Tax.DataSource = dtComments1;

                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                    //    if (dtComments1.Rows[i]["Tax Type"].ToString() != "" && dtComments1.Rows[i]["Tax Type"].ToString().Substring(0, 2) != "X:")
                    //    {
                    //        Gv_Tax.Rows[i].Cells[6].Value = dtComments1.Rows[i]["Tax Type"].ToString();

                    //    }
                      
                    //}




                    //DataGridViewComboBoxColumn ddl_TaxStatus = new DataGridViewComboBoxColumn();
                    //Gv_Tax.Columns.Add(ddl_TaxStatus);
                    //ddl_TaxStatus.HeaderText = "Tax Status";
                    //// Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                    //Hashtable htstatus = new Hashtable();
                    //DataTable dtstatus = new DataTable();
                    //htstatus.Add("@Trans", "BIND_STATUS");

                    //dtstatus = dataaccess.ExecuteSP("Sp_Tax_Type", htstatus);
                    //ddl_TaxStatus.DataSource = dtstatus;

                    //ddl_TaxStatus.DisplayMember = "Value";
                    //ddl_TaxStatus.ValueMember = "Value";

                    ////Gv_Tax.Columns[2].HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                    //ddl_TaxStatus.DisplayIndex = 4;
                  


                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                    //    if (dtComments1.Rows[i]["Tax Status"].ToString() != "" && dtComments1.Rows[i]["Tax Status"].ToString().Substring(0, 2) != "X:")
                    //    {
                    //        Gv_Tax.Rows[i].Cells[7].Value = dtComments1.Rows[i]["Tax Status"].ToString();
                    //    }
                    //}



                    //DataGridViewComboBoxColumn ddl_TaxPeriod = new DataGridViewComboBoxColumn();
                    //Gv_Tax.Columns.Add(ddl_TaxPeriod);
                    //ddl_TaxPeriod.HeaderText = "Tax Period";
                    //// Gv_Deed.Columns["Deed Type"].DisplayIndex = 2;
                    //Hashtable htperiod = new Hashtable();
                    //DataTable dtperiod = new DataTable();
                    //htperiod.Add("@Trans", "BIND_PERIOD");

                    //dtperiod = dataaccess.ExecuteSP("Sp_Tax_Type", htperiod);
                    //ddl_TaxPeriod.DataSource = dtperiod;

                    //ddl_TaxPeriod.DisplayMember = "Value";
                    //ddl_TaxPeriod.ValueMember = "Value";

                    ////Gv_Tax.Columns[2].HeaderText = "Mortgage Type";
                    //// Gv_Deed.Columns[2].DataPropertyName = "Deed Type";
                    //ddl_TaxPeriod.DisplayIndex = 5;
                   


                    //for (int i = 0; i < dtComments1.Rows.Count; i++)
                    //{
                    //    if (dtComments1.Rows[i]["Tax Period"].ToString() != "" && dtComments1.Rows[i]["Tax Period"].ToString().Substring(0, 2) != "X:")
                    //    {
                    //        Gv_Tax.Rows[i].Cells[8].Value = dtComments1.Rows[i]["Tax Period"].ToString();
                    //    }
                    //}
                    Gv_Tax.DataSource = dtComments1;
                    Gv_Tax.Columns[0].Visible = false;
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Tax.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Tax.Rows.Count; i++)
                    {
                        Gv_Tax.Rows[i].Cells[Gv_Tax.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Tax.Columns.Count; j++)
                        {


                            if (Gv_Tax.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                             //   Gv_Tax.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                    for (int i = 0; i < Gv_Tax.Rows.Count; i++)
                    {
                        for (int j = 0; j < Gv_Tax.Columns.Count; j++)
                        {
                            if (Status == 1)
                            {
                                if (j != 0 && Gv_Tax.Rows[i].Cells[j].Value != null)
                                {
                                    if (Gv_Tax.Rows[i].Cells[j].Value.ToString().Length >= 2)
                                    {
                                        string HibemarkValue = Gv_Tax.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                        if (HibemarkValue == "X:")
                                        {
                                            Gv_Tax.Rows[i].Cells[j].Value = "";
                                        }
                                    }
                                }
                            }


                        }
                    }
                }
                
            }


            else
            {
                Gv_Tax.DataSource = null;
            }
            //foreach (DataGridViewRow row in Gv_Tax.Rows)
            //{
            //    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[2];
            //    cbCell.Items.Add("dfdf");

            //}
          
        }

        private void Assessment_Grid_Bind()
        {
            
            Gv_Assessment.DataSource = null;
            Gv_Assessment.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Assessment", htComments1);

            if (dtComments1.Rows.Count > 0)
            {
                Gv_Assessment.DataSource = dtComments1;
                Gv_Assessment.Columns[0].Visible = false;
                lbl_Assessment.Visible = true;
                lbl_Assessment_Count.Text = dtComments1.Rows.Count.ToString();
                if (Status == 0)
                {
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Assessment.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Assessment.Rows.Count; i++)
                    {
                        Gv_Assessment.Rows[i].Cells[Gv_Assessment.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Assessment.Columns.Count; j++)
                        {


                            if (Gv_Assessment.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                Gv_Assessment.Rows[i].Cells[j].ReadOnly = true;

                            }
                        }
                    }
                }
                if (Status == 1)
                {
                for (int i = 0; i < Gv_Assessment.Rows.Count; i++)
                    {
                        for (int j = 0; j < Gv_Assessment.Columns.Count; j++)
                        {
                           
                                if (j != 0 && Gv_Assessment.Rows[i].Cells[j].Value != null)
                                {
                                    if (Gv_Assessment.Rows[i].Cells[j].Value.ToString().Length >=2)
                                    {
                                        string HibemarkValue = Gv_Assessment.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                        if (HibemarkValue == "X:")
                                        {
                                            Gv_Assessment.Rows[i].Cells[j].Value = "";
                                        }
                                    }
                                }
                            }


                        }

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                Gv_Assessment.Columns.Add(btnDelete);
                btnDelete.HeaderText = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.Name = "btnDelete";
                for (int i = 0; i < Gv_Assessment.Rows.Count; i++)
                {
                    Gv_Assessment.Rows[i].Cells[Gv_Assessment.Columns.Count - 1].Value = "Delete";
                    for (int j = 0; j < Gv_Assessment.Columns.Count; j++)
                    {


                        if (Gv_Assessment.Rows[i].Cells[j].Value == DBNull.Value)
                        {
                           // Gv_Assessment.Rows[i].Cells[j].ReadOnly = true;

                        }
                    }
                }
                    }
                

              
            }
            else
            {
                Gv_Assessment.DataSource = null;
            }
        }

        private void Legal_Description_Grid_Bind()
        {
         
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Legal_Description", htComments1);
            if (dtComments1.Rows.Count > 0)
            {
                Txt_Legal_Description.Text = dtComments1.Rows[0]["Description"].ToString();
                Marker_Legal_Description_Id = dtComments1.Rows[0]["Marker_Legal_Description_Id"].ToString();
            }
            Hashtable htComments2 = new Hashtable();
            DataTable dtComments2 = new System.Data.DataTable();
            htComments2.Add("@Trans", "SELECT");
            htComments2.Add("@Marker_Legal_Description_Id", Marker_Legal_Description_Id);
            htComments2.Add("@Legal_Description_Information", Information);
            dtComments2 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments2);
            if (dtComments2.Rows.Count > 0)
            {
                Txt_Legal_Description.Text = dtComments2.Rows[0]["Value"].ToString();
                if (Txt_Legal_Description.Text.ToString().Substring(0, 2).ToUpper() != "X:" && Status==1)
                {
                    Txt_Legal_Description.Text = "";
                }
            }
        }




        private void imageBox_Selected(object sender, EventArgs e)
        {
            //Deed
            int Db_X=0, Db_Y=0, Db_Height=0,Db_Width=0,Db_Result=0;
            Last_PageNo = PageNo;
            if (Status == 0)
            {
                if (Tab_Page == "Deed")
                {

                    if (Gv_Deed.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_Deed.CurrentCell.ColumnIndex;
                        Row_Index = Gv_Deed.CurrentRow.Index;
                        Information = Gv_Deed.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            if (Tab_Page == "Deed")
                            {
                                htselect.Add("@Trans", "SELECT");
                                htselect.Add("@Marker_Deed_Id", Marker_Deed_Id);
                                htselect.Add("@Deed_Information", Information);
                                dtselect = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htselect);
                                for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                                if (Db_Result == 0)
                                {
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);

                                    Hashtable htComments = new Hashtable();
                                    DataTable dtComments = new System.Data.DataTable();

                                    DateTime date = new DateTime();
                                    date = DateTime.Now;
                                    string dateeval = date.ToString("dd/MM/yyyy");
                                    string time = date.ToString("hh:mm tt");
                                    htComments.Add("@Trans", "INSERT");
                                    htComments.Add("@Marker_Deed_Id", Marker_Deed_Id);
                                    htComments.Add("@Value", this.FormatRectangle(Rect));
                                    htComments.Add("@Deed_Information", Information);
                                    htComments.Add("@X", X);
                                    htComments.Add("@Y", Y);
                                    htComments.Add("@Width", Width);
                                    htComments.Add("@Height", Height);
                                    htComments.Add("@PageNo", PageNo);
                                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                                    Deed_Grid_Bind();
                                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                                    //}
                                }
                            }
                        }
                        Gv_Deed.Focus();
                        Gv_Deed.CurrentCell = Gv_Deed.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }
                }

                //Mortgage
                if (Tab_Page == "Mortgage")
                {
                    if (Gv_mortgage.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_mortgage.CurrentCell.ColumnIndex;
                        Row_Index = Gv_mortgage.CurrentRow.Index;
                        Information = Gv_mortgage.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Mortgage_Id", Marker_Mortgage_Id);
                            htselect.Add("@Mortgage_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htselect);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (Db_Result == 0)
                             {
                                 Rectangle Rect = new Rectangle(X, Y, Width, Height);

                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Mortgage_Id", Marker_Mortgage_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Mortgage_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                                 Mortgage_Grid_Bind();
                             }
                        }
                        Gv_mortgage.Focus();
                        Gv_mortgage.CurrentCell = Gv_mortgage.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }

                }

                //Tax
                if (Tab_Page == "Tax")
                {
                    if (Gv_Tax.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_Tax.CurrentCell.ColumnIndex;
                        Row_Index = Gv_Tax.CurrentRow.Index;
                        Information = Gv_Tax.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Tax_Id", Marker_Tax_Id);
                            htselect.Add("@Tax_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htselect);
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (Db_Result == 0)
                             {
                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Tax_Id", Marker_Tax_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Tax_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                 Tax_Grid_Bind();
                             }
                        }
                        Gv_Tax.Focus();
                        Gv_Tax.CurrentCell = Gv_Tax.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }
                }
                //Judgement

                if (Tab_Page == "Judgement")
                {
                    if (Gv_Judgement.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_Judgement.CurrentCell.ColumnIndex;
                        Row_Index = Gv_Judgement.CurrentRow.Index;
                        Information = Gv_Judgement.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Judgment_Id", Marker_Judgement_Id);
                            htselect.Add("@Judgment_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htselect);
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (Db_Result == 0)
                             {
                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Judgment_Id", Marker_Judgement_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Judgment_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                 Judgement_Grid_Bind();
                             }
                        }
                        Gv_Judgement.Focus();
                        Gv_Judgement.CurrentCell = Gv_Judgement.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }
                }
                //Assessment
                if (Tab_Page == "Assessment")
                {

                    if (Gv_Assessment.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_Assessment.CurrentCell.ColumnIndex;
                        Row_Index = Gv_Assessment.CurrentRow.Index;
                        Information = Gv_Assessment.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Assessment_Id", Marker_Assessment_Id);
                            htselect.Add("@Assessment_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htselect);
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (Db_Result == 0)
                             {
                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Assessment_Id", Marker_Assessment_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Assessment_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                 Assessment_Grid_Bind();
                             }
                        }
                        Gv_Assessment.Focus();
                        Gv_Assessment.CurrentCell = Gv_Assessment.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }
                }




                //Additional Information
                if (Tab_Page == "Additional Information")
                {
                    if (Gv_Additional_Information.CurrentCell != null)
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();
                        Col_Index = Gv_Additional_Information.CurrentCell.ColumnIndex;
                        Row_Index = Gv_Additional_Information.CurrentRow.Index;
                        Information = Gv_Additional_Information.Columns[Col_Index].HeaderText.ToString();
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Additional_Information_Id", Marker_Additional_Information_Id);
                            htselect.Add("@Additional_Information_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htselect);
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (Db_Result == 0)
                             {
                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Additional_Information_Id", Marker_Additional_Information_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Additional_Information_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                                 Additional_Information_Grid_Bind();
                             }
                        }
                        Gv_Additional_Information.Focus();
                        Gv_Additional_Information.CurrentCell = Gv_Additional_Information.Rows[Row_Index].Cells[Col_Index];
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                    }
                }



                //Legal Description
                if (Tab_Page == "Legal Description")
                {

                    LegalSubmit();
                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new System.Data.DataTable();

                        Information = "Legal Description";
                        X = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.X.ToString()));
                        Y = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Y.ToString()));
                        Width = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Width.ToString()));
                        Height = (int)Math.Ceiling(float.Parse(imageBox.SelectionRegion.Height.ToString()));

                        if (X != 0 && Y != 0 && Width != 0 && Height != 0)
                        {
                            htselect.Add("@Trans", "SELECT");
                            htselect.Add("@Marker_Legal_Description_Id", Marker_Legal_Description_Id);
                            htselect.Add("@Legal_Description_Information", Information);
                            dtselect = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htselect);
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                             for (int i = 0; i < dtselect.Rows.Count; i++)
                                {
                                    Db_X =int.Parse(dtselect.Rows[i]["X"].ToString());
                                    Db_Y = int.Parse(dtselect.Rows[i]["Y"].ToString());
                                    Db_Height = int.Parse(dtselect.Rows[i]["Height"].ToString());
                                    Db_Width = int.Parse(dtselect.Rows[i]["Width"].ToString());
                                    if (X == Db_X && Y == Db_Y && Width == Db_Width && Height == Db_Height)
                                    {
                                        Db_Result = 1;
                                    }

                                }
                             if (dtselect.Rows.Count > 0)
                             {
                                 Txt_Legal_Description.Text = dtselect.Rows[0]["Value"].ToString();
                             }
                             if (Db_Result == 0)
                             {
                                 Hashtable htComments = new Hashtable();
                                 DataTable dtComments = new System.Data.DataTable();

                                 DateTime date = new DateTime();
                                 date = DateTime.Now;
                                 string dateeval = date.ToString("dd/MM/yyyy");
                                 string time = date.ToString("hh:mm tt");
                                 htComments.Add("@Trans", "INSERT");
                                 htComments.Add("@Marker_Legal_Description_Id", Marker_Legal_Description_Id);
                                 htComments.Add("@Value", this.FormatRectangle(Rect));
                                 htComments.Add("@Legal_Description_Information", Information);
                                 htComments.Add("@X", X);
                                 htComments.Add("@Y", Y);
                                 htComments.Add("@Width", Width);
                                 htComments.Add("@Height", Height);
                                 htComments.Add("@PageNo", PageNo);
                                 dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments);
                                 Legal_Description_Grid_Bind();
                                 Highlited();
                                 PageNo = Last_PageNo;
                                 Image_View();
                             }
                        }
                    }
            }

        }

        private void Tab_Typing_SelectedIndexChanged(object sender, EventArgs e)
        {

            Tab_Page = Tab_Typing.SelectedTab.Text.ToString();
            Cur_Col = 0;
            Cur_Row = 0;
            Pre_Col_Index = 0;
            Pre_Row_Index = 0;
            lbl_Total_Mark.Text = "/0";
            lbl_Current_Mark.Text = "0";
            if (Tab_Page == "Deed")
            {
                Deed_Grid_Bind();
            }
            else if (Tab_Page == "Mortgage")
            {
                Mortgage_Grid_Bind();
            }
            else if (Tab_Page == "Tax")
            {
                Tax_Grid_Bind();
            }
            else if (Tab_Page == "Judgement")
            {
                Judgement_Grid_Bind();
            }
            else if (Tab_Page == "Assessment")
            {
                Assessment_Grid_Bind();
            }
            else if (Tab_Page == "Additional Information")
            {
                Additional_Information_Grid_Bind();
            }


            if (Tab_Page == "Legal Description")
            {
               // LegalSubmit();
              
                Hashtable htComments1 = new Hashtable();
                DataTable dtComments1 = new System.Data.DataTable();
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Order_Id", Orderid);
                dtComments1 = dataaccess.ExecuteSP("Sp_Legal_Description", htComments1);
                if (dtComments1.Rows.Count > 0)
                {
                    Txt_Legal_Description.Text = dtComments1.Rows[0]["Description"].ToString();
                    Marker_Legal_Description_Id = dtComments1.Rows[0]["Marker_Legal_Description_Id"].ToString();
                    if (Txt_Legal_Description.Text.ToString().Substring(0, 2).ToUpper() == "X:" && Status == 1)
                    {
                        Txt_Legal_Description.Text = "";
                    }
                }
              
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
            PageNo = 0;
          
            toolStripTextBox1.Text = (PageNo + 1).ToString();
            Image_View();
            Clear_Rect();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (PageNo > 0)
            {
                PageNo = PageNo - 1;
                toolStripTextBox1.Text = (PageNo + 1).ToString();
                Image_View();
                Clear_Rect();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           
            if (PageNo < Page_Count-1)
            {
                PageNo = PageNo + 1;
                toolStripTextBox1.Text = (PageNo + 1).ToString();
                Image_View();
                Clear_Rect();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           
            PageNo = Page_Count - 1;
            toolStripTextBox1.Text = (PageNo + 1).ToString();
            Image_View();
            Clear_Rect();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (toolStripTextBox1.Text != "")
            {
                PageNo = int.Parse(toolStripTextBox1.Text);
                Image_View();
                imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
            }
        }
        private void Highlited()
        {
            imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
            imageBox.SelectionColor = Color.Yellow;
            dt_Select.Clear();
            ht_Select.Clear();
            if (Tab_Page == "Deed")
            {
                if (Information != "Deed Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Deed_Id", Marker_Deed_Id);
                    ht_Select.Add("@Deed_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", ht_Select);



                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);

                }
            }
            //Mortgage
            if (Tab_Page == "Mortgage")
            {

                if (Information != "Mortgage Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Mortgage_Id", Marker_Mortgage_Id);
                    ht_Select.Add("@Mortgage_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
                }
            }
            //Tax
            if (Tab_Page == "Tax")
            {
                if (Information != "Tax Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Tax_Id", Marker_Tax_Id);
                    ht_Select.Add("@Tax_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);

                }

            }
            //Judgement
            if (Tab_Page == "Judgement")
            {

                if (Information != "Judgement Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Judgment_Id", Marker_Judgement_Id);
                    ht_Select.Add("@Judgment_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);

                }

            }

            //Assessment
            if (Tab_Page == "Assessment")
            {

                if (Information != "Assessment Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Assessment_Id", Marker_Assessment_Id);
                    ht_Select.Add("@Assessment_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
                }

            }


            //Additional Information
            if (Tab_Page == "Additional Information")
            {
                if (Information != "Additional Information Count")
                {

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Additional_Information_Id", Marker_Additional_Information_Id);
                    ht_Select.Add("@Additional_Information_Information", Information);
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);

                }

            }





            //Legal_Description
            if (Tab_Page == "Legal Description")
            {

               

                    ht_Select.Add("@Trans", "SELECT");
                    ht_Select.Add("@Marker_Legal_Description_Id", Marker_Legal_Description_Id);
                    ht_Select.Add("@Legal_Description_Information", "Legal Description");
                    dt_Select = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString());
                    }
                    else
                    {
                        X = 0;
                        Y = 0;
                        Height = 0;
                        Width = 0;
                        PageNo = 0;

                    }
                    toolStripTextBox1.Text = (PageNo + 1).ToString();
                    Image_View();
                    imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);

           

            }
            if (X != 0 && Y != 0 && Height != 0 && Width != 0)
            {
                lbl_Total_Mark.Text = "/" + dt_Select.Rows.Count;
            }
            else
            {
                lbl_Total_Mark.Text = "/0";
            }
           


        }

        private void Gv_Deed_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            if (Gv_Deed.CurrentCell != null &&  Status == 1)
            {
                if (Gv_Deed.CurrentCell.Value == null)
                {
                    Gv_Deed.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0 &&  Gv_Deed.CurrentCell.Value.ToString() == "")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new System.Data.DataTable();
                    htComments1.Add("@Trans", "SELECT");
                    htComments1.Add("@Marker_Deed_Id", Marker_Deed_Id);
                    htComments1.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                    dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments1);
                    if (dtComments1.Rows.Count <= 0)
                    {
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments.Add("@Trans", "INSERT");
                        htComments.Add("@Marker_Deed_Id", Marker_Deed_Id);
                        htComments.Add("@Value", Value_Data);
                        htComments.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments.Add("@X", X);
                        htComments.Add("@Y", Y);
                        htComments.Add("@Width", Width);
                        htComments.Add("@Height", Height);
                        htComments.Add("@PageNo", PageNo);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }
            }
            if (Status == 1)
            {
                Gv_Deed.ReadOnly = false;
            }
         
        }
     
        private void Gv_Deed_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Deed.Rows.Count > 0)
            {
                if (Gv_Deed.Rows.Count-1 >= e.RowIndex)
                {
                    if (e.RowIndex != -1 && Gv_Deed.Rows[e.RowIndex].Cells[0].Value!=null)
                    {
                        Last_PageNo = PageNo;
                        Tab_Change = 0;
                        Marker_Deed_Id = Gv_Deed.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_Deed.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_Deed.Rows.Count != 0)
                        {
                            if (Gv_Deed.Rows[e.RowIndex].Cells[0].Value != null)
                            {
                                if (Gv_Deed.CurrentRow != null)
                                {
                                    for (int i = 0; i < Gv_Deed.Columns.Count; i++)
                                    {
                                        if (Gv_Deed.Rows[e.RowIndex].Cells[i].Value != null)
                                        {
                                            htComments.Clear();
                                            dtComments.Clear();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Deed_Id", Gv_Deed.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Deed_Information", Gv_Deed.Columns[i].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Deed.Rows[e.RowIndex].Cells[i].Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                                        }
                                    }
                                }
                            }
                        }
                        if (Gv_Deed.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_Deed.Rows[e.RowIndex].Cells[0].Value != null)
                        {
                            htComments.Clear();
                            dtComments.Clear();

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Deed_Id", Gv_Deed.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed", htComments);
                            Deed_Grid_Bind();
                        }

                    }
                   

                }
            }
           
        }
     

 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Image Zoom In
                Last_PageNo = PageNo;
            if (keyData == Keys.F10)
            {
                imageBox.ZoomIn();
            }
            //Image Zoom Out
            if (keyData == Keys.F9)
            {
                imageBox.ZoomOut();
            }
       
                if (Tab_Page == "Deed")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_Deed.CurrentCell != null)
                    {
                        if (Gv_Deed.CurrentCell.ColumnIndex <= Gv_Deed.Columns.Count - 1)
                        {
                           
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                             
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_Deed.Rows.Count >= Gv_Deed.CurrentRow.Index - 1 && Gv_Deed.Columns.Count >= Gv_Deed.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex];
                                    Gv_Deed.BeginEdit(true);
                                    Marker_Deed_Id = Gv_Deed.CurrentRow.Cells[0].Value.ToString();

                                    if (Gv_Deed.ColumnCount-1 > Gv_Deed.CurrentCell.ColumnIndex)
                                    {
                                        Information = Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                    //else
                                    //{
                                    //    Information = Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    //}
                                    Cur_Row = Gv_Deed.CurrentRow.Index;
                                    Cur_Col = Gv_Deed.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();
                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();

                                    }
                                
                                    lbl_Current_Mark.Text = "0";
                                    
                                }
                                //  }
                            }
                            if (keyData == Keys.Enter)
                            {

                                Highlited();
                                int Cell_Cur = Gv_Deed.CurrentCell.ColumnIndex;
                                int Row_Cur = Gv_Deed.CurrentRow.Index;
                               
                                if (Tab_Change < dt_Select.Rows.Count)
                                {
                                   
                                    if (dt_Select.Rows.Count > 0  )
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change+1).ToString();
                                    }
                                    else
                                    {
                                        X = 0;
                                        Y = 0;
                                        Width = 0;
                                        Height = 0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text = "0";
                                    }
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);

                                    imageBox.SelectionColor = Color.Yellow;

                                    Esc = 1;


                                    //if (Status == 0)
                                    //{
                                            imageBox.HorizontalScroll.Value = X;
                                            imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        //}
                                    //int cx;
                                    //int cy;
                                    int cx = imageBox.Image.Height;
                                    int cy = imageBox.Height;

                                    //imageBox.ScrollTo(X, (Y * (imageBox.Image.Height / imageBox.Height)));
                                    //imageBox.AutoScrollPosition = new Point(X, (Y * (imageBox.Image.Height / imageBox.Height)));

                                    //if (Y > imageBox.Height)
                                    //{
                                    //    imageBox.VerticalScroll.Value=  imageBox.Height;
                                    //}
                                    //else
                                    //{
                                  
                                    Colr = 1;

                                    Gv_Deed.BeginEdit(true);
                                   

                                    Tab_Change = Tab_Change + 1;
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Deed_Id", Gv_Deed.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);
                                       
                                    }
                                   // lbl_Current_Mark.Text = Tab_Change.ToString();
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Deed_Id", Gv_Deed.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);

                                    }
                                    Tab_Change = 1;
                                    if (Gv_Deed.CurrentCell.ColumnIndex < Gv_Deed.ColumnCount-1)
                                    {
                                        Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                                        Marker_Deed_Id = Gv_Deed.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Deed_Id", Gv_Deed.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Deed_Information", Gv_Deed.Columns[Gv_Deed.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Deed.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", htComments);

                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count>0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                      
                                    }
                               

                                }
                               
                              

                                mul_Select = 0;
                             //   Gv_Deed.BeginEdit(true);
                               
                                return true;
                               
                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                        }
                        Gv_Deed.BeginEdit(true); 
                    }
                }

                //Mortgage

                if (Tab_Page == "Mortgage")
                {

                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_mortgage.CurrentCell != null)
                    {
                        if (Gv_mortgage.CurrentCell.ColumnIndex <= Gv_mortgage.Columns.Count - 1)
                        {
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //  Highlited();
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_mortgage.Rows.Count >= Gv_mortgage.CurrentRow.Index - 1 && Gv_mortgage.Columns.Count >= Gv_mortgage.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_mortgage.CurrentCell = Gv_mortgage.Rows[Gv_mortgage.CurrentRow.Index].Cells[Gv_mortgage.CurrentCell.ColumnIndex];
                                    Gv_mortgage.BeginEdit(true);
                                    Marker_Mortgage_Id = Gv_mortgage.CurrentRow.Cells[0].Value.ToString();
                                   // Information = Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    if (Gv_mortgage.ColumnCount-1 > Gv_mortgage.CurrentCell.ColumnIndex)
                                    {
                                        Information = Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                    //else
                                    //{
                                    //    Information = Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    //}
                                    Cur_Row = Gv_mortgage.CurrentRow.Index;
                                    Cur_Col = Gv_mortgage.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();
                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();
                                    }
                                    lbl_Current_Mark.Text = "0";
                                    //imageBox.HorizontalScroll.Value = X;
                                    //imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                }
                            }
                            if (keyData == Keys.Enter)
                            {
                                Highlited();
                                int Cell_Cur = Gv_mortgage.CurrentCell.ColumnIndex;
                                int Row_Cur = Gv_mortgage.CurrentRow.Index;
                             
                                if (Tab_Change < dt_Select.Rows.Count)
                                {
                                    //if (Gv_mortgage.Rows.Count > 1)
                                    //{
                                    //    Gv_mortgage.CurrentCell = Gv_mortgage.Rows[Gv_mortgage.CurrentRow.Index - 1].Cells[Gv_mortgage.CurrentCell.ColumnIndex];
                                    //}
                                    // lbl_Markno.Text = (Tab_Change + 1).ToString();
                                    if (dt_Select.Rows.Count > 0)
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                                    }
                                    else
                                    {
                                        X = 0;
                                        Y = 0;
                                        Width = 0;
                                        Height = 0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text = "0";
                                    }
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionColor = Color.Yellow;
                                    Esc = 1;
                                    imageBox.HorizontalScroll.Value = X;
                                    imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                    Gv_mortgage.BeginEdit(true);
                                    Colr = 1;
                                    Tab_Change = Tab_Change + 1;
                                   // lbl_Current_Mark.Text = Tab_Change.ToString();
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();
                                        htComments.Clear();
                                        dtComments.Clear();
                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                                    }
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();
                                        htComments.Clear();
                                        dtComments.Clear();
                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value",Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                                    }
                                    Tab_Change = 1;
                                    if (Gv_mortgage.CurrentCell.ColumnIndex < Gv_mortgage.ColumnCount - 1)
                                    {
                                        Gv_mortgage.CurrentCell = Gv_mortgage.Rows[Gv_mortgage.CurrentRow.Index].Cells[Gv_mortgage.CurrentCell.ColumnIndex + 1];
                                        Marker_Mortgage_Id = Gv_mortgage.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();
                                            htComments.Clear();
                                            dtComments.Clear();
                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_mortgage.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count > 0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                    }
                                }
                              
                                //if (Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString() == "Dated Date" || Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString() == "Recorded Date"
                                //       || Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString() == "Maturity Date" || Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString() == "Latest Assignment Recorded Date" ||
                                //       Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString() == "Latest Assignment Dated Date")
                                //{
                                //    if (ValidateDate(Value_Data) != false)
                                //    {
                                //        Hashtable htComments = new Hashtable();
                                //        DataTable dtComments = new DataTable();
                                //        htComments.Clear();
                                //        dtComments.Clear();
                                //        htComments.Add("@Trans", "UPDATE");
                                //        htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                //        htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //        htComments.Add("@Value", cnt.Text);
                                //        dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);

                                //    }
                                //}
                                //else
                                //{
                                //    Hashtable htComments = new Hashtable();
                                //    DataTable dtComments = new DataTable();
                                //    htComments.Clear();
                                //    dtComments.Clear();
                                //    htComments.Add("@Trans", "UPDATE");
                                //    htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                //    htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //    htComments.Add("@Value", cnt.Text);
                                //    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);

                                //}
                                mul_Select = 0;
                                Gv_mortgage.BeginEdit(true);

                                return true;
                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                        }
                    }
                }




                //Tax

                if (Tab_Page == "Tax")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_Tax.CurrentCell != null)
                    {
                        if (Gv_Tax.CurrentCell.ColumnIndex <= Gv_Tax.Columns.Count - 1)
                        {
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //  Highlited();
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_Tax.Rows.Count >= Gv_Tax.CurrentRow.Index - 1 && Gv_Tax.Columns.Count >= Gv_Tax.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_Tax.CurrentCell = Gv_Tax.Rows[Gv_Tax.CurrentRow.Index].Cells[Gv_Tax.CurrentCell.ColumnIndex];
                                    Gv_Tax.BeginEdit(true);
                                    Marker_Tax_Id = Gv_Tax.CurrentRow.Cells[0].Value.ToString();
                                //    Information = Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    if (Gv_Tax.CurrentCell.ColumnIndex < Gv_Tax.Columns.Count-1)
                                    {
                                        Information = Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                    //else
                                    //{
                                    //    Information = Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    //}
                                    Cur_Row = Gv_Tax.CurrentRow.Index;
                                    Cur_Col = Gv_Tax.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                   if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();

                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();

                                    }

                                   lbl_Current_Mark.Text = "0";
                                    //imageBox.HorizontalScroll.Value = X;
                                    //imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                }


                                //  }
                            }
                            if (keyData == Keys.Enter)
                            {
                                Highlited();
                                int Cell_Cur = Gv_Tax.CurrentCell.ColumnIndex;
                                int Row_Cur = Gv_Tax.CurrentRow.Index;
                                if (Tab_Change < dt_Select.Rows.Count)
                                {

                                    //if (Gv_Tax.Rows.Count > 1)
                                    //{
                                    //    Gv_Tax.CurrentCell = Gv_Tax.Rows[Gv_Tax.CurrentRow.Index - 1].Cells[Gv_Tax.CurrentCell.ColumnIndex];
                                    //}
                                    //  lbl_Markno.Text = (Tab_Change + 1).ToString();
                                    if (dt_Select.Rows.Count > 0)
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                                    }
                                    else
                                    {
                                        X = 0;
                                        Y = 0;
                                        Width = 0;
                                        Height = 0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text = "0";
                                    }
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionColor = Color.Yellow;
                                    Esc = 1;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    Esc = 1;
                                    imageBox.HorizontalScroll.Value = X;
                                    imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                    Gv_Tax.BeginEdit(true);
                                    Colr = 1;
                                    Tab_Change = Tab_Change + 1;
                                   // lbl_Current_Mark.Text = Tab_Change.ToString();
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value",Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                    }
                                    
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", Value_Data);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                    }
                                    Tab_Change = 1;
                                    if (Gv_Tax.CurrentCell.ColumnIndex < Gv_Tax.ColumnCount - 1)
                                    {
                                        Gv_Tax.CurrentCell = Gv_Tax.Rows[Gv_Tax.CurrentRow.Index].Cells[Gv_Tax.CurrentCell.ColumnIndex + 1];
                                        Marker_Tax_Id = Gv_Tax.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Tax.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count > 0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                    }
                                }
                               
                               
                                //if (Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString() == "Tax Date")
                                //{
                                //    if (ValidateDate(Value_Data) != false)
                                //    {
                                       
                                //            Gv_mortgage.CurrentCell = Gv_mortgage.Rows[Row_Cur].Cells[Cell_Cur];
                                        
                                //    }
                                //}
                                //else
                                //{
                                //    Hashtable htComments = new Hashtable();
                                //    DataTable dtComments = new DataTable();

                                //    htComments.Add("@Trans", "UPDATE");
                                //    htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                                //    htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //    htComments.Add("@Value", cnt.Text);
                                //    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                 
                                //        Tax_Grid_Bind();
                                //        Gv_Tax.CurrentCell = Gv_Tax.Rows[Row_Cur].Cells[Cell_Cur];
                                   
                                //}
                                mul_Select = 0;
                                Gv_Tax.BeginEdit(true);
                                return true;
                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                        }
                    }
                }
                //Judgement

                if (Tab_Page == "Judgement")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_Judgement.CurrentCell != null)
                    {
                        if (Gv_Judgement.CurrentCell.ColumnIndex <= Gv_Judgement.Columns.Count - 1)
                        {
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //  Highlited();
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_Judgement.Rows.Count >= Gv_Judgement.CurrentRow.Index - 1 && Gv_Judgement.Columns.Count >= Gv_Judgement.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_Judgement.CurrentCell = Gv_Judgement.Rows[Gv_Judgement.CurrentRow.Index].Cells[Gv_Judgement.CurrentCell.ColumnIndex];
                                    Gv_Judgement.BeginEdit(true);
                                    Marker_Judgement_Id = Gv_Judgement.CurrentRow.Cells[0].Value.ToString();
                                    //Information = Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    if (Gv_Judgement.CurrentCell.ColumnIndex < Gv_Judgement.Columns.Count-1)
                                    {
                                        Information = Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                    //else
                                    //{
                                    //    Information = Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    //}
                                    Cur_Row = Gv_Judgement.CurrentRow.Index;
                                    Cur_Col = Gv_Judgement.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();
                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();

                                    }

                                    lbl_Current_Mark.Text = "0";
                                    //imageBox.HorizontalScroll.Value = X;
                                    //imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                }
                            }
                            if (keyData == Keys.Enter)
                            {
                                Highlited();
                                int Cell_Cur = Gv_Judgement.CurrentCell.ColumnIndex;
                                int Row_Cur = Gv_Judgement.CurrentRow.Index;
                                if (Tab_Change < dt_Select.Rows.Count)
                                {

                                    //if (Gv_Judgement.Rows.Count > 1)
                                    //{
                                    //    Gv_Judgement.CurrentCell = Gv_Judgement.Rows[Gv_Judgement.CurrentRow.Index - 1].Cells[Gv_Judgement.CurrentCell.ColumnIndex];
                                    //}
                                    //lbl_Markno.Text = (Tab_Change + 1).ToString();
                                    if (dt_Select.Rows.Count > 0)
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                                    }
                                    else
                                    {
                                        X = 0;
                                        Y = 0;
                                        Width = 0;
                                        Height = 0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text = "0";
                                    }
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionColor = Color.Yellow;
                                    Esc = 1;
                                  
                                    Esc = 1;
                                    imageBox.HorizontalScroll.Value = X;
                                    imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                    Gv_Judgement.BeginEdit(true);
                                    Colr = 1;
                                    Tab_Change = Tab_Change + 1;
                                   // lbl_Current_Mark.Text = Tab_Change.ToString();
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                    }
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                    }
                                    Tab_Change = 1;
                                    if (Gv_Judgement.CurrentCell.ColumnIndex < Gv_Judgement.ColumnCount - 1)
                                    {
                                        Gv_Judgement.CurrentCell = Gv_Judgement.Rows[Gv_Judgement.CurrentRow.Index].Cells[Gv_Judgement.CurrentCell.ColumnIndex + 1];
                                        Marker_Judgement_Id = Gv_Judgement.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Judgement.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count > 0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                    }
                                }
                             
                                //if (Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString() == "Dated Date" || Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString() == "Recorded Date")
                                //{
                                //    if (ValidateDate(Value_Data) != false)
                                //    {
                                //        Hashtable htComments = new Hashtable();
                                //        DataTable dtComments = new DataTable();

                                //        htComments.Add("@Trans", "UPDATE");
                                //        htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                //        htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //        htComments.Add("@Value", cnt.Text);
                                //        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                       
                                //    }
                                //}
                                //else
                                //{
                                //    Hashtable htComments = new Hashtable();
                                //    DataTable dtComments = new DataTable();

                                //    htComments.Add("@Trans", "UPDATE");
                                //    htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                //    htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //    htComments.Add("@Value", cnt.Text);
                                //    dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                //    //if (Cell_Cur == 14)
                                //    //{
                                //    //    Judgement_Grid_Bind();
                                //    //    Gv_Judgement.CurrentCell = Gv_Judgement.Rows[Row_Cur].Cells[Cell_Cur];
                                //    //}
                                //}
                                mul_Select = 0;
                                Gv_Judgement.BeginEdit(true);
                                return true;
                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                        }
                    }
                }

                //Additional Information

                if (Tab_Page == "Additional Information")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_Additional_Information.CurrentCell != null)
                    {
                        if (Gv_Additional_Information.CurrentCell.ColumnIndex <= Gv_Additional_Information.Columns.Count - 1)
                        {
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //  Highlited();
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_Additional_Information.Rows.Count >= Gv_Additional_Information.CurrentRow.Index - 1 && Gv_Additional_Information.Columns.Count >= Gv_Additional_Information.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_Additional_Information.CurrentCell = Gv_Additional_Information.Rows[Gv_Additional_Information.CurrentRow.Index].Cells[Gv_Additional_Information.CurrentCell.ColumnIndex];
                                    Gv_Additional_Information.BeginEdit(true);
                                    Marker_Additional_Information_Id = Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString();
                                    if (Gv_Additional_Information.CurrentCell.ColumnIndex < Gv_Additional_Information.Columns.Count - 1)
                                    {
                                        Information = Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                  //  Information = Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    Cur_Row = Gv_Additional_Information.CurrentRow.Index;
                                    Cur_Col = Gv_Additional_Information.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();
                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();

                                    }

                                    lbl_Current_Mark.Text = "0";
                                    //imageBox.HorizontalScroll.Value = X;
                                    //imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                }
                            }
                            if (keyData == Keys.Enter)
                            {
                                Highlited();
                                if (Tab_Change < dt_Select.Rows.Count)
                                {

                                    //if (Gv_Additional_Information.Rows.Count > 1)
                                    //{
                                    //    Gv_Additional_Information.CurrentCell = Gv_Additional_Information.Rows[Gv_Additional_Information.CurrentRow.Index - 1].Cells[Gv_Additional_Information.CurrentCell.ColumnIndex];
                                    //}
                                    //  lbl_Markno.Text = (Tab_Change + 1).ToString();
                                    if (dt_Select.Rows.Count > 0)
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                                    }
                                    else
                                    {
                                        X = 0;
                                        Y = 0;
                                        Width = 0;
                                        Height = 0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text ="0";
                                    }
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionColor = Color.Yellow;
                                    Esc = 1;
                                    imageBox.HorizontalScroll.Value = X;
                                    imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                    Gv_Additional_Information.BeginEdit(true);
                                    Colr = 1;
                                    Tab_Change = Tab_Change + 1;
                                   // lbl_Current_Mark.Text = Tab_Change.ToString();
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Additional_Information_Id", Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                                    }
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Additional_Information_Id", Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                                    }
                                    Tab_Change = 1;
                                    if (Gv_Additional_Information.CurrentCell.ColumnIndex < Gv_Additional_Information.ColumnCount - 1)
                                    {
                                        Gv_Additional_Information.CurrentCell = Gv_Additional_Information.Rows[Gv_Additional_Information.CurrentRow.Index].Cells[Gv_Additional_Information.CurrentCell.ColumnIndex + 1];
                                        Marker_Additional_Information_Id = Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Additional_Information_Id", Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Additional_Information.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count > 0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                    }
                                }
                              
                                //if (Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString() == "Dated Date" || Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString() == "Recorded Date")
                                //{
                                //    if (ValidateDate(Value_Data) != false)
                                //    {
                                        
                                //    }
                                //}
                                //else
                                //{
                                //    Hashtable htComments = new Hashtable();
                                //    DataTable dtComments = new DataTable();

                                //    htComments.Add("@Trans", "UPDATE");
                                //    htComments.Add("@Marker_Additional_Information_Id", Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString());
                                //    htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //    htComments.Add("@Value", cnt.Text);
                                //    dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);

                                //}
                                mul_Select = 0;
                                Gv_Additional_Information.BeginEdit(true);
                                return true;

                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                           
                        }



                    }

                }


                //Assessment

                if (Tab_Page == "Assessment")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new DataTable();
                    if (Gv_Assessment.CurrentCell != null)
                    {
                        if (Gv_Assessment.CurrentCell.ColumnIndex <= Gv_Assessment.Columns.Count - 1)
                        {
                            if (keyData == Keys.Tab)
                            {
                                Colr = 0;
                                //  Highlited();
                                //if (Tab_Change < dt_Select.Rows.Count)
                                //{
                                Tab_Change = 0;
                                htComments1.Clear();
                                dtComments1.Clear();
                                if (Gv_Assessment.Rows.Count >= Gv_Assessment.CurrentRow.Index - 1 && Gv_Assessment.Columns.Count >= Gv_Assessment.CurrentCell.ColumnIndex - 1)
                                {
                                    Gv_Assessment.CurrentCell = Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[Gv_Assessment.CurrentCell.ColumnIndex];
                                    Gv_Assessment.BeginEdit(true);
                                    Marker_Assessment_Id = Gv_Assessment.CurrentRow.Cells[0].Value.ToString();
                                    if (Gv_Assessment.CurrentCell.ColumnIndex < Gv_Assessment.Columns.Count - 1)
                                    {
                                        Information = Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex + 1].HeaderText.ToString();
                                    }
                                   
                                    Cur_Row = Gv_Assessment.CurrentRow.Index;
                                    Cur_Col = Gv_Assessment.CurrentCell.ColumnIndex;
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    if (Status == 1)
                                    {
                                        this.UpdateStatusBar();
                                        Highlited();
                                        Esc = 0;
                                        PageNo = Last_PageNo;
                                        Image_View();

                                    }

                                    lbl_Current_Mark.Text = "0";
                                    //imageBox.HorizontalScroll.Value = X;
                                    //imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                }


                                // }
                            }
                            if (keyData == Keys.Enter)
                            {
                                Highlited();
                                if (Tab_Change < dt_Select.Rows.Count)
                                {

                                    //if (Gv_Assessment.Rows.Count > 1)
                                    //{
                                    //    Gv_Assessment.CurrentCell = Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index - 1].Cells[Gv_Assessment.CurrentCell.ColumnIndex];
                                    //}
                                    // lbl_Markno.Text = (Tab_Change + 1).ToString();
                                    if (dt_Select.Rows.Count > 0)
                                    {
                                        X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                        Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                        Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                        Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                        PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                        lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                                    }
                                    else
                                    {
                                        X =0;
                                        Y = 0;
                                        Width = 0;
                                        Height =0;
                                        PageNo = 0;
                                        lbl_Current_Mark.Text = "0";
                                    }
                                    imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                                    imageBox.SelectionColor = Color.Yellow;
                                    
                                    Rectangle Rect = new Rectangle(X, Y, Width, Height);
                                    Esc = 1;
                                    imageBox.HorizontalScroll.Value = X;
                                    imageBox.VerticalScroll.Value = (Y * 2) - Height;
                                    imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                    Gv_Assessment.BeginEdit(true);
                                    Colr = 1;
                                    Tab_Change = Tab_Change + 1;
                                    //lbl_Current_Mark.Text = Tab_Change.ToString();
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Assessment_Id", Gv_Assessment.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                    }
                                }
                                else
                                {
                                    if (cnt != null)
                                    {
                                        Hashtable htComments = new Hashtable();
                                        DataTable dtComments = new DataTable();

                                        htComments.Add("@Trans", "UPDATE");
                                        htComments.Add("@Marker_Assessment_Id", Gv_Assessment.CurrentRow.Cells[0].Value.ToString());
                                        htComments.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                                        htComments.Add("@Value", cnt.Text);
                                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                    }
                                    Tab_Change = 1;
                                    if (Gv_Assessment.CurrentCell.ColumnIndex < Gv_Assessment.ColumnCount - 1)
                                    {
                                        Gv_Assessment.CurrentCell = Gv_Assessment.Rows[Gv_Assessment.CurrentRow.Index].Cells[Gv_Assessment.CurrentCell.ColumnIndex + 1];
                                        Marker_Assessment_Id = Gv_Assessment.CurrentRow.Cells[0].Value.ToString();
                                        Information = Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString();
                                        if (cnt != null)
                                        {
                                            Hashtable htComments = new Hashtable();
                                            DataTable dtComments = new DataTable();

                                            htComments.Add("@Trans", "UPDATE");
                                            htComments.Add("@Marker_Assessment_Id", Gv_Assessment.CurrentRow.Cells[0].Value.ToString());
                                            htComments.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                                            htComments.Add("@Value", Gv_Assessment.CurrentCell.Value.ToString());
                                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                        }
                                        Highlited();
                                        if (dt_Select.Rows.Count > 0)
                                        {
                                            imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                            imageBox.SelectionColor = Color.Yellow;
                                            imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                            imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                        }
                                    }
                                }
                             
                                //if (Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString() == "Dated Date" || Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString() == "Recorded Date")
                                //{
                                //    if (ValidateDate(Value_Data) != false)
                                //    {
                                       
                                //    }
                                //}
                                //else
                                //{
                                //    Hashtable htComments = new Hashtable();
                                //    DataTable dtComments = new DataTable();

                                //    htComments.Add("@Trans", "UPDATE");
                                //    htComments.Add("@Marker_Assessment_Id", Gv_Assessment.CurrentRow.Cells[0].Value.ToString());
                                //    htComments.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                                //    htComments.Add("@Value", cnt.Text);
                                //    dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                //}
                                mul_Select = 0;
                                Gv_Assessment.BeginEdit(true);
                                return true;
                            }
                            if (keyData == Keys.Escape)
                            {
                                Esc = 0;
                            }
                        }
                    }
                }

                if (Tab_Page == "Legal Description")
                {
                    if (keyData == Keys.Enter)
                    {
                        Txt_Legal_Description.Text = Txt_Legal_Description.Text + "\r\n";
                        Highlited();
                        if (Tab_Change < dt_Select.Rows.Count)
                        {
                            //  lbl_Markno.Text = (Tab_Change+1).ToString();
                            if (dt_Select.Rows.Count > 0)
                            {
                                X = int.Parse(dt_Select.Rows[Tab_Change]["X"].ToString());
                                Y = int.Parse(dt_Select.Rows[Tab_Change]["Y"].ToString());
                                Width = int.Parse(dt_Select.Rows[Tab_Change]["Width"].ToString());
                                Height = int.Parse(dt_Select.Rows[Tab_Change]["Height"].ToString());
                                PageNo = int.Parse(dt_Select.Rows[Tab_Change]["PageNo"].ToString());
                                lbl_Current_Mark.Text = (Tab_Change + 1).ToString();
                            }
                            else
                            {
                                X = 0;
                                Y = 0;
                                Width = 0;
                                Height = 0;
                                PageNo = 0;
                                lbl_Current_Mark.Text ="0";
                            }
                            imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
                            imageBox.SelectionColor = Color.Yellow;
                            Esc = 1;
                            LegalSubmit();
                            Rectangle Rect = new Rectangle(X, Y, Width, Height);
                            imageBox.HorizontalScroll.Value = X;
                            imageBox.VerticalScroll.Value = (Y * 2) - Height;
                            imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                          //  Esc = 0;
                            Colr = 1;
                            Tab_Change = Tab_Change + 1;
                            //lbl_Current_Mark.Text = Tab_Change.ToString();
                        }
                        else
                        {
                            Tab_Change = 0;
                            lbl_Current_Mark.Text = Tab_Change.ToString();
                        }
                        Txt_Legal_Description.SelectionStart = Txt_Legal_Description.Text.Length;
                        mul_Select = 0;

                        return true;
                    }
                    if (keyData == Keys.Escape)
                    {
                        Esc = 0;
                    }
                }

               

            return base.ProcessCmdKey(ref msg, keyData);
         

        }
        protected void LegalSubmit()
        {

            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            int Count_Value = 0;
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "BIND");
            ht.Add("@Order_Id", Orderid);
            dt = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", ht);
          
            Count_Value = dt.Rows.Count + 1;
            if (dt.Rows.Count <= 0)
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Legal_Description_Number", Count_Value);
                //htComments.Add("@Legal_Description_Information", "Description");
                htComments.Add("@Order_Id", Orderid);
                htComments.Add("@Path", "Legal Description");
                MarkerId = dataaccess.ExecuteSPForScalar("Sp_Marker_Legal_Description", htComments);

                htComments1.Add("@Trans", "INSERT");
                htComments1.Add("@Marker_Legal_Description_Id", MarkerId);
                htComments1.Add("@Value", Txt_Legal_Description.Text);
                htComments1.Add("@Legal_Description_Information", "Description");
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments1);
                htComments1.Clear();
                dtComments1.Clear();
                Marker_Legal_Description_Id =MarkerId.ToString();

            }

            htComments1.Clear();
            dtComments1.Clear();
                htComments1.Add("@Trans", "UPDATE");
                htComments1.Add("@Marker_Legal_Description_Id", Marker_Legal_Description_Id);
                htComments1.Add("@Value", Txt_Legal_Description.Text);
                htComments1.Add("@Legal_Description_Information", "Description");
                dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", htComments1);
                htComments1.Clear();
                dtComments1.Clear();
            // txt_Legal.Text = "";
           
        }
        private void Gv_Deed_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Gv_Deed.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void imageBox_Click(object sender, EventArgs e)
        {
            //imageBox.Location = new System.Drawing.Point(X, Y);
            //imageBox.Size = new System.Drawing.Size(Width, Height);
            //Point point = imageBox.PointToClient(Cursor.Position);
           
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox1.Checked == true)
            {
                Status = 1;
                Deed_Grid_Bind();
                Mortgage_Grid_Bind();
                Tax_Grid_Bind();
                Judgement_Grid_Bind();
                Assessment_Grid_Bind();
                Additional_Information_Grid_Bind();
            }
            else
            {
                Status = 0;
                Deed_Grid_Bind();
                Mortgage_Grid_Bind();
                Tax_Grid_Bind();
                Judgement_Grid_Bind();
                Assessment_Grid_Bind();
                Additional_Information_Grid_Bind();
            }

        }

        private void imageBox_SelectionRegionChanged(object sender, EventArgs e)
        {
            //imageBox.SelectionRegion = new Rectangle(X, Y, Width, Height);
            //imageBox.AutoScrollPosition = new Point(X, Y - 150);
            if (Status == 0)
            {
                imageBox.SelectionColor = Color.Green;
                if (Gv_Deed.CurrentCell != null && checkBox1.Checked != true && Cur_Col != 1 && Gv_Deed.CurrentCell.Style.BackColor != System.Drawing.Color.Chocolate)
                {
                   // Gv_Deed.CurrentCell.Value = this.FormatRectangle(imageBox.SelectionRegion);
                }
            }
        }

        private void imageBox_MouseClick(object sender, MouseEventArgs e)
        {
                for (int i = 0; i < dt_Select.Rows.Count; i++)
                {
                    int X1 = int.Parse(dt_Select.Rows[i]["X"].ToString());
                    int Y1 = int.Parse(dt_Select.Rows[i]["Y"].ToString());
                    int Width1 = int.Parse(dt_Select.Rows[i]["Width"].ToString());
                    int Height1 = int.Parse(dt_Select.Rows[i]["Height"].ToString());
                    int PageNo1 = int.Parse(dt_Select.Rows[i]["PageNo"].ToString());
                    _SelectItem = new RectangleF(X1, Y1, Width1, Height1);
                    if (_SelectItem.Contains(imageBox.PointToImage(e.Location)))
                    {

                        if (Tab_Page == "Deed")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Deed_Child_Id"].ToString());
                        }
                        else if (Tab_Page == "Mortgage")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Mortgage_Child_Id"].ToString());
                        }
                        else if (Tab_Page == "Tax")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Tax_Child_Id"].ToString());
                        }
                        else if (Tab_Page == "Assessment")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Assessment_Child_Id"].ToString());
                        }

                        else if (Tab_Page == "Additional Information")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Additional_Information_Child_Id"].ToString());
                        }
                        else if (Tab_Page == "Judgement")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Judgment_Child_Id"].ToString());
                        }
                        else if (Tab_Page == "Legal Description")
                        {
                            Selection_Id = int.Parse(dt_Select.Rows[i]["Marker_Legal_Description_Child_Id"].ToString());
                        }


                        imageBox.SelectionRegion = new Rectangle(X1, Y1, Width1, Height1);
                        imageBox.SelectionColor = Color.Red;
                    }
                }
            
        }
        private void imageBox_KeyDown(object sender, KeyEventArgs e)
        {
            Last_PageNo = PageNo;
            if (Tab_Page == "Deed")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Deed_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Deed_Child", ht_Delete);
                       
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Deed_Grid_Bind();
                    }
                }
            }

            if (Tab_Page == "Mortgage")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Mortgage_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Mortgage_Grid_Bind();
                    }
                }
            }
            if (Tab_Page == "Judgement")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Judgment_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Judgement_Grid_Bind();
                    }
                }
            }
            if (Tab_Page == "Tax")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Tax_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Tax_Grid_Bind();
                    }
                }
            }
            if (Tab_Page == "Assessment")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Assessment_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Assessment_Grid_Bind();
                    }
                }
            }

            if (Tab_Page == "Additional Information")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Additional_Information_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Additional_Information_Grid_Bind();
                    }
                }
            }



            if (Tab_Page == "Legal Description")
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Selection_Id != null)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Marker_Legal_Description_Child_Id", Selection_Id);
                        dt_Delete = dataaccess.ExecuteSP("Sp_Marker_Legal_Description_Child", ht_Delete);
                        Highlited();
                        PageNo = Last_PageNo;
                        Image_View();
                        Legal_Description_Grid_Bind();
                    }
                }
            }



        }

        private void btn_Mortgage_Click(object sender, EventArgs e)
        {
            Hashtable htCount = new Hashtable();
            DataTable dtCount = new System.Data.DataTable();
            htCount.Add("@Trans", "BIND");
            htCount.Add("@Order_Id", Orderid);
            dtCount = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htCount);

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            int Mortgage_Count = dtCount.Rows.Count + 1;
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Mortgage_Number", Mortgage_Count);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path", "Mortgage");
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);
            Mortgage_Grid_Bind();
        }

        private void btn_Tax_Click(object sender, EventArgs e)
        {

            Hashtable htCount = new Hashtable();
            DataTable dtCount = new System.Data.DataTable();
            htCount.Add("@Trans", "BIND");
            htCount.Add("@Order_Id", Orderid);
            dtCount = dataaccess.ExecuteSP("Sp_Marker_Tax", htCount);

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            int Tax_Count = dtCount.Rows.Count + 1;
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Tax_Number", Tax_Count);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path", "Tax");
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
            Tax_Grid_Bind();
        }

        private void btn_Judgement_Click(object sender, EventArgs e)
        {
            Hashtable htCount = new Hashtable();
            DataTable dtCount = new System.Data.DataTable();
            htCount.Add("@Trans", "BIND");
            htCount.Add("@Order_Id", Orderid);
            dtCount = dataaccess.ExecuteSP("Sp_Marker_Judgment", htCount);

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            int Judgement_Count = dtCount.Rows.Count + 1;
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Judgment_Number", Judgement_Count);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path", "Judgment");
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
            Judgement_Grid_Bind();
        }

        private void btn_Assessment_Click(object sender, EventArgs e)
        {
            Hashtable htCount = new Hashtable();
            DataTable dtCount = new System.Data.DataTable();
            htCount.Add("@Trans", "BIND");
            htCount.Add("@Order_Id", Orderid);
            dtCount = dataaccess.ExecuteSP("Sp_Marker_Assessment", htCount);

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            int Assessment_Count = dtCount.Rows.Count + 1;
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Assessment_Number", Assessment_Count);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path", "Assessment");
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
            Assessment_Grid_Bind();
        }

        private void btn_Legal_Description_Click(object sender, EventArgs e)
        {
            //Hashtable htCount = new Hashtable();
            //DataTable dtCount = new System.Data.DataTable();
            //htCount.Add("@Trans", "BIND");
            //htCount.Add("@Order_Id", Orderid);
            //dtCount = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htCount);

            //Hashtable htComments = new Hashtable();
            //DataTable dtComments = new System.Data.DataTable();
            //int Legal_Description_Count = dtCount.Rows.Count + 1;
            //htComments.Add("@Trans", "INSERT");
            //htComments.Add("@Legal_Description_Number", Legal_Description_Count);
            //htComments.Add("@Order_Id", Orderid);
            //htComments.Add("@Path", "Legal_Description");
            //dtComments = dataaccess.ExecuteSP("Sp_Marker_Legal_Description", htComments);
            Legal_Description_Grid_Bind();
        }

        private void Gv_mortgage_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (Gv_mortgage.Rows.Count > 0)
            {
                if (Gv_mortgage.Rows.Count - 1 >= e.RowIndex)
                {
                    Tab_Change = 0;
                    if (e.RowIndex != -1)
                    {
                        Last_PageNo = PageNo;
                        Marker_Mortgage_Id = Gv_mortgage.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_mortgage.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_mortgage.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_mortgage.Rows[e.RowIndex].Cells[0].Value != null)
                        {

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage", htComments);
                            Mortgage_Grid_Bind();
                        }
                        if (Gv_mortgage.Rows.Count > 0)
                        {
                            if (Gv_mortgage.Rows.Count - 1 >= e.RowIndex)
                            {
                                if (Gv_mortgage.Rows[e.RowIndex].Cells[0].Value != null)
                                {
                                    if (Gv_mortgage.CurrentRow != null)
                                    {
                                        for (int i = 0; i < Gv_mortgage.Columns.Count; i++)
                                        {
                                            if (Gv_mortgage.Rows[e.RowIndex].Cells[i].Value != null)
                                            {
                                                htComments.Clear();
                                                dtComments.Clear();

                                                htComments.Add("@Trans", "UPDATE");
                                                htComments.Add("@Marker_Mortgage_Id", Gv_mortgage.CurrentRow.Cells[0].Value.ToString());
                                                htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[i].HeaderText.ToString());
                                                htComments.Add("@Value", Gv_mortgage.Rows[e.RowIndex].Cells[i].Value.ToString());
                                                dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                                            }
                                        }
                                    }

                                }
                            }
                        }

                        
                     
                    }
                }
            }
        }
        private void Gv_Tax_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Tax.Rows.Count > 0)
            {
                if (Gv_Tax.Rows.Count - 1 >= e.RowIndex)
                {
                    if (e.RowIndex != -1)
                    {
                        Last_PageNo = PageNo;
                        Tab_Change = 0;
                        Marker_Tax_Id = Gv_Tax.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_Tax.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_Tax.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_Tax.Rows[e.RowIndex].Cells[0].Value != null)
                        {

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Tax_Id", Gv_Tax.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax", htComments);
                            Tax_Grid_Bind();
                        }
                        if (Gv_Tax.Rows.Count > 0)
                        {
                             if (Gv_Tax.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Tax.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        if (Gv_Tax.CurrentRow != null)
                        {
                            for (int i = 0; i < Gv_Tax.Columns.Count; i++)
                            {
                                if (Gv_Tax.Rows[e.RowIndex].Cells[i].Value != null)
                                {//{
                                    htComments.Clear();
                                    dtComments.Clear();

                                    htComments.Add("@Trans", "UPDATE");
                                    htComments.Add("@Marker_Tax_Id", Gv_Tax.CurrentRow.Cells[0].Value.ToString());
                                    htComments.Add("@Tax_Information", Gv_Tax.Columns[i].HeaderText.ToString());
                                    htComments.Add("@Value", Gv_Tax.Rows[e.RowIndex].Cells[i].Value.ToString());
                                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                                }
                            }
                        }
                    }
                            }
                        }
                       
                      
                    }
                }
            }
            
        }

        private void Gv_Judgement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Judgement.Rows.Count > 0)
            {
                if (Gv_Judgement.Rows.Count - 1 >= e.RowIndex)
                {
                    if (e.RowIndex != -1)
                    {
                        Last_PageNo = PageNo;
                        Tab_Change = 0;
                        Marker_Judgement_Id = Gv_Judgement.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_Judgement.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_Judgement.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_Judgement.Rows[e.RowIndex].Cells[0].Value != null)
                        {

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Judgment_Id", Gv_Judgement.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment", htComments);
                            Judgement_Grid_Bind();
                        }
                        if (Gv_Judgement.Rows.Count > 0)
                        {
                            if (Gv_Judgement.Rows.Count - 1 >= e.RowIndex)
                            {
                                if (Gv_Judgement.Rows[e.RowIndex].Cells[0].Value != null)
                                {
                                    if (Gv_Judgement.CurrentRow != null)
                                    {
                                        for (int i = 0; i < Gv_Judgement.Columns.Count; i++)
                                        {
                                            if (Gv_Judgement.Rows[e.RowIndex].Cells[i].Value !=null)
                                            {
                                                htComments.Clear();
                                                dtComments.Clear();

                                                htComments.Add("@Trans", "UPDATE");
                                                htComments.Add("@Marker_Judgment_Id", Gv_Judgement.CurrentRow.Cells[0].Value.ToString());
                                                htComments.Add("@Judgment_Information", Gv_Judgement.Columns[i].HeaderText.ToString());
                                                htComments.Add("@Value", Gv_Judgement.Rows[e.RowIndex].Cells[i].Value.ToString());
                                                dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                       
                        
                    }
                }
            }
        }

        private void Gv_Assessment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Assessment.Rows.Count > 0)
            {
                if (Gv_Assessment.Rows.Count - 1 >= e.RowIndex)
                {
                    if (e.RowIndex != -1)
                    {
                        Last_PageNo = PageNo;
                        Tab_Change = 0;
                        Marker_Assessment_Id = Gv_Assessment.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_Assessment.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_Assessment.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_Assessment.Rows[e.RowIndex].Cells[0].Value != null)
                        {

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Assessment_Id", Gv_Assessment.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment", htComments);
                            Assessment_Grid_Bind();
                        }
                        if (Gv_Assessment.Rows.Count > 0)
                        {
                            if (Gv_Assessment.Rows.Count - 1 >= e.RowIndex)
                            {
                                if (Gv_Assessment.Rows[e.RowIndex].Cells[0].Value != null)
                                {
                                    if (Gv_Assessment.CurrentRow != null)
                                    {
                                        for (int i = 0; i < Gv_Assessment.Columns.Count; i++)
                                        {
                                            if (Gv_Assessment.Rows[e.RowIndex].Cells[i].Value != null)
                                            {
                                                htComments.Clear();
                                                dtComments.Clear();

                                                htComments.Add("@Trans", "UPDATE");
                                                htComments.Add("@Marker_Assessment_Id", Gv_Assessment.CurrentRow.Cells[0].Value.ToString());
                                                htComments.Add("@Assessment_Information", Gv_Assessment.Columns[i].HeaderText.ToString());
                                                htComments.Add("@Value", Gv_Assessment.Rows[e.RowIndex].Cells[i].Value.ToString());
                                                dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                       
                        
                    }
                }
            }
        }

        private void Gv_Additional_Information_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Additional_Information.Rows.Count > 0)
            {
                if (Gv_Additional_Information.Rows.Count-1 >= e.RowIndex)
                {
                    if (e.RowIndex != -1)
                    {
                        Last_PageNo = PageNo;
                        Tab_Change = 0;
                        Marker_Additional_Information_Id = Gv_Additional_Information.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Information = Gv_Additional_Information.Columns[e.ColumnIndex].HeaderText.ToString();
                        Pre_Row_Index = e.RowIndex;
                        Pre_Col_Index = e.ColumnIndex;
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();
                        if (Gv_Additional_Information.Columns[e.ColumnIndex].HeaderText.ToString() == "Delete" && Gv_Additional_Information.Rows[e.RowIndex].Cells[0].Value != null)
                        {

                            htComments.Add("@Trans", "DELETE");
                            htComments.Add("@Marker_Order_Additional_Information_Id", Gv_Additional_Information.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
                            Additional_Information_Grid_Bind();
                        }
                        if (Gv_Additional_Information.Rows.Count > 0)
                        {
                            if (Gv_Additional_Information.Rows.Count - 1 >= e.RowIndex)
                            {
                                if (Gv_Additional_Information.Rows[e.RowIndex].Cells[0].Value != null)
                                {
                                    if (Gv_Additional_Information.CurrentRow != null)
                                    {
                                        for (int i = 0; i < Gv_Additional_Information.Columns.Count; i++)
                                        {
                                            if (Gv_Additional_Information.Rows[e.RowIndex].Cells[i].Value != null)
                                            {
                                                htComments.Clear();
                                                dtComments.Clear();

                                                htComments.Add("@Trans", "UPDATE");
                                                htComments.Add("@Marker_Additional_Information_Id", Gv_Additional_Information.CurrentRow.Cells[0].Value.ToString());
                                                htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[i].HeaderText.ToString());
                                                htComments.Add("@Value", Gv_Additional_Information.Rows[e.RowIndex].Cells[i].Value.ToString());
                                                dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        
                       
                    }
                }
            }
        }
        private void Additional_Information_Grid_Bind()
        {
            Gv_Additional_Information.DataSource = null;
            Gv_Additional_Information.Columns.Clear();
            Hashtable htComments1 = new Hashtable();
            DataTable dtComments1 = new System.Data.DataTable();
            htComments1.Add("@Trans", "SELECT_ADDITIONAL_INFO");
            htComments1.Add("@Order_Id", Orderid);
            dtComments1 = dataaccess.ExecuteSP("Sp_Additional_Info_Rec", htComments1);
            if (dtComments1.Rows.Count > 0)
            {
                Gv_Additional_Information.DataSource = dtComments1;
                Gv_Additional_Information.Columns[0].Visible = false;
                lbl_AdditionalInfo.Visible = true;
                lbl_add_info_count.Text = dtComments1.Rows.Count.ToString();
                if (Status == 0)
                {
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Additional_Information.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Additional_Information.Rows.Count; i++)
                    {
                        Gv_Additional_Information.Rows[i].Cells[Gv_Additional_Information.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Additional_Information.Columns.Count; j++)
                        {


                            if (Gv_Additional_Information.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                                Gv_Additional_Information.Rows[i].Cells[j].ReadOnly = true;
                            }
                        }
                    }
                }
                else
                {
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    Gv_Additional_Information.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    for (int i = 0; i < Gv_Additional_Information.Rows.Count; i++)
                    {
                        Gv_Additional_Information.Rows[i].Cells[Gv_Additional_Information.Columns.Count - 1].Value = "Delete";
                        for (int j = 0; j < Gv_Additional_Information.Columns.Count; j++)
                        {


                            if (Gv_Additional_Information.Rows[i].Cells[j].Value == DBNull.Value)
                            {
                              //  Gv_Additional_Information.Rows[i].Cells[j].ReadOnly = true;
                            }
                        }
                    }
                }
                    
                for (int i = 0; i < Gv_Additional_Information.Rows.Count; i++)
                {

                    for (int j = 0; j < Gv_Additional_Information.Columns.Count; j++)
                    {
                        if (Status == 1)
                        {
                            if (j != 0)
                            {
                                if (Gv_Additional_Information.Rows[i].Cells[j].Value.ToString().Length >= 2)
                                {
                                    string HibemarkValue = Gv_Additional_Information.Rows[i].Cells[j].Value.ToString().Substring(0, 2).ToUpper();
                                    if (HibemarkValue == "X:")
                                    {
                                        Gv_Additional_Information.Rows[i].Cells[j].Value = "";
                                    }
                                }
                            }
                        }


                    }
                }
               
            }
            else
            {
                Gv_Additional_Information.DataSource = null;
            }
          


        }

        private void btn_Additional_Information_Click(object sender, EventArgs e)
        {
            Hashtable htCount = new Hashtable();
            DataTable dtCount = new System.Data.DataTable();
            htCount.Add("@Trans", "BIND");
            htCount.Add("@Order_Id", Orderid);
            dtCount = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htCount);
            int Additional_Information_Count = dtCount.Rows.Count + 1;
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            htComments.Add("@Trans", "INSERT");
            htComments.Add("@Additional_Information_Number", Additional_Information_Count);
            htComments.Add("@Order_Id", Orderid);
            htComments.Add("@Path","Additional Information");
            dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information", htComments);
            Additional_Information_Grid_Bind();
        }

        private void Txt_Legal_Description_Click(object sender, EventArgs e)
        {
            if (Tab_Page == "Legal Description")
            {
                Hashtable htComments1 = new Hashtable();
                DataTable dtComments1 = new System.Data.DataTable();
                htComments1.Add("@Trans", "SELECT");
                htComments1.Add("@Order_Id", Orderid);
                dtComments1 = dataaccess.ExecuteSP("Sp_Legal_Description", htComments1);
                if (dtComments1.Rows.Count > 0)
                {
                    Txt_Legal_Description.Text = dtComments1.Rows[0]["Description"].ToString();
                    Marker_Legal_Description_Id = dtComments1.Rows[0]["Marker_Legal_Description_Id"].ToString();
                    Highlited();
                }
            }
          
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Templete.Templete_View_Package Temp_Viewer = new Ordermanagement_01.Templete.Templete_View_Package(Orderid, Order_NO, Client, ddl_Subprocess.Text);
            Temp_Viewer.Show();
           // this.Close();
        }

        private void Txt_Legal_Description_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Gv_mortgage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_mortgage.CurrentCell != null && Status == 1 && Gv_mortgage.CurrentCell.Value!=null)
            {
                if (Gv_mortgage.CurrentCell.Value == null)
                {
                    Gv_mortgage.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0 && Gv_mortgage.CurrentCell.Value.ToString() == "" )
                {
                    Hashtable htComments = new Hashtable();
                    DataTable dtComments = new DataTable();
                    htComments.Add("@Trans", "SELECT");
                    htComments.Add("@Marker_Mortgage_Id", Marker_Mortgage_Id);
                    htComments.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                    dtComments = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments);
                    if (dtComments.Rows.Count <= 0)
                    {
                        Hashtable htComments1 = new Hashtable();
                        DataTable dtComments1 = new DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments1.Add("@Trans", "INSERT");
                        htComments1.Add("@Marker_Mortgage_Id", Marker_Mortgage_Id);
                        htComments1.Add("@Value", "");
                        htComments1.Add("@Mortgage_Information", Gv_mortgage.Columns[Gv_mortgage.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments1.Add("@X", X);
                        htComments1.Add("@Y", Y);
                        htComments1.Add("@Width", Width);
                        htComments1.Add("@Height", Height);
                        htComments1.Add("@PageNo", PageNo);
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Mortgage_Child", htComments1);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }
            }
         
        }

        private void Gv_Tax_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Tax.CurrentCell != null && Status == 1)
            {
                if (Gv_Tax.CurrentCell.Value == null)
                {
                    Gv_Tax.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0  && Gv_Tax.CurrentCell.Value.ToString() == "")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new System.Data.DataTable();
                  
                        htComments1.Add("@Trans", "SELECT");
                        htComments1.Add("@Marker_Tax_Id", Marker_Tax_Id);
                        htComments1.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                        dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments1);
                        if (dtComments1.Rows.Count <= 0)
                        {
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments.Add("@Trans", "INSERT");
                        htComments.Add("@Marker_Tax_Id", Marker_Tax_Id);
                        htComments.Add("@Value", "");
                        htComments.Add("@Tax_Information", Gv_Tax.Columns[Gv_Tax.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments.Add("@X", X);
                        htComments.Add("@Y", Y);
                        htComments.Add("@Width", Width);
                        htComments.Add("@Height", Height);
                        htComments.Add("@PageNo", PageNo);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Tax_Child", htComments);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }

            }
        }

        private void Gv_Judgement_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Judgement.CurrentCell != null && Status == 1)
            {
                if (Gv_Judgement.CurrentCell.Value == null)
                {
                    Gv_Judgement.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0 && Gv_Judgement.CurrentCell.Value.ToString() == "")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new System.Data.DataTable();
                    htComments1.Add("@Trans", "SELECT");
                    htComments1.Add("@Marker_Judgment_Id", Marker_Judgement_Id);
                    htComments1.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                    dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments1);
                    if (dtComments1.Rows.Count <= 0)
                    {
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments.Add("@Trans", "INSERT");
                        htComments.Add("@Marker_Judgment_Id", Marker_Judgement_Id);
                        htComments.Add("@Value", "");
                        htComments.Add("@Judgment_Information", Gv_Judgement.Columns[Gv_Judgement.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments.Add("@X", X);
                        htComments.Add("@Y", Y);
                        htComments.Add("@Width", Width);
                        htComments.Add("@Height", Height);
                        htComments.Add("@PageNo", PageNo);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Judgment_Child", htComments);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }
            }
        }

        private void Gv_Assessment_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Assessment.CurrentCell != null && Status == 1)
            {
                if (Gv_Assessment.CurrentCell.Value == null)
                {
                    Gv_Assessment.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0 && Gv_Assessment.CurrentCell.Value.ToString() == "")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new System.Data.DataTable();
                    htComments1.Add("@Trans", "SELECT");
                    htComments1.Add("@Marker_Assessment_Id", Marker_Assessment_Id);
                    htComments1.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                    dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments1);
                    if (dtComments1.Rows.Count <= 0)
                    {
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments.Add("@Trans", "INSERT");
                        htComments.Add("@Marker_Assessment_Id", Marker_Assessment_Id);
                        htComments.Add("@Value", "");
                        htComments.Add("@Assessment_Information", Gv_Assessment.Columns[Gv_Assessment.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments.Add("@X", X);
                        htComments.Add("@Y", Y);
                        htComments.Add("@Width", Width);
                        htComments.Add("@Height", Height);
                        htComments.Add("@PageNo", PageNo);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Assessment_Child", htComments);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }
            }
        }

        private void Gv_Additional_Information_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Additional_Information.CurrentCell != null && Status == 1)
            {
                if (Gv_Additional_Information.CurrentCell.Value == null)
                {
                    Gv_Additional_Information.CurrentCell.Value = "";
                }
                if (X == 0 && Y == 0 && Width == 0 && Height == 0 && Gv_Additional_Information.CurrentCell.Value.ToString() == "")
                {
                    Hashtable htComments1 = new Hashtable();
                    DataTable dtComments1 = new System.Data.DataTable();
                    htComments1.Add("@Trans", "SELECT");
                    htComments1.Add("@Marker_Additional_Information_Id", Marker_Additional_Information_Id);
                    htComments1.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                    dtComments1 = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments1);
                    if (dtComments1.Rows.Count <= 0)
                    {
                        Hashtable htComments = new Hashtable();
                        DataTable dtComments = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        htComments.Add("@Trans", "INSERT");
                        htComments.Add("@Marker_Additional_Information_Id", Marker_Additional_Information_Id);
                        htComments.Add("@Value", "");
                        htComments.Add("@Additional_Information_Information", Gv_Additional_Information.Columns[Gv_Additional_Information.CurrentCell.ColumnIndex].HeaderText.ToString());
                        htComments.Add("@X", X);
                        htComments.Add("@Y", Y);
                        htComments.Add("@Width", Width);
                        htComments.Add("@Height", Height);
                        htComments.Add("@PageNo", PageNo);
                        dtComments = dataaccess.ExecuteSP("Sp_Marker_Additional_Information_Child", htComments);
                    }
                    // Deed_Grid_Bind();
                    // Gv_Deed.CurrentCell = Gv_Deed.Rows[Gv_Deed.CurrentRow.Index].Cells[Gv_Deed.CurrentCell.ColumnIndex + 1];
                    //}

                }
            }

        }

        private void tab_Tax_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Deed_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Deed_Count_Click(object sender, EventArgs e)
        {

        }

        private void Gv_Deed_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void Gv_mortgage_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void Gv_Tax_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        //private void Gv_Judgement_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (e.Control is ComboBox)
        //    {
        //        ComboBox cb = e.Control as ComboBox;
        //        cb.DropDownStyle = ComboBoxStyle.Simple;

        //    }
        //}







        private bool ValidateDate(string stringDateValue)
        {
            try
            {
                CultureInfo CultureInfoDateCulture = new CultureInfo("en-US");
                DateTime d = DateTime.ParseExact(stringDateValue, "MM/dd/yyyy", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (toolStripTextBox1.Text != "" && toolStrip1.Text!=null)
            {
                PageNo = int.Parse(toolStripTextBox1.Text) - 1;
                Image_View();
                imageBox.SelectionRegion = new Rectangle(0, 0, 0, 0);
            }
        }

        private void lbl_Total_Mark_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Current_Mark_Click(object sender, EventArgs e)
        {

        }

        private void ddl_ClientSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientSelect.SelectedIndex > 0)
            {
                dbc.BindSubProcessNumber(ddl_Subprocess, int.Parse(ddl_ClientSelect.SelectedValue.ToString()));
            }
        }

        private void Gv_Deed_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Deed.Rows.Count > 0)
            {
                if (Gv_Deed.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Deed.CurrentRow != null)
                    {
                        Cur_Row = Gv_Deed.CurrentRow.Index;
                        Cur_Col = Gv_Deed.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {
                            
                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_Deed.Focus();
                    }
                }
            }
            lbl_Current_Mark.Text = "0";
        }

        private void Gv_mortgage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_mortgage.Rows.Count > 0)
            {
                if (Gv_mortgage.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_mortgage.CurrentRow != null)
                    {
                        Cur_Row = Gv_mortgage.CurrentRow.Index;
                        Cur_Col = Gv_mortgage.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {

                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_mortgage.Focus();
                    }
                }
            }
            lbl_Current_Mark.Text = "0";
        }

        private void Gv_Tax_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gv_Tax.Rows.Count > 0)
            {
                if (Gv_Tax.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Tax.CurrentRow != null)
                    {
                        Cur_Row = Gv_Tax.CurrentRow.Index;
                        Cur_Col = Gv_Tax.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {

                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_Tax.Focus();
                    }
                }
            }
            lbl_Current_Mark.Text = "0";
        }
        
        private void Gv_Judgement_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if (Gv_Judgement.Rows.Count > 0)
            {
                if (Gv_Judgement.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Judgement.CurrentRow != null)
                    {
                        Cur_Row = Gv_Judgement.CurrentRow.Index;
                        Cur_Col = Gv_Judgement.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {

                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_Judgement.Focus();
                    }
                }
               
            }
             lbl_Current_Mark.Text = "0";
        }
        
        private void Gv_Assessment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              if (Gv_Assessment.Rows.Count > 0)
            {
                if (Gv_Assessment.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Assessment.CurrentRow != null)
                    {
                        Cur_Row = Gv_Assessment.CurrentRow.Index;
                        Cur_Col = Gv_Assessment.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {

                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_Assessment.Focus();
                    }
                }
            }
              lbl_Current_Mark.Text = "0";
        }
        
        private void Gv_Additional_Information_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              if (Gv_Additional_Information.Rows.Count > 0)
            {
                if (Gv_Additional_Information.Rows.Count - 1 >= e.RowIndex)
                {
                    if (Gv_Additional_Information.CurrentRow != null)
                    {
                        Cur_Row = Gv_Additional_Information.CurrentRow.Index;
                        Cur_Col = Gv_Additional_Information.CurrentCell.ColumnIndex;
                        int Hori_Value = imageBox.HorizontalScroll.Value;
                        int Verti_Value = imageBox.VerticalScroll.Value;

                        Highlited();
                        imageBox.SelectionColor = Color.Yellow;
                        if (Status == 0)
                        {
                            if (X == 0 && Y == 0 && Height == 0 && Width == 0)
                            {
                                //PageNo = Last_PageNo;
                                // Image_View();
                            }

                            if (dt_Select.Rows.Count > 0)
                            {

                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - Height;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                                PageNo = int.Parse(dt_Select.Rows[0]["PageNo"].ToString()); ;
                                Image_View();
                            }
                            else
                            {
                                imageBox.HorizontalScroll.Value = Hori_Value;
                                imageBox.VerticalScroll.Value = Verti_Value;
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }

                        }
                        else
                        {

                            if (dt_Select.Rows.Count > 0)
                            {
                                imageBox.SelectionRegion = new Rectangle(int.Parse(dt_Select.Rows[0]["X"].ToString()), int.Parse(dt_Select.Rows[0]["Y"].ToString()), int.Parse(dt_Select.Rows[0]["Width"].ToString()), int.Parse(dt_Select.Rows[0]["Height"].ToString()));
                                imageBox.SelectionColor = Color.Yellow;
                                imageBox.HorizontalScroll.Value = int.Parse(dt_Select.Rows[0]["X"].ToString());
                                imageBox.VerticalScroll.Value = (int.Parse(dt_Select.Rows[0]["Y"].ToString()) * 2) - int.Parse(dt_Select.Rows[0]["Height"].ToString());
                                imageBox.AutoScrollPosition = new Point(imageBox.HorizontalScroll.Value, imageBox.VerticalScroll.Value);
                            }
                        }
                        Gv_Additional_Information.Focus();
                    }
                }
            }
              lbl_Current_Mark.Text = "0";
        }

   
    }
}
