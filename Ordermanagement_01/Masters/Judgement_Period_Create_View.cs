using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;

namespace Ordermanagement_01.Masters
{
    public partial class Judgement_Period_Create_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();

        int userid = 0, Task, Task_Confirm_Id;
        string State_Id, user_Role;
        public Judgement_Period_Create_View(int user_id,string STATE_ID,string USER_ROLE)
        {
            InitializeComponent();
            userid = user_id;
            user_Role = USER_ROLE;
            State_Id = STATE_ID;
            if (user_Role == "2")
            {

                this.ControlBox = false;
            }
            else 
            {

                this.ControlBox = true;
            }
        }
       


        private void Bind_JudgeMentPeriod()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT_BY_STATE_WISE");
            ht.Add("@State",State_Id);
            dt = dataaccess.ExecuteSP("Sp_Judgement_Period", ht);
            if (dt.Rows.Count > 0)
            {

                // Grd_Judgement.DataSource = dt;

                lbl_Header.Text = dt.Rows[0]["State"].ToString() + "- JUDGMENT STATUTES LIMITATIONS";
                txt_Where_Judgement.Text = dt.Rows[0]["Where_judgment_recorded"].ToString();
                txt_Judgment_Lien_SOL.Text = dt.Rows[0]["Judgment_Lien_SOL"].ToString();
                txt_UCC_duration.Text = dt.Rows[0]["UCC_duration"].ToString();
                txt_UCC_Duration_Manafature_Homes.Text = dt.Rows[0]["UCC_Duration_Manafature_Homes"].ToString();
                txt_Mechanic_Liens_dudration.Text = dt.Rows[0]["Mechanic_Liens_dudration"].ToString();
                txt_State_Tax_Lien_SOL.Text = dt.Rows[0]["State_Tax_Lien_SOL"].ToString();
                txt_Federal_Tax_Lien_SOL.Text = dt.Rows[0]["Federal_Tax_Lien_SOL"].ToString();
                txt_Hoa.Text = dt.Rows[0]["HOA_Liens"].ToString();
                txt_Munciple.Text = dt.Rows[0]["Municipal_Lien"].ToString();

                txt_Notes.Text = dt.Rows[0]["Notes"].ToString();





            }
            else
            {

                //Grd_Judgement.DataSource = null;
                //Grd_Judgement.Rows.Clear();
            }

            

        }

        private void Judgement_Period_Create_View_Load(object sender, EventArgs e)
        {
            txt_Federal_Tax_Lien_SOL.ForeColor = System.Drawing.Color.Blue;
            Bind_JudgeMentPeriod();
        }


    }
}
