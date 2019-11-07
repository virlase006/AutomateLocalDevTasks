using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Threading;
using System.Management.Automation.Runspaces;

namespace MLocalRun
{
    public partial class GetGitRepo : Form
    {
       
        private int GetGitRepoScriptResult = 0;
        private PowerShellScriptExecutor powerShellScriptExecutor;
        public GetGitRepo()

        {  // create Powershell runspace
            powerShellScriptExecutor = new PowerShellScriptExecutor(this);          
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task task1 = Task.Factory.StartNew(() => ExecuteGetGitRepoScript());
            Task task2 = Task.Factory.StartNew(() => GetResult()).ContinueWith((result) =>
            {
                bool scriptPass = EvaluatePassOrFail(GetGitRepoScriptResult, PowershellConstants.ScriptStage.GetGitRepo);
                if (scriptPass)
                {
                    SetupRedis();
                }
                else
                {
                   DialogResult userAction =  MessageBox.Show("Get git repo failed. Make sure you dont have any uncommit changes. Click Try Again to force checkout or cancel and save changes before checkout.", "Git checkout failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    if (userAction == DialogResult.Abort)
                    {
                        GetGitRepoScriptResult = 0;
                        return;
                    }
                    else if (userAction == DialogResult.Ignore)
                    {
                        SetupRedis();
                    }
                    else if (userAction == DialogResult.Retry)
                    {
                        GetGitRepoScriptResult = 0;
                        button2_Click(sender, e);
                    }
                }
            });
           
            
           
        }

        private int GetResult()
        {
            while (GetGitRepoScriptResult == 0)
            {
                Thread.Sleep(5000);
                GetGitRepoScriptResult = powerShellScriptExecutor.GetResult();
            }
            return GetGitRepoScriptResult;
        }

        private void SetupRedis()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread
              
                var redisSetup = new SetupRedis(txt_gitRepoPath.Text);
                // redisSetup.Closed += (s, args) => this.Close();
                redisSetup.Show();
                redisSetup.StartPosition = this.StartPosition;
                  this.Hide();
            });
            
          
        }

        private void ExecuteGetGitRepoScript()
        {
            var script = @"C:\Users\virs\Documents\PowerShell\GetGitRepo.ps1";
            var parameters = GetGitRepoScriptParams();
            powerShellScriptExecutor.ExecutePowerShellScript(script, this, parameters, txt_powershellOutput);
        }

        private bool EvaluatePassOrFail(int getGitScriptResult, PowershellConstants.ScriptStage stage)
        {
            if (stage == PowershellConstants.ScriptStage.GetGitRepo)
            {
                return ((getGitScriptResult == Convert.ToInt32(PowershellConstants.GetGitRepoResponseCodes.ExistingRepoCheckoutSucceed))
                    || (getGitScriptResult == Convert.ToInt32(PowershellConstants.GetGitRepoResponseCodes.NewRepoCheckoutSucceed))) ? true : false;

            }
            else 
            {
                return true;
            }

      
        }

        private List<KeyValuePair<string, string>> GetGitRepoScriptParams()
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("USERNAME", txt_gitUsername.Text));
            parameters.Add(new KeyValuePair<string, string>("PASSWORD", ""));
            parameters.Add(new KeyValuePair<string, string>("STYLELABSDIR", txt_gitRepoPath.Text));
            parameters.Add(new KeyValuePair<string, string>("VERSION", txt_gitVersion.Text));
            parameters.Add(new KeyValuePair<string, string>("RepoExist", (Convert.ToInt32(check_DoYouHaveGit.Checked)).ToString()));
            return parameters;

        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SetChooseGitRepoLabelText(check_DoYouHaveGit.Checked);
        }

        private void SetChooseGitRepoLabelText(bool isChecked)
        {
            if (isChecked)
            {
                lbl_chooseGit.Text = "Choose existing git repo folder.";
            }
            else
            {
                lbl_chooseGit.Text = "Choose where you CLONE git repo. ";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_repoError.Visible = false;
            check_DoYouHaveGit.Checked = true;
            txt_gitUsername.Parent = panel_GitLogin;
            txt_gitVersion.Parent = panel_GitLogin;
            var filePath = txt_gitRepoPath.Text;
            if (filePath == "" || filePath == null)
            {
                panel_GitLogin.Hide();
            }
            else
            {
                panel_GitLogin.Show();
            }
        }

        private void btn_GitRepoBrowse_Click(object sender, EventArgs e)
        {
          txt_gitRepoPath.Text =  PowershellConstants.OpenFolderDailogAndGetPath();
        }

       

        private void txt_gitRepoPath_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txt_gitRepoPath.Text))
            {
                if (Directory.Exists(txt_gitRepoPath.Text))
                {
                    panel_GitLogin.Show();
                    lbl_repoError.Visible = false;
                }
                else
                {
                    panel_GitLogin.Hide();
                    lbl_repoError.Text = "Invalid path !!!";
                    lbl_repoError.Visible = true;
                }
            }
        }
    }
}
