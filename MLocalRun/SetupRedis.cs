using Newtonsoft.Json.Linq;
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
        JObject configJson;
        IScriptExecutor bashScriptExecutor;
        string GitRepoPath = "";

        public SetupRedis(JObject configJson)
        {
            this.configJson = configJson;
            GitRepoPath = configJson["gitRepoPath"].ToString();
            InitializeComponent();

        }



        private void button_browseRedisFile_Click(object sender, EventArgs e)
        {
            txt_RdbPath.Text = Utils.OpenFileAndGetPath("rdb");
        }

        private bool IsRedisRunning()
        {
            bashScriptExecutor = new BashScriptExecutor(txt_powershellOutput);
            bool isRuning = false;
            return Task.Factory.StartNew(() => bashScriptExecutor.ExecuteScript("-c \"" + "redis-cli ping" + "\"")).ContinueWith((r) =>
            {
                var text = SafeReadTextBox(txt_powershellOutput);
                isRuning = text.Contains("PONG");

            }).ContinueWith((r) =>
            {
                return isRuning;
            }).Result;

        }
        private void SafeWriteTextBox(RichTextBox txtBox, string text)
        {

            txtBox.Invoke((MethodInvoker)delegate
            {
                txtBox.AppendText(text);

            });

        }
        private string SafeReadTextBox(RichTextBox txtBox)
        {
            string text = "";
            txtBox.Invoke((MethodInvoker)delegate
            {
                text = txtBox.Text;
            });
            return text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            configJson["pathToRedis"] = txt_RedisPath.Text;
            Task.Factory.StartNew(() => IsRedisRunning()).ContinueWith((result) =>
            {

                if (result.Result)
                {
                    SafeWriteTextBox(txt_powershellOutput,"\n Killing running instance of redis");
                    KillRedis();
                   

                }
                string redisFilePathParsed = ParseWindowsPathToBashPath(txt_RdbPath.Text);
                var command = ExtractAndParseBashCommand(redisFilePathParsed);
                Task.Factory.StartNew(() => bashScriptExecutor.ExecuteScript(command)).ContinueWith((r) =>
                {

                    ProcessOutput(SafeReadTextBox(txt_powershellOutput));
                }
                 );
            });






        }

        private void ProcessOutput(string output)
        {

            if (output.Contains("ERROR"))
            {
                output = output.Substring(output.IndexOf("ERROR"));
                MessageBox.Show($"Failed to setup redis: {output}", "Failed to setup redis", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                SafeWriteTextBox(txt_powershellOutput , "\n" + output);
                ChooseRedisIndex();
            }
        }

        private void ChooseRedisIndex()
        {
            SafeWriteTextBox(txt_powershellOutput, "");
            var keySpaces = bashScriptExecutor.ExecuteScript("-c \"redis-cli info KeySpace\"");

            var keySpacesArray = SafeReadTextBox(txt_powershellOutput).Split('\n').ToList<string>();
            List<string> dbIndexes = new List<string>();
            foreach (var keyspace in keySpacesArray)
            {
                if (keyspace.Contains("db"))
                {
                    dbIndexes.Add(keyspace);
                }
            }
            
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread

                var chooseRedis = new ChooseRedisIndex(dbIndexes, configJson);
                chooseRedis.StartPosition = FormStartPosition.Manual;
                chooseRedis.Location = this.Location;
                chooseRedis.Size = this.Size;
                chooseRedis.Show();              
                this.Hide();
            });
        }

        private string ExtractAndParseBashCommand(string redisFilePathParsed)
        {
            var bashScritp = System.IO.File.ReadAllText(@"../../../Scripts/CopyRedisFile.ps1");
            bashScritp = bashScritp.Replace("bash", "");
            bashScritp = bashScritp.Replace("PathToRedis", txt_RedisPath.Text.Replace("\\", "/"));
            bashScritp = bashScritp.Replace("BashPath", redisFilePathParsed.Replace("\\", "/"));
            return bashScritp;
        }

        private string ParseWindowsPathToBashPath(string windowsPath)
        {
            return windowsPath.Replace("C:", "");
        }

        private void KillRedis()
        {
            Task.Factory.StartNew(() =>
                bashScriptExecutor.ExecuteScript("-c \"killall redis-server\"")).ContinueWith((r) =>
                {
                    return;
                });
        }


        private void SetupRedis_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(configJson.GetValue("pathToRedis").ToString()))
            {
                txt_RedisPath.Text = configJson.GetValue("pathToRedis").ToString();
            }

            // txt_RedisPath.Text = "/home/vds/redis-5.0.4/utils";
            lbl_repoError.Visible = false;
        }

        private void txt_RdbPath_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_RdbPath.Text))
            {
                if (System.IO.File.Exists(txt_RdbPath.Text))
                {
                    button2.Enabled = true;
                    lbl_repoError.Visible = false;
                }
                else
                {


                    button2.Enabled = false;
                    lbl_repoError.Text = "Invalid path !!!";
                    lbl_repoError.Visible = true;
                    lbl_repoError.Show();
                }
            }
        }
    }
}
