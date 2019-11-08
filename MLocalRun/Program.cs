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
            JsonHelper jsonHelper = new JsonHelper();
            var configJson = jsonHelper.ReadJsonFromFile(@"C:\Users\adv\source\repos\vdsGitHub\AutomateLocalDevTasks\MLocalRun\configuration.json");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChooseRedisIndex(new List<string>() { "1","2"} ,configJson));
        }
    }
}
