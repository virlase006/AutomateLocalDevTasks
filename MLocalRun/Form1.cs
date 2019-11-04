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
    public partial class Form1 : Form
    {
        private Runspace runSpace;

        /// <summary>
        /// The active PipelineExecutor instance
        /// </summary>
        private PipelineExecutor pipelineExecutor;

        public Form1()
        {  // create Powershell runspace
            runSpace = RunspaceFactory.CreateRunspace();
            // open it
            runSpace.Open();

            InitializeComponent();
        }
        private void pipelineExecutor_OnDataEnd(PipelineExecutor sender)
        {
            if (sender.Pipeline.PipelineStateInfo.State == PipelineState.Failed)
            {
                txt_powershellOutput.AppendText(string.Format("Error in script: {0}", sender.Pipeline.PipelineStateInfo.Reason));
            }
            else
            {
                txt_powershellOutput.AppendText("Ready\n");
            }
        }

        private void pipelineExecutor_OnDataReady(PipelineExecutor sender, ICollection<PSObject> data)
        {
            foreach (PSObject obj in data)
            {
                txt_powershellOutput.AppendText(obj.ToString() + "\n");
            }
        }

        void pipelineExecutor_OnOutputReady(PipelineExecutor sender, ICollection<object> data)
        {
            foreach (object e in data)
            {
                txt_powershellOutput.AppendText(e.ToString() + "\n");
            }
        }
            private void button2_Click(object sender, EventArgs e)
            {
                var script = @"C:\Users\virs\Documents\PowerShell\GetGitRepo.ps1";

                txt_powershellOutput.AppendText("Starting script... \n");
                Command command = new Command(script);
                command.Parameters.Add("USERNAME", txt_gitUsername.Text);
                command.Parameters.Add("PASSWORD", "");
                command.Parameters.Add("STYLELABSDIR", txt_gitRepoPath.Text);
                command.Parameters.Add("VERSION", txt_gitVersion.Text);
                command.Parameters.Add("RepoExist", Convert.ToInt32(check_DoYouHaveGit.Checked));
                pipelineExecutor = new PipelineExecutor(runSpace, this, command);
                pipelineExecutor.OnDataReady += new PipelineExecutor.DataReadyDelegate(pipelineExecutor_OnDataReady);
                pipelineExecutor.OnDataEnd += new PipelineExecutor.DataEndDelegate(pipelineExecutor_OnDataEnd);
                pipelineExecutor.OnOutputReady += new PipelineExecutor.ErrorReadyDelegate(pipelineExecutor_OnOutputReady);
                pipelineExecutor.Start();

           

            }

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
                if (check_DoYouHaveGit.Checked)
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

                FolderBrowserDialog ofd = new FolderBrowserDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = ofd.SelectedPath;
                    txt_gitRepoPath.Text = sourceFile;

                }
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
