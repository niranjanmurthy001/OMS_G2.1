using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01
{
    public partial class EmployeeCounty_Link : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int State_Id, County_Id;
        string OrderNo;

        public EmployeeCounty_Link(int StateId,int CountyId,string Order_no)
        {
            InitializeComponent();
            OrderNo = Order_no;
            State_Id = StateId;
            County_Id = CountyId;
          
            Grdiview_Bind_Tax_County_Link();
            Bind_County_Judgment_Link();
            this.WindowState = FormWindowState.Maximized;
        }
        protected void Bind_County_Judgment_Link()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");          
            htselect.Add("@County_Id", County_Id);
            dtselect = dataaccess.ExecuteSP("SP_County_Judgememts_Links", htselect);
            if (dtselect.Rows.Count > 0 && dtselect != null)
            {
                if (dtselect.Rows[0]["State"].ToString() != "")
                {
                    txtState.Text = dtselect.Rows[0]["State"].ToString();
                }
                if (dtselect.Rows[0]["County"].ToString() != "")
                {
                    txtCounty.Text = dtselect.Rows[0]["County"].ToString();
                }
                if (dtselect.Rows[0]["Online_Index"].ToString() != "")
                {
                    txtOnlineIndex.Text = dtselect.Rows[0]["Online_Index"].ToString();
                }
                else
                {
                    txtOnlineIndex.Text = "";
                }
                if (dtselect.Rows[0]["Website_Name"].ToString() != "")
                {
                    txtWebsiteName.Text = dtselect.Rows[0]["Website_Name"].ToString();
                }
                else
                {
                    txtWebsiteName.Text = "";
                }
                if (dtselect.Rows[0]["Subscription_Type"].ToString() != "")
                {
                    txtSubscriptionType.Text = dtselect.Rows[0]["Subscription_Type"].ToString();
                }
                else
                {
                    txtSubscriptionType.Text = "";
                }
                if (dtselect.Rows[0]["Subscription_Cost"].ToString() != "")
                {
                    txtSubscriptionCost.Text = dtselect.Rows[0]["Subscription_Cost"].ToString();
                }
                else
                {
                    txtSubscriptionCost.Text = "";
                }
                if (dtselect.Rows[0]["Recorder_Weblink"].ToString() != "")
                {
                    linkRecorderWeblink.Text = dtselect.Rows[0]["Recorder_Weblink"].ToString();
                }
                else
                {
                    linkRecorderWeblink.Text = "";
                }
                if (dtselect.Rows[0]["Image_Subscription"].ToString() != "")
                {
                    txtImageSubscription.Text = dtselect.Rows[0]["Image_Subscription"].ToString();
                }
                else
                {
                    txtImageSubscription.Text = "";
                }
                if (dtselect.Rows[0]["Image_Cost"].ToString() != "")
                {
                    txtImageCost.Text = dtselect.Rows[0]["Image_Cost"].ToString();
                }
                else
                {
                    txtImageCost.Text = "";
                }
                if (dtselect.Rows[0]["Images_Free"].ToString() != "")
                {
                    txtImagesFree.Text = dtselect.Rows[0]["Images_Free"].ToString();
                }
                else
                {
                    txtImagesFree.Text = "";
                }
                if (dtselect.Rows[0]["Images_From_Technically"].ToString() != "")
                {
                    txtImagesFromTechnically.Text = dtselect.Rows[0]["Images_From_Technically"].ToString();
                }
                else
                {
                    txtImagesFromTechnically.Text = "";
                }
                if (dtselect.Rows[0]["Index_Data_Starts_From"].ToString() != "")
                {
                    txtIndexDataStartsFrom.Text = dtselect.Rows[0]["Index_Data_Starts_From"].ToString();
                }
                else
                {
                    txtIndexDataStartsFrom.Text = "";
                }
                if (dtselect.Rows[0]["Images_Starts_From"].ToString() != "")
                {
                    txtImagesStartsFrom.Text = dtselect.Rows[0]["Images_Starts_From"].ToString();
                }
                else
                {
                    txtImagesStartsFrom.Text = "";
                }
                if (dtselect.Rows[0]["Index_User_Id"].ToString() != "")
                {
                    txtIndexUserId.Text = dtselect.Rows[0]["Index_User_Id"].ToString();
                }
                else
                {
                    txtIndexUserId.Text = "";
                }
                if (dtselect.Rows[0]["Index_Password"].ToString() != "")
                {
                    txtIndexPassword.Text = dtselect.Rows[0]["Index_Password"].ToString();
                }
                else
                {
                    txtIndexPassword.Text = "";
                }
                if (dtselect.Rows[0]["CCR_S"].ToString() != "")
                {
                    txtCCRs.Text = dtselect.Rows[0]["CCR_S"].ToString();
                }
                else
                {
                    txtCCRs.Text = "";
                }
                if (dtselect.Rows[0]["Assessor_Map"].ToString() != "")
                {
                    txtAssessorMap.Text = dtselect.Rows[0]["Assessor_Map"].ToString();
                }
                else
                {
                    txtAssessorMap.Text = "";
                }
                if (dtselect.Rows[0]["Plat_Map"].ToString() != "")
                {
                    txtPlatMap.Text = dtselect.Rows[0]["Plat_Map"].ToString();
                }
                else
                {
                    txtPlatMap.Text = "";
                }
                if (dtselect.Rows[0]["Judgement_OR_Lien"].ToString() != "")
                {
                    linkJudgement_Lien.Text = dtselect.Rows[0]["Judgement_OR_Lien"].ToString();
                }
                else
                {
                    linkJudgement_Lien.Text = "";
                }
                if (dtselect.Rows[0]["Judgement_OR_Lien_Images"].ToString() != "")
                {
                    txtJudgementLienImages.Text = dtselect.Rows[0]["Judgement_OR_Lien_Images"].ToString();
                }
                else
                {
                    txtJudgementLienImages.Text = "";
                }
                if (dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Prothonotary"].ToString() != "")
                {
                    linkJudgement_Lien_Prothonotary.Text = dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Prothonotary"].ToString();
                }
                else
                {
                    linkJudgement_Lien_Prothonotary.Text = "";
                }
                if (dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Muncipal_Orphan"].ToString() != "")
                {
                    linkJudgement_Lien_Muncipal_Orphans_Court.Text = dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Muncipal_Orphan"].ToString();
                }
                else
                {
                    linkJudgement_Lien_Muncipal_Orphans_Court.Text = "";
                }
                if (dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Superior_Court"].ToString() != "")
                {
                    linkJudgement_Lien_Superior_Court.Text = dtselect.Rows[0]["Judgement_OR_Lien_Web_Link_Superior_Court"].ToString();
                }
                else
                {
                    linkJudgement_Lien_Superior_Court.Text = "";
                }
                if (dtselect.Rows[0]["JG_User_Id"].ToString() != "")
                {
                    txtJGUserId.Text = dtselect.Rows[0]["JG_User_Id"].ToString();
                }
                else
                {
                    txtJGUserId.Text = "";
                }
                if (dtselect.Rows[0]["JG_Password"].ToString() != "")
                {
                    txtJGPassword.Text = dtselect.Rows[0]["JG_Password"].ToString();
                }
                else
                {
                    txtJGPassword.Text = "";
                }
                if (dtselect.Rows[0]["Data_Tree_Images"].ToString() != "")
                {
                    txtDataTreeImages.Text = dtselect.Rows[0]["Data_Tree_Images"].ToString();
                }
                else
                {
                    txtDataTreeImages.Text = "";
                }
                if (dtselect.Rows[0]["Comments"].ToString() != "")
                {
                    txtComments.Text = dtselect.Rows[0]["Comments"].ToString();
                }
                else
                {
                    txtComments.Text = "";
                }
            }
            else {
                MessageBox.Show("County Info not available");
            }

        }

       

        protected void Grdiview_Bind_Tax_County_Link()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_BY_STATE_COUNTY");
            htselect.Add("@State", State_Id);
            htselect.Add("@County", County_Id);
            dtselect = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htselect);
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    
                    Grd_Tax_County_Link.Rows.Add();
                    Grd_Tax_County_Link.Rows[i].Cells[0].Value = dtselect.Rows[i]["County_Assement_Link_Id"].ToString();
                    Grd_Tax_County_Link.Rows[i].Cells[1].Value = dtselect.Rows[i]["CountyTax_Link"].ToString();
                    Grd_Tax_County_Link.Rows[i].Cells[2].Value = dtselect.Rows[i]["Tax_PhoneNo"].ToString();

                    grd_Assor_Link.Rows.Add();
                    grd_Assor_Link.Rows[i].Cells[0].Value = dtselect.Rows[i]["Assessor_Link"].ToString();
                    grd_Assor_Link.Rows[i].Cells[1].Value = dtselect.Rows[i]["Assessor_PhoneNo"].ToString();
                   
                }

            }
            else
            {
                Grd_Tax_County_Link.DataSource = null;
            }

        }

        private void Grd_Tax_County_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 3)
            {
                string url = Grd_Tax_County_Link.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (url != "" && url!="N/A")
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
        }

        private void EmployeeCounty_Link_Load(object sender, EventArgs e)
        {
            lbl_orderno.Text = OrderNo + " Links";
        }
       
        private void linkRecorderWeblink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckURI(linkRecorderWeblink.Text))
            {
                System.Diagnostics.Process.Start(linkRecorderWeblink.Text);
            }
            else {
                MessageBox.Show("Unable to open the link");
            }
        }

       

        private void linkJudgement_Lien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckURI(linkJudgement_Lien.Text))
            {
                System.Diagnostics.Process.Start(linkJudgement_Lien.Text);
            }
            else
            {
                MessageBox.Show("Unable to open the link");
            }
        }

        private void linkJudgement_Lien_Prothonotary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckURI(linkJudgement_Lien_Prothonotary.Text))
            {
                System.Diagnostics.Process.Start(linkJudgement_Lien_Prothonotary.Text);
            }
            else
            {
                MessageBox.Show("Unable to open the link");
            }
        }

        private void linkJudgement_Lien_Superior_Court_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckURI(linkJudgement_Lien_Superior_Court.Text))
            {
                System.Diagnostics.Process.Start(linkJudgement_Lien_Superior_Court.Text);
            }
            else
            {
                MessageBox.Show("Unable to open the link");
            }
        }

        private void linkJudgement_Lien_Muncipal_Orphans_Court_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckURI(linkJudgement_Lien_Muncipal_Orphans_Court.Text))
            {
                System.Diagnostics.Process.Start(linkJudgement_Lien_Muncipal_Orphans_Court.Text);
            }
            else
            {
                MessageBox.Show("Unable to open the link");
            }
        }

        private bool CheckURI(string p)
        {        
            Uri uriResult;
            bool isURI = Uri.TryCreate(p, UriKind.RelativeOrAbsolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return isURI;
        }
    }
}
