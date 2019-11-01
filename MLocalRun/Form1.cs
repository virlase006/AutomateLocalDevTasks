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

namespace MLocalRun
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
                // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.
                var script = new StreamReader(@"C:\Users\adv\Downloads\GetGitRepo.ps1");


                string newScript = script.ReadToEnd();
                  //  File.ReadAllText(@"C:\Users\adv\Downloads\GetGitRepo.ps1");
                PowerShellInstance.AddScript(newScript);

                // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
                PowerShellInstance.AddParameter("USERNAME", txt_gitUsername.Text);
                PowerShellInstance.AddParameter("PASSWORD", "");
                PowerShellInstance.AddParameter("STYLELABSDIR", txt_gitRepoPath.Text);
                PowerShellInstance.AddParameter("VERSION", txt_gitVersion.Text);
                PowerShellInstance.AddParameter("RepoExist", Convert.ToInt32(check_DoYouHaveGit.Checked));

                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
               
                foreach (PSObject outputItem in PSOutput)
                {
                    // if null object was dumped to the pipeline during the script then a null
                    // object may be present here. check for null to prevent potential NRE.
                    if (outputItem != null)
                    {
                       
                        Console.WriteLine(outputItem.BaseObject.ToString() + "\n"); 

                    }
                }          
                
            }
          

            //var ps1File = @"C:\Users\adv\Downloads\GetGitRepo.ps1";
            //var filename = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";
            //var arguments = $"-NoProfile -ExecutionPolicy unrestricted -FILE \"{ps1File}\" advandizda  \"\"  C:\\Users\\adv\\Documents\\PowershellTest\\stylelabs.m release\\3.1.3 1";


            //var startInfo = new ProcessStartInfo()
            //{
            //    FileName = filename,
            //    Arguments = arguments,
            //    UseShellExecute = true
            //};

            //var proc = Process.Start(startInfo);

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
