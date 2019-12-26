using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Samples : Form
    {
        public Tax_Samples()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        private void Tax_Samples_Load(object sender, EventArgs e)
        {
            //dataGridView1.AutoGenerateColumns = false;

            dt.Columns.Add("Tax Columns");
            dt.Columns.Add("Tax Data Entry");
            dt.Columns.Add("Tax Data Entry1");
            for (int j = 0; j < 5; j++)
            {
                dt.Rows.Add("");
            }
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns[0].Width = 100;

            /*
             * First method : Convert to an existed cell type such ComboBox cell,etc
             */
            //adding the first column to all textbox
            DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();
            this.dataGridView1[0,0] = TextBoxCell;
            this.dataGridView1[0, 0].Value = "Tax Type:";

            DataGridViewTextBoxCell TextBoxCell1 = new DataGridViewTextBoxCell();
            this.dataGridView1[0, 1] = TextBoxCell1;
            this.dataGridView1[0, 1].Value = "Tax Year:";

            DataGridViewTextBoxCell TextBoxCell2 = new DataGridViewTextBoxCell();
            this.dataGridView1[0, 2] = TextBoxCell2;
            this.dataGridView1[0, 2].Value = "Installment:";

            DataGridViewTextBoxCell TextBoxCell3 = new DataGridViewTextBoxCell();
            this.dataGridView1[0, 3] = TextBoxCell3;
            this.dataGridView1[0, 3].Value = "Base Amount:";

            DataGridViewTextBoxCell TextBoxCell4 = new DataGridViewTextBoxCell();
            this.dataGridView1[0, 4] = TextBoxCell4;
            this.dataGridView1[0, 4].Value = "Status:";



            //Second 
            DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();
            ComboBoxCell.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[1, 0] = ComboBoxCell;
            this.dataGridView1[1, 0].Value = "bbb";

            DataGridViewTextBoxCell TextBoxCell5 = new DataGridViewTextBoxCell();
            this.dataGridView1[1, 1] = TextBoxCell5;
            this.dataGridView1[1, 1].Value = string.Empty;

            DataGridViewComboBoxCell ComboBoxCell5 = new DataGridViewComboBoxCell();
            ComboBoxCell5.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[1, 2] = ComboBoxCell5;
            this.dataGridView1[1, 2].Value = "bbb";

            DataGridViewTextBoxCell TextBoxCell6 = new DataGridViewTextBoxCell();
            this.dataGridView1[1, 3] = TextBoxCell6;
            this.dataGridView1[1, 3].Value = string.Empty;

            DataGridViewComboBoxCell ComboBoxCell6 = new DataGridViewComboBoxCell();
            ComboBoxCell6.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[1, 4] = ComboBoxCell6;
            this.dataGridView1[1, 4].Value = "bbb";



            //Third
            DataGridViewComboBoxCell ComboBoxCell1 = new DataGridViewComboBoxCell();
            ComboBoxCell1.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[2, 0] = ComboBoxCell1;
            this.dataGridView1[2, 0].Value = "bbb";

            DataGridViewTextBoxCell TextBoxCell51 = new DataGridViewTextBoxCell();
            this.dataGridView1[2, 1] = TextBoxCell51;
            this.dataGridView1[2, 1].Value = string.Empty;

            DataGridViewComboBoxCell ComboBoxCell51 = new DataGridViewComboBoxCell();
            ComboBoxCell51.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[2, 2] = ComboBoxCell51;
            this.dataGridView1[2, 2].Value = "bbb";

            DataGridViewTextBoxCell TextBoxCell61 = new DataGridViewTextBoxCell();
            this.dataGridView1[2, 3] = TextBoxCell61;
            this.dataGridView1[2, 3].Value = string.Empty;

            DataGridViewComboBoxCell ComboBoxCell61 = new DataGridViewComboBoxCell();
            ComboBoxCell61.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
            this.dataGridView1[2, 4] = ComboBoxCell61;
            this.dataGridView1[2, 4].Value = "bbb";
            dataGridView1.DataSource = dt;
            //DataGridViewCheckBoxCell CheckBoxCell = new DataGridViewCheckBoxCell();
            //CheckBoxCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView1[0, 2] = CheckBoxCell;
            //this.dataGridView1[0, 2].Value = true;

            /*
             * Second method : Add control to the host in the cell
             */
            //DateTimePicker dtp = new DateTimePicker();
            //dtp.Value = DateTime.Now.AddDays(-10);
            ////add DateTimePicker into the control collection of the DataGridView
            //this.dataGridView1.Controls.Add(dtp);
            ////set its location and size to fit the cell
            //dtp.Location = this.dataGridView1.GetCellDisplayRectangle(0, 3, true).Location;
            //dtp.Size = this.dataGridView1.GetCellDisplayRectangle(0, 3, true).Size;
        }


        private DataGridViewComboBoxColumn CreateComboBoxColumn(DataGridViewComboBoxColumn ColumnName)
        {
            DataGridViewComboBoxColumn column =
                new DataGridViewComboBoxColumn();
            {
                //column.DataPropertyName = ColumnName.TitleOfCourtesy.ToString();
                //column.HeaderText = ColumnName.TitleOfCourtesy.ToString();
                //column.DropDownWidth = 160;
                //column.Width = 90;
                //column.MaxDropDownItems = 3;
                //column.FlatStyle = FlatStyle.Flat;
            }
            return column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Column_Count = dataGridView1.Columns.Count;
            int Rowcount = dataGridView1.Rows.Count;

            dt.Columns.Add("Tax Data Entry12");

          
             this.dataGridView1.DataSource = dt;

          

                DataGridViewComboBoxCell ComboBoxCellA1= new DataGridViewComboBoxCell();
                ComboBoxCellA1.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
                this.dataGridView1[Column_Count, 0] = ComboBoxCellA1;
                this.dataGridView1[Column_Count, 0].Value = "bbb";

                DataGridViewTextBoxCell TextBoxCellA1 = new DataGridViewTextBoxCell();
                this.dataGridView1[Column_Count, 1] = TextBoxCellA1;
                this.dataGridView1[Column_Count, 1].Value = string.Empty;

                DataGridViewComboBoxCell ComboBoxCellA2 = new DataGridViewComboBoxCell();
                ComboBoxCellA2.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
                this.dataGridView1[Column_Count, 2] = ComboBoxCellA2;
                this.dataGridView1[Column_Count, 2].Value = "bbb";

                DataGridViewTextBoxCell TextBoxCellA2 = new DataGridViewTextBoxCell();
                this.dataGridView1[Column_Count, 3] = TextBoxCellA2;
                this.dataGridView1[Column_Count, 3].Value = string.Empty;

                DataGridViewComboBoxCell ComboBoxCellA3 = new DataGridViewComboBoxCell();
                ComboBoxCellA3.Items.AddRange(new string[] { "aaa", "bbb", "ccc" });
                this.dataGridView1[Column_Count, 4] = ComboBoxCellA3;
                this.dataGridView1[Column_Count, 4].Value = "bbb";


            
                

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Column_Count = dataGridView1.Columns.Count;
            dt.Columns.RemoveAt(Column_Count - 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
