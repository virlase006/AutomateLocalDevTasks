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
using Newtonsoft.Json.Linq;

namespace MLocalRun
{
    public partial class GetGitRepo : Form
    {
        private JObject configJson;
        private IScriptExecutor powerShellScriptExecutor;
        public GetGitRepo(JObject jObject)
        {
            // create Powershell runspace
            configJson = jObject;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            configJson["username"] = txt_gitUsername.Text;
            configJson["gitRepoPath"] = txt_gitRepoPath.Text;
            Task.Factory.StartNew(()=> 
                ExecuteGetGitRepoScript()
            ).ContinueWith((result)=> {

               
                bool scriptPass = EvaluatePassOrFail(result.Result, Utils.ScriptStage.GetGitRepo);
                PostScriptActions(sender, e, scriptPass);
            });
            

        }

        private void PostScriptActions(object sender, EventArgs e, bool scriptPass)
        {
            if (scriptPass)
            {
                SetupRedis();
            }
            else
            {
                DialogResult userAction = MessageBox.Show("Get git repo failed. Make sure you dont have any uncommit changes. Click Try Again to force checkout or cancel and save changes before checkout.", "Git checkout failed", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (userAction == DialogResult.Abort)
                {
                    return;
                }
                else if (userAction == DialogResult.Ignore)
                {
                    SetupRedis();
                }
                else if (userAction == DialogResult.Retry)
                {

                    button2_Click(sender, e);
                }
            }
        }

        private void SetupRedis()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread

                var redisSetup = new SetupRedis(configJson);
                // redisSetup.Closed += (s, args) => this.Close();
                redisSetup.Show();
                redisSetup.StartPosition = this.StartPosition;
                this.Hide();
            });


        }

        private int ExecuteGetGitRepoScript()
        {
            var parameters = GetGitRepoScriptParams();
            powerShellScriptExecutor = new PowerShellScriptExecutor(this, txt_powershellOutput, parameters);
            var script = @"../../../Scripts/GetGitRepo.ps1";
            return  Task.Factory.StartNew(()=>powerShellScriptExecutor.ExecuteScript(script)).ContinueWith((result)=> {
                return result.Result;
            }).Result;
        }

        private bool EvaluatePassOrFail(int getGitScriptResult, Utils.ScriptStage stage)
        {
            if (stage == Utils.ScriptStage.GetGitRepo)
            {
                return ((getGitScriptResult == Convert.ToInt32(Utils.GetGitRepoResponseCodes.ExistingRepoCheckoutSucceed))
                    || (getGitScriptResult == Convert.ToInt32(Utils.GetGitRepoResponseCodes.NewRepoCheckoutSucceed))) ? true : false;

            }
            else
            {
                return false;
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
            if (!String.IsNullOrEmpty(configJson.GetValue("username").ToString()))
            {
                txt_gitUsername.Text = configJson.GetValue("username").ToString();
            }
            if (!String.IsNullOrEmpty(configJson.GetValue("gitRepoPath").ToString()))
            {
                txt_gitRepoPath.Text = configJson.GetValue("gitRepoPath").ToString();
            }

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
            ValidatePath();
        }

        private void btn_GitRepoBrowse_Click(object sender, EventArgs e)
        {
            txt_gitRepoPath.Text = Utils.OpenFolderDailogAndGetPath();
        }



        private void txt_gitRepoPath_TextChanged(object sender, EventArgs e)
        {
            ValidatePath();
        }

        private void ValidatePath()
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
