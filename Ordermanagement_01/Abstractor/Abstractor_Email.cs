using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.Collections;
using System.IO;
using RTF;
using System.Net.Mime;
using Outlook = Microsoft.Office.Interop.Outlook;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using MarkupConverter;
using System.Web;
namespace Ordermanagement_01
{
    public partial class Abstractor_Email : Form
    {
        public Abstractor_Email()
        {
            InitializeComponent();
        }
        string bodys;
        string real, Real2;
        string Exact;
        Classes.HtmlToText HtmlToText = new Classes.HtmlToText();
        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        //Load All email and Update into Database


       

       

        private void Abstractor_Email_Load(object sender, EventArgs e)
        {
            List_Of_Folders();
           // Inbox_Items();
        }

        

        private void fileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
               fileToolStripMenuItem2.ForeColor = Color.Black;
           
            //else
            //{
            //    fileToolStripMenuItem2.ForeColor = Color.White;
            //}
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
                editToolStripMenuItem1.ForeColor = Color.Black;
            
            //else
            //{
            //    editToolStripMenuItem1.ForeColor = Color.White;
            //}
        }

        private void replyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
                replyToolStripMenuItem1.ForeColor = Color.Black;
            
            //else
            //{
            //    replyToolStripMenuItem1.ForeColor = Color.White;
            //}
        }

        private void helpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
                replyToolStripMenuItem1.ForeColor = Color.Black;
           
            //else
            //{
            //    replyToolStripMenuItem1.ForeColor = Color.White;
            //}
        }

        private void fileToolStripMenuItem2_MouseHover(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.Black;
        }

        private void fileToolStripMenuItem2_MouseLeave(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.White;
        }

        private void fileToolStripMenuItem2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.Black;
        }

        private void fileToolStripMenuItem2_DropDownClosed(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.White;
        }

        private void fileToolStripMenuItem2_DropDownOpened(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.Black;

        }

        private void editToolStripMenuItem1_DropDownOpened(object sender, EventArgs e)
        {
            editToolStripMenuItem1.ForeColor = Color.Black;
        }

        private void editToolStripMenuItem1_DropDownClosed(object sender, EventArgs e)
        {
            editToolStripMenuItem1.ForeColor = Color.White;
        }

        private void replyToolStripMenuItem1_DropDownOpened(object sender, EventArgs e)
        {
            replyToolStripMenuItem1.ForeColor = Color.Black;
        }

        private void replyToolStripMenuItem1_DropDownClosed(object sender, EventArgs e)
        {
            replyToolStripMenuItem1.ForeColor = Color.White;
        }

        private void helpToolStripMenuItem2_DropDownOpened(object sender, EventArgs e)
        {
            helpToolStripMenuItem2.ForeColor = Color.Black;
        }

        private void helpToolStripMenuItem2_DropDownClosed(object sender, EventArgs e)
        {
            helpToolStripMenuItem2.ForeColor = Color.White;
        }


        public void List_Of_Folders()
        {

            Outlook.NameSpace ns = null;
            Outlook.Stores stores = null;
            Outlook.Store store = null;
            Outlook.MAPIFolder rootFolder = null;
            Outlook.Folders folders = null;
            Outlook.MAPIFolder folder = null;
            string folderList = string.Empty;

            try
            {
                Outlook.Application oApp = new Outlook.Application();
                ns = oApp.Session;
                stores = ns.Stores;
                store = stores[1];
                rootFolder = store.GetRootFolder();
                folders = rootFolder.Folders;

                for (int i = 1; i < folders.Count; i++)
                {
                    folder = folders[i];
                    folderList += folder.Name + Environment.NewLine;
                    //if (folder != null)
                    //   // Marshal.ReleaseComObject(folder);
                }
                MessageBox.Show(folderList);
            }
            //finally
            //{
            //    if (folders != null)
            //        Marshal.ReleaseComObject(folders);
            //    if (folders != null)
            //        Marshal.ReleaseComObject(folders);
            //    if (rootFolder != null)
            //        Marshal.ReleaseComObject(rootFolder);
            //    if (store != null)
            //        Marshal.ReleaseComObject(store);
            //    if (stores != null)
            //        Marshal.ReleaseComObject(stores);
            //    if (ns != null)
            //        Marshal.ReleaseComObject(ns);
            //}
            catch (Exception ex)
            { 
            

            }
        }
    
     
       
    }
}
