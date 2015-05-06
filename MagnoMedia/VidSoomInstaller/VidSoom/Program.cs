using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace VidSoom
{
    static class Program
    {
       

        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmMain());
            return 0;
        }



    }
}
