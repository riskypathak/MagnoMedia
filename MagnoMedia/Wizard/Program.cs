using MagnoMedia.Common;
using MagnoMedia.Windows.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            list.Add("99b0618d-d635-4071-8e42-8cebb453602f");
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
