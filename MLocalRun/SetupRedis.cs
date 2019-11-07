using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MLocalRun
{
    public partial class SetupRedis : Form
    {
        int GetRedisSetupResult;
        PowerShellScriptExecutor powerShellScriptExecutor;
        string GitRepoPath = "";

        public SetupRedis(string PathToRepo)
        {
            GitRepoPath = PathToRepo;
            InitializeComponent();
            powerShellScriptExecutor = new PowerShellScriptExecutor(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_ESPath.Text = PowershellConstants.OpenFolderDailogAndGetPath();
        }

        private void button_browseRedisFile_Click(object sender, EventArgs e)
        {
            txt_RdbPath.Text = PowershellConstants.OpenFileAndGetPath("rdb");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task task1 = Task.Factory.StartNew(() => ExecuteSetupRedisScript());
            Task task2 = Task.Factory.StartNew(() => GetResult()).ContinueWith((result) =>
            {
                bool scriptPass = EvaluatePassOrFail(GetRedisSetupResult, PowershellConstants.ScriptStage.GetGitRepo);
                if (scriptPass)
                {
                    MessageBox.Show("Done", "setup done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //SetupRedis();
                }
                else
                {
                    DialogResult userAction = MessageBox.Show("Failed to setup redis", "Redis setup failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (userAction == DialogResult.Abort)
                    {
                        GetRedisSetupResult = 0;
                        return;
                    }
                    else if (userAction == DialogResult.Ignore)
                    {
                        MessageBox.Show("Done", "setup done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SetupRedis();
                    }
                    else if (userAction == DialogResult.Retry)
                    {
                        GetRedisSetupResult = 0;
                        button2_Click(sender, e);
                    }
                }
            });
        }

        private bool EvaluatePassOrFail(object getGitRepoScriptResult, PowershellConstants.ScriptStage getGitRepo)
        {
           return  true;
        }

        private int GetResult()
        {
            return 1;
        }

        private void ExecuteSetupRedisScript()
        {
            var script = @"C:\Users\virs\Documents\PowerShell\RedisSetup.ps1";
            var parameters = GetSetupRedisParamsParams();
            powerShellScriptExecutor.ExecutePowerShellScript(script, this, parameters, txt_powershellOutput);
        }

        private List<KeyValuePair<string,string>> GetSetupRedisParamsParams()
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("PathToElasticSearch", txt_ESPath.Text));           
            parameters.Add(new KeyValuePair<string, string>("PathToRepo", GitRepoPath));
            parameters.Add(new KeyValuePair<string, string>("PathToNewRedisFile", txt_RdbPath.Text));
            parameters.Add(new KeyValuePair<string, string>("PathToRedis", txt_RedisPath.Text));
            return parameters;
        }
    }
}
