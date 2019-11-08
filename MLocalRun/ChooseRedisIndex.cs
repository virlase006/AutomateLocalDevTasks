using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    public partial class ChooseRedisIndex : Form
    {
        JObject configJson;
        PowerShellScriptExecutor powerShellScriptExecutor;
        List<string> RedisIndexs;
        String GitRepo = "";

        public int SetupElasticSearchScriptResult { get; private set; }

        public ChooseRedisIndex(List<string> keyspaces, JObject configJson)
        {
            this.configJson = configJson;
            GitRepo = configJson["gitRepoPath"].ToString();
            powerShellScriptExecutor = new PowerShellScriptExecutor(this);
            RedisIndexs = keyspaces;
            InitializeComponent();
        }

        private void ChooseRedisIndex_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(configJson.GetValue("pathToElasticsearch").ToString()))
            {
                txt_PathToElasticSearch.Text = configJson.GetValue("pathToElasticsearch").ToString();
            }
            //txt_PathToElasticSearch.Text = @"C:\Users\virs\Downloads\elasticsearch\elasticsearch";
            comboBox1.Items.AddRange(RedisIndexs.ToArray());
            comboBox1.SelectedItem = comboBox1.Items[0]; ;
         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            configJson["pathToElasticsearch"] = txt_PathToElasticSearch.Text;
            var selectedDb = RedisIndexs.ElementAt(comboBox1.SelectedIndex);
            var dbIndex = selectedDb.Substring(2, 1);
            Task task1 = Task.Factory.StartNew(() => ExecuteSetupElasticSearchScript(dbIndex));
            Task task2 = Task.Factory.StartNew(() => GetResult()).ContinueWith((result)=> {
                if (result.Result == 1)
                {
                    DialogResult userAction = MessageBox.Show("Setup compelete. Do you want to save the new configuration? ", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (userAction == DialogResult.Yes)
                    {
                        JsonHelper jsonHelper = new JsonHelper();
                        jsonHelper.WriteJsonFile(ConfigurationManager.AppSettings.Get("jsonFile").ToString(), configJson);
                    }
                    else 
                    {
                        this.Invoke((MethodInvoker)delegate
                        {

                            this.Close();
                        });

                    }
                }
                else 
                {
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private int GetResult()
        {
            while (SetupElasticSearchScriptResult == 0)
            {
                Thread.Sleep(1000);
                SetupElasticSearchScriptResult = powerShellScriptExecutor.GetResult();
            }
            return SetupElasticSearchScriptResult;
        }

        private void ExecuteSetupElasticSearchScript(string dbIndex)
        {
            var script = @"../../../Scripts/SetupElasticSearch.ps1";
            var parameters = GetElasticSearchScriptParams(dbIndex);
            powerShellScriptExecutor.ExecutePowerShellScript(script, this, parameters, txt_powershellOutput);

        }

        private List<KeyValuePair<string,string>> GetElasticSearchScriptParams(string dbIndex)
        {

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("KeyIndex", dbIndex));
            parameters.Add(new KeyValuePair<string, string>("PathToRepo", GitRepo));
            parameters.Add(new KeyValuePair<string, string>("PathToElasticSearch", txt_PathToElasticSearch.Text));
            return parameters;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_PathToElasticSearch.Text = PowershellConstants.OpenFolderDailogAndGetPath();
        }
    }
}
