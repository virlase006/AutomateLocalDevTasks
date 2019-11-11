using System;
using System.Collections.Generic;
using System.Configuration;
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
            var jsonFile = ConfigurationManager.AppSettings.Get("jsonFile");
            var configJson = jsonHelper.ReadJsonFromFile(jsonFile);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GetGitRepo(configJson));
        }
    }
}
