using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLocalRun
{
    class PowerShellScriptExecutor: IScriptExecutor
    {
        //ISynchronizeInvoke Invoker;
        int result;
        private bool shouldReturn;
        RichTextBox OutputTextBox;
        StringBuilder Output ;
        ISynchronizeInvoke Invoker;
        List<KeyValuePair<string, string>> Parameters { get; set; }
        public PowerShellScriptExecutor(ISynchronizeInvoke invoker, RichTextBox outputTextBox, List<KeyValuePair<string, string>> parameters)
        {
            Invoker = invoker;
            OutputTextBox = outputTextBox;
            runSpace = RunspaceFactory.CreateRunspace();
            Parameters = parameters;           
            runSpace.Open();
        }

        static private Runspace runSpace;
     
        /// <summary>
        /// The active PipelineExecutor instance
        /// </summary>
        private PipelineExecutor pipelineExecutor;

        public int ExecuteScript(string script, List<KeyValuePair<string, string>> parameters)
        {
            this.Parameters = parameters;
            return ExecuteScript(script);
        }
        public int ExecuteScript(string script)
        {
            result = 0;
            shouldReturn = false;

            Task.Factory.StartNew(() => RunProcess(script));
            return Task.Factory.StartNew(() => GetResult()).ContinueWith((res) =>
               {
                   return result = res.Result;
               }).Result;
                    
        }

        private void RunProcess(string script)
        {
            Command command = new Command(script);
            foreach (var param in this.Parameters)
            {
                command.Parameters.Add(param.Key, param.Value);
            }
            pipelineExecutor = new PipelineExecutor(runSpace, Invoker, command);
            pipelineExecutor.OnDataReady += new PipelineExecutor.DataReadyDelegate(pipelineExecutor_OnDataReady);
            pipelineExecutor.OnDataEnd += new PipelineExecutor.DataEndDelegate(pipelineExecutor_OnDataEnd);
            pipelineExecutor.OnOutputReady += new PipelineExecutor.ErrorReadyDelegate(pipelineExecutor_OnOutputReady);
            pipelineExecutor.Start();
        }

        private void pipelineExecutor_OnDataEnd(PipelineExecutor sender)
        {
            if (sender.Pipeline.PipelineStateInfo.State == PipelineState.Failed)
            {
                shouldReturn = true;
                result = -1;
                OutputTextBox.AppendText(string.Format("Error in script: {0}", sender.Pipeline.PipelineStateInfo.Reason));
            }
            else
            {

                var allOutputs = OutputTextBox.Text.Split('\n');
                result = Convert.ToInt32(allOutputs[allOutputs.Length - 2]);
                shouldReturn = true;
            }

        }

        private void pipelineExecutor_OnDataReady(PipelineExecutor sender, ICollection<PSObject> data)
        {
            foreach (PSObject obj in data)
            {
                OutputTextBox.AppendText(obj.ToString() + "\n");
            }
        }

        void pipelineExecutor_OnOutputReady(PipelineExecutor sender, ICollection<object> data)
        {
            foreach (object e in data)
            {
                OutputTextBox.AppendText(e.ToString() + "\n");
            }
        }

        public int GetResult()
        {
            while (!shouldReturn)
            {
                Thread.Sleep(1000);
            }
            return result;
        }
    }
}
