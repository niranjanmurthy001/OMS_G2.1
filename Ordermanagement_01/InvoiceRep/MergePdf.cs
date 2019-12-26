using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PdfSharp;
using System.Text.RegularExpressions;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class MergePdf : Form
    {
        public MergePdf()
        {
            InitializeComponent();
        }

        

        public void ExtractPage(string sourcePdfPath, string outputPdfPath, int pageNumber)
{
    PdfReader reader = null;
    Document document = null;
    PdfCopy pdfCopyProvider = null;
    PdfImportedPage importedPage = null;

    try
    {
        // Intialize a new PdfReader instance with the contents of the source Pdf file:
        reader = new PdfReader(sourcePdfPath);
 
        // Capture the correct size and orientation for the page:
        document = new Document();
 
        // Initialize an instance of the PdfCopyClass with the source 
        // document and an output file stream:
        pdfCopyProvider = new PdfCopy(document, 
            new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

        document.Open();
 
        // Extract the desired page number:
        importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
        pdfCopyProvider.AddPage(importedPage);
        document.Close();
        reader.Close();
    }
    catch (Exception ex)
    {
        throw ex;
    }
}

      public void loadPdf()
      {
      
           // string[] files = Directory.GetFiles(txtPath.Text, "*.pdf");

          string P111 = @"\\C:Users\DRNASM0001\Desktop\Sample\15-59989-Search Package.pdf";
          string P112 = @"\\C:Users\DRNASM0001\Desktop\Sample\Invoice.pdf";
          DirectoryInfo dirInfo = new DirectoryInfo(P111);
          DirectoryInfo dirInfo1 = new DirectoryInfo(P112);
          FileInfo[] files = dirInfo.GetFiles(P111);
          FileInfo[] files1 = dirInfo1.GetFiles(P112);
        

            List<byte[]> filesByte = new List<byte[]>();
            List<byte[]> filesBytes = new List<byte[]>();
         List<byte[]> filesByteoutput = new List<byte[]>();
            //foreach (string file in files)
            //{
            //    filesByte.Add(File.ReadAllBytes(file));
            //}
        
            //   foreach (string file in files1)
            //{
            //    filesBytes.Add(File.ReadAllBytes(file));
            //}
          filesByteoutput.AddRange(filesByte);
          filesByteoutput.AddRange(filesBytes);

            // Call pdf merger
           MergeFiles(filesBytes);
      }
        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
      
            
            Document document = new Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();
                int documentPageCounter = 0;

                // Iterate through all pdf documents
                for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    // Create pdf reader
                    PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                        // Write header
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                            importedPage.Width < importedPage.Height ? 0 : 1);

                        // Write footer
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                            importedPage.Width < importedPage.Height ? 0 : 1);

                        pageStamp.AlterContents();

                        copy.AddPage(importedPage);
                    }

                    copy.FreeReader(reader);
                    reader.Close();
                }

                document.Close();
                return ms.GetBuffer();
            }
        }
     
        private void MergePdf_Load(object sender, EventArgs e)
        {
            Merge_11();
          //  loadPdf();
        }

        static void Variant1()
        {
            // Get some file names
            //string[] files = GetFiles();

            //// Open the output document
            //PdfDocument outputDocument = new PdfDocument();

            //// Iterate files
            //foreach (string file in files)
            //{
            //    // Open the document to import pages from it.
            //    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

            //    // Iterate pages
            //    int count = inputDocument.PageCount;
            //    for (int idx = 0; idx < count; idx++)
            //    {
            //        // Get the page from the external document...
            //        PdfPage page = inputDocument.Pages[idx];
            //        // ...and add it to the output document.
            //        outputDocument.AddPage(page);
            //    }
            //}

            //// Save the document...
            //const string filename = "ConcatenatedDocument1_tempfile.pdf";
            //outputDocument.Save(filename);
            //// ...and start a viewer.
            //Process.Start(filename);
        }

        public void Merge_11()
        {

            string[] lstFiles = new string[3];
            lstFiles[0] = @"C:/Users/DRNASM0001/Desktop/15-59989-Search Package.pdf";
            lstFiles[1] = @"C:/Users/DRNASM0001/Desktop/Invoice.pdf";
            //lstFiles[2] = @"C:/pdf/3.pdf";

            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = @"C:/Users/DRNASM0001/Desktop/Invoicemerge.pdf";


            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            //Open the output file
            sourceDocument.Open();

            try
            {
                //Loop through the files list
                for (int f = 0; f < lstFiles.Length - 1; f++)
                {
                    int pages = get_pageCcount(lstFiles[f]);

                    reader = new PdfReader(lstFiles[f]);
                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    reader.Close();
                }
                //At the end save the output file
                sourceDocument.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private int get_pageCcount(string file)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }

        


    }

    }


