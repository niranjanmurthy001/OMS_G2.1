using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading; // Required for this example


namespace Ordermanagement_01.Classes
{  
    class Load_Progres
    {
        private bool isProcessRunning = false;

        public void Start_progres()
        {

            // If a process is already running, warn the user and cancel the operation
            if (isProcessRunning)
            {
                MessageBox.Show("A process is already running.");
                return;
            }

            // Initialize the dialog that will contain the progress bar
            Progres_Dialog progressDialog = new Progres_Dialog();

            
            // Initialize the thread that will handle the background process
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {
                    // Set the flag that indicates if a process is currently running
                    isProcessRunning = true;

                    // Set the dialog to operate in indeterminate mode
                    progressDialog.SetIndeterminate(true);

                    // Pause the thread for five seconds
                    Thread.Sleep(500);

                    // Show a dialog box that confirms the process has completed
                    //  MessageBox.Show("Thread completed!");

                    // Close the dialog if it hasn't been already

                    if (progressDialog.InvokeRequired)
                        progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                    // Reset the flag that indicates if a process is currently running
                    isProcessRunning = false;
                }
            ));

            // Start the background process thread
            backgroundThread.Start();

            // Open the dialog
            progressDialog.ShowDialog();
        }

    }
}
