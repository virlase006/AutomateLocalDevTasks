using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChooseRedisIndex(new List<string>() { "db1:keys=28270,expires=0,avg_ttl=0" },@"C:\Users\virs\Desktop\Test\stylelabs.m"));
        }
    }
}
