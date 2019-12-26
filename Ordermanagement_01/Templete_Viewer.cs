using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class Templete_Viewer : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int SubProcessId;
        string Path;
        public Templete_Viewer(int Sub_ProcessId)
        {
            SubProcessId = Sub_ProcessId;
            InitializeComponent();
            DataAccess dataaccess = new DataAccess();
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECTSUBPROCESSWISE");
            htselect.Add("@Subprocess_Id", Sub_ProcessId);
            dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
            Path = dtselect.Rows[0]["Templete_Upload_Path"].ToString();
            //pdfViewerControl1.InputFile = Path;
        }
    }
}
