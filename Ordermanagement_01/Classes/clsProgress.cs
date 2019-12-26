using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace InfiniteProgressBar
{

    class clsProgress
    {
     
        private static Thread th = new Thread(new ThreadStart(showProgressForm));
        public void startProgress()
        {

            th = new Thread(new ThreadStart(showProgressForm));
            th.Name = "first";
            th.Start();
            // System.Threading.Thread.Sleep(10);
        }

        private static void showProgressForm()
        {
            frmProgress sForm = new frmProgress();
            sForm.ShowDialog();
        }

        public void stopProgress()
        {

            try
            {
                System.Threading.Thread.Sleep(100);
                if (th.IsAlive)
                {
                    th.Abort();
                    th = null;

                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                    th.Abort();
                }
             
            }
            

            catch (ThreadAbortException exception)
            {
                System.Threading.Thread.Sleep(100);
                Thread.ResetAbort();
            }

        }

        class ThreadAbortTest
        {
            public static void Main()
            {
                ThreadStart myThreadDelegate = new ThreadStart(new ThreadStart(showProgressForm));
                Thread myThread = new Thread(myThreadDelegate);
                myThread.Start();
                Thread.Sleep(100);
             
                myThread.Abort();
                myThread.Join();
                
            }
        }

        



    

    }
}
