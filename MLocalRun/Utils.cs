using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    public static class Utils
    {
        public enum ScriptStage 
        {
        GetGitRepo = 1,
        SetupRdb =2
        }
         public enum GetGitRepoResponseCodes 
        {
            NewRepoCheckoutFailed = -1,
            NewRepoCheckoutSucceed = 1,
            ExisitngRepoCheckoutFailed = -2,
            ExistingRepoCheckoutSucceed = 2
        }
        public static string OpenFolderDailogAndGetPath()
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.SelectedPath;
            }
            return String.Empty;
        }
        public static string OpenFileAndGetPath(string ext)
        {
            
            FileDialog ofd = new OpenFileDialog();
            ofd.Filter = "rdb files (*.rdb)|*.rdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return String.Empty;
        }


    }
}
