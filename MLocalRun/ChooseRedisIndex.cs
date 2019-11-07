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
    public partial class ChooseRedisIndex : Form
    {
        PowerShellScriptExecutor powerShellScriptExecutor;
        List<string> RedisIndexs;
        String GitRepo = "";
        public ChooseRedisIndex(List<string> keyspaces, string gitRepo)
        {
            GitRepo = gitRepo;
            powerShellScriptExecutor = new PowerShellScriptExecutor(this);
            RedisIndexs = keyspaces;
            InitializeComponent();
        }

        private void ChooseRedisIndex_Load(object sender, EventArgs e)
        {
            txt_PathToElasticSearch.Text = @"C:\Users\virs\Downloads\elasticsearch\elasticsearch";
            comboBox1.Items.AddRange(RedisIndexs.ToArray());
            comboBox1.SelectedItem = comboBox1.Items[0]; ;
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedDb = RedisIndexs.ElementAt(comboBox1.SelectedIndex);
            var dbIndex = selectedDb.Substring(2, 1);
            Task task1 = Task.Factory.StartNew(() => ExecuteSetupElasticSearchScript(dbIndex));
            Task task2 = Task.Factory.StartNew(() => GetResult());
        }

        private void GetResult()
        {
            return;
        }

        private void ExecuteSetupElasticSearchScript(string dbIndex)
        {
            var script = @"C:\Users\virs\Documents\PowerShell\SetupElasticSearch.ps1";
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
