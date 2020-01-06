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
        private string repo;
        public GetGitRepo(JObject jObject)
        {
            // create Powershell runspace
            configJson = jObject;
            InitializeComponent();
        }

        //Check Git
        private void button2_Click(object sender, EventArgs e)
        {
            //Clear the Checkout changeset area
            txt_powershellOutput.Clear();
            //checkBoxChangeset.Enabled = false;
            textBox_Changeset.Enabled = false;
            

            configJson["username"] = txt_gitUsername.Text;
            if (check_DoYouHaveGit.Checked == true)
            {
                configJson["gitRepoPath"] = txt_gitRepoPath.Text;
            }
            else
            {
                configJson["gitRepoPath"] = txt_gitRepoPath.Text.Contains("stylelabs.m") ? txt_gitRepoPath.Text : txt_gitRepoPath.Text + "\\stylelabs.m";

            }

            if (cmbReleaseVersions.SelectedItem == null)
            {
                MessageBox.Show("The specified git branch is invalid. Please select or input another one", "Invalid Git branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
        
                repo = cmbReleaseVersions.SelectedItem.ToString();

                Task.Factory.StartNew(() =>
                  ExecuteGetGitRepoScript()
                ).ContinueWith((result) => {

                    bool scriptPass = EvaluatePassOrFail(result.Result, Utils.ScriptStage.GetGitRepo);
                    PostScriptActions(sender, e, scriptPass);
                });
            }
        }

        private void PostScriptActions(object sender, EventArgs e, bool scriptPass)
        {
            if (scriptPass)
            {
                //SetupRedis();
                //label_GitPleaseWait.Text = "Done";
                //checkBoxChangeset.Enabled = true;

                progressBar_Changeset.Value = 100;

                label_GitPleaseWait.Visible = false;  
                progressBar_Changeset.Visible = false;

                radioButton_Yes.Enabled = true;
                radioButton_No.Enabled = true;
                radioButton_No.Checked = true;
                textBox_Changeset.Enabled = false;
                
                button_Changeset.Enabled = true;
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

        private void PostChangeSetScriptActions(object sender, EventArgs e, bool scriptPass)
        {
            if (scriptPass)
            {
                SetupRedis();
            }
            else
            {
                DialogResult userAction = MessageBox.Show("Invalid changeset for this branch. \n\n Abort - Change the provided changeset  \n Retry - Try the changeset \n Ignore - Continue to Redis setup", "Invalid changeset", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (userAction == DialogResult.Abort)
                {
                    textBox_Changeset.SelectAll();
                    textBox_Changeset.Focus();
                    return;
                }
                else if (userAction == DialogResult.Ignore)
                {
                    MessageBox.Show("You are on branch: " + repo, "Continue to Redis setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetupRedis();
                }
                else if (userAction == DialogResult.Retry)
                {
                    button_Changeset_Click(sender, e);
                }
            }
        }

        private void SetupRedis()
        {
          try
          { 
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread

                var redisSetup = new SetupRedis(configJson);
                // redisSetup.Closed += (s, args) => this.Close();
                redisSetup.Show();
                redisSetup.StartPosition = FormStartPosition.Manual;
                redisSetup.Location = this.Location;
                //redisSetup.Size = this.Size;
                this.Hide();
            });
          }
          catch(Exception ex)
          {
              MessageBox.Show(ex.Message.ToString(), "Redis error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }

        private int ExecuteGetGitRepoScript()
        {
            label_GitPleaseWait.Visible = true;
            progressBar_Changeset.Visible = true;

            progressBar_Changeset.Value = 40;
            progressBar_Changeset.Value = 80;

            var parameters = GetGitRepoScriptParams();
            powerShellScriptExecutor = new PowerShellScriptExecutor(this, txt_powershellOutput, parameters);
            var script = @"../../../Scripts/GetGitRepo.ps1";
            return  Task.Factory.StartNew(()=>powerShellScriptExecutor.ExecuteScript(script)).ContinueWith((result)=> {
                return result.Result;
            }).Result;
        }

        private int ExecuteGetGitChangesetScript()
        {
            label_GitPleaseWait.Visible = true;
            progressBar_Changeset.Visible = true;
            var parameters = GetGitChangesetScriptParams();
            powerShellScriptExecutor = new PowerShellScriptExecutor(this, txt_powershellOutput, parameters);
            var script = @"../../../Scripts/GetGitChangeset.ps1";
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
            //parameters.Add(new KeyValuePair<string, string>("VERSION", txt_gitVersion.Text));
            parameters.Add(new KeyValuePair<string, string>("VERSION", repo));
            parameters.Add(new KeyValuePair<string, string>("RepoExist", (Convert.ToInt32(check_DoYouHaveGit.Checked)).ToString()));
            return parameters;
        }

        private List<KeyValuePair<string, string>> GetGitChangesetScriptParams()
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("USERNAME", txt_gitUsername.Text));
            parameters.Add(new KeyValuePair<string, string>("PASSWORD", ""));
            parameters.Add(new KeyValuePair<string, string>("STYLELABSDIR", txt_gitRepoPath.Text));
            parameters.Add(new KeyValuePair<string, string>("VERSION", repo));
            parameters.Add(new KeyValuePair<string, string>("RepoExist", (Convert.ToInt32(check_DoYouHaveGit.Checked)).ToString()));
            parameters.Add(new KeyValuePair<string, string>("CHANGESET", textBox_Changeset.Text));
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

            //additional
            //Load release versions

            LoadAllReleaseVersions();


            lbl_repoError.Visible = false;
            check_DoYouHaveGit.Checked = true;
            txt_gitUsername.Parent = panel_GitLogin;
            //txt_gitVersion.Parent = panel_GitLogin;
            cmbReleaseVersions.Parent = panel_GitLogin;
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
            
            txt_gitVersion.Hide();

            TextBox.CheckForIllegalCrossThreadCalls = false;
            CheckBox.CheckForIllegalCrossThreadCalls = false;

            //checkBoxChangeset.Enabled = false;
            textBox_Changeset.Enabled = false;
            label_GitPleaseWait.Visible = false;
            progressBar_Changeset.Visible = false;

            radioButton_Yes.Enabled = false;
            radioButton_No.Enabled = false;
            button_Changeset.Enabled = false;
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

        private void LoadAllReleaseVersions()
        {
            string[] releaseVersions = ReleaseVersions();

            foreach (string version in releaseVersions)
            {
              cmbReleaseVersions.Items.Add(version);
            }

            cmbReleaseVersions.SelectedIndex = cmbReleaseVersions.Items.IndexOf("master");
        }

    #region Release Versions
    private string[] ReleaseVersions()
        {
            string[] releaseVersions;
               releaseVersions = new string[44]{"release/2.5.0",
                  "release/2.5.1",
                  "release/2.5.2",
                  "release/2.5.3",
                  "release/2.6.1",
                  "release/2.6.2",
                  "release/2.6.3",
                  "release/2.7.1",
                  "release/2.7.3",
                  "release/2.8.0",
                  "release/2.8.1",
                  "release/2.8.2",
                  "release/2.8.3",
                  "release/2.9.0",
                  "release/2.9.1",
                  "release/2.9.2",
                  "release/2.9.2-diageo",
                  "release/2.9.2-hf1",
                  "release/2.9.3",
                  "release/2.9.3-gm",
                  "release/2.9.4",
                  "release/2.9.5",
                  "release/2.9.6",
                  "release/2.10.0",
                  "release/2.10.1",
                  "release/2.10.2",
                  "release/2.10.3",
                  "release/2.10.4",
                  "release/3.0.0",
                  "release/3.0.1",
                  "release/3.0.2",
                  "release/3.0.3",
                  "release/3.0.4",
                  "release/3.0.5",
                  "release/3.0.6",
                  "release/3.1.0",
                  "release/3.1.1",
                  "release/3.1.2",
                  "release/3.1.3",
                  "release/3.1.4",
                  "release/3.2.0",
                  "release/3.2.1",
                  "release/3.2.2",
                  "master"};

              return releaseVersions;
        }
    #endregion

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void cmbReleaseVersions_SelectedIndexChanged(object sender, EventArgs e)
    {
      repo = cmbReleaseVersions.SelectedItem.ToString();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Tool created for support team to set-up git and redis locally");
    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void button_Changeset_Click(object sender, EventArgs e)
    {
      if (radioButton_Yes.Checked == true && textBox_Changeset.Text != string.Empty)
      {
        txt_powershellOutput.Text = string.Empty;

        Task.Factory.StartNew(() =>
                ExecuteGetGitChangesetScript()
            ).ContinueWith((result) => {


              bool scriptPass = EvaluatePassOrFail(result.Result, Utils.ScriptStage.SetupRdb);
              PostChangeSetScriptActions(sender, e, scriptPass);
            });

        label_GitPleaseWait.Visible = false;
        progressBar_Changeset.Visible = false;
      }
      else
      {
        SetupRedis();
      }
    }

    private void radioButton_Yes_CheckedChanged(object sender, EventArgs e)
    {
        textBox_Changeset.Enabled = true;
        textBox_Changeset.Focus();
    }

    private void radioButton_No_CheckedChanged(object sender, EventArgs e)
    {
        textBox_Changeset.Enabled = false;
    }

    private void txt_powershellOutput_TextChanged(object sender, EventArgs e)
    {
        txt_powershellOutput.SelectionStart = txt_powershellOutput.Text.Length;
        txt_powershellOutput.ScrollToCaret();
    }
  }
}
