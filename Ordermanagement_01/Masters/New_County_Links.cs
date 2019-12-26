using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Masters
{
    public partial class New_County_Links : DevExpress.XtraEditors.XtraForm
    {
        DataAccess dataaccess = new DataAccess();
        int editvalue = 0;
        int State_Id, County_Id;
       // ViewCountyJudgementsLinks vcjl = new ViewCountyJudgementsLinks(1);
       
        public New_County_Links(int State_Id,int County_Id)
        {
            this.State_Id = State_Id;
            this.County_Id = County_Id;
            InitializeComponent();
        }
        private void New_County_Links_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Bind_State();
            if (State_Id != 0 && County_Id != 0)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "SELECT_STATE_COUNTY");
                ht.Add("@State_Id", State_Id);
                ht.Add("@County_Id", County_Id);
                dt = dataaccess.ExecuteSP("SP_County_Judgememts_Links", ht);
                if (dt.Rows.Count > 0)
                {
                 

                    try
                    {
                        SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                        look_Up_Edit_States.EditValue = State_Id;
                        look_Up_Edit_County.EditValue = County_Id;
                        txt_Online_Index.Text = dt.Rows[0]["Online_Index"].ToString();
                        txt_Web_Name.Text = dt.Rows[0]["Website_Name"].ToString();
                        txt_Sub_Type.Text = dt.Rows[0]["Subscription_Type"].ToString();
                        txt_Sub_Cost.Text = dt.Rows[0]["Subscription_Cost"].ToString();
                        txt_Rec_Weblink.Text = dt.Rows[0]["Recorder_Weblink"].ToString();
                        txt_Img_Sub.Text = dt.Rows[0]["Image_Subscription"].ToString();
                        txt_Img_Cost.Text = dt.Rows[0]["Image_Cost"].ToString();
                        txt_Img_Free.Text = dt.Rows[0]["Images_Free"].ToString();
                        txt_Img_Tech.Text = dt.Rows[0]["Images_From_Technically"].ToString();
                        txt_Index_Data_Sts_From.Text = dt.Rows[0]["Index_Data_Starts_From"].ToString();
                        txt_Img_From.Text = dt.Rows[0]["Images_Starts_From"].ToString();
                        txt_User_Id.Text = dt.Rows[0]["Index_User_Id"].ToString();
                        txt_Index_Pwd.Text = dt.Rows[0]["Index_Password"].ToString();
                        txt_Ccrs.Text = dt.Rows[0]["CCR_S"].ToString();
                        txt_Assessor_Map.Text = dt.Rows[0]["Assessor_Map"].ToString();
                        txt_Plat_Map.Text = dt.Rows[0]["Plat_Map"].ToString();
                        txt_Jud_Lien.Text = dt.Rows[0]["Judgement_OR_Lien"].ToString();
                        txt_Jud_Lien_Img.Text = dt.Rows[0]["Judgement_OR_Lien_Images"].ToString();
                        txt_Jud_Web_Pro.Text = dt.Rows[0]["Judgement_OR_Lien_Web_Link_Prothonotary"].ToString();
                        txt_Jud_Lien_Web_Link_Muncipal.Text = dt.Rows[0]["Judgement_OR_Lien_Web_Link_Muncipal_Orphan"].ToString();
                        txt_Lien_Web_Sup_Court.Text = dt.Rows[0]["Judgement_OR_Lien_Web_Link_Superior_Court"].ToString();
                        txt_Jg_User_Id.Text = dt.Rows[0]["JG_User_Id"].ToString();
                        txt_Jg_Pwd.Text = dt.Rows[0]["JG_Password"].ToString();
                        txt_Tree_Img.Text = dt.Rows[0]["Data_Tree_Images"].ToString();
                        txt_Comments.Text = dt.Rows[0]["Comments"].ToString();

                        btn_Submit.Text = "Edit";
                        btn_Clear.Visible = false;
  

                    }
                    catch (Exception ex)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Something Went Wrong");
                    }

                    finally
                    {
                        SplashScreenManager.CloseForm(false);
                    }

                }
            }
        }
        public bool Validations()
        {
            if (Convert.ToInt32 (look_Up_Edit_States.EditValue)== 0)
            {
                XtraMessageBox.Show("Please Select State");
                look_Up_Edit_States.Focus();
                return false;
            }
            if (Convert.ToInt32(look_Up_Edit_County.EditValue) == 0)
            {
                XtraMessageBox.Show("Please Select County");
                look_Up_Edit_County.Focus();
                return false;
            }
            if(txt_Rec_Weblink.Text.Trim()!=string.Empty)
            {
          bool b=    CheckURI(txt_Rec_Weblink.Text);
                if (b == false)
                {
                    XtraMessageBox.Show("Enter The Correct Formate of URL");
                    txt_Rec_Weblink.Focus();
                    return false;
                }

            }

            if (txt_Jud_Lien.Text.Trim()!=string.Empty)
            {
             bool b=CheckURI(txt_Jud_Lien.Text);
                if(b==false)
                {
                    XtraMessageBox.Show("Enter The Correct Formate of URL");
                    txt_Jud_Lien.Focus();
                    return false;
                }
            }
            return true;
        }
        private bool CheckURI(string p)
        {
            try
            {
                Uri uriResult;
                bool isURI = Uri.TryCreate(p, UriKind.RelativeOrAbsolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return isURI;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    
        
        public void Bind_State()
        {
            Hashtable ht_Bind = new Hashtable();
            DataTable dt_Bind  = new DataTable();
            ht_Bind.Add("@Trans", "SELECT_STATE");
            dt_Bind = dataaccess.ExecuteSP("Sp_County", ht_Bind);
            DataRow dr = dt_Bind.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_Bind.Rows.InsertAt(dr, 0);
            look_Up_Edit_States.Properties.DataSource = dt_Bind;
            look_Up_Edit_States.Properties.Columns.Clear();
            look_Up_Edit_States.Properties.DisplayMember = "State";
            look_Up_Edit_States.Properties.ValueMember = "State_ID";

            look_Up_Edit_States.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("State", 100);
            look_Up_Edit_States.Properties.Columns.Add(col);

        }
        private void look_Up_Edit_States_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(look_Up_Edit_States.EditValue) > 0)
            {
                editvalue = (int)look_Up_Edit_States.EditValue;
                Bind_County(editvalue);
            }
        }
        private void Bind_County(int State_Id)
        {
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();
            ht_County.Add("@Trans", "SELECT_COUNTY");
            ht_County.Add("@State_Id", State_Id);
             dt_County = dataaccess.ExecuteSP("Sp_County", ht_County);
            DataRow dr = dt_County.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_County.Rows.InsertAt(dr, 0);
            look_Up_Edit_County.Properties.DataSource = dt_County;
            look_Up_Edit_County.Properties.Columns.Clear();
            look_Up_Edit_County.Properties.DisplayMember = "County";
            look_Up_Edit_County.Properties.ValueMember = "County_ID";

            look_Up_Edit_County.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("County", 100);
            look_Up_Edit_County.Properties.Columns.Add(col);

        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Validations() != false)
            {


                try
                {
                    SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                    if (btn_Submit.Text == "Submit")
                    {
                        //DataRow row = gridViewCounty_Info.GetDataRow(e.RowHandle);
                        var ht = new Hashtable();
                        ht.Add("@Trans", "MERGE");
                        ht.Add("@State_Id", Convert.ToInt32(look_Up_Edit_States.EditValue));
                        ht.Add("@County_Id", Convert.ToInt32(look_Up_Edit_County.EditValue));
                        ht.Add("@Online_Index", txt_Online_Index.Text);
                        ht.Add("@Website_Name", txt_Web_Name.Text);
                        ht.Add("@Subscription_Type", txt_Sub_Type.Text);
                        ht.Add("@Subscription_Cost", txt_Sub_Cost.Text);
                        ht.Add("@Recorder_Weblink", txt_Rec_Weblink.Text);
                        ht.Add("@Image_Subscription", txt_Img_Sub.Text);
                        ht.Add("@Image_Cost", txt_Img_Cost.Text);
                        ht.Add("@Images_Free", txt_Img_Free.Text);
                        ht.Add("@Images_From_Technically", txt_Img_Tech.Text);
                        ht.Add("@Index_Data_Starts_From", txt_Index_Data_Sts_From.Text);
                        ht.Add("@Images_Starts_From", txt_Img_From.Text);
                        ht.Add("@Index_User_Id", txt_User_Id.Text);
                        ht.Add("@Index_Password", txt_Index_Pwd.Text);
                        ht.Add("@CCR_S", txt_Ccrs.Text);
                        ht.Add("@Assessor_Map", txt_Assessor_Map.Text);
                        ht.Add("@Plat_Map", txt_Plat_Map.Text);
                        ht.Add("@Judgement_OR_Lien", txt_Jud_Lien.Text);
                        ht.Add("@Judgement_OR_Lien_Images", txt_Jud_Lien_Img.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Prothonotary", txt_Jud_Web_Pro.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Muncipal_Orphan", txt_Jud_Lien_Web_Link_Muncipal.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Superior_Court", txt_Lien_Web_Sup_Court.Text);
                        ht.Add("@JG_User_Id", txt_Jg_User_Id.Text);
                        ht.Add("@JG_Password", txt_Jg_Pwd.Text);
                        ht.Add("@Data_Tree_Images", txt_Tree_Img.Text);
                        ht.Add("@Comments", txt_Comments.Text);
                        int count = new DataAccess().ExecuteSPForCRUD("SP_County_Judgememts_Links", ht);
                        if (count == 1)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Inserted county info");
                            btn_Clear_Click(sender, e);
            
                            //vcjl.BindCountyJudgementsInfo();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Something went wrong");
                }
              

                try
                {

               
                    if (btn_Submit.Text == "Edit")
                    {
                        SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);



                        //DataRow row = gridViewCounty_Info.GetDataRow(e.RowHandle);
                        var ht = new Hashtable();
                        ht.Add("@Trans", "MERGE");
                        ht.Add("@State_Id", Convert.ToInt32(look_Up_Edit_States.EditValue));
                        ht.Add("@County_Id", Convert.ToInt32(look_Up_Edit_County.EditValue));
                        ht.Add("@Online_Index", txt_Online_Index.Text);
                        ht.Add("@Website_Name", txt_Web_Name.Text);
                        ht.Add("@Subscription_Type", txt_Sub_Type.Text);
                        ht.Add("@Subscription_Cost", txt_Sub_Cost.Text);
                        ht.Add("@Recorder_Weblink", txt_Rec_Weblink.Text);
                        ht.Add("@Image_Subscription", txt_Img_Sub.Text);
                        ht.Add("@Image_Cost", txt_Img_Cost.Text);
                        ht.Add("@Images_Free", txt_Img_Free.Text);
                        ht.Add("@Images_From_Technically", txt_Img_Tech.Text);
                        ht.Add("@Index_Data_Starts_From", txt_Index_Data_Sts_From.Text);
                        ht.Add("@Images_Starts_From", txt_Img_From.Text);
                        ht.Add("@Index_User_Id", txt_User_Id.Text);
                        ht.Add("@Index_Password", txt_Index_Pwd.Text);
                        ht.Add("@CCR_S", txt_Ccrs.Text);
                        ht.Add("@Assessor_Map", txt_Assessor_Map.Text);
                        ht.Add("@Plat_Map", txt_Plat_Map.Text);
                        ht.Add("@Judgement_OR_Lien", txt_Jud_Lien.Text);
                        ht.Add("@Judgement_OR_Lien_Images", txt_Jud_Lien_Img.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Prothonotary", txt_Jud_Web_Pro.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Muncipal_Orphan", txt_Jud_Lien_Web_Link_Muncipal.Text);
                        ht.Add("@Judgement_OR_Lien_Web_Link_Superior_Court", txt_Lien_Web_Sup_Court.Text);
                        ht.Add("@JG_User_Id", txt_Jg_User_Id.Text);
                        ht.Add("@JG_Password", txt_Jg_Pwd.Text);
                        ht.Add("@Data_Tree_Images", txt_Tree_Img.Text);
                        ht.Add("@Comments", txt_Comments.Text);
                        int count = new DataAccess().ExecuteSPForCRUD("SP_County_Judgememts_Links", ht);
                        if (count == 1)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Updated county info");
                            this.Close();
                            btn_Clear_Click(sender, e);


                            //vcjl.BindCountyJudgementsInfo();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Something went wrong");
                }
               

            }
        }
        
                







           

        
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            look_Up_Edit_States.EditValue = 0;
            look_Up_Edit_County.EditValue = 0;
            txt_Online_Index.Text = "";
            txt_Web_Name.Text = "";
            txt_Sub_Type.Text = "";
            txt_Sub_Cost.Text = "";
            txt_Rec_Weblink.Text = "";
            txt_Img_Sub.Text = "";
            txt_Img_Cost.Text = "";
            txt_Img_Free.Text = "";
            txt_Img_Tech.Text = "";
            txt_Index_Data_Sts_From.Text = "";
            txt_Img_From.Text="";
            txt_User_Id.Text = "";
            txt_Index_Pwd.Text = "";
            txt_Ccrs.Text = "";
            txt_Assessor_Map.Text = "";
            txt_Plat_Map.Text="";
            txt_Jud_Lien.Text = "";
            txt_Jud_Lien_Img.Text = "";
            txt_Jud_Web_Pro.Text = "";
            txt_Jud_Lien_Web_Link_Muncipal.Text = "";
            txt_Lien_Web_Sup_Court.Text = "";
            txt_Jg_User_Id.Text = "";
            txt_Jg_Pwd.Text = "";
            txt_Tree_Img.Text="";
            txt_Comments.Text = "";
            txt_Jg_User_Id.Text= "";

        }

    
     
    }
    
}