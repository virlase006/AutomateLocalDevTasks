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
         //   txt_ESPath.Text = PowershellConstants.OpenFolderDailogAndGetPath();
        }

        private void button_browseRedisFile_Click(object sender, EventArgs e)
        {
            txt_RdbPath.Text = PowershellConstants.OpenFileAndGetPath("rdb");
        }

        private bool IsRedisRunning()
        {

            txt_powershellOutput.AppendText("\n Checking if redis already running");
            return powerShellScriptExecutor.RunBashCommand("-c \"" + "redis-cli ping" + "\"") .Contains( "PONG" )? true : false;
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (IsRedisRunning())
            {

                txt_powershellOutput.AppendText("\n Killing running instance of redis");
                KillRedis();
            }

            string redisFilePathParsed = ParseWindowsPathToBashPath(txt_RdbPath.Text);
            var command = ExtractAndParseBashCommand(redisFilePathParsed);
            var output = powerShellScriptExecutor.RunBashCommand(command);
            ProcessOutput(output);

        }

        private void ProcessOutput(string OUTPUT)
        {
            if (OUTPUT.Contains("ERROR"))
            {
                MessageBox.Show($"Failed to setup redis: {OUTPUT}", "Failed to setup redis", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txt_powershellOutput.AppendText("\n" + OUTPUT);
                ChooseRedisIndex();
            }
        }

        private void ChooseRedisIndex()
        {
            var keySpaces = powerShellScriptExecutor.RunBashCommand("-c \"redis-cli info KeySpace\"");
            var keySpacesArray = keySpaces.Split('\n').ToList<string>();
            List<string> dbIndexes = new List<string>() ;
            foreach (var keyspace in keySpacesArray)
            {
                if (keyspace.Contains("db"))
                {
                    dbIndexes.Add(keyspace);
                }
            }
            var chooseRedis = new ChooseRedisIndex(dbIndexes, GitRepoPath);
            chooseRedis.Show();
        }

        private string ExtractAndParseBashCommand(string redisFilePathParsed)
        {
            var bashScritp = System.IO.File.ReadAllText(@"C:\Users\virs\Documents\PowerShell\CopyRedisFile.ps1");
            bashScritp = bashScritp.Replace("bash", "");
            bashScritp = bashScritp.Replace("PathToRedis", txt_RedisPath.Text.Replace("\\","/"));
            bashScritp =  bashScritp.Replace("BashPath", redisFilePathParsed.Replace("\\","/"));
            return bashScritp;
        }

        private string ParseWindowsPathToBashPath(string windowsPath)
        {
           return windowsPath.Replace("C:", "");
        }

        private void KillRedis()
        {
            powerShellScriptExecutor.RunBashCommand("-c \"killall redis-server\"");
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
            var script = @"C:\Users\virs\Documents\PowerShell\CopyRedisFile.ps1";
            var parameters = GetSetupRedisParamsParams();
            powerShellScriptExecutor.ExecutePowerShellScript(script, this, parameters, txt_powershellOutput);
        }

        private List<KeyValuePair<string,string>> GetSetupRedisParamsParams()
        {
            var parameters = new List<KeyValuePair<string, string>>();
            //  parameters.Add(new KeyValuePair<string, string>("PathToElasticSearch", txt_ESPath.Text));          
         
            parameters.Add(new KeyValuePair<string, string>("PathToRepo", GitRepoPath));
            parameters.Add(new KeyValuePair<string, string>("PathToNewRedisFile", txt_RdbPath.Text));
            parameters.Add(new KeyValuePair<string, string>("PathToRedis", txt_RedisPath.Text));
            return parameters;
        }

        private void SetupRedis_Load(object sender, EventArgs e)
        {
            txt_RedisPath.Text = "/home/vds/redis-5.0.4/utils";
             lbl_repoError.Visible = false;         
        }

        private void txt_RdbPath_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_RdbPath.Text))
            {
                if (System.IO.File.Exists(txt_RdbPath.Text))
                {
                  
                    lbl_repoError.Visible = false;
                }
                else
                {
                    
                    lbl_repoError.Text = "Invalid path !!!";
                    lbl_repoError.Visible = true;
                    lbl_repoError.Show();
                }
            }
        }
    }
}
