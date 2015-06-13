using MagnoMedia.Common;
using MagnoMedia.Windows.Utilities;
using System;
using System.Windows.Forms;
using System.Linq;

namespace MagnoMedia.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //risky
            var list = args.ToList();
            list.Add("e949484a-2dfa-4e9e-a5d4-c2af785a635f");
            args = list.ToArray();

            if (args.Length == 1)
            {
                StaticData.SessionCode = args[0];
            }
            else
            {
                return;
            }

            Logging.Log.Info("Application Started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}
